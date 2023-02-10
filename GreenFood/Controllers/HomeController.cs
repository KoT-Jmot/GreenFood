using GreenFood.Domain.Models;
using GreenFood.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GreenFood.Web.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        private readonly ApplicationContext _db;
        public HomeController(ApplicationContext db)
        {
            _db = db;
        }
        [Route("Index")]
        public string Index()
        {
            return "Hello world!";
        }
    }
}
