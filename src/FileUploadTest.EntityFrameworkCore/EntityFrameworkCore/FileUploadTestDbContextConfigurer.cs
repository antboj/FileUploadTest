using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace FileUploadTest.EntityFrameworkCore
{
    public static class FileUploadTestDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<FileUploadTestDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<FileUploadTestDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
