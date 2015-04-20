using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PvEOnline.AIs;
using PvEOnline.Logic.Dungeons;
using PvEOnline.Dependencies;

namespace PvEOnline.Logic.Units.Classes
{
    public class PClass : Unit
    {
        public PClass(String filename) : this(filename, filename) { }
        public PClass(String filename, String name)
        {
            this.filename = filename;
            folder = "Classes";
            this.name = name;
            usable = true;
        }
        public PClass(string filename, string name, int x, int y) : this (filename, name)
        {
            pos = new Vector2(x, y);
        }
        public override void loadAi(Dungeon d, UnitManager uM, int seed)
        {
            base.loadAi(d, uM, seed);
            this.ai.loadSkillIcons(uM.gameRef.Content, folder);
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime, SpriteBatch sp)
        {
            base.Draw(gameTime, sp);
            if(state==State.Moving){
                Color col = ai.getColor();
                sp.DrawLine(pos, ai.getDest(), col, 2);
                sp.DrawPolyLine(ai.getRoute().ToArray(), col, 2, false);
            }
        }
    }
}
