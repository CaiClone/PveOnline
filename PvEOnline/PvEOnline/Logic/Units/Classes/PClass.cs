using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PvEOnline.Logic.Units.Classes
{
    class PClass : Unit
    {
        public PClass(String filename) : this(filename, filename) { }
        public PClass(String filename, String name)
        {
            this.filename = filename;
            folder = "Classes/";
            this.name = name;
            usable = true;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime, SpriteBatch sp)
        {
            sp.Draw(this.sprite, pos, Color.White);
        }
    }
}
