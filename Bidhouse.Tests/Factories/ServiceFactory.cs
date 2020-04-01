using Bidhouse.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bidhouse.Tests.Factories
{
    public  class ServiceFactory
    {
        public ApplicationDbContext Context { get; }

        public UserManager<User> UserManager { get; }

        public RoleManager<Role> RoleManager { get; }

        public  ServiceFactory()
        {
            var services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>
            (
                options => options.UseInMemoryDatabase(Guid.NewGuid().ToString())

            );


            IdentityBuilder builder = services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);

            builder.AddEntityFrameworkStores<ApplicationDbContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>();
    
            var context = new DefaultHttpContext();
            context.Features.Set<IHttpAuthenticationFeature>(new HttpAuthenticationFeature());
            services.AddSingleton<IHttpContextAccessor>(h => new HttpContextAccessor { HttpContext = context });
            var serviceProvider = services.BuildServiceProvider();
            this.Context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            this.UserManager = serviceProvider.GetRequiredService<UserManager<User>>();
            this.RoleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
        }

    }
}
