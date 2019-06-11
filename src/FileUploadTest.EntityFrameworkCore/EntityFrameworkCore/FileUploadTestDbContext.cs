using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using FileUploadTest.Authorization.Roles;
using FileUploadTest.Authorization.Users;
using FileUploadTest.Models;
using FileUploadTest.MultiTenancy;

namespace FileUploadTest.EntityFrameworkCore
{
    public class FileUploadTestDbContext : AbpZeroDbContext<Tenant, Role, User, FileUploadTestDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<UserInfo> UserInfos { get; set; }

        public FileUploadTestDbContext(DbContextOptions<FileUploadTestDbContext> options)
            : base(options)
        {
        }
    }
}
