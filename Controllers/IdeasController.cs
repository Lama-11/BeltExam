using BeltExam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BeltExam.Controllers
{
    public class IdeasController : Controller
    {
        private MyContext _db;
        private int? uid
        {
            get { return HttpContext.Session.GetInt32("UserId"); }
        }
        private bool isLoggedIn
        {
            get { return uid != null; }
        }

        // here we can "inject" our context service into the constructor
        public IdeasController(MyContext context)
        {
            _db = context;
        }
        [HttpGet("brigth_ideas")]
        public IActionResult Dashboard()
        {
            if (!isLoggedIn)
            {
                return RedirectToAction("Index", "Home");
            }
            
            User u = _db.Users.FirstOrDefault(u => u.UserId == (int)uid);
            List<Ideas> AllIdeas = 
            _db.ideas.Include(i => i.PostedBy)
            .Include(i => i.Likes)
            .OrderByDescending(i => i.Likes.Count)
            .ToList();
            ViewBag.User = u;
            ViewBag.Ideas = AllIdeas;
            return View();
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreateIdea(Ideas NewIdea)
        {
            if (!isLoggedIn)
            {
                return RedirectToAction("Index", "Home");
            }

                if (ModelState.IsValid)
                {
                    NewIdea.UserId = (int)uid;
                    _db.ideas.Add(NewIdea);
                    _db.SaveChanges();
                    return RedirectToAction("Dashboard");
                }
                User u = _db.Users.FirstOrDefault(u => u.UserId == (int)uid);
                List<Ideas> AllIdeas = _db.ideas.Include(i => i.Likes)
                .Include(i => i.PostedBy)
                .OrderByDescending(i => i.Likes.Count)
                .ToList();
                ViewBag.User = u;
                ViewBag.Ideas = AllIdeas;
                return View("Dashboard");
            }

        [HttpGet("brigth_ideas/{ideaId}")]
        public IActionResult OneIdea(int ideaId)
        {
            if (!isLoggedIn)
            {
                return RedirectToAction("Index", "Home");
            }
            Ideas thisIdea = _db
            .ideas
            .Include(m => m.PostedBy)
            .Include(m => m.Likes)
            .ThenInclude(f => f.Users)
            .FirstOrDefault(m => m.IdeaId == ideaId);
            User u = _db.Users.FirstOrDefault(u => u.UserId == (int)uid);
            ViewBag.User = u;
            return View(thisIdea);
        }
        [HttpGet]
        [Route("delete/{ideaId}")]
        public IActionResult Delete(int ideaId)
        {
            if (!isLoggedIn)
            {
                return RedirectToAction("Index", "Home");
            }
            Ideas IdeaToDelete = _db.ideas.FirstOrDefault(i => i.IdeaId == ideaId);
            _db.Remove(IdeaToDelete);
            _db.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        [HttpGet("like/{ideaId}")]
        public IActionResult Like(int ideaId)
        {
            // create new Like instance
            Like like = new Like();
            like.UserId = (int)uid;
            like.IdeaId = ideaId;
            _db.Likes.Add(like);
            _db.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        [Route("user/{userId}")]
        public IActionResult OneUser(int userId)
        {
            User u = _db.Users.Where(u => u.UserId == userId)
            .Include(i => i.Idea)
            .Include(l => l.Likes)
            .FirstOrDefault();
            ViewBag.OneUser = u;
            return View();
        }
    }
}