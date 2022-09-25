using BackgroundLogic.Models;
using WebApi.Models;

namespace WebApi.Helpers.Interfaces
{
    public interface IInitiativeConverter
    {
        List<InitiativeCRUDModel> ConvertListFromCreatures(List<CreatureModel> creatureModels);
    }
}
