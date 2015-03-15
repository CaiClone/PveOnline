using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PvEOnline.Logic.Units;
using PvEOnline.Logic.Dungeons;
using Microsoft.Xna.Framework.Input;
using PvEOnline.Skills;
using Microsoft.Xna.Framework.Content;

namespace PvEOnline.AIs
{
    public abstract class AI
    {
        protected Unit unit;
        protected Dungeon dun;
        protected UnitManager uMan;
        protected Random rnd;
        protected List<Skill> skills;
        public AI(Unit u, Dungeon d, UnitManager uM, int seed)
        {
            unit = u;
            dun = d;
            uMan = uM;
            rnd = new Random(seed);
            skills = new List<Skill>();
        }
        protected int PickAction(List<int> probs, Random rnd)
        {
            int totalWeight = probs.Sum();
            int weightedPick = rnd.Next(totalWeight);
            foreach (int item in probs)
            for(int i=0;i<probs.Count;i++)
            {
                if (weightedPick < probs[i])
                    return i;
                weightedPick -= probs[i];
            }
            throw new InvalidOperationException("List must have changed...");
        }
        protected void ShuffleList<T>(List<T> list, Random rnd) //Fisher-Yates
        {
            int len = list.Count;
            for (int i = 0; i < len; i++)
            {
                int p = i +(int)(rnd.NextDouble()*(len-1));
                T t = list[p];
                list[p] = list[i];
                list[i] = t;
            }
        }
        protected void Shuffle<T>(T[] arr, Random rnd)
        {
            int len = arr.Length;
            for (int i = 0; i < len; i++)
            {
                int p = i + (int)(rnd.NextDouble() * (len - 1));
                T t = arr[p];
                arr[p] = arr[i];
                arr[i] = t;
            }
        }
        public abstract void Update();

        public int getSkillNum()
        {
            return skills.Count;
        }

        public void skillStart(int skillnum)
        {
            skills[skillnum].Start();
        }
        public List<Skill> getSkills()
        {
            return skills;
        }

        public void loadSkillIcons(ContentManager cont, string folder)
        {
            foreach(Skill s in skills)
            {
                s.loadIcon(cont,folder);
            }
        }
    }
    public enum Elements
    {
        Fire,
        Water,
        Light,
        Dark
    }
}
