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
        public static void DrawPrettyText(string txt, SpriteBatch spf, SpriteFont font, Vector2 pos,int maxX)
        {
            Vector2 cpos = pos;
            Color ccol = CONST.COLORS[0];
            float maxY = font.MeasureString(txt.Substring(0,1)).Y;
            String buf = "";
            String word = "";
            for(int i=0;i<txt.Length;i++)
            {
                if (txt[i] == ' ')
                {
                    if (cpos.X-pos.X + font.MeasureString(buf + word).X > maxX)
                    {
                        cpos = drawText(buf, spf, font, cpos, ccol);
                        cpos = new Vector2(pos.X, cpos.Y + maxY);
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
                            cpos = drawText(buf, spf, font, cpos, ccol);
                            buf = "";
                            cpos = new Vector2(pos.X, cpos.Y + maxY);
                            i += 2;
                            break;
                        case 'c':
                            cpos = drawText(buf, spf, font, cpos, ccol);
                            buf = "";
                            ccol = CONST.COLORS[(int)Char.GetNumericValue(txt[i + 2])];
                            i += 2;
                            break;
                    }
                }
                else if (txt[i] == '>')
                {
                    buf += word;
                    word = "";
                    cpos = drawText(buf, spf, font, cpos, ccol);
                    buf = "";
                    ccol = CONST.COLORS[0];
                }
                else word += txt[i];
            }
            if (cpos.X - pos.X + font.MeasureString(buf + word).X > maxX)
            {
                cpos = drawText(buf, spf, font, cpos, ccol);
                cpos = new Vector2(pos.X, cpos.Y + maxY);
                buf = "";
            }
            drawText(buf+word, spf, font, cpos, ccol);
        }
        private static Vector2 drawText(string txt, SpriteBatch spf, SpriteFont font, Vector2 pos, Color col)
        {
            spf.DrawString(font, txt, pos, col);
            return pos + new Vector2(font.MeasureString(txt).X, 0);
        }
    }
}
