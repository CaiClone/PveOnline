using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PvEOnline.Logic.Units;
using PvEOnline.Logic.Dungeons;
using PvEOnline.Skills.Guardian;
using PvEOnline.Logic.Units.Classes;

namespace PvEOnline.AIs.Classes
{
    public class Guardian:AI
    {
        public Guardian(Unit u, Dungeon d, UnitManager uM, int seed)
            : base(u, d, uM, seed)
        {
            skills.Add(new EqualizingPunch((PClass)u));
            skills.Add(new EqualizingPunch((PClass)u));
        }
        public override void Update()
        {
        }
    }
}
