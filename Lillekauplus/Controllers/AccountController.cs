using Lillekauplus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers.Entities;
using System.Web.Security;

namespace Lillekauplus.Controllers
{
    public class AccountController : Controller
    {
        UserContext db = new UserContext();

        public int getRoleId()
        {
            int roleid = 0;
            foreach (var u in db.Users)
            {
                if (u.Name == User.Identity.Name)
                {
                    roleid = u.RoleId;
                    break;
                }
            }
            return roleid;
        }

        public ActionResult Login()
        {
            ViewBag.Roleid = getRoleId();
            ViewBag.Username = User.Identity.Name;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                Models.User user = null;
                using (UserContext db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Name == model.Name && u.Password == model.Password);
                }

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    ViewBag.Roleid = getRoleId();
                    ViewBag.Username = User.Identity.Name;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Такого пользователя не существует");
                }
            }

            return View(model);
        }

        public ActionResult Register()
        {
            ViewBag.Roleid = getRoleId();
            ViewBag.Username = User.Identity.Name;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                Models.User user = null;
                using (UserContext db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Name == model.Name);
                }

                if (user == null)
                {
                    using (UserContext db = new UserContext())
                    {
                        db.Users.Add(new Models.User { Name = model.Name, Password = model.Password, Age = model.Age, RoleId = 2 });
                        db.SaveChanges();

                        user = db.Users.Where(u => u.Name == model.Name && u.Password == model.Password).FirstOrDefault();
                    }

                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Name, true);
                        ViewBag.Roleid = getRoleId();
                        ViewBag.Username = User.Identity.Name;

                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            return View(model);
        }
    }
}