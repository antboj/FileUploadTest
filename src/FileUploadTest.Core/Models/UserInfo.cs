using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FileUploadTest.Models
{
    public class UserInfo : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Image { get; set; }
    }
}
