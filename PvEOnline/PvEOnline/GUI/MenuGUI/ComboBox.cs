using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PvEOnline.GUI.MenuGUI
{
    class ComboBox : Control
    {
        private ControlList cList;
        private Text current;
        private Button exp;
        private static int margin = 10;
        private bool expanded;
        public ComboBox(List<string> texts,Vector2 pos)
        {
            if (texts.Count == 0) throw new ArgumentException("Can't create empty ComboBox", "comboEmpty");
            current = new Text(texts[0], pos, Color.White);
            this.pos=pos;
            cList = new ControlList(new Vector2(pos.X,pos.Y+current.getSize().Y));
            foreach(string s in texts){
                Button b = new Button(s,Vector2.Zero,Color.Orange,Color.OrangeRed);
                b.Selected+=ListSelected;
                cList.Add(b);
            }
            exp = new Button("V", new Vector2(this.pos.X + margin + current.getSize().X, this.pos.Y), Color.White, Color.Orange, Color.DarkOrange);
            exp.Selected += Expand;
            expanded = false;
        }
        private void Expand(object sender, EventArgs e)
        {
            expanded = true;
        }
        private void ListSelected(object sender, EventArgs e)
        {
            if(sender is Button) //duh
                current.text = ((Button)sender).text;
            exp.pos = new Vector2(this.pos.X + margin+ current.getSize().X, this.pos.Y);
            expanded = false;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if(expanded)
                cList.Draw(spriteBatch);
            current.Draw(spriteBatch);
            exp.Draw(spriteBatch);
        }
        public override void HandleInput()
        {
            if(expanded)
                cList.HandleInput();
            exp.HandleInput();
        }
        public override Vector2 getSize()
        {
            throw new NotImplementedException();
        }
        public override void Update(GameTime gameTime)
        {
            exp.Update(gameTime);
            if (expanded)
                cList.Update(gameTime);
            current.Update(gameTime);
        }
        public string getCurrent()
        {
            return current.text;
        }
    }
}
