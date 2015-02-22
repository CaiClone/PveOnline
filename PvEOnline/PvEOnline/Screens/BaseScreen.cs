using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace PvEOnline.Screens
{
    public abstract class BaseScreen : DrawableGameComponent
    {
        protected Game1 gameRef;
        public BaseScreen(Game1 game)
            : base(game)
        {
            gameRef = game;
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
