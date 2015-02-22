using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PvEOnline.AIs;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using DataTypes;

namespace PvEOnline.Logic.Units
{

    public abstract class Unit
    {
        protected StatsData stats;
        protected Vector2 pos;
        protected string name;
        protected string folder;
        protected string filename;
        bool usable;
        int owner;
        protected Texture2D sprite;

        public void LoadContent(ContentManager cont)
        {
            stats = cont.Load<StatsData>(@"Units/" + folder + filename);
            sprite = cont.Load<Texture2D>(@"Sprites/" +folder+ stats.sprite);
        }
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch sp);
    }
}
