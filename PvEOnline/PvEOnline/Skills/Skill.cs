using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PvEOnline.Logic.Units;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PvEOnline.Skills
{
    public abstract class Skill
    {
        public int CD = 0;
        public int range = 0;
        public string info = "Forgot to add this skill info, blame the devs";
        public Texture2D icon;
        public abstract void Start();
        protected string name;
        public void loadIcon(ContentManager cont, string folder)
        {
            icon = cont.Load<Texture2D>(@"GUI/icons/" + folder + "/" + name);
        }
    }
}
