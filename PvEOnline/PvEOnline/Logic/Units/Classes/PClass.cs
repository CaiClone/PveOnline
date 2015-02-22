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
        public PClass(String filename, String name)
        {
            loadStats(filename);
            this.name = name;
        }
        public override void Update(GameTime gameTime)
        {
            //Do nothing
        }
        public override void Draw(GameTime gameTime, SpriteBatch sp)
        {
            sp.Draw(this.sprite, pos, Color.White);
        }
    }
}
