using BackgroundLogic.Models;

namespace WebApi.Models.Factories.Interfaces
{
    public interface IInitiativeCRUDFactory
    {
        InitiativeCRUDModel CreateInitiativeCRUDModel();
        InitiativeCRUDModel CreateInitiativeCRUDModel(CreatureModel creatureModel);
    }
}
