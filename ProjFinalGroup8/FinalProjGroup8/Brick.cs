using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using Microsoft.Xna.Framework.Audio;

namespace Project1
{
	public class Brick
	{
		public Vector2 Position;
		public bool IsHit = false;
		public static int Width = 50;
		public static int Height = 0;
		
		

		private static Random random = new Random();

		public Brick(int xPosition, int yPosition)
		{
			Position = new Vector2(xPosition, yPosition);
		}

		

		public void Update()
		{
			// This can be expanded if you want any logic for brick animation or other features

		}

		public void Draw(SpriteBatch spriteBatch, Texture2D brickTexture)
		{
			if (!IsHit)
				spriteBatch.Draw(brickTexture, Position, Color.White);
		}
	}

	public class MultipleBricksManager
	{
        public List<Brick> Bricks = new List<Brick>();
        private Random random = new Random();
        private int brickInterval = 3000;
        private int BrickAppearance = 0;
        private SoundEffect breakSound;
        private int brickWidth = 50;
        private int brickHeight = 20;

        public MultipleBricksManager(SoundEffect breakSound)
        {
            this.breakSound = breakSound;
        }

        public void UpdateBricks(GameTime gameTime, ref int score)
        {
            BrickAppearance += gameTime.ElapsedGameTime.Milliseconds;

            if (BrickAppearance >= brickInterval)
            {
                Bricks.Add(new Brick(random.Next(100, 1100), random.Next(100, 500)));
                BrickAppearance = 0;
            }

            for (int i = 0; i < Bricks.Count; i++)
            {
                if (Bricks[i].IsHit)
                {
                    Bricks.RemoveAt(i);
                    breakSound.Play();
                    score++; 
                    i--;
                }
            }
        }

        public void CheckCollisions(Ball ball)
        {
            foreach (var brick in Bricks)
            {
                if (!brick.IsHit && ball.GetBounds().Intersects(new Rectangle((int)brick.Position.X, (int)brick.Position.Y, brickWidth, brickHeight)))
                {
                    brick.IsHit = true;
                    ball.ReverseYDirection();
                }
            }
        }
    }
}
