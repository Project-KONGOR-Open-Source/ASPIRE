﻿namespace MERRICK.Database.Models.Context;

public sealed class MerrickContext : DbContext
{
    public MerrickContext(DbContextOptions options) : base(options)
    {
        Database.SetCommandTimeout(60); // 1 Minute - Helps Prevent Migrations From Timing Out When Many Records Need To Update
    }

    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Clan> Clans => Set<Clan>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Token> Tokens => Set<Token>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        ConfigureRoles(builder.Entity<Role>());
    }

    private static void ConfigureRoles(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(nameof(Roles));

        builder.HasData
        (
            new Role
            {
                ID = Guid.Parse($"00000000-0000-0000-0000-{Constants.UserRoles.Administrator.GetHashCode():X12}"),
                Name = Constants.UserRoles.Administrator
            },

            new Role
            {
                ID = Guid.Parse($"00000000-0000-0000-0000-{Constants.UserRoles.User.GetHashCode():X12}"),
                Name = Constants.UserRoles.User
            }
        );
    }
}
