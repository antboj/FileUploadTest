using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using FileUploadTest.Authorization;

namespace FileUploadTest
{
    [DependsOn(
        typeof(FileUploadTestCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class FileUploadTestApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<FileUploadTestAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(FileUploadTestApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
