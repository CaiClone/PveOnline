using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PvEOnline.Logic.Units.Classes;

namespace PvEOnline.Skills.Guardian
{
    public class HeartOfStone : Skill
    {
        public HeartOfStone(PClass caster): base(caster)
        {
            CD = 50000;
            range = 0;
            name = "Heart of Stone";
            info = "<c3"+name+"><n>The Guardian uses all his resolution to ignore all damage during <c80.5 Seconds for every 10 resolution used>";
        }
        public override void Start()
        {
            startCD();
        }
    }
}
