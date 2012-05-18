using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shooter
{
    class Enemy
    {

        // animation representing the enemy
        public Animation EnemyAnimation;
        //the position of the enemy ship relative to the top left corner of the screen
        public Vector2 Position;
        // the state of the enemy ship
        public bool Active;
        //the hit point of the enemy. if this goes to zero the enemy dies
        public int Health;
        // the amout of damage the enemy inflicts on the player ship
        public int Damage;
        // the amount of score the enemy will give to the player
        public int Value;
        // get the width of the enemy ship
        public int Width
        {
            get { return EnemyAnimation.FrameWidth; }
        }
        public int Height
        {
            get { return EnemyAnimation.FrameHeight; }
        }

        // the speed at which the enemy moves
        float enemyMoveSpeed;

        public void Initialize(Animation animation,Vector2 position)
        {
            //load the enemy ship texture
            EnemyAnimation = animation;
            //set the position of the enemy
            Position = position;
            //we initialize the enemy to be active so it will be updated in the game
            Active = true;
            // set the health of hte enemy
            Health = 10;
            //set the amout of damage the enemy can do
            Damage = 10;
            //set how fast the enemy moves
            enemyMoveSpeed = 6f;
            //set the score value of the enemy
            Value = 100;
        }
        public void Update(GameTime gameTime)
        {
            //the enemy alwasy moves to the left so decremtn its x position
            Position.X -= enemyMoveSpeed;
            //update the position of the animation
            EnemyAnimation.Position = Position;
            //update animation
            EnemyAnimation.Update(gameTime);
            //if the enemy is past the screen or its health reaches 0 then deactivate it
            if (Position.X < -Width || Health <= 0)
            {
                // by setting the active flag to false the game will remove this object from the active game list
                Active = false;

            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //draw the animation
            EnemyAnimation.Draw(spriteBatch);

        }

    }
}
