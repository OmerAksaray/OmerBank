using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore;
using OmerBank.Models.Context;
using OmerBank.Repository;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml;

namespace OmerBank.Controllers
{
    [Authorize] 
    public class AccountController : BaseController
    {
        private readonly IAccountRepository _accountRepository;
        private readonly MyContext _context;

        public AccountController(IAccountRepository accountRepository, MyContext context)
        {
            _accountRepository = accountRepository;
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var account = await _context.Accounts
                .Include(a => a.Kinds)
                .FirstOrDefaultAsync(a => a.ID == GetCurrentUserId());

            return View(account);
        }

        [HttpGet]
        public async Task<IActionResult> Transfer()
        {
            var account = await _context.Accounts
                .Include(a => a.Kinds)
                .FirstOrDefaultAsync(a => a.ID == GetCurrentUserId());
            return View(account);
        }

        [HttpPost]
        public async Task<IActionResult> Transfer(int id, int transferIban, string name, decimal amount)
        {
            id = GetCurrentUserId();
            await _accountRepository.TransferMoney(id, transferIban, name, amount);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Convert()
        {
            var account = await _context.Accounts
                .Include(a => a.Kinds)
                .FirstOrDefaultAsync(a => a.ID == GetCurrentUserId());
            var currencyInf = "http://www.tcmb.gov.tr/kurlar/today.xml";
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(currencyInf);
            var USD = xmlDoc.SelectSingleNode("Tarih_Date/Currency [@CrossOrder='0']/BanknoteSelling").InnerText;
            var EUR = xmlDoc.SelectSingleNode("Tarih_Date/Currency [@CrossOrder='9']/BanknoteSelling").InnerText;
            ViewBag.USD = USD;
            ViewBag.EUR = EUR;
            return View(account);
        }

        [HttpPost]
        public async Task<IActionResult> Convert(int id, string name, string convertName, decimal amount)
        {
            id = GetCurrentUserId();
            await _accountRepository.ConvertMoney(id, name, convertName, amount);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Deposit()
        {
            var account = await _context.Accounts
                .Include(a => a.Kinds)
                .FirstOrDefaultAsync(a => a.ID == GetCurrentUserId());

            return View(account);
        }

        [HttpPost]
        public async Task<IActionResult> Deposit(int id, string name, decimal amount)
        {
            id = GetCurrentUserId();
            try
            {
                Console.WriteLine($"Depositing {amount} to {name} for AccountID: {id}");
                await _accountRepository.DepositAsync(id, name, amount);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Deposit: {ex.Message}");

                return RedirectToAction("Transfer");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Draw()
        {
            var account = await _context.Accounts
                .Include(a => a.Kinds)
                .FirstOrDefaultAsync(a => a.ID == GetCurrentUserId());

            return View(account);
        }

        [HttpPost]
        public async Task<IActionResult> Draw(int id, string name, decimal amount)
        {
            id = GetCurrentUserId();
            try
            {
                Console.WriteLine($"Depositing {amount} to {name} for AccountID: {id}");
                await _accountRepository.WithDraw(id, name, amount);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Deposit: {ex.Message}");

                return RedirectToAction("Transfer");
            }
        }
    }
}
