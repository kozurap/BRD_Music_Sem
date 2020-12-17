﻿using System;
 using System.Collections.Generic;
 using System.Linq;
 using BRD_Music_Sem.Models;
 using DataGate.Core;
 using ProjectArt.MVCPattern.Attributes;
using Controller = ProjectArt.MVCPattern.Controller;
using IActionResult = ProjectArt.MVCPattern.IActionResult;

namespace BRD_Music_Sem.Controllers
{
    [Controller("Index")]    
    public class IndexController : Controller
    {
        private DataGateORM _db;

        public IndexController(DataGateORM db)
        {
            _db = db;
        }
        [Action(Pattern = "~/",Method = MethodType.GET)]
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}