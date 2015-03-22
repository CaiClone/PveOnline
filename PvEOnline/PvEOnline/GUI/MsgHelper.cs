using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PvEOnline.GUI
{
    public static class MsgHelper
    {
        public static void DrawPrettyText(Message msg, SpriteBatch sp,SpriteFont spf, Vector2 pos)
        {
            for (int i = 0; i < msg.lines.Count; i++)
                sp.DrawString(spf, msg.lines[i], pos + msg.pos[i], msg.colors[i]);
        }
        public static Message CalcPrettyText(string txt, SpriteFont font, int maxX)
        {
            Vector2 cpos = Vector2.Zero;
            Color ccol = CONST.COLORS[0];
            float sizeY = font.MeasureString(txt.Substring(0, 1)).Y;
            string buf = "";
            string word = "";
            Message msg = new Message();
            for (int i = 0; i < txt.Length; i++)
            {
                if (txt[i] == ' ')
                {
                    if (cpos.X + font.MeasureString(buf + word).X > maxX)
                    {
                        msg.Add(buf, ccol, cpos);
                        cpos = new Vector2(0, cpos.Y + sizeY);
                        buf = "";
                    }
                    buf += word + txt[i];
                    word = "";
                }
                else if (txt[i] == '<')
                {
                    buf += word;
                    word = "";
                    switch (txt[i + 1])
                    {
                        case 'n':
                            msg.Add(buf, ccol, cpos);
                            buf = "";
                            cpos = new Vector2(0, cpos.Y + sizeY);
                            i += 2;
                            break;
                        case 'c':
                            msg.Add(buf, ccol, cpos);
                            cpos.X += font.MeasureString(buf).X;
                            buf = "";
                            ccol = CONST.COLORS[(int)Char.GetNumericValue(txt[i + 2])];
                            i += 2;
                            break;
                    }
                }
                else if (txt[i] == '>')
                {
                    if (cpos.X + font.MeasureString(buf + word).X > maxX)
                    {
                        msg.Add(buf, ccol, cpos);
                        cpos = new Vector2(0, cpos.Y + sizeY);
                        buf = "";
                    }
                    buf += word;
                    word = "";
                    msg.Add(buf, ccol, cpos);
                    cpos.X += font.MeasureString(buf).X;
                    buf = "";
                    ccol = CONST.COLORS[0];
                }
                else word += txt[i];
            }
            if (cpos.X + font.MeasureString(buf + word).X > maxX)
            {
                msg.Add(buf, ccol, cpos);
                cpos = new Vector2(0, cpos.Y + sizeY);
                buf = "";
            } 
            msg.Add(buf, ccol, cpos);
            return msg;
        }
    }
}
public class Message
{
    public Message()
    {
        lines = new List<string>();
        colors = new List<Color>();
        pos = new List<Vector2>();
    }
    public void Add(String s, Color c, Vector2 displ)
    {
        lines.Add(s);
        colors.Add(c);
        pos.Add(displ);
    }
    public List<string> lines;
    public List<Color> colors;
    public List<Vector2> pos;
    public float sizeY;
}