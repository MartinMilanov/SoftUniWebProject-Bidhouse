using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Bidhouse.Tests.Factories
{
    public static class DbContextFactory
    {
        public static ApplicationDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var modelBuilder = new ModelBuilder(new ConventionSet());

            var dbContext = new ApplicationDbContext(options);

            var onModelCreatingMethod = dbContext.GetType().GetMethod(
                "OnModelCreating",
                BindingFlags.Instance | BindingFlags.NonPublic);

            onModelCreatingMethod.Invoke(dbContext, new object[] { modelBuilder });

            return dbContext;
        }
    }
}
