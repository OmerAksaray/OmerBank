using Microsoft.EntityFrameworkCore;
using OmerBank.Models.Context;
using OmerBank.Models.Entities;
using System.Xml;

namespace OmerBank.Repository
{
    public class AccountRepository:Repository<Account>, IAccountRepository
    {
        private readonly MyContext _context;
        private readonly DbSet<Account> _dbSet;
        private readonly IAccountRepository _accountRepository;
        private readonly IKindRepository _kindRepository;

        public AccountRepository(MyContext context, IKindRepository kindRepository) : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Account>();
            _kindRepository = kindRepository;

           
        }

        public async Task DepositAsync(int id, string name, decimal amount)
        {
            try
            {
                if (amount < 0)
                {
                    throw new Exception("Yatırmak istediğiniz tutar 0'dan küçük olamaz!");
                }

                var myAccount = await GetByIdAsync(id);
                if (myAccount == null)
                {
                    throw new Exception("Iban bulunamadı!");
                }

                var kind = await _kindRepository.GetByKindWithAccountIdAsync(id, name);
                if (kind == null)
                {
                    throw new Exception("Para birimi bulunamadı!");
                }

                kind.Money += amount;

                _context.Entry(kind).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task WithDraw(int id, string name, decimal amount)
        {
            if (amount < 0)
            {
                throw new Exception("Çekmek istediğiniz tutar 0 dan küçük olamaz!");
            }
            var myAccount = await GetByIdAsync(id);
            if (myAccount == null)
            {
                throw new Exception("Iban bulunamadı!");
            }
            var kind = await _kindRepository.GetByKindWithAccountIdAsync(id, name);
            if (kind.Money-amount<0)
            {
                throw new Exception("Hesabınızda o kadar para bulunmamaktadır!");
            }

            kind.Money -= amount;

            await _kindRepository.UpdateAsync(kind);

            await _context.SaveChangesAsync();
        }
        public async Task ConvertMoney(int id, string name, string convertName ,decimal amount )
        {
            if (amount < 0)
            {
                throw new Exception("Çevirmek istediğiniz tutar 0 dan küçük olamaz!");
            }
            var myAccount = await GetByIbanAsync(id);
            if (myAccount == null)
            {
                throw new Exception("Iban bulunamadı!");
            }
            var kind = await _kindRepository.GetByKindWithAccountIdAsync(id, name);
            var convertKind= await _kindRepository.GetByKindWithAccountIdAsync(id, convertName);
            if (convertKind == null)
            {
                throw new Exception("Bu para birimine dönüşüm sağlamıyoruz!");
            }
            var currencyInf = "http://www.tcmb.gov.tr/kurlar/today.xml";
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(currencyInf);
            var USD= Convert.ToDecimal(xmlDoc.SelectSingleNode("Tarih_Date/Currency [@CrossOrder='0']/BanknoteSelling").InnerText);
            var EUR = Convert.ToDecimal(xmlDoc.SelectSingleNode("Tarih_Date/Currency [@CrossOrder='9']/BanknoteSelling").InnerText);


            if (convertName.ToLower()=="dolar") { 
            if(kind.Money - (amount * USD)<0)
            {
                    throw new Exception("Hesabınızda yeterli tl yok!");
            }
                kind.Money -= (amount * USD);
                convertKind.Money += (amount);
            }
            else if(convertName.ToLower() == "euro")
            {
                if (kind.Money - (amount * EUR) < 0)
                {
                    throw new Exception("Hesabınızda yeterli tl yok!");
                }
                kind.Money -= (amount * EUR);
                convertKind.Money += (amount);
            }
            _context.SaveChanges();

        }
        public async Task TransferMoney(int id, int transferIban,string name,decimal amount)
        {
            if (amount < 0)
            {
                throw new Exception("Göndermek istediğiniz tutar 0 dan küçük olamaz!");
            }
            var myAccount = await GetByIdAsync(id);
            var transferAccount = await GetByIbanAsync(transferIban);
            if (myAccount == null || transferAccount == null)
            {
                throw new Exception("Iban bulunamadı!");
            }
            var myKind = await _kindRepository.GetByKindWithAccountIdAsync(id, name);
            var transferKind = await _kindRepository.GetByKindWithAccountIdAsync(transferAccount.ID, name);
            if (transferKind == null)
            {
                Kind kind = new Kind(name, 0, transferAccount.ID);
                transferKind = kind;
                _kindRepository.AddAsync(transferKind);
                _context.SaveChanges();
            }
            if (amount > myKind.Money)
            {
                throw new Exception("Hesabındaki paradan daha fazla miktarda para gönderemezssin!");
            }
            transferKind.Money += amount;
            myKind.Money -= amount;
            await _kindRepository.UpdateAsync(myKind);
            await _kindRepository.UpdateAsync(transferKind);
            await _context.SaveChangesAsync();
        }

        public async Task<Account> GetByIbanAsync(int iban)
        {
            return await _dbSet.FirstOrDefaultAsync(a => a.IBAN == iban);
        }


    }

}
