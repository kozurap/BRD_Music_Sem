using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BRD_Music_Sem.Models;
using BRD_Music_Sem.Models.ViewModel;
using DataGate.Core;
using Microsoft.AspNetCore.Http;
using Npgsql;
using ProjectArt.MVCPattern;
using ProjectArt.MVCPattern.Attributes;

namespace BRD_Music_Sem.Controllers
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
            return View("TrackList");
        }
        [Action("GetList",Method = MethodType.GET)]
        public IActionResult GetList()
        {
            return Json(_db.Get<TrackListModel>().ToList().Values.ToList());
        }

        [Action("~/TrackList", Method = MethodType.POST)]
        public IActionResult Post(string name, string author)
        {
            try
            {
                Regex reg = new Regex(@"[A-ZА-Я0-9]?[a-zа-я0-9]+ ?[A-ZА-Я0-9]?[a-zа-я0-9]*");
                if (reg.IsMatch(author) == true)
                {
                    List<TrackListModel> tracks = _db.Get<TrackListModel>().ToList().Values.ToList();
                    TrackListModel check = tracks.FirstOrDefault(u => u.Author == author);
                    if (check.Name == "....." && check!=null)
                    {
                        _db.Remove<TrackListModel>(check.Id);
                    }
                    TrackListModel track = new TrackListModel() { Name = name, Author = author };
                    _db.Insert<TrackListModel>(track);
                    return View("TrackList");
                }
                else
                    return ServerError();              
            }
            catch
            {
                Console.WriteLine("Invalid input");
                return ServerError();
            }
        }
        [Action("~/TrackList/SearchTrack", Method = MethodType.GET)]
        public IActionResult Search(string name)
        {
            return Json(_db.Get<TrackListModel>().ToList().Values.ToList().Where(u => u.Name == name));
        }





        [Action(Pattern = "~/GroupList",Method = MethodType.GET)]
        public IActionResult Group()
        {
            return View("GroupList");
        }
        [Action("GetGList",Method = MethodType.GET)]
        public IActionResult GetGroupList()
        {
            return Json(_db.Get<TrackListModel>().ToList().Values.Select(u=>u.Author).Distinct().ToList());
        }

        [Action("~/GroupList", Method = MethodType.POST)]
        public IActionResult Post(string author)
        {
            try
            {
                Regex reg = new Regex(@"[A-ZА-Я0-9]?[a-zа-я0-9]+ ?[A-ZА-Я0-9]?[a-zа-я0-9]*");
                if (reg.IsMatch(author)==true)
                {
                    TrackListModel track = new TrackListModel() { Name = ".....", Author = author };
                    _db.Insert<TrackListModel>(track);
                    return View("GroupList");
                }
                return ServerError();
                 
            }
            catch
            {
                Console.WriteLine("Invalid input");
                return ServerError();
            }
        }
        [Action("~/GroupList/SearchGroup", Method = MethodType.GET)]
        public IActionResult SearchGr(string author)
        {
            return Json(_db.Get<TrackListModel>().ToList().Values.Where(u => u.Author == author).ToList());
        }

        [Action(Pattern = "~/GroupList/{name}", Method = MethodType.GET)]
        public IActionResult GroupView(string name)
        {
            var query = _db.Get<TrackListModel>().ToList().Values.Where(u => u.Author == name).Select(u => u.Name).ToList();
            return View("GroupView", new GroupViewModel()
            {
                Name = name,
                Tracks = query
            });
        }
    }
}