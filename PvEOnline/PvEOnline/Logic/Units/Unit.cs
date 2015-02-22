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

namespace PvEOnline.Logic.Units
{

    public abstract class Unit
    {
        protected Stats stats;
        protected Vector2 pos;
        protected String name;

        bool usable;
        int owner;
        protected Texture2D sprite;

        public void LoadContent(ContentManager cont)
        {
            sprite = cont.Load<Texture2D>(@"Sprites/" + stats.sprite);
        }
        public void loadStats(String statsFilename)
        {
            String filename = statsFilename + ".xml";
            try
            {
                FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                XmlSerializer xs = new XmlSerializer(typeof(Stats));
                this.stats = (Stats)xs.Deserialize(fs);
                fs.Close();
            }
            catch (System.IO.FileNotFoundException e) //We do not have a settigns file let's create it then
            {
                Console.WriteLine("Can't find" + filename);
                stats = new Stats(); //default
                FileStream fs = new FileStream(filename, FileMode.Create);
                XmlSerializer xs = new XmlSerializer(typeof(Stats));
                xs.Serialize(fs, stats);
                fs.Close();
            }
        }
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch sp);
    }
}
