using Library.UI.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace Library.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IHttpClientFactory httpClientFactory;

        public AuthController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            this.configuration = configuration;
            this.httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) { return View(model); }

                var client = httpClientFactory.CreateClient();
                var httpRequestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(configuration.GetValue<string>("ApiUrl") + "/auth/register"),
                    Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
                };
                var httpResponseMessage = await client.SendAsync(httpRequestMessage);
                httpResponseMessage.EnsureSuccessStatusCode();

                var response = await httpResponseMessage.Content.ReadAsStringAsync();
                
                if(response is not null)
                {
                    return RedirectToAction("Login", "Front", new { message = "Successfully Registered" });
                }

                return View("~/Views/Auth/Register.cshtml", model);
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Front", new { error = e.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) { return View(model); }

                var client = httpClientFactory.CreateClient();
                var httpRequestMessage = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(configuration.GetValue<string>("ApiUrl") + "/auth/login"),
                    Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
                };
                var httpResponseMessage = await client.SendAsync(httpRequestMessage);
                httpResponseMessage.EnsureSuccessStatusCode();

                var response = await httpResponseMessage.Content.ReadFromJsonAsync<UserViewModel>();

                if (response is not null)
                {
                    // Save user data as session
                    HttpContext.Session.SetString("JWT", response.token);
                    HttpContext.Session.SetString("Role", response.role);
                    HttpContext.Session.SetString("UserId", response.user);

                    return RedirectToAction("Books", "Front");
                }

                return View("~/Views/Auth/Login.cshtml", model);
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Front", new { error = e.Message });
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Clear();
                return View("~/Views/Auth/Login.cshtml");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Front", new { error = e.Message });
            }
        }

    }
}
