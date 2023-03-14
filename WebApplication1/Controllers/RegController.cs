using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("Reg")]
    public class RegController : Controller
    {
        //HttpClient создается 1 раз на все время работы приложения
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
        public async Task<IActionResult> send(Registration reg)
        {
            if (ModelState.IsValid)
            {
                if (reg.UserName != null && reg.PhoneNumber != null && reg.Email != null && reg.Password != null)
                {
                    string jsonRequest = JsonSerializer.Serialize(reg);
                    string jsonResponse = await PostRequestAsync("http://localhost:9637/Accounts/SignUp", jsonRequest);
                    return RedirectToAction("Complete");
                }
            }
            return View("Index");
        }

        [Route("Complete")]
        public IActionResult Complete()
        {
            ViewBag.Message = "Вы успешно зерегистрировались!";
            return View();
        }
    }
}
