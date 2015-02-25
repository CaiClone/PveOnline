using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PvEOnline.GUI.MenuGUI
{
    public class ControlManager : List<Control>
    {
        public static SpriteFont spriteFont;
        public ControlManager(SpriteFont spF)
            : base()
        {
            spriteFont = spF;
        }
        public void Update(GameTime gameTime)
        {
            if (Count == 0)
                return;
            foreach (Control c in this)
            {
                if (c.enabled)
                {
                    c.Update(gameTime);
                    c.HandleInput();
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Control c in this)
            {
                if (c.visible)
                    c.Draw(spriteBatch);
            }
        }
    }
}
