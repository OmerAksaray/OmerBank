
using Azure;
using Microsoft.EntityFrameworkCore;
using OmerBank.JWTHandling;
using OmerBank.Models.Context;
using OmerBank.Models.Entities;
using Org.BouncyCastle.Security;
using System.Web.Helpers;
using System.Web.Mvc;

namespace OmerBank.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly MyContext _context;
  

            public AuthRepository(MyContext context)
            {
            _context = context;
         
            }
            public async Task<ApplicationUserProfile> Login(string userName, string password)
            {
                var user = await _context.ApplicationProfiles.FirstOrDefaultAsync(p => p.UserName == userName);
                if (user == null)
                {
                    throw new Exception("Kullanıcı bulunamadı!");
                }
                if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    throw new Exception("Hatalı şifre!");
                }

                return user;
            }

        public async Task<ApplicationUserProfile> Register(ApplicationUserProfile user, string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            user.Password = hashedPassword;
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> UserExist(string username)
        {
            return await _context.ApplicationProfiles.AnyAsync(a => a.UserName == username);
        }

    }
}
