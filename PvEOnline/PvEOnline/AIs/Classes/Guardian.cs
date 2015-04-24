using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PvEOnline.Logic.Units;
using PvEOnline.Logic.Dungeons;
using PvEOnline.Skills.GuardianSkills;
using PvEOnline.Logic.Units.Classes;
using Microsoft.Xna.Framework;

namespace PvEOnline.AIs.Classes
{
    public class Guardian:AI
    {
        private static int MAXKINDNESS = 10;
        private int kindness = MAXKINDNESS;
        private int lastDamage = 0;
        public Guardian(Unit u, Dungeon d, UnitManager uM, int seed)
            : base(u, d, uM, seed)
        {
            skills.Add(new HeartOfStone((PClass)u,this));
            skills.Add(new EqualizingPunch((PClass)u,this));
        }
        public override void Update()
        {
            behaviour_getInAutoattackRange();
            gainKindness(1);
        }

        public void gainKindness(int num)
        {
            if (kindness < MAXKINDNESS)
                kindness+=num;
        }
        public void useKindness(int used)
        {
            kindness -= used;
        }

        public int getKindness()
        {
            return kindness;
        }
        public override Color getColor()
        {
            return Color.Aqua;
        }
        public override void recieveDamage(ref int num, DamageType flags)
        {
            lastDamage = num;
        }
        public int getlastDamage()
        {
            return lastDamage;
        }
    }
}
