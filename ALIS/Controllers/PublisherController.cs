﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALIS.Controllers
{
    public class PublisherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}