using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PvEOnline.Dependencies;

namespace PvEOnline
{
    public class InputHandler : GameComponent
    {
        private static KeyboardState keybState;
        private static KeyboardState lKeybState;
        private static MouseState mouseState;
        private static MouseState lmouseState;

        public InputHandler(Game1 game) : base(game) { }
        public override void Update(GameTime gameTime)
        {
            lKeybState = keybState;
            keybState = Keyboard.GetState();

            lmouseState = mouseState;
            mouseState = Mouse.GetState();
            base.Update(gameTime);
        }

        public static bool KeyPressed(Keys key)
        {
            return keybState.IsKeyDown(key) && lKeybState.IsKeyUp(key);
        }

        public static bool LeftMousePressed()
        {
            return mouseState.LeftButton == ButtonState.Pressed && lmouseState.LeftButton == ButtonState.Released;
        }

        public static bool LeftMouseReleased()
        {
            return mouseState.LeftButton == ButtonState.Released && lmouseState.LeftButton == ButtonState.Pressed;
        }

        public static bool LeftMouseDown()
        {
            return mouseState.LeftButton == ButtonState.Pressed;
        }
        public static bool RightMousePressed()
        {
            return mouseState.RightButton == ButtonState.Pressed && lmouseState.RightButton == ButtonState.Released;
        }

        public static bool RightMouseReleased()
        {
            return mouseState.RightButton == ButtonState.Released && lmouseState.RightButton == ButtonState.Pressed;
        }

        public static bool RightMouseDown()
        {
            return mouseState.RightButton == ButtonState.Pressed;
        }

        public static Vector2 MousePosition()
        {
            return Resolution.getMousePos();
        }
    }
}
