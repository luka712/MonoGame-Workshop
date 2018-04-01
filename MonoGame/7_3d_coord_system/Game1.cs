using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _7_3d_coord_system
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {

        // demo 
        bool applySizeCorrection = true;

        const float speed = 0.03f;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        BasicEffect effect;
        VertexPositionTexture[] verts = new VertexPositionTexture[4];
        Vector3 position = Vector3.Zero;
        Vector3 size = Vector3.One;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // add vertex coordinates
            verts[0].Position = new Vector3(0, 0, 0);
            verts[1].Position = new Vector3(0, 1, 0);
            verts[2].Position = new Vector3(1, 0, 0);
            verts[3].Position = new Vector3(1, 1, 0);

            // add texture coordinates
            verts[0].TextureCoordinate = new Vector2(0, 1);
            verts[1].TextureCoordinate = new Vector2(0, 0);
            verts[2].TextureCoordinate = new Vector2(1, 1);
            verts[3].TextureCoordinate = new Vector2(1, 0);

            var aspectRatio = (float)graphics.PreferredBackBufferWidth / (float)graphics.PreferredBackBufferHeight;

            // setup effect/shader
            effect = new BasicEffect(GraphicsDevice);
            effect.TextureEnabled = true;
            effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), aspectRatio, .1f, 1000f);
            effect.View = Matrix.CreateLookAt(new Vector3(0, 0, 10), Vector3.Zero, Vector3.Up);

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
            var texture = Content.Load<Texture2D>("player");
            effect.Texture = texture;

            if (texture.Width > texture.Height && applySizeCorrection)
            {
                size.Y = (float)texture.Height / (float)texture.Width;
            }
            else if (applySizeCorrection)
            {
                size.X = (float)texture.Width / (float)texture.Height;
            }
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
            var elapsedTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            var keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.A))
                position.X -= speed * elapsedTime;
            else if (keyboard.IsKeyDown(Keys.D))
                position.X += speed * elapsedTime;

            if (keyboard.IsKeyDown(Keys.W))
                position.Y += speed * elapsedTime;
            else if(keyboard.IsKeyDown(Keys.S))
                position.Y -= speed * elapsedTime;

            if (keyboard.IsKeyDown(Keys.Up))
                position.Z += speed * elapsedTime;
            else if (keyboard.IsKeyDown(Keys.Down))
                position.Z -= speed * elapsedTime;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.BlendState = BlendState.AlphaBlend;

            var world = Matrix.CreateTranslation(position) * Matrix.CreateScale(size);
            effect.World = world;

            // TODO: Add your drawing code here
            foreach (var pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, verts, 0, 2);
            }

            base.Draw(gameTime);
        }
    }
}
