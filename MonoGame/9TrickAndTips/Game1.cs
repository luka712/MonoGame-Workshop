#define DELTA_TIME

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace _9TrickAndTips
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        Stopwatch stopwatch = new Stopwatch();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D playerTexture;
        SpriteFont font;
        Vector2 position;
        float moveSpeed = 3f;
        float framerate;

        List<Vector2> someVectors;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;

            IsFixedTimeStep = true;
            graphics.SynchronizeWithVerticalRetrace = false;
        }

       
        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            playerTexture = Content.Load<Texture2D>("player");
            font = Content.Load<SpriteFont>("File");
        }

      
        protected override void UnloadContent()
        {
        }

       
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            framerate = 1.0f / (float)gameTime.ElapsedGameTime.TotalSeconds;

            // TODO: Add your update logic here
            var keyboardState = Keyboard.GetState();
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (keyboardState.IsKeyDown(Keys.A))
            {
#if DELTA_TIME
                position.X -= moveSpeed * elapsedTime;
#else
                position.X -= moveSpeed;
#endif
            }
            else if (keyboardState.IsKeyDown(Keys.D))
            {
#if DELTA_TIME
                position.X += moveSpeed * elapsedTime;
#else
                position.X += moveSpeed;
#endif
            }

            if (keyboardState.IsKeyDown(Keys.W))
            {
#if DELTA_TIME
                position.Y -= moveSpeed * elapsedTime;
#else
                position.Y -= moveSpeed;
#endif
            }
            else if (keyboardState.IsKeyDown(Keys.S))
            {
#if DELTA_TIME
                position.Y += moveSpeed * elapsedTime;
#else
                position.Y += moveSpeed;
#endif
            }


            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.DrawString(font, $"Framerate: {framerate.ToString("n2")}", Vector2.One * 10, Color.White);
            spriteBatch.Draw(playerTexture, position, Color.White);
            ShowParamsIssue(1, 2, 3);
            Console.Write("{0}{1}{2}", 1, 2, 3.0, 4,5);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void ShowParamsIssue(params int[] someNumbers)
        {
            spriteBatch.DrawString(font, $"Type: {someNumbers.ToString()}", new Vector2(10, 30), Color.White);
        }

        private void ShowBoxingIssue(params object[] someNumbers)
        {
            spriteBatch.DrawString(font, $"Type: {someNumbers.ToString()}", new Vector2(10, 30), Color.White);
        }
    }
}
