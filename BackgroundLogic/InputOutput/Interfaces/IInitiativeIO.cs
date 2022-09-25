using BackgroundLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundLogic.InputOutput.Interfaces
{
    public interface IInitiativeIO
    {
        void Insert(CreatureModel model);
        void Update(CreatureModel model);
        void Delete(int id);
        List<CreatureModel> SelectAll();
    }
}
