using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Library.UI.Models.Borrowing;

namespace Library.UI.Controllers
{
    public class BorrowingController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public BorrowingController(IHttpClientFactory httpClientFactory)
        {
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
                var httpRequestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("http://localhost:5224/api/borrowing/borrow"),
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
                var httpRequestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("http://localhost:5224/api/borrowing/return"),
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
