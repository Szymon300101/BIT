using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundLogic.Models.InputModels
{

    public class BattleMapInputModel : BattleMapModel
    {
        public BattleMapInputModel()
        {

        }
        public BattleMapInputModel(BattleMapModel model)
        {
            Turn = model.Turn;
            BackgroundPath = model.BackgroundPath;
        }

        public BattleMapModel ToLogic()
        {
            BattleMapModel model;
            model = new BattleMapModel();
            model.Turn = Turn;
            model.BackgroundPath = BackgroundPath;

            return model;
        }
    }
}
