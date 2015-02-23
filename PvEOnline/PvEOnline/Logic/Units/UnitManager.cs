using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PvEOnline.Logic.Units
{
    public class UnitManager
    {
        List<Unit> units;
        List<Unit> selected;
        
        Game1 gameRef;
        Texture2D selUnitTex;
        public UnitManager(Game1 game)
        {
            units = new List<Unit>();
            selected = new List<Unit>();
            gameRef = game;
            selUnitTex = game.Content.Load<Texture2D>(@"GUI/SelUnit");
        }
        public void Add(Unit u)
        {
            u.LoadContent(gameRef.Content);
            units.Add(u);
        }
        public void Update(GameTime gameTime)
        {
            foreach (Unit u in units)
                u.Update(gameTime);
        }
        public void Draw(GameTime gameTime)
        {
            foreach (Unit u in selected)
                drawSelection(u);
            foreach (Unit u in units)
                u.Draw(gameTime, gameRef.spriteBatch);
        }
        private void drawSelection(Unit u)
        {
            gameRef.spriteBatch.Draw(selUnitTex, u.pos, Color.Lime);
        }
        public void Select(Vector2 point)
        {
            selected.Clear();
            bool found=false;
            int i = 0;
            Rectangle rect = new Rectangle((int)point.X - CONST.RECTSELSIZE / 2, (int)point.Y - CONST.RECTSELSIZE / 2, CONST.RECTSELSIZE, CONST.RECTSELSIZE);
            while (i<units.Count() && !found)
            {
                if(units[i].usable)
                    if (rect.Contains((int)units[i].pos.X + CONST.TILESIZEX / 2, (int)units[i].pos.Y + CONST.TILESIZEY / 2))
                    {
                        found = true;
                        selected.Add(units[i]);
                    }
                i++;
            }
        }

        public void SelectRect(Rectangle rect)
        {
            selected.Clear();
            foreach (Unit u in units)
                if (u.usable)
                    if (rect.Contains((int)u.pos.X + CONST.TILESIZEX / 2, (int)u.pos.Y + CONST.TILESIZEY / 2))
                        selected.Add(u);
        }
        public void OrderMove(Vector2 dest)
        {
            foreach(Unit u in selected)
                u.setDest(dest);
        }
    }
}
