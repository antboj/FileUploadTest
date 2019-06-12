using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Abp;
using Abp.AspNetCore.Mvc.Controllers;
using Abp.Domain.Repositories;
using FileUploadTest.Models;
using FileUploadTest.UserInfoService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using ContentDispositionHeaderValue = Microsoft.Net.Http.Headers.ContentDispositionHeaderValue;
using MediaTypeHeaderValue = Microsoft.Net.Http.Headers.MediaTypeHeaderValue;

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
            var imgName = await Upload(input.Image);
            var userInfo = new UserInfo
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                Username = input.Username,
                Role = input.Role,
                Image = imgName
            };
            await _repository.InsertAsync(userInfo);
        }

        //[HttpPost("api/UserInfo/Upload")]
        protected async Task<string> Upload([FromForm]IFormFile file)
        {
            var uploadsFolder = _environment.WebRootPath + "\\uploads\\";
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var uniqueName = Guid.NewGuid() + "_" + file.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueName);
            if (file.Length > 0)
            {
                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileSteam);
                }
            }

            return uniqueName;
        }

        [HttpPost("api/UserInfo/Upload")]
        public async Task StreamUpload()
        {
            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            {
                throw new AbpException("rip");
            }

            var formAccumulator = new KeyValueAccumulator();
            string targetFilePath = _environment.WebRootPath + "\\uploads\\";

            if (!Directory.Exists(targetFilePath))
            {
                Directory.CreateDirectory(targetFilePath);
            }

            var boundary = MultipartRequestHelper.GetBoundary(
                MediaTypeHeaderValue.Parse(Request.ContentType),
                FormOptions.DefaultMultipartBoundaryLengthLimit);

            var reader = new MultipartReader(boundary, HttpContext.Request.Body);

            var section = await reader.ReadNextSectionAsync();

            while (section != null)
            {
                var hasContentDispositionHeader =
                    ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition);

                if (hasContentDispositionHeader)
                {
                    if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                    {
                        var fullPath = Path.Combine(targetFilePath, contentDisposition.FileName.ToString());
                        using (var targetStream = System.IO.File.Create(fullPath))
                        {
                            await section.Body.CopyToAsync(targetStream);
                        }
                    }
                    else if (MultipartRequestHelper.HasFormDataContentDisposition(contentDisposition))
                    {
                        var key = HeaderUtilities.RemoveQuotes(contentDisposition.Name);
                        //var encoding = Encoding.GetEncoding(section.ToString());
                        using (var streamReader = new StreamReader(
                            section.Body,
                            detectEncodingFromByteOrderMarks: true))
                        {
                            // The value length limit is enforced by MultipartBodyLengthLimit
                            var value = await streamReader.ReadToEndAsync();
                            if (String.Equals(value, "undefined", StringComparison.OrdinalIgnoreCase))
                            {
                                value = String.Empty;
                            }

                            formAccumulator.Append(key.ToString(), value);

                            if (formAccumulator.ValueCount > FormOptions.DefaultBufferBodyLengthLimit)
                            {
                                throw new AbpException("rip");
                            }
                        }
                    }
                    section = await reader.ReadNextSectionAsync();
                }
                
            }
            var userInfo = new UserInfoDto();
            var formValueProvider = new FormValueProvider(
                BindingSource.Form,
                new FormCollection(formAccumulator.GetResults()),
                CultureInfo.CurrentCulture);
            var bindingSuccessful = await TryUpdateModelAsync(userInfo, prefix: "",
                valueProvider: formValueProvider);
            if (bindingSuccessful)
            {
                var us = new UserInfo
                {
                    Username = userInfo.Username,
                    LastName = userInfo.LastName,
                    FirstName = userInfo.FirstName,
                    Role = userInfo.Role,
                    Image = userInfo.Image.FileName
                };
                _repository.Insert(us);
            }
        }
    }
}
