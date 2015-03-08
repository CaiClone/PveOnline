using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PvEOnline.Logic.Units;
using PvEOnline.Logic.Dungeons;

namespace PvEOnline.AIs
{
    public class None :AI
    {
        public None(Unit u, Dungeon d, UnitManager uM, int seed) :base(u,d,uM,seed)
        {
        }
        public override void Update()
        {
            //hey believe me this is the best strategy
        }
    }
}
