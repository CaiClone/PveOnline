using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PvEOnline.Skills;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace PvEOnline.GUI
{
    public class GameUI
    {
        private SpriteFont spf;
        private List<string> keys;
        private Texture2D range;
        public void drawSkills(List<Skill> list, SpriteBatch sp)
        {
            Vector2 pos = new Vector2(1700,900);
            int len = list.Count;
            for(int i=0;i<len;i++){
                sp.Draw(list[i].icon, pos, Color.White);
                sp.DrawString(spf, keys[i], pos + new Vector2(CONST.ICONSIZE-25, CONST.ICONSIZE -25), Color.Black);
                pos -= new Vector2(CONST.ICONSIZE+ CONST.PADDING, 0);
            }
        }
        public void Load(ContentManager cm, Keys[] keyb)
        {
            keys = new List<string>();
            spf = cm.Load<SpriteFont>(@"Font/MenuFont");
            range = cm.Load<Texture2D>(@"GUI/Range");
            foreach (Keys k in keyb)
                keys.Add(k.ToString());
        }
        public void hoverSkills(List<Skill> list,Vector2 mouse, SpriteBatch sp)
        {
            //sp.Draw(range,
        }
    }
}
