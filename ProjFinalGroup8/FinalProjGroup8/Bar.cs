using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
	public class Bar
	{
		public Vector2 Position;
		public int Speed = 10;
		private int width = 300;
		private int height = 20;

		private Texture2D texture;

		public Bar(Texture2D barTexture, int screenWidth)
		{
			texture = barTexture;
			Position = new Vector2(screenWidth / 2 - width / 2, 800); // Bar at the bottom
		}

		public void Update(int screenWidth)
		{
			KeyboardState state = Keyboard.GetState();

			if (state.IsKeyDown(Keys.Left))
			{
				Position.X -= Speed;
			}
			if (state.IsKeyDown(Keys.Right))
			{
				Position.X += Speed;
			}

			// Ensure the bar stays within screen bounds
			Position.X = MathHelper.Clamp(Position.X, 0, screenWidth - width);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(texture, Position, Color.White);
		}

		public Rectangle GetBounds()
		{
			return new Rectangle((int)Position.X, (int)Position.Y, width, height);
		}
	}
}
