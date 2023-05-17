using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Users.Bll.Models;

[Table("users")]
[Index(nameof(Login), IsUnique = true, Name = "IX_users_login")]
public record User
{
    [Key, Column("guid")] public Guid Guid { get; set; }

    [Column("login")] public string Login { get; set; } = null!;
    [Column("password")] public string Password { get; set; } = null!;

    [Column("name")] public string Name { get; set; } = null!;
    [Column("gender")] public int Gender { get; set; }
    [Column("birthday")] public DateTime? Birthday { get; set; }

    [Column("admin")] public bool Admin { get; set; }

    [Column("created_on")] public DateTime CreatedOn { get; set; }
    [Column("created_by")] public string CreatedBy { get; set; } = null!;

    [Column("modified_on")] public DateTime? ModifiedOn { get; set; }
    [Column("modified_by")] public string? ModifiedBy { get; set; }

    [Column("revoked_on")] public DateTime? RevokedOn { get; set; }
    [Column("revoked_by")] public string? RevokedBy { get; set; }
}