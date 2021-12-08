using BackgroundLogic.InputOutput;
using Microsoft.AspNetCore.SignalR;

namespace SignalCore.Hubs
{
    public class BattleMapHub : Hub
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
    }
}
