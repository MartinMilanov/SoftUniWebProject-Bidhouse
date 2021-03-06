﻿using Bidhouse.Models;
using Bidhouse.ViewModels.AuthModels;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext db)
        {
            this.db = db;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<string> ChangePassword(string id, ChangePasswordInputModel input)
        {
            var user = await this.userManager.FindByIdAsync(id);
            if (user == null)
            {
                return "User not found";
            }
            var result = await this.userManager.ChangePasswordAsync(user, input.CurrentPassword, input.NewPassword);

            if (result.Succeeded == false)
            {
                return "Wrong password";
            }
            return user.PasswordHash;
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await this.userManager.FindByNameAsync(username);

            if (user == null)
            {
                return null;
            }


            var result = await this.signInManager.CheckPasswordSignInAsync(user, password, false);

            if (result.Succeeded)
            {
                return user;
            }

            return null;
        }

        public async Task<User> Register(RegisterInputModel input)
        {

            var user = new User()
            {
                UserName = input.Username,
                Email = input.Email,
                WorkPosition = input.JobPosition,
                City = input.City,
                ImageUrl = "/images/default-user-photo.png"
            };

            var result = await this.userManager.CreateAsync(user, input.Password);

            var queryUser = await userManager.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName);
            await this.userManager.AddToRoleAsync(queryUser, "User");


            if (result.Succeeded)
            {
                return user;
            }

            return null;

        }

        public async Task<bool> UserExists(string username)
        {
            if (await db.Users.AnyAsync(x => x.UserName == username))
            {
                return true;
            }
            return false;
        }

    }
}
