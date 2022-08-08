using BackgroundLogic.InputOutput;
using BackgroundLogic.Models;
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

        public async Task UpdateCreature(CreatureCRUDModel creature)
        {
            CreatureIO.UpdateRecord(creature.ToLogic());

            await Clients.All.RefreshCreatures();
        }

        public async Task RemoveCreature(int creatureId)
        {
            CreatureIO.DeleteRecord(creatureId);

            await Clients.All.RefreshCreatures();
        }






        public async Task EnrollToInitiative(int creatureId)
        {
            InitiativeIO.AddRecord(CreatureIO.Select(creatureId));

            await Clients.All.RefreshInitiative();
        }

        public async Task AddToInitiative(InitiativeCRUDModel record)
        {
            InitiativeIO.AddRecord(record.ToLogic());

            await Clients.All.RefreshInitiative();
        }

        public async Task UpdateInitiative(InitiativeCRUDModel record)
        {
            InitiativeIO.UpdateRecord(record.ToLogic());

            await Clients.All.RefreshInitiative();
        }

        public async Task RemoveFromInitiative(int creatureId)
        {
            InitiativeIO.DeleteRecord(creatureId);

            await Clients.All.RefreshInitiative();
        }
    }
}
