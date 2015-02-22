using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PvEOnline.Screens
{
    public class ScreenManager : GameComponent
    {
        Stack<BaseScreen> screens = new Stack<BaseScreen>();
        public ScreenManager(Game1 game)
            : base(game)
        {

        }
        public void PushScreen(BaseScreen s)
        {
            screens.Push(s);
            Game.Components.Add(s);
        }
        public void PopScreen()
        {
            if (screens.Count > 0)
            {
                BaseScreen olds = screens.Pop();
                Game.Components.Remove(olds);
                Game.Components.Add(screens.Peek());
            }
        }
    }
}
