using BackgroundLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundLogic.Models
{
    public class InitiativeInputModel: CreatureModel
    {
        public CreatureTypeEnum CreatureType { get; set; }
        public InitiativeInputModel()
        {

        }
        public InitiativeInputModel(CreatureModel model)
        {
            Id = model.Id;
            Name = model.Name;
            Initiative = model.Initiative;
            HP = model.HP;
            AC = model.AC;
            MaxHP = model.MaxHP;
            InitiativeBonus = model.InitiativeBonus;
        }

        public CreatureModel ToCreature()
        {
            return new CreatureModel()
            {
                Id = Id,
                Name = Name,
                Initiative = Initiative,
                HP = HP,
                AC = AC,
                MaxHP = MaxHP,
                InitiativeBonus = InitiativeBonus
            };
        }
    }
}