using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("Login")]
    public class LoginController : Controller
    {
        // HttpClient создается 1 раз на все время работы приложения
        private static readonly HttpClient client = new HttpClient();

        // POST
        private static async Task<string> PostRequestAsync(string url, string json)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
            requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");
            using HttpResponseMessage responce = await client.SendAsync(requestMessage);
            return await responce.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        [Route("Index")]
        public ActionResult Index()
        {
            return View("Index");
        }

        [Route("send")]
        public async Task<IActionResult> send(Login log)
        {
            if (ModelState.IsValid)
            {
                if (log.Password != null && log.Email != null)
                {
                    string jsonRequest = JsonSerializer.Serialize(log);
                    string jsonResponse = await PostRequestAsync("http://localhost:9637/Accounts/SignIn", jsonRequest);
                    return RedirectToAction("Complete");
                }
            }
            return View("Index");
        }

        [Route("Complete")]
        public IActionResult Complete()
        {
            ViewBag.Message = "Вы успешно вошли!";
            return View();
        }
    }
}
