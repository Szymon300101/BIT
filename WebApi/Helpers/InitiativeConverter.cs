using BackgroundLogic.Helpers.Interfaces;
using BackgroundLogic.Models;
using WebApi.Helpers.Interfaces;
using WebApi.Models;
using WebApi.Models.Factories.Interfaces;

namespace WebApi.Helpers
{
    public class InitiativeConverter : IInitiativeConverter
    {
        private readonly IPathMenager _pathLookup;

        public InitiativeConverter(IPathMenager pathMenager)
        {
            _pathLookup = pathMenager;
        }

        public List<InitiativeCRUDModel> ConvertListFromCreatures(List<CreatureModel> creatureModels)
        {
            List<InitiativeCRUDModel> initiativeModels = new List<InitiativeCRUDModel>();
            foreach (CreatureModel creature in creatureModels)
                initiativeModels.Add(new InitiativeCRUDModel(creature, _pathLookup));

            return initiativeModels;
        }
    }
}
