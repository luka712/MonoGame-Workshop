using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace _8Model
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Model treeModel;
        Camera camera;
        List<Tree> trees = new List<Tree>();
        private VertexPositionTexture[] verts = new VertexPositionTexture[]
        {
            new VertexPositionTexture { Position = new Vector3(-1,-1,0), TextureCoordinate = new Vector2(-1,1) },
            new VertexPositionTexture { Position = new Vector3(-1,1,0), TextureCoordinate = new Vector2(-1,-1) },
            new VertexPositionTexture { Position = new Vector3(1,-1,0), TextureCoordinate = new Vector2(1,1) },
            new VertexPositionTexture { Position = new Vector3(1,1,0), TextureCoordinate = new Vector2(1,-1) }
        };
        BasicEffect groundEffect;


        float aspectRatio;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            aspectRatio = (float)graphics.PreferredBackBufferWidth / (float)graphics.PreferredBackBufferHeight;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            camera = new Camera
            {
                ViewMatrix = Matrix.CreateLookAt(new Vector3(0, 5, 20), Vector3.Zero, Vector3.Up),
                ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), aspectRatio, 0.1f, 1000f)
            };


            var rand = new Random();
            trees = new List<Tree>();
            // add some random trees.
            for (int i = 0; i < 15; i++)
            {
                trees.Add(new Tree { Position = new Vector3(rand.Next(-10, 10), 0, rand.Next(-20, 10)) });
            }


            groundEffect = new BasicEffect(GraphicsDevice)
            {
                FogEnabled = true,
                FogColor = Color.Gray.ToVector3(),
                FogStart = 10,
                FogEnd = 40,
                TextureEnabled = true,
                View = camera.ViewMatrix,
                Projection = camera.ProjectionMatrix,
                World = Matrix.Identity * Matrix.CreateScale(30, 30, 1) * Matrix.CreateRotationX(MathHelper.ToRadians(270)) * Matrix.CreateTranslation(0, -3, 0)
            };


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

            treeModel = Content.Load<Model>("lowpolytree");

            // we need texture for our ground, which is set on effect
            var texture = Content.Load<Texture2D>("ground");
            groundEffect.Texture = texture;
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
            foreach (var mesh in treeModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    // tree model contains 2 meshes, on of which is cylinder
                    if (mesh.Name == "Cylinder")
                    {
                        effect.DiffuseColor = Color.Green.ToVector3();
                    }
                    else
                    {
                        effect.DiffuseColor = Color.Brown.ToVector3();
                    }
                    effect.EnableDefaultLighting();
                    effect.View = camera.ViewMatrix;
                    effect.Projection = camera.ProjectionMatrix;
                    effect.FogEnabled = true;
                    effect.FogColor = Color.Gray.ToVector3();
                    effect.FogStart = 10;
                    effect.FogEnd = 40;
                    foreach (var tree in trees)
                    {
                        effect.World = Matrix.CreateTranslation(tree.Position);
                        mesh.Draw();
                    }
                }
            }

            foreach (var pass in groundEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, verts, 0, 2);
            }


            base.Draw(gameTime);
        }
    }
}
