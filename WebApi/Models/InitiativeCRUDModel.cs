using BackgroundLogic.Helpers;
using BackgroundLogic.Helpers.Interfaces;
using BackgroundLogic.Models;

namespace WebApi.Models
{
    public class InitiativeCRUDModel
    {
        public int Id { get; private set; }
        public string Group { get; private set; }
        public string Name { get; private set; }
        public int Initiative { get; private set; }
        public int AC { get; private set; }
        public int HP { get; private set; }
        public int MaxHP { get; private set; }
        public int InitiativeBonus { get; private set; }
        public int PositionX { get; private set; }
        public int PositionY { get; private set; }

        private string imagePathPath;
        public string ImagePath
        {
            get
            {
                if (!String.IsNullOrEmpty(imagePathPath))
                    return _pathLookup.GetProgDataPath(imagePathPath);
                else
                    return "";
            }
            private set
            {
                imagePathPath = _pathLookup.GetPartPath(value);
            }
        }

        private readonly IPathMenager _pathLookup;

        public InitiativeCRUDModel(IPathMenager pathMenager)
        {
            Name = "";
            _pathLookup = pathMenager;
        }

        public InitiativeCRUDModel(CreatureModel model, IPathMenager pathMenager)
        {
            Id=model.Id;
            Group = model.Group;
            Name=model.Name;
            Initiative = model.Initiative;
            AC=model.AC;
            HP=model.HP;
            MaxHP=model.MaxHP;
            InitiativeBonus=model.InitiativeBonus;
            PositionX=model.PositionX;
            PositionY=model.PositionY;
            imagePathPath = model.ImagePath;

            _pathLookup = pathMenager;
        }

        public CreatureModel ToLogic()
        {
            CreatureModel creatureModel = new CreatureModel();
            creatureModel.Id=Id;
            creatureModel.Group = Group;
            creatureModel.Name=Name;
            creatureModel.Initiative=Initiative;
            creatureModel.AC=AC;
            creatureModel.HP=HP;
            creatureModel.MaxHP=MaxHP;
            creatureModel.InitiativeBonus=InitiativeBonus;
            creatureModel.PositionX=PositionX;
            creatureModel.PositionY=PositionY;
            creatureModel.ImagePath= imagePathPath;

            return creatureModel;
        }
    }
}
