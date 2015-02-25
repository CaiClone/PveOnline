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
        protected StatsData pstats;
        public StatsData cStats;
        public Vector2 pos;
        private Vector2 dest;
        protected string name;
        protected string folder;
        protected string filename;
        public bool usable;
        protected int owner;
        protected Texture2D sprite;
        protected State state;
        public int hp { get {return hp;} }

        public void LoadContent(ContentManager cont)
        {
            pstats = cont.Load<StatsData>(@"Units/" + folder + filename);
            sprite = cont.Load<Texture2D>(@"Sprites/" +folder+ pstats.sprite);
        }
        public void setDest(Vector2 dest)
        {
            this.dest = dest;
            state = State.Moving;
        }
        private Rectangle getRectangle()
        {
            return new Rectangle((int)pos.X, (int)pos.Y, (int)sprite.Width, (int)sprite.Height);
        }
        public virtual void Update(GameTime gameTime)
        {
            switch(state)
            {
                case State.Moving:
                    Rectangle rect =getRectangle();
                    Vector2 direction = new Vector2(rect.Center.X,rect.Bottom-15)- dest;
                    if (Math.Abs(direction.Length()) > 2f)
                    {
                        direction.Normalize();
                        pos -= (direction*CONST.BASESPEED) * pstats.moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else state = State.Idle;
                    break;
            }
        }
        public abstract void Draw(GameTime gameTime, SpriteBatch sp);
    }
    public enum State
    {
        Idle,
        Moving
    }
}
