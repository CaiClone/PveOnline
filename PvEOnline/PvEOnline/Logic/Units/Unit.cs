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
        public Vector2 pos;
        private Vector2 dest;
        protected string name;
        protected string folder;
        protected string filename;
        public bool usable;
        protected int owner;
        protected Texture2D sprite;
        protected State state;

        public void LoadContent(ContentManager cont)
        {
            stats = cont.Load<StatsData>(@"Units/" + folder + filename);
            sprite = cont.Load<Texture2D>(@"Sprites/" +folder+ stats.sprite);
        }
        public void setDest(Vector2 dest)
        {
            this.dest = dest;
            state = State.Moving;
        }
        private Vector2 getCenter()
        {
            return pos + new Vector2(CONST.TILESIZEX / 2, CONST.TILESIZEY / 2);
        }
        public virtual void Update(GameTime gameTime)
        {
            switch(state)
            {
                case State.Moving:
                    Vector2 direction = getCenter() - dest;
                    if (direction.Length() > 0.02)
                    {
                        direction.Normalize();
                        pos -= (direction*CONST.BASESPEED) * stats.moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
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
