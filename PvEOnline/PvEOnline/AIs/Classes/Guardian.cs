using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PvEOnline.Logic.Units;
using PvEOnline.Logic.Dungeons;

namespace PvEOnline.AIs.Classes
{
    public class Guardian:AI
    {
        public Guardian(Unit u, Dungeon d, UnitManager uM, int seed)
            : base(u, d, uM, seed)
        {
        }
        public override void Update()
        {
        }
    }
}
