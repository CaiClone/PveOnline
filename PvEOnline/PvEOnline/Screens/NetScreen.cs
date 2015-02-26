using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PvEOnline.GUI.MenuGUI;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ExitGames.Client.Photon.LoadBalancing;

namespace PvEOnline.Screens
{
    public class NetScreen : BaseScreen
    {
        public NetScreen(Game1 game) : base(game) { }
        private ControlManager cm;

        private Button backB;
        private Button createB;
        private Button refreshB;
        private ComboBox combo;
        private ControlList roomL;

        private NetManager net;
        public override void Initialize()
        {
            cm = new ControlManager(gameRef.Content.Load<SpriteFont>(@"Font/MenuFont"));
            net = new NetManager();
            cm.Add(new Text("Select or create a new room:", new Vector2(20, 30), Color.White));

            backB = new Button("Back", new Vector2(1500, 900), Color.White, Color.Orange);
            createB = new Button("Create Room", new Vector2(1500, 800), Color.White, Color.Orange);
            combo = new ComboBox(loadDungeonNames(), new Vector2(1500, 100));
            refreshB = new Button("Refresh", new Vector2(1000, 800), Color.White, Color.Orange);
            roomL = new ControlList(new Vector2(100, 200));

            backB.Selected += new EventHandler(MenuSelected);
            createB.Selected += new EventHandler(MenuSelected);
            refreshB.Selected += new EventHandler(MenuSelected);

            cm.Add(backB);
            cm.Add(createB);
            cm.Add(refreshB);
            cm.Add(combo);
            cm.Add(roomL);
            //createB.enabled = false;

            //net 
            net.Start();
            base.Initialize();
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
        private void MenuSelected(object sender, EventArgs e)
        {
            if (sender == backB)
                gameRef.screenManager.PushScreen(new GameScreen(gameRef));
            //gameRef.screenManager.PopScreen();
            else if (sender == createB)
                net.newRoom(combo.getCurrent(),gameRef.settings.playerName,5);
            else if (sender == refreshB)
                refreshRoomList();
        }
        private void refreshRoomList()
        {
            Dictionary<string, RoomInfo> rooms = net.getRoomList();
            roomL.Clear();
            if (rooms.Count>0)
            {
                foreach (var kvp in rooms){
                    Button b = new Button((kvp.Key+"        "+kvp.Value.PlayerCount+"/"+kvp.Value.MaxPlayers), Vector2.Zero, Color.Turquoise, Color.Teal);
                    b.Selected += joinRoom;
                    roomL.Add(b);
                }
            }
        }
        private void joinRoom(object sender, EventArgs e)
        {
            Console.WriteLine(sender);
        }
        private List<string> loadDungeonNames()
        {
            return new List<string>(new string[] { "Boss1", "Boss2", "Boss3 Larger" }); //Shut up don't judge me, I'm lazy, I'll do it later
        }
    }
}
