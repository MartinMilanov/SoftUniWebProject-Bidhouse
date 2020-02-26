﻿// <auto-generated />
using Bidhouse;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bidhouse.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200224132918_AddedName")]
    partial class AddedName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Bidhouse.Models.Bid", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BidderId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ReceiverId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("StatusOfBid")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BidderId");

                    b.HasIndex("PostId");

                    b.HasIndex("ReceiverId");

                    b.ToTable("Bids");
                });

            modelBuilder.Entity("Bidhouse.Models.Post", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("LowPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostType")
                        .HasColumnType("int");

                    b.Property<decimal>("RoofPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Bidhouse.Models.Report", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReportType")
                        .HasColumnType("int");

                    b.Property<string>("ReportedPostId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ReporterId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ReportedPostId");

                    b.HasIndex("ReporterId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("Bidhouse.Models.Review", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("ReviewedUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ReviewerId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ReviewedUserId");

                    b.HasIndex("ReviewerId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Bidhouse.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkPosition")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Bidhouse.Models.Bid", b =>
                {
                    b.HasOne("Bidhouse.Models.User", "Bidder")
                        .WithMany("BidsSent")
                        .HasForeignKey("BidderId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Bidhouse.Models.Post", "Post")
                        .WithMany("Bids")
                        .HasForeignKey("PostId");

                    b.HasOne("Bidhouse.Models.User", "Receiver")
                        .WithMany("BidsReceived")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Bidhouse.Models.Post", b =>
                {
                    b.HasOne("Bidhouse.Models.User", "Creator")
                        .WithMany("Posts")
                        .HasForeignKey("CreatorId");
                });

            modelBuilder.Entity("Bidhouse.Models.Report", b =>
                {
                    b.HasOne("Bidhouse.Models.Post", "ReportedPost")
                        .WithMany("Reports")
                        .HasForeignKey("ReportedPostId");

                    b.HasOne("Bidhouse.Models.User", "Reporter")
                        .WithMany("Reports")
                        .HasForeignKey("ReporterId");
                });

            modelBuilder.Entity("Bidhouse.Models.Review", b =>
                {
                    b.HasOne("Bidhouse.Models.User", "ReviewedUser")
                        .WithMany("ReviewsGotten")
                        .HasForeignKey("ReviewedUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Bidhouse.Models.User", "Reviewer")
                        .WithMany("ReviewsSent")
                        .HasForeignKey("ReviewerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
