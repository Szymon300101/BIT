using BackgroundLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundLogic.Models
{
    public class CreatureModel
    {
        public CreatureTypeEnum CreatureType { get; set; }

        public int Id { get; set; }
        public string Group { get; set; }
        public string Name { get; set; }
        public int Initiative { get; set; }
        public int HP { get; set; }
        public int AC { get; set; }
        public int MaxHP { get; set; }
        public int InitiativeBonus { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public string ImagePath { get; set; }
    }
}
