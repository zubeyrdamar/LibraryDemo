using Library.UI.Models.Responses;
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

        public async Task<IActionResult> Books()
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                var httpResponseMessage = await client.GetAsync("http://localhost:5224/api/books");
                httpResponseMessage.EnsureSuccessStatusCode();

                List<BookResponseDTO> response = [.. await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<BookResponseDTO>>()];
                ViewBag.Books = response;

                return View("~/Views/Book/List.cshtml");
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
