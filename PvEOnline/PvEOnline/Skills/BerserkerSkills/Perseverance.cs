using PvEOnline.AIs.Classes;
using PvEOnline.Logic.Units.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PvEOnline.Skills.BerserkerSkills
{
    class Perseverance : Skill
    {
        private int damage;
        public Perseverance(PClass caster, Berserker ai)
            : base(caster)
        {
            CD = 7000;
            range = 100;
            costDesc = "0";
            name = "Perseverance";
            info = "<c2" + name + "><n>The Berserker punches his target, dealing <c1[" + damage + "]> damage and reduces damage taken for 8 seconds <c7(Automatically used by the AI every 10 seconds)>";
        }
        public override void Start()
        {
            startCD();
        }
    }
}
