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
    // Baza danych Battlemapy posiada tylko jeden rekord, który jest edytowany
    /// <summary>
    /// Klasa obsługująca bazę stanu Battlemapy. 
    /// </summary>
    public class BattleMapIO
    {

        private static readonly string dataPath = "DataBase/BattleMapData.json"; //ścieżka względna pliku bazy danych

        /// <summary>
        /// Pobiera ztan Battlemapy z bazy
        /// </summary>
        /// <returns></returns>
        public static BattleMapModel GetData()
        {
            BattleMapModel data = new BattleMapModel();

            string fullPath = FileIO.GetProgDataPath(dataPath);

            //deseralizacja rekordu
            BattleMapInputModel rawData = JsonConvert.DeserializeObject<BattleMapInputModel>(FileIO.ReadTxt(fullPath));

            //konwersja z InputModelu
            data = rawData.ToLogic();

            return data;
        }

        /// <summary>
        /// Ustawia stan Battlemapy w bazie na podany
        /// </summary>
        /// <param name="newModel"></param>
        public static void UpdateRecord(BattleMapModel newModel)
        {
            string fullPath = FileIO.GetProgDataPath(dataPath);

            //konwersja na InutModel
            BattleMapInputModel ioModel = new BattleMapInputModel(newModel);

            //serializacja i zapis
            string output = JsonConvert.SerializeObject(ioModel);
            FileIO.WriteText(fullPath, output);

        }

        /// <summary>
        /// Przywraca wartości początkowe stanu.
        /// </summary>
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

        /// <summary>
        /// Ustawia marker tury na następne stworzenie
        /// </summary>
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
