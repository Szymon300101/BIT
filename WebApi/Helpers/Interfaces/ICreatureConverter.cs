using BackgroundLogic.Models;
using WebApi.Models;

namespace WebApi.Helpers.Interfaces
{
    public interface ICreatureConverter
    {
        List<CreatureCRUDModel> ConvertListFromLogicToCRUD(List<CreatureModel> logicModels);
    }
}
