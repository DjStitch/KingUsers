using System.ComponentModel.DataAnnotations;
using KingsUsers.Models;

public class Group
{
    public int GroupId { get; set; }
    [Required(ErrorMessage = "GroupName is required.")]
    [MaxLength(50)]
    public string GroupName { get; set; }

    public ICollection<UserGroup> UserGroups { get; set; }
    public ICollection<GroupPermission> GroupPermissions { get; set; }
}