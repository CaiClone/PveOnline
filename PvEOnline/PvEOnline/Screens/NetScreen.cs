using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PvEOnline.GUI.MenuGUI;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PvEOnline.Screens
{
    public class NetScreen : BaseScreen
    {
        public NetScreen(Game1 game) : base(game) { }
        private ControlManager cm;

        private Button backB;
        public override void Initialize()
        {
            cm = new ControlManager(gameRef.Content.Load<SpriteFont>(@"Font/MenuFont"));
            cm.Add(new Text("Select or create a new room:", new Vector2(100, 200), Color.White));
            backB = new Button("Back", new Vector2(1000, 1000), Color.White, Color.Orange);
            backB.Selected += new EventHandler(MenuSelected);
            cm.Add(backB);
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            cm.Update(gameTime);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            cm.Draw(gameRef.spriteBatch);
            base.Draw(gameTime);
        }
        private void MenuSelected(object sender, EventArgs e)
        {
        }
    }
}
