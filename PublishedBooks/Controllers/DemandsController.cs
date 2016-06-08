using PublishedBooksDAL.Repositories;
using PublishedBooksDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PublishedBooks.Controllers
{
    public class DemandsController : Controller
    {
        IRepository<User> usersRepository;

        public DemandsController(IRepository<User> users)
        {
            usersRepository = users;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Demanded Books";
            var curUser = usersRepository.SearchFor(u => u.Username == HttpContext.User.Identity.Name)?.First();

            return View(curUser?.Demands);
        }

        [HttpPost]
        public ActionResult Add(string booktitle)
        {
            if (ModelState.IsValid)
            {
                var curUser = usersRepository.SearchFor(u => u.Username == HttpContext.User.Identity.Name).First();
                if (curUser.Demands == null)
                    curUser.Demands = new List<string>();
                curUser.Demands.Add(booktitle);
                usersRepository.Update(curUser);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Remove(string booktitle)
        {
            if (ModelState.IsValid)
            {
                var curUser = usersRepository.SearchFor(u => u.Username == HttpContext.User.Identity.Name).First();
                curUser.Demands.Remove(booktitle);
                usersRepository.Update(curUser);
            }

            return RedirectToAction("Index");
        }
    }
}