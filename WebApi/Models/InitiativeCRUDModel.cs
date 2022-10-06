using BackgroundLogic.Helpers;
using BackgroundLogic.Models;

namespace WebApi.Models
{
    public class InitiativeCRUDModel
    {
        public int Id { get; set; }
        public string Group { get; set; }
        public string Name { get; set; }
        public int Initiative { get; set; }
        public int AC { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int InitiativeBonus { get; set; }
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        private string imagePathPath;
        public string ImagePath
        {
            get
            {
                if (!String.IsNullOrEmpty(imagePathPath))
                    return PathLookup.GetProgDataPath(imagePathPath);
                else
                    return "";
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    imagePathPath = PathLookup.GetPartPath(value);
            }
        }

        public InitiativeCRUDModel()
        {
            Name = "";
        }

        public InitiativeCRUDModel(CreatureModel model)
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
