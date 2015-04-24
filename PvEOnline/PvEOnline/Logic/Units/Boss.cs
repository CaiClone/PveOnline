using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PvEOnline.Dependencies;

namespace PvEOnline.Logic.Units
{
    public class Boss :Unit
    {
        public Boss(String filename) : this(filename, filename) { }
        public Boss(String filename, String name)
        {
            this.filename = filename;
            folder = "Bosses";
            this.name = name;
            usable = false;
        }
        public Boss(string filename, string name, int x, int y)
            : this(filename, name)
        {
            pos = new Vector2(x, y);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
