using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using FileUploadTest.Configuration;
using FileUploadTest.Web;

namespace FileUploadTest.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class FileUploadTestDbContextFactory : IDesignTimeDbContextFactory<FileUploadTestDbContext>
    {
        public FileUploadTestDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<FileUploadTestDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            FileUploadTestDbContextConfigurer.Configure(builder, configuration.GetConnectionString(FileUploadTestConsts.ConnectionStringName));

            return new FileUploadTestDbContext(builder.Options);
        }
    }
}
