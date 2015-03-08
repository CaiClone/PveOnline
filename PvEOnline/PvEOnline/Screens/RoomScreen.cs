using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PvEOnline.Net;
using PvEOnline.GUI.MenuGUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PvEOnline.Screens
{
    public class RoomScreen : BaseScreen
    {
        private NetManagerT net;
        private ControlManager cm;

        private Button go;
        public RoomScreen(Game1 game, NetManagerT network)
            : base(game)
        {
            net = network;
        }
        public override void Initialize()
        {
            cm = new ControlManager(gameRef.Content.Load<SpriteFont>(@"Font/MenuFont"));
            go = new Button("Go", new Vector2(100, 1000), Color.Green, Color.GreenYellow);
            go.Selected += new EventHandler(MenuSelected);
            cm.Add(go);
        }

        private void MenuSelected(object sender, EventArgs e)
        {
            if (sender == go)
            {
                int seed = new Random().Next(1000000);
                net.SendStart(seed);
                gameRef.screenManager.PushScreen(new GameScreen(gameRef, net, seed));
            }
        }
        public override void Update(GameTime gameTime)
        {
            cm.Update(gameTime);
            net.Update();
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            cm.Draw(gameRef.spriteBatch);
            base.Draw(gameTime);
        }
    }
}
