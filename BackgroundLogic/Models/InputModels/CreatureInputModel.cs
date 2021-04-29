using BackgroundLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundLogic.Models.InputModels
{
    class CreatureInputModel:CreatureModel
    {

        public CreatureInputModel()
        {

        }
        public CreatureInputModel(CreatureModel model)
        {
            CreatureType = model.CreatureType;
            Id = model.Id;
            Name = model.Name;
            HP = model.HP;
            AC = model.AC;
            MaxHP = model.HP;
            InitiativeBonus = model.InitiativeBonus;
        }

        public CreatureModel ToCreature()
        {
            CreatureModel model;
            switch (this.CreatureType)
            {
                case CreatureTypeEnum.player:
                    model = new PlayerModel();
                    break;
                case CreatureTypeEnum.enemy:
                    model = new EnemyModel();
                    break;
                default:
                    model = new CreatureModel();
                    model.CreatureType = CreatureTypeEnum.npc;
                    break;
            }

            model.Id = Id;
            model.Name = Name;
            model.Initiative = 0;
            model.HP = HP;
            model.AC = AC;
            model.MaxHP = MaxHP;
            model.InitiativeBonus = InitiativeBonus;

            return model;
        }
    }
}
