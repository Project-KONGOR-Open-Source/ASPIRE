﻿// <auto-generated />
using System;
using MERRICK.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MERRICK.Database.Migrations
{
    [DbContext(typeof(MerrickContext))]
    partial class MerrickContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MERRICK.Database.Entities.Account", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccountType")
                        .HasColumnType("int");

                    b.Property<int>("AscensionLevel")
                        .HasColumnType("int");

                    b.Property<Guid?>("ClanID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ClanTier")
                        .HasColumnType("int");

                    b.Property<string>("HardwareIDCollection")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IPAddressCollection")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsMain")
                        .HasColumnType("bit");

                    b.Property<string>("MACAddressCollection")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("SelectedStoreItems")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SystemInformationCollection")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimestampCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TimestampLastActive")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("ClanID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("UserID");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("MERRICK.Database.Entities.Clan", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<DateTime>("TimestampCreated")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("Name", "Tag")
                        .IsUnique();

                    b.ToTable("Clans");
                });

            modelBuilder.Entity("MERRICK.Database.Entities.Role", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ID");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            ID = new Guid("00000000-0000-0000-0000-00009eaf9c10"),
                            Name = "Administrator"
                        },
                        new
                        {
                            ID = new Guid("00000000-0000-0000-0000-000091cf21a9"),
                            Name = "User"
                        });
                });

            modelBuilder.Entity("MERRICK.Database.Entities.Token", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Purpose")
                        .HasColumnType("int");

                    b.Property<DateTime?>("TimestampConsumed")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TimestampCreated")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("MERRICK.Database.Entities.User", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("GoldCoins")
                        .HasColumnType("int");

                    b.Property<string>("OwnedStoreItems")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PBKDF2PasswordHash")
                        .IsRequired()
                        .HasMaxLength(84)
                        .HasColumnType("nvarchar(84)");

                    b.Property<int>("PlinkoTickets")
                        .HasColumnType("int");

                    b.Property<Guid>("RoleID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SRPPasswordHash")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("SRPPasswordSalt")
                        .IsRequired()
                        .HasMaxLength(22)
                        .HasColumnType("nvarchar(22)");

                    b.Property<string>("SRPSalt")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<int>("SilverCoins")
                        .HasColumnType("int");

                    b.Property<int>("TotalExperience")
                        .HasColumnType("int");

                    b.Property<int>("TotalLevel")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("EmailAddress")
                        .IsUnique();

                    b.HasIndex("RoleID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MERRICK.Database.Entities.Account", b =>
                {
                    b.HasOne("MERRICK.Database.Entities.Clan", "Clan")
                        .WithMany("Members")
                        .HasForeignKey("ClanID");

                    b.HasOne("MERRICK.Database.Entities.User", "User")
                        .WithMany("Accounts")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clan");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MERRICK.Database.Entities.User", b =>
                {
                    b.HasOne("MERRICK.Database.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("MERRICK.Database.Entities.Clan", b =>
                {
                    b.Navigation("Members");
                });

            modelBuilder.Entity("MERRICK.Database.Entities.User", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
