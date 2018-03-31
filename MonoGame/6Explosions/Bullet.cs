using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _6Explosions
{
    public class Bullet
    {
        private Rectangle drawRectangle;
        private readonly Texture2D texture;
        private readonly Vector2 direction;

        public Rectangle CollisionRectange => drawRectangle;

        public Bullet(Texture2D texture, Rectangle drawRectangle, Vector2 direction)
        {
            this.texture = texture;
            this.drawRectangle = drawRectangle;
            this.direction = direction;
        }

        public void Update(GameTime gameTime)
        {
            var moveAmount = (float)(Constants.BulletSpeed * gameTime.ElapsedGameTime.TotalMilliseconds);

            drawRectangle.X += (int)(moveAmount * direction.X);
            drawRectangle.Y += (int)(moveAmount * direction.Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, drawRectangle, Color.White);
        }
    }
}
