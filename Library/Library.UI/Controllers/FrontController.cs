using Microsoft.AspNetCore.Mvc;

namespace Library.UI.Controllers
{
    public class FrontController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        public FrontController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public IActionResult Register()
        {
            return View("~/Views/Auth/Register.cshtml");
        }

        public IActionResult Login(string message)
        {
            ViewBag.Message = message;
            return View("~/Views/Auth/Login.cshtml");
        }

        public async Task<IActionResult> Users()
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var httpResponseMessage = await client.GetAsync("http://localhost:5224/api/auth");
                httpResponseMessage.EnsureSuccessStatusCode();
                var response = await httpResponseMessage.Content.ReadAsStringAsync();

                ViewBag.Response = response;

                return View("~/Views/Auth/Users.cshtml");
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}
