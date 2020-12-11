﻿using System;
 using System.Collections.Generic;
 using System.Linq;
 using BRD_Sport_Sem.Models;
 using DataGate.Core;
 using ProjectArt.MVCPattern.Attributes;
using Controller = ProjectArt.MVCPattern.Controller;
using IActionResult = ProjectArt.MVCPattern.IActionResult;

namespace BRD_Sport_Sem.Controllers
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

        [Action(Pattern = "~/", Method = MethodType.GET)]
        public IActionResult GetRandomTracks()
        {
            Random r = new Random();
            List<TrackListModel> tracks = new List<TrackListModel>();
            var fullList  = _db.Get<TrackListModel>().ToList().Values.ToList();
            var count = fullList.Count;
            for (int i = 0; i < 10; i++)
            {
                tracks.Add(fullList[r.Next(count)]);
            }

            return Json(tracks);
        }
    }
}