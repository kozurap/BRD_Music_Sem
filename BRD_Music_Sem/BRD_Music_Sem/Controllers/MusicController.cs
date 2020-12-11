using System;
using System.Collections.Generic;
using System.Linq;
using BRD_Sport_Sem.Models;
using BRD_Sport_Sem.Models.ViewModel;
using DataGate.Core;
using Microsoft.AspNetCore.Http;
using Npgsql;
using ProjectArt.MVCPattern;
using ProjectArt.MVCPattern.Attributes;

namespace BRD_Sport_Sem.Controllers
{
    [Controller("Music")]
    public class MusicController : Controller
    {
        private DataGateORM _db;

        public MusicController(DataGateORM db)
        {
            _db = db;
        }
        [Action(Pattern = "~/TrackList",Method = MethodType.GET)]
        public IActionResult Music()
        {
            return View("Music");
        }
        [Action(Pattern = "~/TrackList",Method = MethodType.GET)]
        public IActionResult GetList()
        {
            return Json(_db.Get<TrackListModel>().ToList().Values.ToList());
        }
        [Action(Pattern = "~/GroupList",Method = MethodType.GET)]
        public IActionResult Group()
        {
            return View("Group");
        }
        [Action(Pattern = "~/GroupList",Method = MethodType.GET)]
        public IActionResult GetGroupList()
        {
            return Json(_db.Get<TrackListModel>().ToList().Values.Select(u=>u.Author).Distinct().ToList());
        }

        [Action(Pattern = "~/GroupList/{name}", Method = MethodType.GET)]
        public IActionResult GroupView(string name)
        {
            var query = _db.Get<TrackListModel>().ToList().Values.Where(u => u.Name == name)
                .Select(u => u.Author).ToList();
            return View("GroupView", new GroupViewModel()
            {
                Name = name,
                Tracks = query
            });
        }
    }
}