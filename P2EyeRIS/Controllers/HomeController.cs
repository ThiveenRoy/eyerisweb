﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P2EyeRIS.Models;

namespace P2EyeRIS.Controllers
{
    public class HomeController : Controller
    {
        

        public IActionResult Index()
        {
            return View();
            //insert login verification
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
    }
}
