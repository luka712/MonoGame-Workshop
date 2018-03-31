using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace _6Explosions
{
    public static class ParticleManager
    {
        private static Vector2 playerDividePass = new Vector2(3, 3);
        private static int playerParticlesExplodeCount = 10;
        private static Random rand = new Random();
        private static Texture2D sparkParticleTexture;

        public static List<Particle> activeParticlesList;
        public static Stack<Particle> inactiveParticlesStack;

        public static void Initialize(Texture2D sparkParticleTexture)
        {
            ParticleManager.sparkParticleTexture = sparkParticleTexture;
            activeParticlesList = new List<Particle>();
            inactiveParticlesStack = new Stack<Particle>();

            for (int i = 0; i < Constants.ParticlesCount; i++)
            {
                inactiveParticlesStack.Push(new Particle());
            }
        }

        public static void Update(GameTime gameTime)
        {
            var elapsedTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            for (int i = activeParticlesList.Count - 1; i >= 0; i--)
            {
                var particle = activeParticlesList[i];
                var position = particle.DrawRectangle.Location.ToVector2();

                position += particle.Direction * particle.Speed * elapsedTime;
                particle.DrawRectangle = new Rectangle(position.ToPoint(), particle.DrawRectangle.Size);

                if (particle.ParticleType == ParticleType.Ship)
                {
                    particle.Rotation += particle.RandomRotation;
                }
                else
                {
                    particle.Rotation = (float)Math.Atan2(particle.Direction.Y, particle.Direction.X);
                }

                particle.Color = particle.Color * 0.99f;
                if (particle.Color.A < 0.01f)
                {
                    activeParticlesList.RemoveAt(i);
                    inactiveParticlesStack.Push(particle);
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            for (int i = activeParticlesList.Count - 1; i > 0; i--)
            {
                var particle = activeParticlesList[i];
                spriteBatch.Draw(particle.Texture, particle.DrawRectangle, particle.SourceRectangle,
                    particle.Color, particle.Rotation, particle.Origin, SpriteEffects.None, 0f);
            }
        }

        public static void ExplodeEnemy(Enemy enemy)
        {
            var width = (int)(enemy.DrawRectangle.Width / playerDividePass.X);
            var height = (int)(enemy.DrawRectangle.Height / playerDividePass.Y);

            if (inactiveParticlesStack.Count < playerDividePass.X * playerDividePass.Y)
                return;

            for (int x = 0; x < playerDividePass.X; x++)
            {
                for (int y = 0; y < playerDividePass.Y; y++)
                {
                    var particle = inactiveParticlesStack.Pop();

                    particle.Texture = enemy.Texture;
                    particle.DrawRectangle = new Rectangle(enemy.Position.ToPoint(), new Point(width, height));
                    particle.SourceRectangle = new Rectangle(new Point(width * x, height * y)
                        , new Point(width, height));
                    particle.Speed = (float)rand.NextDouble();
                    particle.Direction = new Vector2((float)rand.NextDouble() * 2 - 1,
                        (float)rand.NextDouble() * 2 - 1);
                    particle.Color = Color.White;
                    particle.ParticleType = ParticleType.Ship;
                    particle.RandomRotation = (float)(rand.NextDouble() - .5f) / 20f;

                    activeParticlesList.Add(particle);
                }
            }

            if (inactiveParticlesStack.Count < playerParticlesExplodeCount)
                return;


            for (int i = 0; i < playerParticlesExplodeCount; i++)
            {
                var particle = inactiveParticlesStack.Pop();

                particle.Texture = sparkParticleTexture;
                particle.DrawRectangle = new Rectangle(enemy.Position.ToPoint(),
                    (sparkParticleTexture.Bounds.Size.ToVector2() / 5).ToPoint());
                particle.SourceRectangle = null;
                particle.Speed = (float)rand.NextDouble();
                particle.Direction = new Vector2((float)rand.NextDouble() * 2 - 1,
                    (float)rand.NextDouble() * 2 - 1);
                particle.Direction.Normalize();
                particle.Color = Color.Orange;
                particle.ParticleType = ParticleType.Spark;

                activeParticlesList.Add(particle);
            }
        }
    }
}
