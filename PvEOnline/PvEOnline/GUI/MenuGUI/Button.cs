using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PvEOnline.GUI.MenuGUI
{
    public class Button : Control
    {
        private Color normalColor;
        private Color overColor;
        private Color selectedColor;
        private Color color;
        private string text;
        private Vector2 size;
        private bool stick=true;
        private bool selected;

        public Button(string text, Vector2 pos, Color normal, Color overcolor)
            : this(text, pos, normal, overcolor, normal)
        {
            stick = false;
        }
        public Button(string text, Vector2 pos, Color normal, Color overcolor, Color selected)
        {
            this.pos = pos;
            this.normalColor = normal;
            this.overColor = overcolor;
            setText(text);
            color = normalColor;

        }
        private void setText(string text)
        {
            this.text = text;
            size = ControlManager.spriteFont.MeasureString(text);
        }

        private void handleSelected()
        {
            selected = stick;
            color = selected ? selectedColor : normalColor;
            if(!selected) base.OnSelected(null);
        }
        private void handleOver(bool state)
        {
            color = (state) ? overColor : normalColor;
        }
        public override void HandleInput()
        {
            if (overButton(InputHandler.MousePosition()))
            {
                handleOver(true);
                if (InputHandler.LeftMouseReleased()) handleSelected();
            }
            else
            {
                handleOver(false);
            }
        }
        public override void Update(GameTime gameTime)
        {
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(ControlManager.spriteFont, text, pos, color);
        }
        private bool overButton(Vector2 mousepos)
        {
            return (new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y)).Contains((int)mousepos.X, (int)mousepos.Y);
        }
    }
}
