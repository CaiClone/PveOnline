using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PvEOnline.Logic.Units;
using Microsoft.Xna.Framework.Graphics;
using PvEOnline.Logic.Dungeon;

namespace PvEOnline.Screens
{
    public class GameScreen: BaseScreen
    {
        Dungeon dung; //Still searching for that Super-sized Dung.
        public GameScreen(Game1 game)
            : base(game)
        {
        }
        public override void Initialize()
        {
            dung = new Dungeon("Test",gameRef);
            dung.newMap("Test");
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            dung.Update(gameTime);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            dung.Draw(gameTime,gameRef.spriteBatch);
            base.Draw(gameTime);
        }
    }
}
