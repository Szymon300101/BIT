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
    public static class CreatureIO
    {
        private static readonly string dataPath = "DataBase/CreatureData.json";

        public static List<CreatureModel> GetData()
        {
            List<CreatureModel> allData = new List<CreatureModel>();

            string fullPath = FileIO.GetProgDataPath(dataPath);

            List<CreatureInputModel> rawData = JsonConvert.DeserializeObject<List<CreatureInputModel>>(FileIO.ReadTxt(fullPath));

            foreach (var item in rawData)
            {
                allData.Add(item.ToCreature());
            }


            return allData;
        }

        public static CreatureModel Select(int id)
        {
            string fullPath = FileIO.GetProgDataPath(dataPath);

            List<CreatureInputModel> allData = JsonConvert.DeserializeObject<List<CreatureInputModel>>(FileIO.ReadTxt(fullPath));

            CreatureInputModel model = allData.Find(item => item.Id == id);

            return model.ToCreature();
        }

        public static void DeleteRecord(int id)
        {

            string fullPath = FileIO.GetProgDataPath(dataPath);

            List<CreatureInputModel> rawData = JsonConvert.DeserializeObject<List<CreatureInputModel>>(FileIO.ReadTxt(fullPath));

            rawData.RemoveAll(r => r.Id == id);
            string output = JsonConvert.SerializeObject(rawData);

            FileIO.WriteText(fullPath, output);

        }
        public static void AddRecord(CreatureModel newModel)
        {

            string fullPath = FileIO.GetProgDataPath(dataPath);

            List<CreatureInputModel> rawData = JsonConvert.DeserializeObject<List<CreatureInputModel>>(FileIO.ReadTxt(fullPath));

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
            rawData.Add(new CreatureInputModel(newModel));
            string output = JsonConvert.SerializeObject(rawData);

            FileIO.WriteText(fullPath, output);
        }
    }
}
