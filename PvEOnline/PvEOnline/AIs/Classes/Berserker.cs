using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PvEOnline.Logic.Units;
using PvEOnline.Logic.Dungeons;
using PvEOnline.Skills.GuardianSkills;
using PvEOnline.Logic.Units.Classes;
using Microsoft.Xna.Framework;
using PvEOnline.Skills.BerserkerSkills;

namespace PvEOnline.AIs.Classes
{
    public class Berserker : AI
    {
        private int rage = 100;
        public Berserker(Unit u, Dungeon d, UnitManager uM, int seed)
            : base(u, d, uM, seed)
        {
            //skills.Add(new DoubleEdgeShield((PClass)u, this));
            skills.Add(new Perseverance((PClass)u, this));
            skills.Add(new DiplomaticImmunity((PClass)u));
            /*
            skills.Add(new TheBeastDefender((PClass)u, this));//Volia fer un chiste malo sobre Best y Beast. :(
            skills.Add(new ShieldsUp((PClass)u, this));
            skills.Add(new ExcessiveAnger((PClass)u, this));
            skills.Add(new SendHelp((PClass)u, this));//Jejeje
            skills.Add(new Restlessness((PClass)u, this));*/
        }
        public override void Update()
        {
            behaviour_getInAutoattackRange();
        }

        public void useRage(int used)
        {
            rage -= used;
        }

        public int getRage()
        {
            return rage;
        }
        public override Color getColor()
        {
            return Color.Aqua;
        }
        public override float getAutoattackRange()
        {
            return 20f;
        }
    }
}