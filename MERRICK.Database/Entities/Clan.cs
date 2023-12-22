﻿namespace MERRICK.Database.Entities;

[Index(nameof(Name), nameof(Tag), IsUnique = true)]
public class Clan
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(20)]
    public required string Name { get; set; } = null!;

    [StringLength(3)]
    public required string Tag { get; set; } = null!;

    public List<Account> Members { get; set; } = [];

    public DateTime TimestampCreated { get; set; } = DateTime.UtcNow;
}
