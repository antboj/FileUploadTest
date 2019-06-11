using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace FileUploadTest.Controllers
{
    public abstract class FileUploadTestControllerBase: AbpController
    {
        protected FileUploadTestControllerBase()
        {
            LocalizationSourceName = FileUploadTestConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
