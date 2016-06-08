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
using PublishedBooks.Infrastructure.Security;

namespace PublishedBooks.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        IRepository<User> usersRepository;
        IAuthProvider authprovider;

        public AccountController(IRepository<User> users, IAuthProvider auth)
        {
            usersRepository = users;
            authprovider = auth;
        }

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
                if (authprovider.Authenticate(model.Username, model.Password))
                {
                    CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                    serializeModel.Username = model.Username;

                    string userData = JsonConvert.SerializeObject(serializeModel);
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                             1,
                            model.Username,
                             DateTime.Now,
                             DateTime.Now.AddMinutes(15),
                             false,
                             userData);

                    string encTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    if(Response!=null)
                        Response.Cookies.Add(faCookie);

                    return Redirect(returnUrl ?? Url.Action("Index", "Home"));
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