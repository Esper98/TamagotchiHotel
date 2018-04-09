using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamagotchiHotel.Models;

namespace TamagotchiHotel.Repositories
{
    public interface ITamagotchiRepository
    {
        void create(Tamagotchi tamagotchi);
        void Update(Tamagotchi tamagotchi);
        void Delete(Tamagotchi tamagotchi);
        void ChangeKamer(int tamagotchi, int kamer);
        void RemoveKamer(int id);
        Tamagotchi GetTamagotchi(int id);
        IList<Tamagotchi> GetAll();
        IList<Tamagotchi> GetAllAlive();
        IList<Tamagotchi> GetAllAliveWithoutRoom();
        IList<Tamagotchi> GetAllWithRoom();
        void SaveChanges();
    }
}
