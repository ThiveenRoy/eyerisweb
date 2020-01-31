using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Firebase.Database;
using Firebase.Database.Query;

namespace P2EyeRIS.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
            //insert login verification
        }
        public IActionResult studentLogin()
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
            {
                TempData["LoggedStaffName"] = "Tay Zonday"; //hardcoded, can try testing by adding unique username/password
                TempData["LoggedStaffId"] = "S97652931E"; //hardcoded, can try testing by adding unique username/password

                return RedirectToAction("Index", "Staff");
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
        [HttpPost]
        public ActionResult StudentLogin(IFormCollection formData)
        {
            // Read inputs from textboxes
            // username converted to lowercase
            string loginID = formData["username"].ToString().ToLower();
            string password = formData["password"].ToString();

            if (loginID == "user" && password == "password")
            {                 // Redirect user to the "LecturerMain" view through an action
                return RedirectToAction("Chart");
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
        public ActionResult Chart()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}