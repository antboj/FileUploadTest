using Abp.Authorization;
using FileUploadTest.Authorization.Roles;
using FileUploadTest.Authorization.Users;

namespace FileUploadTest.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
