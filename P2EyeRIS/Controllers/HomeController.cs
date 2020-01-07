using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P2EyeRIS.Models;
using Microsoft.AspNetCore.Http;
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
            if (loginID == "fuckboiroy" && password == "lmao")
            {                 // Redirect user to the "LecturerMain" view through an action         
                return RedirectToAction("LecturerMain");         
            }
            else
            {
                // Redirect user back to the index view through an action      
                TempData["gay"] = "Bruh hint : username: fuckboyroi pw: lmao";
                return RedirectToAction("Index");             }    
        } 

                public ActionResult LecturerMain() { 
                    return View();
                }

                public IActionResult Privacy()
        {
            return View();
        }
        public async Task<ActionResult> About()
        {
            //Simulate test user data and login timestamp
            var userId = "12345";
            var currentLoginTime = DateTime.UtcNow.ToString("MM/dd/yyyy HH:mm:ss");

            //Save non identifying data to Firebase
            var currentUserLogin = new LoginData() { TimestampUtc = currentLoginTime };
            var firebaseClient = new FirebaseClient("https://eeyes-68b9c.firebaseio.com");
            var result = await firebaseClient
              .Child("Users/" + userId + "/Logins")
              .PostAsync(currentUserLogin);

            //Retrieve data from Firebase
            var dbLogins = await firebaseClient
              .Child("Users")
              .Child(userId)
              .Child("Logins")
              .OnceAsync<LoginData>();

            var timestampList = new List<DateTime>();

            //Convert JSON data to original datatype
            foreach (var login in dbLogins)
            {
                timestampList.Add(Convert.ToDateTime(login.Object.TimestampUtc).ToLocalTime());
            }

            //Pass data to the view
            ViewBag.CurrentUser = userId;
            ViewBag.Logins = timestampList.OrderByDescending(x => x);
            return View();
        }


    }
}
