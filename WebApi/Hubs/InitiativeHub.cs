using BackgroundLogic.InputOutput;
using Microsoft.AspNetCore.SignalR;
using WebApi.Hubs.Clients;
using WebApi.Models;

namespace WebApi.Hubs
{
    public class InitiativeHub : Hub<IInitiativeClient>
    {
        public async Task AddCreature(CreatureCRUDModel creature)
        {
            CreatureIO.AddRecord(creature.ToLogic());

            await Clients.All.RefreshCreatures();
        }
    }
}
