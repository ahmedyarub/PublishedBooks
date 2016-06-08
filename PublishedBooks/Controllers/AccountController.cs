using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using PublishedBooks.Models;
using PublishedBooksDAL.Entities;
using PublishedBooksDAL.Repositories;
using PublishedBooks.Security;

namespace PublishedBooks.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        MongoDbRepository<User> usersRepository = new MongoDbRepository<User>();

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                var user = usersRepository.SearchFor(u => u.Username == model.Username && u.Password == model.Password).FirstOrDefault();
                if (user != null)
                {
                    CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                    serializeModel.Username = user.Username;

                    string userData = JsonConvert.SerializeObject(serializeModel);
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                             1,
                            user.Username,
                             DateTime.Now,
                             DateTime.Now.AddMinutes(15),
                             false,
                             userData);

                    string encTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    Response.Cookies.Add(faCookie);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Incorrect username and/or password");
            }

            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                usersRepository.Insert(new User() { Username = model.Username, Password = model.Password });
                return RedirectToActionPermanent("Login");
            }

            return View(model);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account", null);
        }
    }
}