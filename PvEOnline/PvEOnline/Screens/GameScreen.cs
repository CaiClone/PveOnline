using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PvEOnline.Logic.Units;
using Microsoft.Xna.Framework.Graphics;
using PvEOnline.Logic.Dungeons;
using PvEOnline.Net;

namespace PvEOnline.Screens
{
    public class GameScreen: BaseScreen
    {
        Dungeon dung; //Still searching for that Super-sized Dung.
        NetManagerT net;
        int seed;
        Boolean online;
        public GameScreen(Game1 game, NetManagerT netManager, int seed)
            : base(game)
        {
            net = netManager;
            online = false;
        }
        public GameScreen(Game1 game, int seed)
            : base(game)
        {
            online = false;
        }
        public override void Initialize()
        {
            dung = new Dungeon("Test",gameRef, net,seed);
            dung.newMap("Test");
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            dung.Update(gameTime);
            if (online) net.Update();
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            dung.Draw(gameTime,gameRef.spriteBatch);
            base.Draw(gameTime);
        }
    }
}
