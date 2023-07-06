using System.ComponentModel.DataAnnotations;

namespace KingsUsers.Models;

public class User
{

    public int UserId { get; set; }
    [Required(ErrorMessage = "Username is required.")]
    [MaxLength(12)]
    public string Username { get; set; } = string.Empty;
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;
    [MaxLength(50)]
    public string Address { get; set; } = string.Empty;

    [MaxLength(100)]

    public string About { get; set; } = string.Empty;

    public ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
}