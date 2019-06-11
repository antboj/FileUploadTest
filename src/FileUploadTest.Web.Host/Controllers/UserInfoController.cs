using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Controllers;
using Abp.Domain.Repositories;
using FileUploadTest.Models;
using FileUploadTest.UserInfoService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileUploadTest.Web.Host.Controllers
{
    public class UserInfoController : AbpController
    {
        private readonly IRepository<UserInfo> _repository;
        private readonly IHostingEnvironment _environment;

        public UserInfoController(IRepository<UserInfo> repository, IHostingEnvironment environment)
        {
            _repository = repository;
            _environment = environment;
        }

        [HttpPost("api/UserInfo/Create")]
        public async Task Create(UserInfoDto input)
        {
            await Upload(input.Image);
            var userInfo = new UserInfo
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                Username = input.Username,
                Role = input.Role,
                Image = input.Image.FileName
            };
            await _repository.InsertAsync(userInfo);
        }

        //[HttpPost("api/UserInfo/Upload")]
        protected async Task Upload([FromForm]IFormFile file)
        {
            if (!Directory.Exists(_environment.WebRootPath + "\\uploads\\"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "\\uploads\\");
            }
            var localFileName = Path.GetFileName(file.FileName);
            //var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads\\", localFileName);
            if (file.Length > 0)
            {
                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileSteam);
                }
            }
        }
    }
}
