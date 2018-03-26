using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _5Bullets
{
    public class Player
    {
        private readonly Texture2D texture;
        private Rectangle drawRectangle;

        public Player(Texture2D texture, Rectangle drawRectangle)
        {
            this.texture = texture;
            this.drawRectangle = drawRectangle;
        }

        public void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            var moveAmount = (int)(Constants.PlayerSpeed * gameTime.TotalGameTime.Milliseconds);

            if (keyboardState.IsKeyDown(Keys.A)) drawRectangle.X -= moveAmount;
            if (keyboardState.IsKeyDown(Keys.D)) drawRectangle.X += moveAmount;
            if (keyboardState.IsKeyDown(Keys.W)) drawRectangle.Y -= moveAmount;
            if (keyboardState.IsKeyDown(Keys.S)) drawRectangle.Y += moveAmount;

            drawRectangle.X = MathHelper.Clamp(drawRectangle.X, 0, Constants.ScreenWidth - drawRectangle.Width);
            drawRectangle.Y = MathHelper.Clamp(drawRectangle.Y, 0, Constants.ScreenHeight - drawRectangle.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, drawRectangle, Color.White);
        }
    }
}
