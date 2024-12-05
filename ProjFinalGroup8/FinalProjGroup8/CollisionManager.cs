using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using System;
using static System.Formats.Asn1.AsnWriter;

namespace Project1
{
	public class CollisionManager
	{
		private Ball ball;
		private Bar bar;
		private MultipleBricksManager bricksManager;
		private SoundEffect hitSound;
		private TimeSpan elapsedTime;
		public int secondsElapsed;

		public CollisionManager(Ball ball, Bar bar,  MultipleBricksManager bricksManager, SoundEffect hitSound)
		{
			this.ball = ball;
			this.bar = bar;
			this.bricksManager = bricksManager;
			this.hitSound = hitSound;
			
		}

		public void Update(GameTime gameTime)
		{
			// Ball hits the bar
			if (ball.GetBounds().Intersects(bar.GetBounds()))
			{
				ball.ReverseYDirection();
				hitSound.Play();
			}

			

			// Ball hits the bricks
			bricksManager.CheckCollisions(ball);
			
		}

		public void UpdateTimer(GameTime gameTime)
		{
			elapsedTime += gameTime.ElapsedGameTime;

			// Calculate seconds passed
			if (elapsedTime.TotalSeconds >= 1)
			{
				secondsElapsed++;
				elapsedTime -= TimeSpan.FromSeconds(1);
			}
		}

		public string GameEndScript(int score, bool gameCompleted)
		{
			if (gameCompleted && score >= 0)
			{
				return $"Game Completed! You scored {score} points!";
			}
			else
			{
				return "Game Over!";
			}
		}
	}
}
