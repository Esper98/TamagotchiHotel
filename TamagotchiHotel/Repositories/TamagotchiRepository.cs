using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TamagotchiHotel.Models;

namespace TamagotchiHotel.Repositories
{
    public class TamagotchiRepository : ITamagotchiRepository
    {
        private TamagotchiHotelEntities db = new TamagotchiHotelEntities();

        public void create(Tamagotchi tamagotchi)
        {
            db.Tamagotchi.Add(tamagotchi);
            SaveChanges();
        }

        public void Delete(Tamagotchi tamagotchi)
        {
            db.Tamagotchi.Remove(tamagotchi);
            SaveChanges();
        }

        public Tamagotchi GetTamagotchi(int id)
        {
            return db.Tamagotchi.Where(t => t.Id == id).ToList().FirstOrDefault();
        }

        public IList<Tamagotchi> GetAll()
        {
            return db.Tamagotchi.ToList();
        }

        public IList<Tamagotchi> GetAllAlive()
        {
            return db.Tamagotchi.Where(t => t.Dood == false).ToList();
        }

        public IList<Tamagotchi> GetAllWithRoom()
        {
            return db.Tamagotchi.Where(t => t.HotelKamerID != null).ToList();
        }

        public IList<Tamagotchi> GetAllAliveWithoutRoom()
        {
            return db.Tamagotchi.Where(t => t.HotelKamerID == null && t.Dood == false).ToList();
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public void ChangeKamer(int tamagotchi, int kamer)
        {
            GetTamagotchi(tamagotchi).HotelKamerID = kamer; 
            db.SaveChanges();
        }

        public void RemoveKamer(int id)
        {
            GetTamagotchi(id).HotelKamerID = null;
            db.SaveChanges();
        }

        public void Update(Tamagotchi tamagotchi)
        {
            db.Entry(tamagotchi).State = EntityState.Modified;
            SaveChanges();
        }
    }
}