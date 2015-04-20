using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PvEOnline.Logic.Units.Classes;
using PvEOnline.AIs.Classes;

namespace PvEOnline.Skills.GuardianSkills
{
    public class EqualizingPunch :Skill
    {
        private int damage;
        public EqualizingPunch(PClass caster,Guardian ai) : base(caster)
        {
            CD = 5000;
            range = 100;
            costDesc = "0";
            name = "Equalizing Punch";
            info = "<c2"+name+"><n>The Guardian punches his target, dealing <c1[" + damage + "+(10% of the last attack recived)]> damage <c7(Automatically used by the AI every 12 seconds)>";
        }
        public override void Start()
        {
            startCD();
        }
    }
}
