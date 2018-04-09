using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TamagotchiHotel.Repositories;
using TamagotchiHotel.Controllers;
using TamagotchiHotel.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TamagotchiHotel.Tests
{
    [TestClass]
    public class UnitTest1
    {
        HotelKamer kamer;
        HotelKamer kamerNotFree;
        KamerRepository kamerRepository;
        TamagotchiRepository tamagotchiRepository;


        Tamagotchi tama;
        Tamagotchi tama2;

        public UnitTest1()
        {
            kamerRepository = new KamerRepository();
            foreach (var item in kamerRepository.GetAll())
            {
                kamerRepository.Delete(item);
            }

            tamagotchiRepository = new TamagotchiRepository();
            foreach (var item in tamagotchiRepository.GetAll())
            {
                tamagotchiRepository.Delete(item);
            }


            kamer = new HotelKamer
            {
                Id = 0,
                KamerType = "GameKamer",
                HoeveelheidBedden = 3
            };

            kamerNotFree = new HotelKamer
            {
                Id = 1,
                KamerType = "RustKamer",
                HoeveelheidBedden = 5,
            };

            tama = new Tamagotchi
            {
                Id = 0,
                Naam = "Esper",
                Centjes = 100,
                Leeftijd = 0,
                Level = 0,
                Dood = false,
                Gezondheid = 100,
                Kleur = "Zwart",
                Verveling = 0
            };

            tama2 = new Tamagotchi
            {
                Id = 1,
                Naam = "Kees",
                Centjes = 100,
                Leeftijd = 0,
                Level = 0,
                Dood = false,
                Gezondheid = 100,
                Kleur = "Zwart",
                Verveling = 0
            };

        }


        [TestMethod]
        public void HotelKamersControllerIndexNotNull()
        {
            // Arrange
            var controller = new HotelKamersController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TamagotchisControllerIndexNotNull()
        {
            // Arrange
            var controller = new TamagotchisController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert

            Assert.IsNotNull(result);
        }



        [TestMethod]
        public void BoekingsControllerIndexNotNull()
        {
            // Arrange
            var controller = new BoekingsController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void RustKamer()
        {
            // Arrange
            OvernachtingsController nacht = new OvernachtingsController();
            int verveling = tama.Verveling;
            int leeftijd = tama.Leeftijd;
            int gezondheid = tama.Gezondheid;
            int centjes = tama.Centjes;
            // Act
            kamerRepository.create(kamerNotFree);
            kamerNotFree.Tamagotchi.Add(tama);
            kamerRepository.SaveChanges();
            nacht.VolgendeNacht();
            tama = tamagotchiRepository.GetTamagotchi(0);
            // Assert
            Assert.AreEqual(verveling + 10, tama.Verveling);
            Assert.AreEqual(gezondheid, tama.Gezondheid);
            Assert.AreEqual(leeftijd + 1, tama.Leeftijd);
            Assert.AreEqual(centjes - 10, tama.Centjes);
        }

        [TestMethod]
        public void GameKamer()
        {
            // Arrange
            OvernachtingsController nacht = new OvernachtingsController();
            int leeftijd = tama.Leeftijd + 1;
            int gezondheid = tama.Gezondheid;
            int centjes = tama.Centjes;
            // Act
            kamerNotFree.KamerType = "GameKamer";
            kamerRepository.create(kamerNotFree);
            kamerNotFree.Tamagotchi.Add(tama);
            kamerRepository.SaveChanges();
            nacht.VolgendeNacht();
            tama = tamagotchiRepository.GetTamagotchi(0);
            // Assert
            Assert.AreEqual(0, tama.Verveling);
            Assert.AreEqual(centjes - 20, tama.Centjes);
            Assert.AreEqual(leeftijd, tama.Leeftijd);
            Assert.AreEqual(gezondheid, tama.Gezondheid);

        }

        [TestMethod]
        public void WerkKamer()
        {
            // Arrange
            OvernachtingsController nacht = new OvernachtingsController();
            int verveling = tama.Verveling + 20;
            if (verveling > 100)
            {
                verveling = 100;
            }
            int centjes = tama.Centjes;
            int leeftijd = tama.Leeftijd + 1;
            int gezondheid = tama.Gezondheid;
            // Act
            kamerNotFree.KamerType = "WerkKamer";
            kamerRepository.create(kamerNotFree);
            kamerNotFree.Tamagotchi.Add(tama);
            kamerRepository.SaveChanges();
            nacht.VolgendeNacht();
            tama = tamagotchiRepository.GetTamagotchi(0);
            // Assert
            Assert.AreEqual(verveling, tama.Verveling);
            Assert.AreEqual(leeftijd, tama.Leeftijd);
            Assert.AreEqual(gezondheid, tama.Gezondheid);
            if (tama.Centjes > 160 || tama.Centjes < 110)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void VerfKamer()
        {
            // Arrange
            OvernachtingsController nacht = new OvernachtingsController();
            int verveling = tama.Verveling -10;
            int centjes = tama.Centjes - 50;
            int leeftijd = tama.Leeftijd + 1;
            int gezondheid = tama.Gezondheid - 10;
            string kleur = tama.Kleur;
            // Act
            kamerNotFree.KamerType = "VerfKamer";
            kamerRepository.create(kamerNotFree);
            kamerNotFree.Tamagotchi.Add(tama);
            kamerRepository.SaveChanges();
            nacht.VolgendeNacht();
            tama = tamagotchiRepository.GetTamagotchi(0);
            // Assert
            Assert.AreNotEqual(kleur, tama.Kleur);
            Assert.AreEqual(leeftijd, tama.Leeftijd);
            Assert.AreEqual(gezondheid, tama.Gezondheid);
            Assert.AreEqual(centjes, tama.Centjes);
        }

        [TestMethod]
        public void VechtKamer()
        {
            // Arrange
            OvernachtingsController nacht = new OvernachtingsController();
            tama2.Centjes = 100;
            tama2.Level = 0;
            tama2.Gezondheid = 100;
            tama.Centjes = 100;
            tama.Level = 0;
            tama.Gezondheid = 100;
            // Act
            kamerNotFree.KamerType = "VechtKamer";
            kamerRepository.create(kamerNotFree);
            kamerNotFree.Tamagotchi.Add(tama);
            kamerNotFree.Tamagotchi.Add(tama2);
            kamerRepository.SaveChanges();
            nacht.VolgendeNacht();
            tama = tamagotchiRepository.GetTamagotchi(0);
            tama2 = tamagotchiRepository.GetTamagotchi(1);
            // Assert
            if (tama.Centjes == 120)
            {
                Assert.AreEqual(1, tama.Level);
                Assert.AreEqual(80, tama2.Centjes);
                Assert.AreEqual(0, tama2.Level);
                Assert.AreEqual(70, tama2.Gezondheid);
                Assert.AreEqual(100, tama.Gezondheid);

                return;
            }
            else
            {
                Assert.AreEqual(80, tama.Centjes);
                Assert.AreEqual(0, tama.Level);
                Assert.AreEqual(70, tama.Gezondheid);
                Assert.AreEqual(100, tama2.Gezondheid);
                Assert.AreEqual(120, tama2.Centjes);
                Assert.AreEqual(1, tama2.Level);
                return;
            }
        }

        [TestMethod]
        public void GetAllFreeKamers()
        {
            // Arrange

            // Act
            kamerRepository.create(kamer);
            var result = kamerRepository.GetAllFree();
            // Assert
            Assert.AreEqual(result.Count, 1);
        }

        [TestMethod]
        public void GetAllNotFree()
        {
            // Arrange
            kamerNotFree.Tamagotchi.Add(tama);
            // Act
            kamerRepository.create(kamer);
            kamerRepository.create(kamerNotFree);

            var result = kamerRepository.GetAllNotFree();
            // Assert
            Assert.AreEqual(result.Count, 1);
        }



        [TestMethod]
        public void ChangeKamer()
        {
            // Arrange
            // Act
            kamerRepository.create(kamer);
            kamerRepository.create(kamerNotFree);
            kamerNotFree.Tamagotchi.Add(tama);
            tamagotchiRepository.create(tama);
            tamagotchiRepository.ChangeKamer(tama.Id, 0);
            tamagotchiRepository.Update(tama);
            // Assert
            Assert.AreEqual(tama.Id, 0);
        }

        public void NameIsvalid()
        {
            // Arrange
            tama.Naam = "Kees";
            var context = new ValidationContext(tama, null, null);
            var result = new List<ValidationResult>();

            // Act
            var valid = Validator.TryValidateObject(tama, context, result, true);

            Assert.IsTrue(valid);
        }

        [TestMethod]
        public void NameIsInvalid()
        {
            // Arrange
            tama.Naam = "telangenaam";
            var context = new ValidationContext(tama, null, null);
            var result = new List<ValidationResult>();

            // Act
            var valid = Validator.TryValidateObject(tama, context, result, true);

            Assert.IsFalse(valid);
        }
    }




}
