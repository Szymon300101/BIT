using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundLogic.Models
{
    class PlayerModel: CreatureModel
    {

        public PlayerModel()
        {
            CreatureType = Helpers.CreatureTypeEnum.player;
        }
    }
}
