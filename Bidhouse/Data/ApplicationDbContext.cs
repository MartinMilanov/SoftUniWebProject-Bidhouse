﻿using Bidhouse.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidhouse
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, string,
        IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
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

            modelBuilder.Entity<UserRole>()
              .HasKey(x => new { x.UserId, x.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.Role)
                .WithMany(x => x.UserRole)
                .HasForeignKey(x => x.RoleId);

            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserRole)
                .HasForeignKey(x => x.UserId);


            modelBuilder.Entity<Post>()
                .HasMany(p => p.Bids)
                .WithOne(b => b.Post)
                .HasForeignKey(b => b.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.BidsSent)
                .WithOne(b => b.Bidder).HasForeignKey(o => o.BidderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasMany(u => u.BidsReceived)
                .WithOne(o => o.Receiver).HasForeignKey(o => o.ReceiverId)
                .OnDelete(DeleteBehavior.NoAction).IsRequired();

            modelBuilder.Entity<User>()
                .HasMany(x => x.ReviewsGotten)
                .WithOne(x => x.ReviewedUser)
                .HasForeignKey(x => x.ReviewedUserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
            .HasMany(x => x.ReviewsSent)
            .WithOne(x => x.Reviewer)
            .HasForeignKey(x => x.ReviewerId)
            .OnDelete(DeleteBehavior.NoAction);




        }
    }
}
