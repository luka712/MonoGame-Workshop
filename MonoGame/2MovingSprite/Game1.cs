using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _2MovingSprite
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D playerTexture;
        Vector2 position;
        float moveSpeed = 0.3f;

        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            playerTexture = Content.Load<Texture2D>("player");

            font = Content.Load<SpriteFont>("Font");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.A))
            {
                position.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            else if (keyboardState.IsKeyDown(Keys.D))
            {
                position.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            if (keyboardState.IsKeyDown(Keys.W))
            {
                position.Y -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            else if (keyboardState.IsKeyDown(Keys.S))
            {
                position.Y += moveSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(playerTexture, position, Color.White);
            spriteBatch.DrawString(font, $"Position: {position}", Vector2.One * 10, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
