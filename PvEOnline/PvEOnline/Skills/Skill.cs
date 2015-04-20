using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PvEOnline.Logic.Units;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using PvEOnline.Logic;

namespace PvEOnline.Skills
{
    public abstract class Skill
    {
        public uint CD = 0;
        public int range = 0;
        public string info = "Forgot to add this skill info, blame the devs";
        public String costDesc = "Forgot";
        public Texture2D icon;
        public abstract void Start();
        public bool ready = true;
        protected string name;
        protected Unit caster;
        protected string cdName;
        public Skill(Unit u)
        {
            caster = u;
        }
        public void loadIcon(ContentManager cont, string folder)
        {
            icon = cont.Load<Texture2D>(@"GUI/icons/" + folder + "/" + name);
        }

        public Rectangle getRangeRect()
        {
            Point center = caster.getRectangle().Center;
            return new Rectangle(center.X - range, center.Y - range, range * 2, range * 2);
        }
        protected void startCD(){
            cdName = caster.name+"_"+name;
            TimerHandler.AddTimer(cdName,CD);
            ready = false;
        }
        public int getTimer()
        {
            int left = TimerHandler.getTimeLeft(cdName);
            if (left <= 0)
            {
                TimerHandler.RemoveTimer(cdName);
                ready = true;
            }
            return left;
        }
    }
}
