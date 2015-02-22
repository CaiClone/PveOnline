using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PvEOnline.AIs;

namespace PvEOnline.Logic.Units
{
    [Serializable]
    public class Stats
    {
        public int hp;
        public int mp;
        public int pAtk;
        public int mAtk;
        public int pDef;
        public int mDef;
        public float critRatio;
        public float atkRange;
        public float moveSpeed;
        public String ai; //<3
        public String sprite;
        public Race race;

        public Stats()//default to avoid crashing if we can't find a file
        {
            hp = 1;
            mp = 1;
            pAtk = 1;
            mAtk = 1;
            pDef = 1;
            critRatio = 0f;
            atkRange = 0f;
            moveSpeed = 1;
            ai = "None";
            sprite = "Error";
            race = Race.error;
        }
    }
    public enum Race
    {
        error,
        Human
    }
}
