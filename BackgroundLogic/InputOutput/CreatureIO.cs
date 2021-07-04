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
    /// <summary>
    /// Klasa obsługująca bazę danych stworzeń (zapisane stworzenia)
    /// </summary>
    public static class CreatureIO
    {
        private static readonly string dataPath = "DataBase/CreatureData.json"; //ścieżka względna pliku bazy danych

        /// <summary>
        /// Metoda zwraca listę rekordów stworzeń.
        /// </summary>
        /// <returns></returns>
        public static List<CreatureModel> GetData()
        {
            List<CreatureModel> allData = new List<CreatureModel>();

            string fullPath = FileIO.GetProgDataPath(dataPath);

            //deserializacja bazy danych
            List<CreatureInputModel> rawData = JsonConvert.DeserializeObject<List<CreatureInputModel>>(FileIO.ReadTxt(fullPath));

            //konwersja rekordów z InputModeli
            foreach (var item in rawData)
            {
                allData.Add(item.ToCreature());
            }


            return allData;
        }

        /// <summary>
        /// Zwraca rekord o podanym Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CreatureModel Select(int id)
        {
            string fullPath = FileIO.GetProgDataPath(dataPath);

            //deserializacja bazy danych
            List<CreatureInputModel> allData = JsonConvert.DeserializeObject<List<CreatureInputModel>>(FileIO.ReadTxt(fullPath));

            //wyszukiwanie właściwego rekordu
            CreatureInputModel model = allData.Find(item => item.Id == id);
            if (model == null) throw new Exception("Nie można pobrać rekordu: rekord nie istnieje.");

            return model.ToCreature();
        }

        /// <summary>
        /// Usuwa rekord o podanym Id
        /// </summary>
        /// <param name="id">Id w bazie danych rekordu do usunięcia</param>
        public static void DeleteRecord(int id)
        {

            string fullPath = FileIO.GetProgDataPath(dataPath);

            //deserializacja aktualnej bazy danych
            List<CreatureInputModel> rawData = JsonConvert.DeserializeObject<List<CreatureInputModel>>(FileIO.ReadTxt(fullPath));

            //usuwanie podanego rekordu
            int cnt = rawData.RemoveAll(r => r.Id == id);
            if (cnt == 0) throw new Exception("Nie można usunąć rekordu: rekord nie istnieje.");

            //serializacja i zapis
            string output = JsonConvert.SerializeObject(rawData);
            FileIO.WriteText(fullPath, output);

        }

        /// <summary>
        /// Zapisuje podany rekord w bazie danych
        /// </summary>
        /// <param name="newModel"></param>
        public static void AddRecord(CreatureModel newModel)
        {

            string fullPath = FileIO.GetProgDataPath(dataPath);

            //deserializacja aktualnej bazy danych
            List<CreatureInputModel> rawData = JsonConvert.DeserializeObject<List<CreatureInputModel>>(FileIO.ReadTxt(fullPath));

            //znajdowanie wolnego Id dla nowego rekordu
            int N = rawData.Count;
            int MaxId = 0;
            for (int i = 0; i < N; i++)
            {
                int id1 = rawData[i].Id;
                if (id1 > MaxId)
                {
                    MaxId = id1;
                }
            }
            newModel.Id = MaxId + 1;

            newModel.Initiative = 0;
             
            //dodawawnie nowego rekordu do listy(z konwersją na InputModel), serializacja i zapis z powrotem do bazy danych
            rawData.Add(new CreatureInputModel(newModel));
            string output = JsonConvert.SerializeObject(rawData);
            FileIO.WriteText(fullPath, output);
        }
    }
}
