using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


//Gia has commented this
//Change1 made by Gia
namespace FinalProjGroup8
{
	//Adding content to game 
	public class Game1 : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;


		private Texture2D ballSprite;
		private Texture2D barSprite;
		private Texture2D backgroundSprite;
		private Texture2D brickSprite;
		private SpriteFont timerFont;

		private Ball ball;
		private Bar bar;
		private Brick brick;

		private MultipleBricksManager bricksManager;
		private CollisionManager collisionManager;


		private bool isGameOn = true;
		private bool gameCompleted = false;

		private TimeSpan elapsedTime;
		private int secondsElapsed;
		private int score;

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			_graphics.PreferredBackBufferWidth = 1200;
			_graphics.PreferredBackBufferHeight = 900;
			_graphics.ApplyChanges();

			elapsedTime = TimeSpan.Zero;
			secondsElapsed = 0;
			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			ballSprite = Content.Load<Texture2D>("ball");
			barSprite = Content.Load<Texture2D>("bar");
			backgroundSprite = Content.Load<Texture2D>("backgroung");
			brickSprite = Content.Load<Texture2D>("brick");
			timerFont = Content.Load<SpriteFont>("timerFont");
			SoundEffect hitSound = Content.Load<SoundEffect>("ding");
			SoundEffect breakSound = Content.Load<SoundEffect>("Brickbreak");


			ball = new Ball(ballSprite, new Vector2(600, 450)); // Starting position
			bar = new Bar(barSprite, _graphics.PreferredBackBufferWidth);
			bricksManager = new MultipleBricksManager(breakSound);
			collisionManager = new CollisionManager(ball, bar, bricksManager, hitSound);

		}

		protected override void Update(GameTime gameTime)
		{
			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				Exit();
			}

			if (isGameOn)
			{

				ball.Update();
				bar.Update(_graphics.PreferredBackBufferWidth);
				bricksManager.UpdateBricks(gameTime);
				collisionManager.Update(gameTime);
				collisionManager.UpdateTimer(gameTime);


				secondsElapsed = collisionManager.secondsElapsed;


				// If the ball goes past the bar, the game ends
				if (ball.Position.Y >= _graphics.PreferredBackBufferHeight)
				{
					GameOver();
				}


			}

			base.Update(gameTime);
		}

		private void GameOver()
		{
			isGameOn = false;
			gameCompleted = true;
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			_spriteBatch.Begin();
			_spriteBatch.Draw(backgroundSprite, Vector2.Zero, Color.White);

			// Draw the ball, bar, and bricks
			ball.Draw(_spriteBatch);
			bar.Draw(_spriteBatch);


			foreach (var brick in bricksManager.Bricks)
			{
				brick.Draw(_spriteBatch, brickSprite);
			}





			_spriteBatch.DrawString(timerFont, "Time: " + secondsElapsed, new Vector2(10, 10), Color.White);
			_spriteBatch.DrawString(timerFont, "Score: " + score, new Vector2(10, 50), Color.White);

			if (!isGameOn)
			{
				string endMessage = collisionManager.GameEndScript(score, gameCompleted);
				Vector2 textSize = timerFont.MeasureString(endMessage);
				Vector2 position = new Vector2((_graphics.PreferredBackBufferWidth - textSize.X) / 2,
													 (_graphics.PreferredBackBufferHeight - textSize.Y) / 2);
				_spriteBatch.DrawString(timerFont, endMessage, position, Color.White);
			}

			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
