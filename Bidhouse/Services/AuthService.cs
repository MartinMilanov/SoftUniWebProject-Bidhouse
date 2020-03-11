using Bidhouse.Models;
using Bidhouse.ViewModels.AuthModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bidhouse.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext db;

        public AuthService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<string> ChangePassword(string id, ChangePasswordInputModel input)
        {
            var user = await this.db.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return null;
            }
            user.Password = this.Hash(input.NewPassword);
            this.db.Users.Update(user);
            this.db.SaveChanges();
            return user.Password;
        }

        public async Task<User> Login(string username, string password)
        {
            User user = await db.Users.FirstOrDefaultAsync(x => x.Username == username);

            if (user == null)
            {
                return null;
            }

            var passwordHash = this.Hash(password);
            if (user.Password != passwordHash)
            {
                return null;
            }

            return user;
        }

        public async Task<User> Register(User user, string password)
        {
            var passwordHash = this.Hash(password);
            user.Password = passwordHash;
            await this.db.Users.AddAsync(user);
            await this.db.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await db.Users.AnyAsync(x=>x.Username == username))
            {
                return true;
            }
            return false;
        }

        private string Hash(string input)
        {
            if (input == null)
            {
                return null;
            }

            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(input));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            return hash.ToString();
        }
    }
}
