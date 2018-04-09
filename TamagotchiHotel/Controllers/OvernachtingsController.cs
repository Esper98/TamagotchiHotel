using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TamagotchiHotel.Models;
using TamagotchiHotel.Repositories;

namespace TamagotchiHotel.Controllers
{
    public class OvernachtingsController : Controller
    {
        IKamerRepository kamerRepository = new KamerRepository();
        ITamagotchiRepository tamagotchiRepository = new TamagotchiRepository();
        // GET: Overnachtings
        Random random = new Random();
        public ActionResult Index()
        {
            return View();
        }
        //We moeten nog checken op het niet hebben van een kamer.
        private void DoKamerMutaties(string kamerType, HotelKamer kamer)
        {
            switch (kamerType)
            {
                case "RustKamer":
                    foreach (var RustkamerTamagotchi in kamer.Tamagotchi)
                    {
                        RustkamerTamagotchi.Centjes -= 10;
                        RustkamerTamagotchi.Gezondheid += 20;
                        RustkamerTamagotchi.Verveling += 10;
                        RustkamerTamagotchi.HotelKamerID = null;
                    }
                    break;

                case "GameKamer":
                    foreach (var GamekamerTamagotchi in kamer.Tamagotchi)
                    {
                        GamekamerTamagotchi.Centjes -= 20;
                        GamekamerTamagotchi.Verveling = 0;
                        GamekamerTamagotchi.HotelKamerID = null;

                    }
                    break;
                case "WerkKamer":
                    foreach (var WerkkamerTamagotchi in kamer.Tamagotchi)
                    {
                        WerkkamerTamagotchi.Centjes += random.Next(10, 61);
                        WerkkamerTamagotchi.Verveling += 20;
                        WerkkamerTamagotchi.HotelKamerID = null;

                    }
                    break;

                case "VerfKamer":
                    foreach (var VerfKamerTamagotchi in kamer.Tamagotchi)
                    {

                        bool geverfd = false;
                        while (!geverfd)
                        {
                            int kleurKeuze = random.Next(1, 14);

                            if (kleurKeuze <= 4 && kleurKeuze >= 1)
                            {
                                if (!VerfKamerTamagotchi.Kleur.Equals("Groen"))
                                {
                                    VerfKamerTamagotchi.Kleur = "Groen";
                                    geverfd = true;
                                }
                            }

                            else if (kleurKeuze <= 8 && kleurKeuze >= 5)
                            {
                                if (!VerfKamerTamagotchi.Kleur.Equals("Blauw"))
                                {
                                    VerfKamerTamagotchi.Kleur = "Blauw";
                                    geverfd = true;
                                }
                            }

                            else if (kleurKeuze <= 12 && kleurKeuze >= 9)
                            {
                                if (!VerfKamerTamagotchi.Kleur.Equals("Rood"))
                                {
                                    VerfKamerTamagotchi.Kleur = "Rood";
                                    geverfd = true;
                                }
                            }

                            else if (kleurKeuze == 13)
                            {
                                if (!VerfKamerTamagotchi.Kleur.Equals("Goud"))
                                {
                                    VerfKamerTamagotchi.Kleur = "Goud";
                                    geverfd = true;
                                }
                            }
                        }

                        VerfKamerTamagotchi.Centjes -= 50;
                        VerfKamerTamagotchi.Gezondheid -= 10;
                        VerfKamerTamagotchi.Verveling -= 10;
                        VerfKamerTamagotchi.HotelKamerID = null;
                        if (VerfKamerTamagotchi.Gezondheid <= 0)
                        {
                            VerfKamerTamagotchi.Gezondheid = 0;
                            VerfKamerTamagotchi.Dood = true;
                        }

                    }

                    break;
                case "VechtKamer":
                    //maken een random aan om een winnaar mee te kiezen

                    //we stoppen de tamagotchis van deze kamer in een nieuwe array om met een index te te kunnen werken
                    List<Tamagotchi> tamagotchis = new List<Tamagotchi>();
                    //de winnaar is gekozen op basis van de indexen, maar kamer.tamagotchi hebben geen index dus moeten we een array gebruiken
                    int winnaar = random.Next(0, kamer.Tamagotchi.Count);
                    //hier voegen we de tamagotchis toe aan de array
                    foreach (var item in kamer.Tamagotchi)
                    {
                        tamagotchis.Add(item);
                    }

                    foreach (var VechtkamerTamagotchi in kamer.Tamagotchi)
                    {
                        //we lopen nu door de orginele collection weer en als de ID niet gelijk staat aan die van de winnen krijgt die de minpunten
                        //tamagotchis[winnaar] is de winnaar en die gaat dus als enige niet de if in maar gaat naar de else
                        if (VechtkamerTamagotchi.Id != tamagotchis[winnaar].Id)
                        {
                            VechtkamerTamagotchi.Centjes -= 20;
                            VechtkamerTamagotchi.Gezondheid -= 30;
                            if (VechtkamerTamagotchi.Gezondheid <= 0)
                            {
                                VechtkamerTamagotchi.Gezondheid = 0;
                                VechtkamerTamagotchi.Dood = true;
                            }
                        }
                        else
                        {
                            if (kamer.Tamagotchi.Count > 1)
                            {
                                VechtkamerTamagotchi.Centjes += (20 * (tamagotchis.Count - 1));
                                VechtkamerTamagotchi.Level += 1;
                            }

                        }
                        VechtkamerTamagotchi.HotelKamerID = null;

                    }
                    break;

                default:
                    break;
            }
        }

        public ActionResult VolgendeNacht()
        {
            //we zoeken eerst naar alle kamers die wel tamagotchis hebben
            foreach (var item in kamerRepository.GetAllNotFree())
            {
                //we doen eerst per kamer de standaard mutaties die elke tamagotchi zal hebben
                foreach (var tamagotchi in item.Tamagotchi)
                {
                    tamagotchi.Leeftijd += 1;
                    if (tamagotchi.Verveling >= 70)
                    {
                        tamagotchi.Gezondheid -= 20;
                    }
                    if (tamagotchi.Gezondheid <= 0)
                    {
                        tamagotchi.Dood = true;
                        //we verwijderen hem hier als die dood is zodat die uit de lijst gaat van de kamer en dus niet de kamer mutaties krijgt
                        tamagotchi.HotelKamerID = null;
                    }
                }
                //als we eem kamer editen waar tamagotchis in zitten dan gaat het mis met geld als een kamer duurder is
                DoKamerMutaties(item.KamerType, item);
            }
            foreach (var item in tamagotchiRepository.GetAllAliveWithoutRoom())
            {
                item.Leeftijd += 1;
                if (item.Verveling >= 70)
                {
                    item.Gezondheid -= 20;
                }

                item.Gezondheid -= 20;
                item.Verveling += 20;
                if (item.Gezondheid <= 0)
                {
                    item.Gezondheid = 0;
                    item.Dood = true;
                }
            }
            tamagotchiRepository.SaveChanges();
            kamerRepository.SaveChanges();
            return RedirectToAction("Index", "Boekings");
        }
    }
}