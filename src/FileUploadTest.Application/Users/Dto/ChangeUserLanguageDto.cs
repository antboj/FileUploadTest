using System.ComponentModel.DataAnnotations;

namespace FileUploadTest.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}