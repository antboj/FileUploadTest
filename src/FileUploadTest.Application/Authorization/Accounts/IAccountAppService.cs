using System.Threading.Tasks;
using Abp.Application.Services;
using FileUploadTest.Authorization.Accounts.Dto;

namespace FileUploadTest.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
