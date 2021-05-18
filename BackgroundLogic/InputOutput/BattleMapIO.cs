using BackgroundLogic.Models;
using BackgroundLogic.Models.InputModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundLogic.InputOutput
{
    public class BattleMapIO
    {

        private static readonly string dataPath = "DataBase/BattleMapData.json";


        public static BattleMapModel GetData()
        {
            BattleMapModel data = new BattleMapModel();

            string fullPath = FileIO.GetProgDataPath(dataPath);

            BattleMapInputModel rawData = JsonConvert.DeserializeObject<BattleMapInputModel>(FileIO.ReadTxt(fullPath));

            data = rawData.ToLogic();

            return data;
        }

        public static void UpdateRecord(BattleMapModel newModel)
        {
            string fullPath = FileIO.GetProgDataPath(dataPath);

            BattleMapInputModel ioModel = new BattleMapInputModel(newModel);

            string output = JsonConvert.SerializeObject(ioModel);

            FileIO.WriteText(fullPath, output);

        }

        public static void Clear()
        {
            BattleMapModel model = new BattleMapModel();
            model.Turn = 0;
            model.BackgroundPath = null;
            model.MovingId = 0;
            model.Width = 24;   //domyślna szerokość i wysokość planszy
            model.Height = 18;

            UpdateRecord(model);
        }
        public static void NewTurn()
        {
            BattleMapModel model = BattleMapIO.GetData();
            List<CreatureModel> initiative = InitiativeIO.GetInitiative();

            if (model.Turn < initiative.Count - 1)
            {
                model.Turn = model.Turn + 1;
            }
            else
            {
                model.Turn = 0;
            }

            UpdateRecord(model);
        }

    }
}
