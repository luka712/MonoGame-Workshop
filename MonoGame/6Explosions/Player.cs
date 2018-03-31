using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _6Explosions
{
    public class Player
    {
        private Rectangle drawRectangle;
        private KeyboardState previousKeyboardState;

        public bool FireButtonPressed { get; private set; }

        public Vector2 Position => drawRectangle.Location.ToVector2();
        public Vector2 PlayerSize => drawRectangle.Size.ToVector2();
        public Rectangle CollisionRectangle => drawRectangle;
        public Rectangle DrawRectangle => drawRectangle;
        public Texture2D Texture { get; }

        public Player(Texture2D texture, Rectangle drawRectangle)
        {
            Texture = texture;
            this.drawRectangle = drawRectangle;
        }

        public void Update(GameTime gameTime)
        {
            var currentKeyboardState = Keyboard.GetState();
            var moveAmount = (int)(Constants.PlayerSpeed * gameTime.ElapsedGameTime.TotalMilliseconds);

            if (currentKeyboardState.IsKeyDown(Keys.A)) drawRectangle.X -= moveAmount;
            if (currentKeyboardState.IsKeyDown(Keys.D)) drawRectangle.X += moveAmount;
            if (currentKeyboardState.IsKeyDown(Keys.W)) drawRectangle.Y -= moveAmount;
            if (currentKeyboardState.IsKeyDown(Keys.S)) drawRectangle.Y += moveAmount;

            drawRectangle.X = MathHelper.Clamp(drawRectangle.X, 0, Constants.ScreenWidth - drawRectangle.Width);
            drawRectangle.Y = MathHelper.Clamp(drawRectangle.Y, 0, Constants.ScreenHeight - drawRectangle.Height);

            FireButtonPressed = currentKeyboardState.IsKeyUp(Keys.Space) &&
                previousKeyboardState.IsKeyDown(Keys.Space);

            previousKeyboardState = currentKeyboardState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, drawRectangle, Color.White);
        }
    }
}
