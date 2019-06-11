using Abp.Application.Services;
using Abp.Application.Services.Dto;
using FileUploadTest.MultiTenancy.Dto;

namespace FileUploadTest.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

