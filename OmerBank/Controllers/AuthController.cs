using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OmerBank.Dtos;
using OmerBank.JWTHandling;
using OmerBank.Models.Context;
using OmerBank.Models.Entities;
using OmerBank.Repository;
using System.Threading.Tasks;

namespace OmerBank.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly MyContext _context;
        private readonly IAuthRepository _authRepository;
        private readonly JWTGenerator _jwtGenerator;
        private readonly IAccountRepository _accountRepository;
        private readonly IApplicationUserProfileRepository _user;

        public AuthController(IAuthRepository authRepository, JWTGenerator jwtGenerator, 
            IAccountRepository accountRepository,MyContext context, IApplicationUserProfileRepository user)
        {
            _authRepository = authRepository;
            _jwtGenerator = jwtGenerator;
            _accountRepository = accountRepository;
            _context = context;
            _user = user;
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] ApplicationUserProfile user)
        {
            if (await _authRepository.UserExist(user.UserName))
            {
                ModelState.AddModelError("UserName", "Username already exists!");
                return BadRequest(ModelState);
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var newUser = new ApplicationUserProfile
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                    };

                    var createdUser = await _authRepository.Register(newUser, user.Password);

                    var accountUser = new Account
                    {
                        IBAN = createdUser.ID,
                        AppUserProfileID = createdUser.ID,
                    };

                    await _accountRepository.AddAsync(accountUser);
                    _context.SaveChanges();
                    await transaction.CommitAsync();

                    return Ok(createdUser);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                   
                    return StatusCode(500, "Internal server error");
                }
            }
        }


        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginUser)
        {
            var user = await _authRepository.Login(loginUser.UserName, loginUser.Password);

            if (user == null)
            {
                return Unauthorized();
            }
            var account= await _context.Accounts.FirstOrDefaultAsync(a=>a.AppUserProfileID==user.ID);
            
            var token = _jwtGenerator.GenerateToken(account);

            return Ok(token);
        }
    }
}
