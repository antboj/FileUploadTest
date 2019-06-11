using System.Threading.Tasks;
using Abp.Application.Services;
using FileUploadTest.Sessions.Dto;

namespace FileUploadTest.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
