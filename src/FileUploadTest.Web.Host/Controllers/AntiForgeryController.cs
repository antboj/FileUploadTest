using Microsoft.AspNetCore.Antiforgery;
using FileUploadTest.Controllers;

namespace FileUploadTest.Web.Host.Controllers
{
    public class AntiForgeryController : FileUploadTestControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
