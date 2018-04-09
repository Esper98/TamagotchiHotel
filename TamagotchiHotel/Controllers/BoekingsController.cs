using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TamagotchiHotel.Models;
using TamagotchiHotel.Repositories;
using TamagotchiHotel.ViewModels;

namespace TamagotchiHotel.Controllers
{
    public class BoekingsController : Controller
    {
        private ITamagotchiRepository tamagotchiRepository = new TamagotchiRepository();
        private IKamerRepository kamerRepository = new KamerRepository();

        BoekingViewModel boeking = new BoekingViewModel();

        // GET: Boekings
        public ActionResult Index()
        {
            boeking = new BoekingViewModel();

            boeking.Kamer = kamerRepository.GetAllFree();
            boeking.Tamagotchi = tamagotchiRepository.GetAllAlive();

            return View(boeking);
        }

        public ActionResult Boeken(int? id)
        {
            boeking = new BoekingViewModel();

            boeking.Tamagotchi = tamagotchiRepository.GetAllAliveWithoutRoom();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var CurrentKamer = new List<HotelKamer>();
            CurrentKamer.Add(kamerRepository.GetKamer((int)id));
            boeking.Kamer = CurrentKamer;
            if (boeking.Kamer.FirstOrDefault() == null)
            {
                return HttpNotFound();
            }
            //wat doet deze regel precies?
            //ViewBag.HotelKamerID = new SelectList(db.HotelKamer, "Id", "KamerType", tamagotchi.HotelKamerID);
            return View(boeking);
        }

        public ActionResult Toevoegen(int? tamagotchiID, int? id)
        {

            HotelKamer kamer = kamerRepository.GetKamer((int)id);


            tamagotchiRepository.ChangeKamer((int)tamagotchiID, kamer.Id);

            if (kamer.HoeveelheidBedden == kamer.Tamagotchi.Count)
            {

                return RedirectToAction("Index");
            }
            return RedirectToAction("Boeken", new { id });

            //wat doet deze regel precies?
            //ViewBag.HotelKamerID = new SelectList(db.HotelKamer, "Id", "KamerType", tamagotchi.HotelKamerID);

        }
        public ActionResult DeleteUitLijst(int? tamagotchiID)
        {
            //zoeken naar de ID van de huide kamer waar je in zit doormidel van de tamagotchi die je wilt gaan verwijderen die er dus nog wel in zit.
            int kamerID = (int)tamagotchiRepository.GetTamagotchi((int)tamagotchiID).HotelKamerID;
            //slaat de kamer op waar we net de id van hebben gevonden
            var kamer = kamerRepository.GetKamer(kamerID);
            //we zitten de hotekamerID van de megegeven tamagotchi op null dus verwijderd die hem ook uit de andere
            tamagotchiRepository.RemoveKamer((int)tamagotchiID);

            //moet op deze manier anders wordt de meegegeven waarde altijd null voor één of andere reden
            return RedirectToAction("Boeken", new { id = kamer.Id });

        }
    }
}