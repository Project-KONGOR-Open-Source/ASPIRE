﻿namespace MERRICK.Database.Models.Entities;

public class Token
{
    [Key]
    public Guid ID { get; set; }

    public required TokenPurpose Purpose { get; set; }

    public required string EmailAddress { get; set; }

    public DateTime TimestampCreated { get; set; } = DateTime.UtcNow;

    public DateTime? TimestampConsumed { get; set; }

    public required string Data { get; set; }
}

public enum TokenPurpose
{
    EmailAddressVerification,
    EmailAddressUpdate
}
