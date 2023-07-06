namespace KingsUsers.Models
{
    public class Permission
    {
        public int PermissionId { get; set; }
        public string PermissionName { get; set; }

        public ICollection<GroupPermission> GroupPermissions { get; set; }

    }
}
