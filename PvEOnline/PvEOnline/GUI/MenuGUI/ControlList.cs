using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PvEOnline.GUI.MenuGUI
{
    class ControlList: Control
    {
        private List<Control> controls;
        private Vector2 curpos;
        public ControlList(Vector2 start)
        {
            controls = new List<Control>();
            pos = start;
            curpos = start;
        }
        public void Add(Control c)
        {
            controls.Add(c);
            c.pos = curpos;
            curpos.Y += c.getSize().Y;
        }
        public override void Update(GameTime gameTime)
        {
            foreach (Control c in controls)
                c.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Control c in controls)
                c.Draw(spriteBatch);
        }
        public override Vector2 getSize()
        {
            throw new NotImplementedException();
        }
        public override void HandleInput()
        {
            foreach (Control c in controls)
                c.HandleInput();
        }
    }
}
