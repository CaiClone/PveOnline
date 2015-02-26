using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PvEOnline.GUI.MenuGUI
{
    public class Text:Control
    {
        public string text;
        private Color color;
        public Text(string text, Vector2 pos, Color color)
        {
            this.pos = pos;
            this.color = color;
            this.text =text;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(ControlManager.spriteFont, text, pos, color);
        }
        public override void HandleInput()
        {
        }
        public override void Update(GameTime gameTime)
        {
        }
        public override Vector2 getSize()
        {
            return ControlManager.spriteFont.MeasureString(text);
        }
    }
}
