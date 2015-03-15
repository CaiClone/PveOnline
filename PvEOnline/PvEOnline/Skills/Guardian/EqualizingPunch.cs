using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PvEOnline.Logic.Units.Classes;

namespace PvEOnline.Skills.Guardian
{
    public class EqualizingPunch :Skill
    {
        private PClass caster;
        private int damage;
        public EqualizingPunch(PClass caster)
        {
            this.caster = caster;
            CD = 5000;
            range = 100;
            name = "Equalizing Punch";
            info = "<c2"+name+"><n>The Guardian punches his enemy, dealing <c1[" + damage + "+(10% of the last attack recived)]> damage <c7(Automatically used by the AI every 12 seconds)>";
        }
        public override void Start()
        {
            Console.Beep();
        }
    }
}
