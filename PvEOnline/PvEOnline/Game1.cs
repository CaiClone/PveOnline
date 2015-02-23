using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using PvEOnline.Dependencies;
using PvEOnline.Screens;
using PvEOnline.Logic;

namespace PvEOnline
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        public Settings settings;
        public ScreenManager screenManager;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            SerializeHelper.SerializeExampleMapInfo();
        }

        protected override void Initialize()
        {
            settings = Settings.loadSettings();
            Resolution.Init(ref graphics);
            Resolution.SetVirtualResolution(CONST.VRESOLUTIONX, CONST.VRESOLUTIONY);
            Resolution.SetResolution((int)settings.resolution.X, (int)settings.resolution.Y,settings.fullscreen);
            this.IsMouseVisible = true;

            screenManager = new ScreenManager(this);
            Components.Add(screenManager);
            screenManager.PushScreen(new GameScreen(this));
            Components.Add(new InputHandler(this));
            Components.Add(new TimerHandler(this));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {

        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }
         
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Resolution.getTransformationMatrix());
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
