using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamagotchiHotel.Models;

namespace TamagotchiHotel.Repositories
{
    public interface IKamerRepository
    {
        IList<HotelKamer> GetAll();
        IList<HotelKamer> GetAllFree();
        IList<HotelKamer> GetAllNotFree();
        IList<Models.Type> GetAllTypes();
        HotelKamer GetKamer(int id);
        void Delete(HotelKamer kamer);
        void create(HotelKamer kamer);
        void Update(HotelKamer kamer);
        void SaveChanges();

    }
}
