using Library.UI.Models.Books;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;

namespace Library.UI.Controllers
{
    public class BooksController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public BooksController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) { return View("~/Views/Book/Create.cshtml", model); }

                if(model.Image == null)
                {
                    return View("~/Views/Book/Create.cshtml", model);
                }

                // image process
                model.ImageUrl = model.Image.FileName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", model.Image.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.Image.CopyToAsync(stream);
                }

                // request
                var client = httpClientFactory.CreateClient();
                var httpRequestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("http://localhost:5224/api/books"),
                    Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
                };
                var httpResponseMessage = await client.SendAsync(httpRequestMessage);
                httpResponseMessage.EnsureSuccessStatusCode();

                var response = await httpResponseMessage.Content.ReadAsStringAsync();

                if (response is not null)
                {
                    return RedirectToAction("Books", "Front", new { message = "Book has been created successfully." });
                }

                return View("~/Views/Book/Create.cshtml", model);
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Front", new { error = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateBookViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) { return View("~/Views/Book/Update.cshtml", model); }

                // image process
                if (model.Image != null)
                {
                    // delete old image
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", model.ImageUrl);
                    System.IO.File.Delete(oldPath);

                    // update & save new image
                    model.ImageUrl = model.Image.FileName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", model.Image.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(stream);
                    }
                }

                // request
                var client = httpClientFactory.CreateClient();
                var httpRequestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Put,
                    RequestUri = new Uri("http://localhost:5224/api/books"),
                    Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
                };
                var httpResponseMessage = await client.SendAsync(httpRequestMessage);
                httpResponseMessage.EnsureSuccessStatusCode();

                var response = await httpResponseMessage.Content.ReadAsStringAsync();

                if (response is not null)
                {
                    return RedirectToAction("Books", "Front", new { message = "Book has been updated successfully." });
                }

                return View("~/Views/Book/Update.cshtml", model);
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Front", new { error = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteBookViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) { return View("~/Views/Book/Delete.cshtml", model); }

                // request
                var client = httpClientFactory.CreateClient();
                var httpRequestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri("http://localhost:5224/api/books/" + model.Id),
                    Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
                };
                var httpResponseMessage = await client.SendAsync(httpRequestMessage);
                httpResponseMessage.EnsureSuccessStatusCode();

                var response = await httpResponseMessage.Content.ReadAsStringAsync();

                if (response is not null)
                {
                    return RedirectToAction("Books", "Front", new { message = "Book has been deleted successfully." });
                }

                return View("~/Views/Book/Delete.cshtml", model);
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Front", new { error = e.Message });
            }
        }
    }
}
