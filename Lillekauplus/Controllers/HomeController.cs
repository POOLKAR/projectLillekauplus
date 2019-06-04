using Lillekauplus.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lillekauplus.Controllers
{
    public class HomeController : Controller
    {
        // создаем контекст данных
        LilledContext db = new LilledContext();

        public ActionResult Index()
        {
            // получаем из бд все объекты Book
            IEnumerable<Lilled> Lilleds = db.Lilleds;
            // передаем все объекты в динамическое свойство Books в ViewBag
            ViewBag.Lilleds = Lilleds;
            // возвращаем представление
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your apllication description page";

            return View();
        }
        [HttpGet]
        public ActionResult Buy(int id)
        {
            ViewBag.LilledId = id;
            return View();
        }
        [HttpPost]
        public string Buy(Purchase purchase)
        {
            purchase.Date = DateTime.Now;
            // добавляем информацию о покупке в базу данных
            db.Purchases.Add(purchase);
            // сохраняем в бд все изменения
            db.SaveChanges();
            return "Спасибо," + purchase.Person + ", за покупку!";
        }

        public ActionResult DeleteLilled(int id)
        {
            Lilled b = db.Lilleds.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            return View(b);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Lilled b = db.Lilleds.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            db.Lilleds.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public ActionResult Delete(int id)
        //{
        //    Lilled b = db.Lilleds.Find(id);
        //    if (b == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(b);
        //}
        //[HttpPost,ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Lilled b = db.Lilleds.Find(id);
        //    if (b == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    db.Lilleds.Remove(b);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        [HttpGet]
        public ActionResult EditLilled(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Lilled lilled = db.Lilleds.Find(id);
            if (lilled != null)
            {
                return View(lilled);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult EditLilled(Lilled lilled)
        {
            db.Entry(lilled).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }
            Lilled lilled = db.Lilleds.Find(id);
            if (lilled !=null)
            {
                return View(lilled);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult Edit(Lilled lilled)
        {
            db.Entry(lilled).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult CreateLilled()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateLilled(Lilled lilled)
        {
            db.Lilleds.Add(lilled); //INSERT
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Lilled lilled)
        {
            db.Lilleds.Add(lilled); //INSERT
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}