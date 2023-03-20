using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using WebApplication1.Models;
using WebApplication1.Static_Links;
using WebApplication1.Service;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApplication1.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {

        [Route("IndexLog")]
        public ActionResult IndexLog()
        {
            return View("IndexLog");
        }

        [Route("sendLog")]
        public async Task<IActionResult> sendLog(Login log, Messanger messanger)
        {
            if (!ModelState.IsValid)
            {
                return View("IndexLog");
            }

            if (log.Password == null && log.Email == null)
            {
                return View("IndexLog");
            }

            string jsonRequest = JsonSerializer.Serialize(log);
            var jsonResponse = await messanger.PostRequestAsync("http://greenfood:80/Accounts/SignIn", jsonRequest);

            if (jsonResponse.StatusCode == 422)
            {
                ModelState.AddModelError("Password", "Неверный логин или пароль!");
                return View("IndexLog");
            }

            return RedirectToAction("CompleteLog");
        }

        [Route("CompleteLog")]
        public IActionResult CompleteLog()
        {
            ViewBag.Message = "Вы успешно вошли!";
            return View();
        }


        [Route("IndexReg")]
        public ActionResult IndexReg()
        {
            return View("IndexReg");
        }


        [Route("sendReg")]
        public async Task<IActionResult> sendReg(Registration reg, Messanger messanger)
        {
            if (!ModelState.IsValid)
            {
                return View("IndexReg");
            }
            if (reg.UserName == null && reg.PhoneNumber == null && reg.Email == null && reg.Password == null)
            {
                return View("IndexReg");
            }
            string jsonRequest = JsonSerializer.Serialize(reg);
            var jsonResponse = await messanger.PostRequestAsync("http://greenfood:80/Accounts/SignUp", jsonRequest);
            

            if (jsonResponse.StatusCode != 201)
            {
                var Error = JsonSerializer.Deserialize<ErrorModel>(jsonResponse.Content);
                ModelState.AddModelError("Password", Error.Message);
                return View("IndexReg");
            }

            return RedirectToAction("CompleteReg");
        }

        [Route("CompleteReg")]
        public IActionResult CompleteReg()
        {
            ViewBag.Message = "Вы успешно зерегистрировались!";
            return View();
        }
    }
}
