using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _6Explosions
{
    public class Enemy
    {
        private Rectangle drawRectangle;
        private readonly Vector2 direction;

        public Rectangle CollisionRectangle => drawRectangle;
        public Rectangle DrawRectangle => drawRectangle;
        public Vector2 Position => drawRectangle.Location.ToVector2();
        public Texture2D Texture { get; }

        public Enemy(Texture2D texture, Rectangle drawRectangle, Vector2 direction)
        {
            Texture = texture;
            this.drawRectangle = drawRectangle;
            this.direction = direction;
        }

        public void Update(GameTime gameTime)
        {
            var moveAmount = (float)(Constants.EnemySpeed * gameTime.ElapsedGameTime.TotalMilliseconds);

            drawRectangle.X += (int)(moveAmount * direction.X);
            drawRectangle.Y += (int)(moveAmount * direction.Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, drawRectangle, Color.White);
        }
    }
}
