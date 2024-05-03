using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AtonApi.Models;

public class User {
    [Key]
    public Guid Guid { get; set; }
    [Required]
    public string Login { get; set; }
    [Required]
    public string Password { get; set; }
    public ushort Gender { get; set; } = 2;
    public bool Admin { get; set; } = false;
    public string? Name { get; set; }
    public DateTime? Birthday { get; set; }
    public DateTime? CreatedOn { get; set; }
    public User? CreatedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public User? ModifiedBy { get; set; }
    public DateTime? RevokedOn { get; set; }
    public User? RevokedBy { get; set; }
}
