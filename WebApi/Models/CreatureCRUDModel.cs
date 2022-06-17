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
        public string ImagePath { get; set; }

        public CreatureCRUDModel()
        {
            Name = "";
            ImagePath = "";
        }
        public CreatureCRUDModel(CreatureModel model)
        {
            Id=model.Id;
            Group = model.Group;
            Name=model.Name;
            AC=model.AC;
            MaxHP=model.MaxHP;
            InitiativeBonus=model.InitiativeBonus;
            ImagePath=model.ImagePath;
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
            creatureModel.ImagePath=ImagePath;

            return creatureModel;
        }
    }
}
