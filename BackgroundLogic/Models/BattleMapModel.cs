using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundLogic.Models
{
    public class BattleMapModel
    {
        public int Turn { get; set; }
        public string BackgroundPath { get; set; }
        public int MovingId { get; set; }

        //wymiary w kratkach
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
