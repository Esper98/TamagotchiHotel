using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TamagotchiHotel.Models;
using TamagotchiHotel.Repositories;

namespace TamagotchiHotel.Controllers
{
    public class HotelKamersController : Controller
    {
        IKamerRepository kamerRepository = new KamerRepository();
        // GET: HotelKamers
        public ActionResult Index()
        {
            var hotelKamer = kamerRepository.GetAll();
            return View(hotelKamer.ToList());
        }

        // GET: HotelKamers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelKamer hotelKamer = kamerRepository.GetKamer((int)id);
            if (hotelKamer == null)
            {
                return HttpNotFound();
            }
            return View(hotelKamer);
        }

        // GET: HotelKamers/Create
        public ActionResult Create()
        {
            ViewBag.KamerType = new SelectList(kamerRepository.GetAllTypes(), "KamerType", "KamerType");
            return View();
        }

        // POST: HotelKamers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,HoeveelheidBedden,KamerType")] HotelKamer hotelKamer)
        {
            if (ModelState.IsValid)
            {
                var hotelKamers = kamerRepository.GetAll().FirstOrDefault();
                if (hotelKamers != null)
                {
                    int newIndex = kamerRepository.GetAll().Max(p => p.Id);
                    hotelKamer.Id = newIndex + 1;
                }
                else
                {
                    hotelKamer.Id = 0;
                }

                kamerRepository.create(hotelKamer);
                return RedirectToAction("Index");
            }

            ViewBag.KamerType = new SelectList(kamerRepository.GetAllTypes(), "KamerType", "KamerType", hotelKamer.KamerType);
            return View(hotelKamer);
        }

        // GET: HotelKamers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelKamer hotelKamer = kamerRepository.GetKamer((int)id);
            if (hotelKamer == null)
            {
                return HttpNotFound();
            }
            ViewBag.KamerType = new SelectList(kamerRepository.GetAllTypes(), "KamerType", "KamerType", hotelKamer.KamerType);
            return View(hotelKamer);
        }

        // POST: HotelKamers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HoeveelheidBedden,KamerType")] HotelKamer hotelKamer)
        {
            if (ModelState.IsValid)
            {

                if (kamerRepository.GetKamer((int)hotelKamer.Id).Tamagotchi.Count > 0)
                {
                    foreach (Tamagotchi tamagotchi in kamerRepository.GetKamer((int)hotelKamer.Id).Tamagotchi)
                    {
                        tamagotchi.HotelKamerID = null;
                    }
                }
                kamerRepository.Delete(kamerRepository.GetKamer((int)hotelKamer.Id));
                kamerRepository.create(hotelKamer);
                return RedirectToAction("Index");
            }
            ViewBag.KamerType = new SelectList(kamerRepository.GetAllTypes(), "KamerType", "KamerType", hotelKamer.KamerType);
            return View(hotelKamer);
        }

        // GET: HotelKamers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelKamer hotelKamer = kamerRepository.GetKamer((int)id);
            if (hotelKamer == null)
            {
                return HttpNotFound();
            }
            return View(hotelKamer);
        }

        // POST: HotelKamers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HotelKamer hotelKamer = kamerRepository.GetKamer((int)id);
            kamerRepository.Delete(hotelKamer);
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
