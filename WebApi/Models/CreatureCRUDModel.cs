using BackgroundLogic.Helpers;
using BackgroundLogic.Helpers.Interfaces;
using BackgroundLogic.Models;

namespace WebApi.Models
{
    public class CreatureCRUDModel
    {
        public int Id { get; set; }
        public string Group { get; set; }
        public string Name { get; set; }
        public int AC { get; set; }
        public int MaxHP { get; set; }
        public int InitiativeBonus { get; set; }

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
            set
            {
                imagePathPath = _pathLookup.GetPartPath(value);
            }
        }

        private readonly IPathMenager _pathLookup;

        public CreatureCRUDModel(IPathMenager pathLookup)
        {
            Name = "";
            _pathLookup = pathLookup;
        }
        public CreatureCRUDModel(CreatureModel model, IPathMenager pathLookup)
        {
            Id=model.Id;
            Group = model.Group;
            Name=model.Name;
            AC=model.AC;
            MaxHP=model.MaxHP;
            InitiativeBonus=model.InitiativeBonus;
            imagePathPath = model.ImagePath;

            _pathLookup = pathLookup;
        }

        public CreatureModel ToLogic()
        {
            CreatureModel creatureModel = new CreatureModel();
            creatureModel.Id=Id;
            creatureModel.Group = Group;
            creatureModel.Name=Name;
            creatureModel.AC=AC;
            creatureModel.MaxHP=MaxHP;
            creatureModel.InitiativeBonus=InitiativeBonus;
            creatureModel.ImagePath= imagePathPath;

            return creatureModel;
        }
    }
}
