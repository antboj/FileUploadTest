using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using FileUploadTest.Configuration;

namespace FileUploadTest.Web.Host.Startup
{
    [DependsOn(
       typeof(FileUploadTestWebCoreModule))]
    public class FileUploadTestWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public FileUploadTestWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FileUploadTestWebHostModule).GetAssembly());
        }
    }
}
