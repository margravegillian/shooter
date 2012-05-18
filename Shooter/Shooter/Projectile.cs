using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Shooter
{
    class Projectile
    {
        //image representing projectile
        public Texture2D Texture;
        // position of the projectile relative to the upper left side of screen
        public Vector2 Position;
        // state of the projectile
        public bool active;
        // the amout of damage the projectile an inflict on an enemy
        public int Damage;
        // represents the viewable boundary of the game
        Viewport viewport;
        // get the width of the projectile ship
        public int Width
        {
            get { return Texture.Width;  }
        }
        //get the height of the projectile ship
        public int Height
        {
            get { return Texture.Height; }
        }
        //determines how fast the projectile moves
        float projectileMoveSpeed;

        public void Initialize(Viewport viewport, Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
            this.viewport = viewport;
            active = true;
            Damage = 2;
            projectileMoveSpeed = 20f;

        }
        public void Update()
        {
            //projectiles alwasy move to the right
            Position.X += projectileMoveSpeed;
            //deactivate the bullet if it goes out of the screen
            if (Position.X + Texture.Width / 2 > viewport.Width)
                active = false;
        }
        public void Draw(SpriteBatch spriteBatch)

        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f, new Vector2(Width / 2, Height / 2), 1f, SpriteEffects.None, 0f);


        }
    }
}
