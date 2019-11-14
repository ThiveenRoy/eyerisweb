using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace P2EyeRIS.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult StaffLogin(IFormCollection formData)
        {
            // Read inputs from textboxes
            // username converted to lowercase
            string loginID = formData["username"].ToString().ToLower();
            string password = formData["password"].ToString();

            if (loginID == "user" && password == "password")
            {                 // Redirect user to the "LecturerMain" view through an action
                return RedirectToAction("LecturerMain");
            }
            //testing>>>>>
            else if (loginID == "no" && password == "no")
            {
                return RedirectToAction("TestChart");
            }
            else
            {
                // Redirect user back to the index view through an action

                return RedirectToAction("Index");
            }
        }

        public ActionResult LecturerMain()
        {
            return View();
        }

        //Below is a test for chart
        public ActionResult TestChart()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}