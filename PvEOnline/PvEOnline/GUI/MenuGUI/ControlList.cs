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
        public void Add(Control c)
        {
           controls.Add(c);
        } 
        public override void Update(GameTime gameTime)
        {
 	        
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Control c in controls)
                c.Draw(spriteBatch);
        }
    }
}
