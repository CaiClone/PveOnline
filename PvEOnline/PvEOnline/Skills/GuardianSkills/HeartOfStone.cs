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
        public HeartOfStone(PClass caster,Guardian ai): base(caster,ai)
        {
            CD = 50000;
            range = 0;
            costDesc = "All";
            name = "Heart of Stone";
            info = "<c3"+name+"><n>The Guardian uses all his kindness to ignore all damage during <c80.1 Seconds for every point of resolution used>";
            this.ai = ai;
        }
        public override void activate()
        {
            int used = ai.getKindness();
            if (used > 0)
            {
                ai.useKindness(used);
                caster.Apply(new b_HeartOfStone((uint)used * 100));
            }
        }
        public override bool Usable()
        {
            return UsableOnSelf();
        }
    }
}
