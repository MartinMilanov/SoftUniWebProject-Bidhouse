using Bidhouse.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>()
                .HasMany(p => p.Bids)
                .WithOne(b => b.Post)
                .HasForeignKey(b => b.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.BidsSent)
                .WithOne(b => b.Bidder).HasForeignKey(o => o.BidderId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.BidsReceived)
                .WithOne(o => o.Receiver).HasForeignKey(o => o.ReceiverId).OnDelete(DeleteBehavior.Restrict).IsRequired();

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Reviewer)
                .WithMany(u => u.ReviewsSent)
                .HasForeignKey(u=>u.ReviewerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                 .HasOne(r => r.ReviewedUser)
                 .WithMany(u => u.ReviewsGotten)
                 .HasForeignKey(u=>u.ReviewedUserId)
                 .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
