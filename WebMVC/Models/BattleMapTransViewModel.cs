using BackgroundLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMVC.Models
{
    public class BattleMapTransViewModel
    {
        public List<CreatureModel> FullInitiative { get; set; }
        public BattleMapModel StateData { get; set; }
    }
}