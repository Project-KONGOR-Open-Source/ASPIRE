﻿namespace MERRICK.Database.Entities;

public class UserToken : IdentityUserToken<Guid>
{
    [Key]
    public Guid ID { get; set; }
}
