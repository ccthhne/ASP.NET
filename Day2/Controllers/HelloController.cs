using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace Day2.Controllers
{
    public class HelloController : Controller
    {
        // 
        // GET: /HelloWorld/

        public string Greeting()
        {
            return "Chào mừng bạn đến với môn học ASP.NET MVC!";
        }

        // 
        // GET: /HelloWorld/Welcome/ 

        public string Welcome(int id1, string name)
        {
            return $"Xin chào {id1} - {name}, chúc bạn học tốt môn học ASP.NET MVC!";
        }
        //IactionResult
        public IActionResult Welcome1(int id1, string name)
        {
            ViewBag.Id = id1;
            ViewData["Name"] = name;
            //ViewData["Message"] = $"Xin chào {numTimes} - {name}, chúc bạn học tốt môn học ASP.NET MVC!";
            return View();
        }
    }
}