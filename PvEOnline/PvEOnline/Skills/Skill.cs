using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PvEOnline.Logic.Units;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using PvEOnline.Logic;
using PvEOnline.AIs;

namespace PvEOnline.Skills
{
    public abstract class Skill
    {
        public uint CD = 0;
        public int range = 0;
        public string info = "Forgot to add this skill info, blame the devs";
        public String costDesc = "Forgot";
        public Texture2D icon;
        public bool ready = true;
        protected string name;
        protected Unit caster;
        protected AI generalAi;
        protected string cdName;
        public Skill(Unit u,AI ai)
        {
            caster = u;
            generalAi = ai;
        }
        public void loadIcon(ContentManager cont, string folder)
        {
            icon = cont.Load<Texture2D>(@"GUI/icons/" + folder + "/" + name);
        }

        public Rectangle getRangeRect()
        {
            return new Rectangle((int)caster.pos.X - range, (int)caster.pos.Y - range, range * 2, range * 2);
        }
        protected void startCD(){
            cdName = caster.name+"_"+name;
            TimerHandler.AddTimer(cdName,CD);
            ready = false;
        }
        public int getTimer()
        {
            if (ready)
                return 0;
            int left = TimerHandler.getTimeLeft(cdName);
            if (left <= 0)
            {
                TimerHandler.RemoveTimer(cdName);
                ready = true;
            }
            return left;
        }
        public abstract bool Usable();
        public abstract void activate();
        public void Start()
        {
            if (Usable())
            {
                startCD();
                activate();
            }
        }
        protected bool UsableOnTarget()
        {
            return caster.getTarget()!=null && generalAi.distToTarget(caster.pos) <= range && UpdateReady();
        }
        protected bool UsableOnSelf()
        {
            return UpdateReady();
        }
        private bool UpdateReady()
        {
            if (ready)
                return true;
            return ready = TimerHandler.CheckTimer(cdName);
        }
    }
}
