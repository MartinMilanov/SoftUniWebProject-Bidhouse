using Bidhouse.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }
    }
}
