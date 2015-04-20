using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PvEOnline.Logic.Units.Classes;
using PvEOnline.Logic.Units.Buffs;
using PvEOnline.AIs.Classes;

namespace PvEOnline.Skills.GuardianSkills
{
    public class HeartOfStone : Skill
    {
        Guardian ai;
        int cost = 1;
        public HeartOfStone(PClass caster,Guardian ai): base(caster)
        {
            CD = 50000;
            range = 0;
            costDesc = "Everything";
            name = "Heart of Stone";
            info = "<c3"+name+"><n>The Guardian uses all his kindness to ignore all damage during <c80.5 Seconds for every 10 resolution used>";
            this.ai = ai;
        }
        public override void Start()
        {
            int used = ai.getKindness();
            if (used > 0)
            {
                startCD();
                ai.useKindness(used);
                caster.Apply(new b_HeartOfStone((uint)used*50));
            }
        }
    }
}
