using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using WebApplication1.Models;
using WebApplication1.Static_Links;

namespace WebApplication1.Controllers
{
    [Route("Reg")]
    public class RegController : Controller
    {
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
                    string jsonResponse = await Post.PostRequestAsync("http://localhost:9637/Accounts/SignUp", jsonRequest);
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
