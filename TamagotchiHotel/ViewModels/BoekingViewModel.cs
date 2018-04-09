using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TamagotchiHotel.Models;

namespace TamagotchiHotel.ViewModels
{
    public class BoekingViewModel
    {
        public IEnumerable<HotelKamer> Kamer { get; set; }

        public IEnumerable<Tamagotchi> Tamagotchi { get; set; }
    }
}