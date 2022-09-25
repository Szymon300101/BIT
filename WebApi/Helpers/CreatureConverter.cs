using BackgroundLogic.Helpers.Interfaces;
using BackgroundLogic.Models;
using WebApi.Helpers.Interfaces;
using WebApi.Models;

namespace WebApi.Helpers
{
    public class CreatureConverter : ICreatureConverter
    {
        readonly IPathMenager _pathLookup;

        public CreatureConverter(IPathMenager pathLookup)
        {
            _pathLookup = pathLookup;
        }

        public List<CreatureCRUDModel> ConvertListFromLogicToCRUD(List<CreatureModel> logicModels)
        {
            List<CreatureCRUDModel> models = new List<CreatureCRUDModel>();
            foreach (CreatureModel creature in logicModels)
                models.Add(new CreatureCRUDModel(creature, _pathLookup));

            return models;
        }
    }
}
