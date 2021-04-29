using BackgroundLogic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundLogic.InputOutput
{
    public static class InitiativeIO
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

            if(newModel.Initiative==0)
            {
                Random randomiser = new Random();
                newModel.Initiative = randomiser.Next(1, 20)+newModel.InitiativeBonus;
            }

            rawData.Add(new InitiativeInputModel(newModel)); 
            string output = JsonConvert.SerializeObject(rawData);

            FileIO.WriteText(fullPath, output);

        }

        public static void UpdateRecord(CreatureModel newModel)
        {

            string fullPath = FileIO.GetProgDataPath(initiativePath);

            List<InitiativeInputModel> rawData = JsonConvert.DeserializeObject<List<InitiativeInputModel>>(FileIO.ReadTxt(fullPath));

            if (newModel.Initiative == 0)
            {
                Random randomiser = new Random();
                newModel.Initiative = randomiser.Next(1, 20) + newModel.InitiativeBonus;
            }

            int id = rawData.FindIndex(item => item.Id == newModel.Id);
            rawData[id].Initiative = newModel.Initiative;
            rawData[id].HP = newModel.HP;

            string output = JsonConvert.SerializeObject(rawData);

            FileIO.WriteText(fullPath, output);

        }
    }
}
