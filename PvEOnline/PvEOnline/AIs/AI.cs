using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PvEOnline.Logic.Units;
using PvEOnline.Logic.Dungeons;
using Microsoft.Xna.Framework.Input;
using PvEOnline.Skills;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PvEOnline.AIs
{
    public abstract class AI
    {
        protected Unit unit;
        protected Dungeon dun;
        protected UnitManager uMan;
        protected Random rnd;
        protected List<Skill> skills;
        protected Stack<Vector2> route;
        public AI(Unit u, Dungeon d, UnitManager uM, int seed)
        {
            unit = u;
            dun = d;
            uMan = uM;
            rnd = new Random(seed);
            skills = new List<Skill>();
            route = new Stack<Vector2>();
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
        protected void behaviour_getInAutoattackRange()
        {
            if(unit.Target!=null)
                if(distToTarget(unit.pos)>getAutoattackRange())
                    unit.setDest(unit.Target.pos);
                else
                    unit.clearRoute();

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

        public void setDest(Vector2 dest)
        {
            if (dest.X > CONST.TILESIZEX && dest.Y > CONST.TILESIZEY)
            {
                route.Clear();
                foreach (Vector2 vec in dun.map.getRoute(unit.pos, dest))
                    route.Push(vec);
                if (route.Count > 1)
                    route.Pop(); //fuck yo start
            }
        }
        public Vector2 getDest()
        {
            return route.Peek();
        }
        /* returns false if it doesn't have a next dest*/
        public bool NextDest()
        {
            route.Pop();
            return (route.Count != 0);
        }
        public Stack<Vector2> getRoute()
        {
            return route;
        }
        public virtual Color getColor()
        {
            return Color.LimeGreen;
        }
        public virtual void Draw(GameTime gameTime, SpriteBatch sp)
        {
            sp.DrawString(dun.spf, dun.map.toMapIndices(unit.pos).ToString(), unit.pos + new Vector2(20, 20), Color.White);
        }
        public float distToTarget(Vector2 point)
        {
            return (unit.Target.pos - point).Length();
        }

        public abstract float getAutoattackRange();
        public void orderMove(Vector2 dest)
        {
            if (unit.Target!=null && distToTarget(dest)> getAutoattackRange())
                unit.Target = null;
            unit.setDest(dest);
        }
        public void leaveTile(Vector2 tilePos)
        {
            //dun.map.leaveTile(unit, tilePos);
        }
        public void enterTile(Vector2 tilePos)
        {
            //dun.map.enterTile(unit, tilePos);
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
