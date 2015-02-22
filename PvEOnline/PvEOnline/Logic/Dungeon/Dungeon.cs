using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PvEOnline.Logic.Units;
using PvEOnline.Logic.Units.Classes;

namespace PvEOnline.Logic.Dungeon
{

    public class Dungeon
    {
        UnitManager uManager;
        string name;
        Map map;
        Game1 gameRef;
        public Dungeon(String name,Game1 game)
        {
            this.name=name;
            gameRef = game;

            uManager = new UnitManager(gameRef);
            uManager.Add(new PClass("Paladin", "TankHealDps"));
        }
        public void newMap(string name)
        {
            map = new Map(name);
            map.LoadContent(gameRef.Content);
        }
        public void Update(GameTime gameTime)
        {
            map.Update(gameTime);
        }
        public void Draw(GameTime gameTime,SpriteBatch sp)
        {
            uManager.Draw(gameTime);
            map.DrawBackground(sp);
        }
    }
}
