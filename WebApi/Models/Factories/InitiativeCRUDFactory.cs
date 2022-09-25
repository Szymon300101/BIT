using BackgroundLogic.Models;
using WebApi.Models.Factories.Interfaces;

namespace WebApi.Models.Factories
{
    public class InitiativeCRUDFactory : IInitiativeCRUDFactory
    {
        public InitiativeCRUDModel CreateInitiativeCRUDModel()
        {
            throw new NotImplementedException();
            //return new InitiativeCRUDModel();
        }

        public InitiativeCRUDModel CreateInitiativeCRUDModel(CreatureModel creatureModel)
        {
            throw new NotImplementedException();
            //return new InitiativeCRUDModel(creatureModel);
        }
    }
}
