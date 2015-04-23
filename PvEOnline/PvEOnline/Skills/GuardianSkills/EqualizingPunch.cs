using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PvEOnline.Logic.Units.Classes;
using PvEOnline.AIs.Classes;
using PvEOnline.Logic.Units;
namespace PvEOnline.Skills.GuardianSkills
{
    public class EqualizingPunch :Skill
    {
        private Guardian ai;
        int damage;
        public EqualizingPunch(PClass caster,Guardian ai) : base(caster,ai)
        {
            CD = 5000;
            range = 100;
            costDesc = "0";
            damage = 100;
            name = "Equalizing Punch";
            info = "<c2"+name+"><n>The Guardian punches his target, dealing <c1[" + damage + "+(10% of the last attack recived)]> damage <c7(Automatically used by the AI every 12 seconds)>";
            this.ai = ai;
        }
        public override void activate()
        {
            int lastDamage = ai.getlastDamage();
            caster.getTarget().DealDamage(damage + (int)(lastDamage * 0.1), DamageType.Physical);
        }
        public override bool Usable()
        {
            return UsableOnTarget();
        }
    }
}
