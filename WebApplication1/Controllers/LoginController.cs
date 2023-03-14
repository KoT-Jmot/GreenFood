using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using WebApplication1.Models;
using WebApplication1.Static_Links;

namespace WebApplication1.Controllers
{
    [Route("Login")]
    public class LoginController : Controller
    {

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
                    string jsonResponse = await Post.PostRequestAsync("http://localhost:9637/Accounts/SignIn", jsonRequest);
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
