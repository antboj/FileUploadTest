using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using FileUploadTest.Roles.Dto;
using FileUploadTest.Users.Dto;

namespace FileUploadTest.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
