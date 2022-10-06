using BackgroundLogic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundLogic.InputOutput
{
    /// <summary>
    /// Klasa obsługująca bazę danych inicjatywy
    /// </summary>
    public static class InitiativeIO
    {
        private static string initiativePath = "DataBase/InitiativeData.json"; //ścieżka względna pliku bazy danych

        /// <summary>
        /// Metoda zwraca listę rekordów inicjatywy.
        /// </summary>
        /// <returns></returns>
        public static List<CreatureModel> GetInitiative()
        {
            List<CreatureModel> allData = new List<CreatureModel>();

            string fullPath = FileIO.GetProgDataPath(initiativePath);

            //deserializacja bazy danych
            List<InitiativeInputModel> rawData = JsonConvert.DeserializeObject<List<InitiativeInputModel>>(FileIO.ReadTxt(fullPath));

            //konwersja rekordów z InputModeli
            foreach (var item in rawData)
            {
                allData.Add(item.ToCreature());
            }


            return allData;
        }

        /// <summary>
        /// Usuwa rekord o podanym Id
        /// </summary>
        /// <param name="id">Id w bazie danych rekordu do usunięcia</param>
        public static void DeleteRecord(int id)
        {

            string fullPath = FileIO.GetProgDataPath(initiativePath);

            //deserializacja aktualnej bazy danych
            List<InitiativeInputModel> rawData = JsonConvert.DeserializeObject<List<InitiativeInputModel>>(FileIO.ReadTxt(fullPath));

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

            string fullPath = FileIO.GetProgDataPath(initiativePath);

            //deserializacja aktualnej bazy danych
            List<InitiativeInputModel> rawData = JsonConvert.DeserializeObject<List<InitiativeInputModel>>(FileIO.ReadTxt(fullPath));

            //znajdowanie wolnego Id dla nowego rekordu
            int N = rawData.Count;
            int MaxId = 0;
            for(int i=0; i<N; i++)
            {
                int id1 = rawData[i].Id;
                if (id1 > MaxId)
                {
                    MaxId = id1;
                }    
            }
            newModel.Id = MaxId + 1;

            //losowanie inicjatywy dla nowego rekordu, jeżeli nie jeszcze jej nie ma
            if(newModel.Initiative==0)
            {
                Random randomiser = new Random();
                newModel.Initiative = randomiser.Next(1, 20)+newModel.InitiativeBonus;
            }
            if (newModel.HP == 0)
                newModel.HP = newModel.MaxHP;

            //dodawawnie nowego rekordu do listy(z konwersją na InputModel), serializacja i zapis z powrotem do bazy danych
            rawData.Add(new InitiativeInputModel(newModel)); 
            string output = JsonConvert.SerializeObject(rawData);
            FileIO.WriteText(fullPath, output);

        }

        /// <summary>
        /// Aktualizacja podanego rekordu w bazie danych. Wyszukuje rekord i zamienia wartości wybranych pól.
        /// </summary>
        /// <param name="newModel"></param>
        public static void UpdateRecord(CreatureModel newModel)
        {

            string fullPath = FileIO.GetProgDataPath(initiativePath);

            //deserializacja aktualnej bazy danych
            List<InitiativeInputModel> rawData = JsonConvert.DeserializeObject<List<InitiativeInputModel>>(FileIO.ReadTxt(fullPath));

            //losowanie inicjatywy dla nowego rekordu, jeżeli nie jeszcze jej nie ma
            if (newModel.Initiative == 0)
            {
                Random randomiser = new Random();
                newModel.Initiative = randomiser.Next(1, 20) + newModel.InitiativeBonus;
            }

            //znajdowanie pozycji potrzebnego rekordu w LIŚCIE pobranej z bazy danych (to nie jest Id w bazie danych)
            int id = rawData.FindIndex(item => item.Id == newModel.Id);
            if (id == -1) throw new Exception("Nie można zaktualizować rekordu: rekord nie istnieje.");

            //aktualizacja wybranych pól
            rawData[id] = new InitiativeInputModel(newModel);

            //powrotna serializacja i zapis
            string output = JsonConvert.SerializeObject(rawData);
            FileIO.WriteText(fullPath, output);

        }
    }
}
