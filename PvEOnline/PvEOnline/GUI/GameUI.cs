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
        private Texture2D msgBox;
        private Rectangle[] rect;
        private Rectangle msgRect;
        private float cdDispY;
        public GameUI()
        {
            rect = new Rectangle[CONST.MAXNSKILLS];
            Vector2 pos = new Vector2(1700,900);
            msgRect = new Rectangle(1300, 650, 500, 200);
            for (int i = 0; i < CONST.MAXNSKILLS; i++)
                rect[i] = new Rectangle((int)pos.X - (CONST.ICONSIZE + CONST.PADDING)*i, (int)pos.Y, CONST.ICONSIZE, CONST.ICONSIZE);
        }
        public void drawSkills(List<Skill> list, SpriteBatch sp)
        {
            int len = list.Count;
            for(int i=len-1;i>-1;i--){
                bool rdy = list[i].ready;
                sp.Draw(list[i].icon, rect[i], rdy? Color.White: Color.Gray);
                sp.DrawString(spf, keys[len-i-1], new Vector2(rect[i].X + CONST.ICONSIZE - 25, rect[i].Y+CONST.ICONSIZE - 25), Color.Black);
                if(!rdy){
                    string remain = timeConvert(list[i].getTimer()).ToString();
                    sp.DrawString(spf, remain, new Vector2(rect[i].X + CONST.ICONSIZE / 2 - spf.MeasureString(remain).X / 2, rect[i].Y + CONST.ICONSIZE / 2 - cdDispY), CONST.COLORS[8]);
                }
            }
        }
        private string timeConvert(int ms)
        {
            return (ms / 1000).ToString();
        }
        public void Load(ContentManager cm, Keys[] keyb)
        {
            keys = new List<string>();
            spf = cm.Load<SpriteFont>(@"Font/MenuFont");
            range = cm.Load<Texture2D>(@"GUI/Range");
            msgBox = cm.Load<Texture2D>(@"GUI/MsgBox");
            cdDispY = spf.MeasureString("1").Y / 2f; //precalculate for speed
            foreach (Keys k in keyb)
                keys.Add(k.ToString());
        }
        public void hoverSkills(List<Skill> list,Vector2 mouse, SpriteBatch sp)
        {
            for (int i = 0; i < list.Count; i++)
                if (rect[i].Contains(new Point((int)mouse.X, (int)mouse.Y)))
                {
                    sp.Draw(range, list[i].getRangeRect(), Color.CadetBlue);
                    sp.Draw(msgBox, msgRect, Color.White);
                    Message msg = MsgHelper.CalcPrettyText(list[i].info, spf, msgRect.Width - CONST.PADDING);
                    MsgHelper.DrawPrettyText(msg,sp,spf, new Vector2(msgRect.X + CONST.PADDING, msgRect.Y + CONST.PADDING));
                    string cdTxt = timeConvert((int)list[i].CD)+ " S";
                    sp.DrawString(spf,cdTxt, new Vector2(rect[i].X + CONST.ICONSIZE/2 - spf.MeasureString(cdTxt).X/2, rect[i].Y + CONST.ICONSIZE), CONST.COLORS[8]);
                }
        }

        public int clickedSkill(Vector2 mouse, int nSkills)
        {
            for (int i = 0; i < nSkills; i++)
                if (rect[i].Contains((int)mouse.X,(int)mouse.Y))
                    return i;
            return -1;
        }
    }
}
