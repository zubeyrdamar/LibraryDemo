using Library.UI.Models.Books;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Library.UI.Controllers
{
    public class FrontController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IHttpClientFactory httpClientFactory;

        public FrontController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            this.configuration = configuration;
            this.httpClientFactory = httpClientFactory;
        }

        public IActionResult Error(string error)
        {
            ViewBag.Error = error;
            return View("~/Views/Error/HttpError.cshtml");
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

        public async Task<IActionResult> Books(string message)
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization =  new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("JWT"));
                var httpResponseMessage = await client.GetAsync(configuration.GetValue<string>("ApiUrl") + "/books");
                httpResponseMessage.EnsureSuccessStatusCode();

                List<BookViewModel> response = [.. await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<BookViewModel>>()];
                ViewBag.Books = response;
                ViewBag.Message = message;

                return View("~/Views/Book/List.cshtml");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Front", new { error = e.Message });
            }
        }

        public async Task<IActionResult> Detail(Guid id)
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("JWT"));
                var httpResponseMessage = await client.GetAsync(configuration.GetValue<string>("ApiUrl") + "/books/" + id);
                httpResponseMessage.EnsureSuccessStatusCode();

                BookViewModel response = await httpResponseMessage.Content.ReadFromJsonAsync<BookViewModel>();
                ViewBag.Book = response;

                return View("~/Views/Book/Detail.cshtml");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Front", new { error = e.Message }); 
            }
        }

        public IActionResult Create()
        {
            return View("~/Views/Book/Create.cshtml");
        }

        public async Task<IActionResult> Update(Guid id)
        {
            try
            {
                var client = httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("JWT"));
                var httpResponseMessage = await client.GetAsync(configuration.GetValue<string>("ApiUrl") + "/books/" + id);
                httpResponseMessage.EnsureSuccessStatusCode();

                UpdateBookViewModel response = await httpResponseMessage.Content.ReadFromJsonAsync<UpdateBookViewModel>();

                return View("~/Views/Book/Update.cshtml", response);
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Front", new { error = e.Message });
            }
        }

        public IActionResult Delete(DeleteBookViewModel model)
        {
            return View("~/Views/Book/Delete.cshtml", model);
        }
    }
}
