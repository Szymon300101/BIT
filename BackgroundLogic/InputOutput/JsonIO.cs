using BackgroundLogic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundLogic.InputOutput
{
    public static class JsonIO
    {
        private static string initiativePath = "DataBase/InitiativeData.json";

        public static List<CreatureModel> GetInitiative()
        {
            List<CreatureModel> allData = new List<CreatureModel>();

            string fullPath = FileIO.GetProgDataPath(initiativePath);

            List<InitiativeInputModel> rawData = JsonConvert.DeserializeObject<List<InitiativeInputModel>>(FileIO.ReadTxt(fullPath));

            foreach (var item in rawData)
            {
                allData.Add(item.ToCreature());
            }


            return allData;
        }

        public static void DeleteRecord(int id)
        {

            string fullPath = FileIO.GetProgDataPath(initiativePath);

            List<InitiativeInputModel> rawData = JsonConvert.DeserializeObject<List<InitiativeInputModel>>(FileIO.ReadTxt(fullPath));

            rawData.RemoveAll(r => r.Id == id);
            string output = JsonConvert.SerializeObject(rawData);

            FileIO.WriteText(fullPath, output);

        }
        public static void AddRecord(CreatureModel newModel)
        {

            string fullPath = FileIO.GetProgDataPath(initiativePath);

            List<InitiativeInputModel> rawData = JsonConvert.DeserializeObject<List<InitiativeInputModel>>(FileIO.ReadTxt(fullPath));

            rawData.Add(new InitiativeInputModel(newModel)); 
            string output = JsonConvert.SerializeObject(rawData);

            FileIO.WriteText(fullPath, output);

        }
    }
}
