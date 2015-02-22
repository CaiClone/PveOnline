using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PvEOnline.Logic.Units;
using Microsoft.Xna.Framework.Graphics;
using PvEOnline.Logic.Units.Classes;

namespace PvEOnline.Screens
{
    public class GameScreen: BaseScreen
    {
        UnitManager uManager;
        public GameScreen(Game1 game)
            : base(game)
        {
        }
        public override void Initialize()
        {
            uManager = new UnitManager(gameRef);
            uManager.Add(new PClass("Paladin", "TankHealDps"));
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            uManager.Update(gameTime);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            uManager.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
