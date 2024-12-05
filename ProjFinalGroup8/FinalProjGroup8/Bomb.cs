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
    public class Bomb
    {
        public Vector2 Position;
        public bool IsActive = false;
        public static int Width = 10;  
        public static int Height = 10;

        private static Random random = new Random();

        public Bomb()
        {
            Position = Vector2.Zero;
        }

        public void Spawn(int screenWidth, int screenHeight)
        {
            Position = new Vector2(
                random.Next(0, screenWidth - Width),
                random.Next(0, screenHeight - Height)
            );
            IsActive = true;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D bombTexture)
        {
            if (IsActive)
            {
                spriteBatch.Draw(bombTexture, Position, Color.White);
            }
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
        }
    }
}
