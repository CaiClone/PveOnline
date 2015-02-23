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
        private UnitManager uManager;
        private string name;
        private Map map;
        private Game1 gameRef;

        private Vector2 selS;
        private Texture2D selTex;
        private float selTS;
        public Dungeon(String name,Game1 game)
        {
            this.name=name;
            gameRef = game;

            uManager = new UnitManager(gameRef);
            uManager.Add(new PClass("Paladin", "TankHealDps"));
            uManager.Add(new PClass("Paladin", "BestHeal"));
            selTex = gameRef.Content.Load<Texture2D>(@"GUI/Selection");
        }
        public void newMap(string name)
        {
            map = new Map(name);
            map.LoadContent(gameRef.Content);
        }
        public void Update(GameTime gameTime)
        {
            HandleControls(gameTime);
            uManager.Update(gameTime);
            map.Update(gameTime);
        }
        public void Draw(GameTime gameTime,SpriteBatch sp)
        {
            map.DrawBackground(sp);
            uManager.Draw(gameTime);
            if (InputHandler.LeftMouseDown() && TimerHandler.CheckTimer("ClickSelectBox")) 
                sp.Draw(selTex, calcRectangle(selS, InputHandler.MousePosition()), Color.White);
        }
        private void HandleControls(GameTime gameTime)
        {
            if (InputHandler.LeftMousePressed())
            {
                selS = InputHandler.MousePosition();
                TimerHandler.AddTimer("ClickSelectBox", CONST.CLICKTIME);
            }
            else if (InputHandler.LeftMouseReleased())
                if (TimerHandler.CheckTimer("ClickSelectBox", true))
                    uManager.SelectRect(calcRectangle(selS, InputHandler.MousePosition()));
                else
                {
                    TimerHandler.RemoveTimer("ClickSelectBox");
                    uManager.Select(selS);
                }
            else if (InputHandler.RightMousePressed())
                uManager.OrderMove(InputHandler.MousePosition());
        }
        private Rectangle calcRectangle(Vector2 start, Vector2 end)
        {
            return new Rectangle((int)Math.Min(start.X, end.X), (int)Math.Min(start.Y, end.Y), (int)Math.Abs(end.X - start.X), (int)Math.Abs(end.Y - start.Y));
        }
    }
}
