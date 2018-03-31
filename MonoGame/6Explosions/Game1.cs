using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace _6Explosions
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {

        private float timeToSpawn = Constants.SpawnTime;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Player player;
        List<Enemy> enemies = new List<Enemy>();
        List<Bullet> playerBullets = new List<Bullet>();

        Texture2D playerBulletTexture;
        Texture2D backgroundTexture;
        Texture2D[] enemyTextures;

        Random random = new Random();

        Rectangle worldRect;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = Constants.ScreenWidth;
            graphics.PreferredBackBufferHeight = Constants.ScreenHeight;
        }

        protected override void Initialize()
        {
            base.Initialize();

            worldRect = new Rectangle(0, 0, Constants.ScreenWidth, Constants.ScreenHeight);
        }

        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);

            // create player
            var texture = Content.Load<Texture2D>("player");
            player = new Player(texture, new Rectangle(350, 450, texture.Width / 2, texture.Height / 2));

            // background
            backgroundTexture = Content.Load<Texture2D>("purple");

            // bullet textures
            playerBulletTexture = Content.Load<Texture2D>("laserBlue01");

            // create enemy textures
            enemyTextures = new Texture2D[20];
            var colors = new[] { "Black", "Blue", "Green", "Red" };
            var index = 0;
            foreach (var color in colors)
            {
                for (int i = 1; i <= 5; i++)
                {
                    enemyTextures[index++] = Content.Load<Texture2D>($"enemy{color}{i}");
                }
            }
            ParticleManager.Initialize(Content.Load<Texture2D>("fire00"));

        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            SpawnEnemy(gameTime);

            player.Update(gameTime);
            if (player.FireButtonPressed)
            {
                SpawnPlayerBullet();
            }

            // Why backwards ?
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                enemies[i].Update(gameTime);

                // if enemy is out of screen bounds, remove it
                if (worldRect.Intersects(enemies[i].CollisionRectangle) == false)
                {
                    enemies.RemoveAt(i);
                    continue;
                }

                // check against player
                if (enemies[i].CollisionRectangle.Intersects(player.CollisionRectangle))
                {
                    enemies.RemoveAt(i);
                }
            }

            // check player bullets
            for (int i = playerBullets.Count - 1; i >= 0; i--)
            {
                playerBullets[i].Update(gameTime);

                // if out of bounds remove it.
                if (worldRect.Intersects(playerBullets[i].CollisionRectange) == false)
                {
                    playerBullets.RemoveAt(i);
                    continue;
                }

                for (int j = enemies.Count - 1; j >= 0; j--)
                {
                    // if collision
                    if (playerBullets[i].CollisionRectange.Intersects(enemies[j].CollisionRectangle))
                    {
                        ParticleManager.ExplodeEnemy(enemies[j]);
                        playerBullets.RemoveAt(i);
                        enemies.RemoveAt(j);
                        break;
                    }
                }
            }

            ParticleManager.Update(gameTime);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, Constants.ScreenWidth, Constants.ScreenHeight), Color.White);
            ParticleManager.Draw(spriteBatch);
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Draw(spriteBatch);
            }
            for (int i = 0; i < playerBullets.Count; i++)
            {
                playerBullets[i].Draw(spriteBatch);
            }
            player.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void SpawnEnemy(GameTime gameTime)
        {
            timeToSpawn -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeToSpawn <= 0)
            {
                timeToSpawn = Constants.SpawnTime;

                int numOfTextures = enemyTextures.Length;
                int randNumber = random.Next(numOfTextures);
                Texture2D texture = enemyTextures[randNumber];

                Point enemySize = new Point(texture.Width / 2, texture.Height / 2);
                int posX = (int)(random.NextDouble() * (Constants.ScreenWidth - enemySize.X));
                int posY = -enemySize.Y;
                Point enemyPos = new Point(posX, posY);

                enemies.Add(new Enemy(texture, new Rectangle(enemyPos, enemySize), Vector2.UnitY));
            }
        }

        private void SpawnPlayerBullet()
        {
            var bulletSize = new Vector2(playerBulletTexture.Width / 2, playerBulletTexture.Height / 2);
            var position = player.Position + player.PlayerSize / 2f - bulletSize / 2f;
            playerBullets.Add(new Bullet(playerBulletTexture,
                new Rectangle(position.ToPoint(), bulletSize.ToPoint()),
                -Vector2.UnitY));
        }
    }
}
