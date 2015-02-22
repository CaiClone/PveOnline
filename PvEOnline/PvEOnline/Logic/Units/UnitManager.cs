using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PvEOnline.Logic.Units
{
    public class UnitManager
    {
        List<Unit> units;
        Game1 gameRef;
        public UnitManager(Game1 game)
        {
            units = new List<Unit>();
            gameRef = game;
        }
        public void Add(Unit u)
        {
            u.LoadContent(gameRef.Content);
            units.Add(u);
        }
        public void Update(GameTime gameTime)
        {
            foreach (Unit u in units)
                u.Update(gameTime);
        }
        public void Draw(GameTime gameTime)
        {
            foreach (Unit u in units)
                u.Draw(gameTime,gameRef.spriteBatch);
        }
    }
}
