﻿using PvEOnline.AIs.Classes;
using PvEOnline.Logic.Units.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PvEOnline.Skills.BerserkerSkills
{
    class DiplomaticImmunity : Skill
    {
        public DiplomaticImmunity(PClass caster,Berserker ai)
            : base(caster,ai)
        {
            CD = 120000;
            range = 100;//Que se lo pueda tirar cuando esté "Relativamente cerca" de un enemigo
            costDesc = "0";
            name = "Diplomatic Immunity";
            info = "<c2" + name + "><n>The Berserker receives 95% less damage during 2 seconds";
        }
        public override void activate()
        {
            
        }
        public override bool Usable()
        {
            return UsableOnSelf();
        }
    }
}

