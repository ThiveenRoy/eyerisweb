using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P2EyeRIS.Models;
using Microsoft.AspNetCore.Http;

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
            else
            {          
                // Redirect user back to the index view through an action      

                return RedirectToAction("Index");             }    
        } 

                public ActionResult LecturerMain() { 
                    return View();
                }

                public IActionResult Privacy()
        {
            return View();
        }

        
    }
}
