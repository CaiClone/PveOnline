using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PvEOnline.GUI.MenuGUI
{
    public abstract class Control
    {
        public event EventHandler Selected;

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void HandleInput();

        public Vector2 pos;
        public bool enabled=true;
        public bool visible = true;
        protected virtual void OnSelected(EventArgs e)
        {
            if (Selected != null)
            {
                Selected(this, e);
            }
        }

    }
}
