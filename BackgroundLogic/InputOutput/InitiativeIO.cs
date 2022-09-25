using BackgroundLogic.InputOutput.Interfaces;
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
    public class InitiativeIO : IInitiativeIO
    {
        private static string initiativePath = "DataBase/InitiativeData.json"; //ścieżka względna pliku bazy danych

        /// <summary>
        /// Metoda zwraca listę rekordów inicjatywy.
        /// </summary>
        /// <returns></returns>
        public List<CreatureModel> SelectAll()
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
        public void Delete(int id)
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
        public void Insert(CreatureModel model)
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
            model.Id = MaxId + 1;

            //losowanie inicjatywy dla nowego rekordu, jeżeli nie jeszcze jej nie ma
            if(model.Initiative==0)
            {
                Random randomiser = new Random();
                model.Initiative = randomiser.Next(1, 20)+ model.InitiativeBonus;
            }

            //dodawawnie nowego rekordu do listy(z konwersją na InputModel), serializacja i zapis z powrotem do bazy danych
            rawData.Add(new InitiativeInputModel(model)); 
            string output = JsonConvert.SerializeObject(rawData);
            FileIO.WriteText(fullPath, output);

        }

        /// <summary>
        /// Aktualizacja podanego rekordu w bazie danych. Wyszukuje rekord i zamienia wartości wybranych pól.
        /// </summary>
        /// <param name="model"></param>
        public void Update(CreatureModel model)
        {

            string fullPath = FileIO.GetProgDataPath(initiativePath);

            //deserializacja aktualnej bazy danych
            List<InitiativeInputModel> rawData = JsonConvert.DeserializeObject<List<InitiativeInputModel>>(FileIO.ReadTxt(fullPath));

            //losowanie inicjatywy dla nowego rekordu, jeżeli nie jeszcze jej nie ma
            if (model.Initiative == 0)
            {
                Random randomiser = new Random();
                model.Initiative = randomiser.Next(1, 20) + model.InitiativeBonus;
            }

            //znajdowanie pozycji potrzebnego rekordu w LIŚCIE pobranej z bazy danych (to nie jest Id w bazie danych)
            int id = rawData.FindIndex(item => item.Id == model.Id);
            if (id == -1) throw new Exception("Nie można zaktualizować rekordu: rekord nie istnieje.");

            //aktualizacja wybranych pól
            rawData[id].Initiative = model.Initiative;
            rawData[id].HP = model.HP;
            if(model.PositionX != -1) rawData[id].PositionX = model.PositionX;
            if(model.PositionY != -1) rawData[id].PositionY = model.PositionY;

            //powrotna serializacja i zapis
            string output = JsonConvert.SerializeObject(rawData);
            FileIO.WriteText(fullPath, output);

        }
    }
}
