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
        private Button createB;
        private ComboBox combo;
        public override void Initialize()
        {
            cm = new ControlManager(gameRef.Content.Load<SpriteFont>(@"Font/MenuFont"));
            cm.Add(new Text("Select or create a new room:", new Vector2(20, 30), Color.White));

            backB = new Button("Back", new Vector2(1500, 900), Color.White, Color.Orange);
            createB = new Button("Create Room", new Vector2(1500, 800), Color.White, Color.Orange);
            combo = new ComboBox(loadDungeonNames(), new Vector2(1500, 100));

            backB.Selected += new EventHandler(MenuSelected);
            createB.Selected += new EventHandler(MenuSelected);

            cm.Add(backB);
            cm.Add(createB);
            cm.Add(combo);
            //createB.enabled = false;
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
            if (sender == backB)
                gameRef.screenManager.PopScreen();
            else if (sender == createB)
                gameRef.screenManager.PushScreen(new GameScreen(gameRef));
        }
        private List<string> loadDungeonNames()
        {
            return new List<string>(new string[] { "Boss1", "Boss2", "Boss3 Larger" }); //Shut up don't judge me, I'm lazy, I'll do it later
        }
    }
}
