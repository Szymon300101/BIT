﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundLogic.Models
{
    class EnemyModel:CreatureModel
    {

        public EnemyModel()
        {
            CreatureType = Helpers.CreatureTypeEnum.enemy;
        }
    }
}
