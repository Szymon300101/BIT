using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundLogic.Models
{
    public class BattleMapModel
    {
        public int Turn { get; set; } //numer (w kolejności tabeli inicjatywy) stworzenia które ma teraz rundę
        public string BackgroundPath { get; set; } //ścieżka względna obrazka tła
        public int MovingId { get; set; } //Id stworzenia które aktualnie się rusza

        //wymiary w kratkach
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
