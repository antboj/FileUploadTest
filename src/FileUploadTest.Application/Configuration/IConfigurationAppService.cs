using System.Threading.Tasks;
using FileUploadTest.Configuration.Dto;

namespace FileUploadTest.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
