using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System.Threading.Tasks;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Project1
{
	public class Ball
	{
		public Vector2 Position;
		public Vector2 Velocity;
		private Texture2D texture;
		private int speed = 6;
		private int radius = 10;

		public Ball(Texture2D ballTexture, Vector2 startPosition)
		{
			texture = ballTexture;
			Position = startPosition;
			Velocity = new Vector2(speed, -speed);
		}

		public void Update()
		{
			Position += Velocity;


			if (Position.X <= 0 || Position.X >= 1200 - texture.Width)
			{
				Velocity.X = -Velocity.X;
			}

			if (Position.Y <= 0)
			{
				Velocity.Y = -Velocity.Y;
			}
		}

		public void ReverseYDirection()
		{
			Velocity.Y = -Velocity.Y;
		}

		public Rectangle GetBounds()
		{
			return new Microsoft.Xna.Framework.Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(texture, Position, Microsoft.Xna.Framework.Color.White);
		}
	}
}
