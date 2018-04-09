using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TamagotchiHotel.Models;

namespace TamagotchiHotel.Repositories
{
    public class KamerRepository : IKamerRepository
    {

        private TamagotchiHotelEntities db = new TamagotchiHotelEntities();

        public void create(HotelKamer kamer)
        {
            db.HotelKamer.Add(kamer);
            SaveChanges();
        }

        public void Delete(HotelKamer kamer)
        {
            if (kamer.Tamagotchi.Count > 0)
            {
                foreach (Tamagotchi tamagotchi in kamer.Tamagotchi)
                {
                    tamagotchi.HotelKamerID = null;
                }
            }
            db.HotelKamer.Remove(kamer);
            SaveChanges();
        }

        public HotelKamer GetKamer(int id)
        {
            HotelKamer kamer = db.HotelKamer.Where(k => k.Id == id).ToList().FirstOrDefault();
            return kamer;
        }

        public IList<HotelKamer> GetAll()
        {
            return db.HotelKamer.ToList();
        }

        public IList<HotelKamer> GetAllFree()
        {
            return db.HotelKamer.Where(k => k.Tamagotchi.Count == 0).ToList();
        }

        public IList<HotelKamer> GetAllNotFree()
        {
            return db.HotelKamer.Where(k => k.Tamagotchi.Count > 0).ToList();
        }

        public IList<Models.Type> GetAllTypes()
        {
            return  db.Type.ToList();
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public void Update(HotelKamer kamer)
        {
            db.Entry(kamer).State = EntityState.Modified;
            SaveChanges();
        }
    }
}