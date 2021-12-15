using BackgroundLogic.InputOutput;
using BackgroundLogic.Models;
using Microsoft.AspNetCore.SignalR;

namespace SignalCore.Hubs
{
    public class MainHub : Hub
    {
        //protected readonly IConfiguration _configuration;

        //public BattleMapHub(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //    FileIO.LoadPath(_configuration["ProgDataDirectory"]);
        //}

        public async Task GetSomeData()
        {
            string dataPath = FileIO.GetProgDataPath();


            await Clients.Caller.SendAsync("GotDataPath", dataPath);
        }
         public async Task ChangeField(string type, string type2, string type3)
         {
             int id;
             int fieldValue;

             return;

             //db operations

             await Clients.All.SendAsync("FieldChanged", type, 0, 0);
             //, float dbId, float value
         }

        /*public async Task ChangeField(string type, string dbId, string value)
        {
            int id;
            int fieldValue;
            if (!int.TryParse(dbId, out id))
                throw new Exception("System error: Błędne id.");
            if (!int.TryParse(value, out fieldValue))
                throw new Exception($"Niepoprawna wartość pola {type}.");

            CreatureModel changedCreature = InitiativeIO.GetInitiative().Find(item => item.Id == id);
            if (changedCreature == null)
                throw new Exception("System error: Błędne id.");

            switch (type)
            {
                case "initiative":
                    changedCreature.Initiative = fieldValue;
                    break;

                default:
                    throw new Exception($"Niepoprawny typ.");
            }
            InitiativeIO.UpdateRecord(changedCreature);


            await Clients.All.SendAsync("FieldChanged", type, dbId, value);
        }*/
    }
}
