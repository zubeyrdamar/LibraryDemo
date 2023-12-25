using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Library.UI.Models.Borrowing;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;

namespace Library.UI.Controllers
{
    public class BorrowingController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IHttpClientFactory httpClientFactory;

        public BorrowingController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            this.configuration = configuration;
            this.httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Borrow(BorrowViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) { return RedirectToAction("Error", "Front", new { error = "You cannot borrow this book because of unknown reasons. Please contact with support." }); }

                // request
                var client = httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("JWT"));
                var httpRequestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(configuration.GetValue<string>("ApiUrl") + "/borrowing/borrow"),
                    Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
                };
                var httpResponseMessage = await client.SendAsync(httpRequestMessage);
                httpResponseMessage.EnsureSuccessStatusCode();

                var response = await httpResponseMessage.Content.ReadAsStringAsync();

                if (response is not null)
                {
                    return RedirectToAction("Books", "Front", new { message = "Book has been borrowed successfully." });
                }

                return View("~/Views/Book/Detail.cshtml", model);
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Front", new { error = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Return(ReturnViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) { return RedirectToAction("Error", "Front", new { error = "You cannot return this book because of unknown reasons. Please contact with support." }); }

                // request
                var client = httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("JWT"));
                var httpRequestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(configuration.GetValue<string>("ApiUrl") + "/borrowing/return"),
                    Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
                };
                var httpResponseMessage = await client.SendAsync(httpRequestMessage);
                httpResponseMessage.EnsureSuccessStatusCode();

                var response = await httpResponseMessage.Content.ReadAsStringAsync();

                if (response is not null)
                {
                    return RedirectToAction("Books", "Front", new { message = "Book has been returned successfully." });
                }

                return View("~/Views/Book/Detail.cshtml", model);
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Front", new { error = e.Message });
            }
        }
    }
}
