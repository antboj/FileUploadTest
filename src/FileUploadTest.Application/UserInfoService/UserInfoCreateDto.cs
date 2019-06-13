using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace FileUploadTest.UserInfoService
{
    public class UserInfoCreateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public IFormFile Image { get; set; }
    }
}
