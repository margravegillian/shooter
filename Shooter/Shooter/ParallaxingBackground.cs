using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Shooter
{
    class ParallaxingBackground
    {   
        // an image representing the parallazing background
        Texture2D texture;

        // an array of positions of the parallaxing background
        Vector2[] positions;
        // the speed chich the background is moving
        int speed;

        public void Initialize(ContentManager content, String texturePath, int screenWidth, int speed)
    {
            //load the background texture we will be using
        texture = content.Load<Texture2D>(texturePath);
            //set the speed of the background
        this.speed = speed;
            //if we divide the screen with the texture width then we can determine the number of tiles we need. 
            //we add 1 to it so that we won't have a gap in the tiling
        positions = new Vector2[screenWidth / texture.Width + 1];

            //set the inital positions of the parallaxing background
        for (int i = 0; i < positions.Length; i++)
        {
            //we need the tiles to be side by side to create a tiling effect
            positions[i] = new Vector2(i * texture.Width, 0);
        }


    
    }
        public void Update()
    {
            //update the positions of the background
        for (int i = 0; i < positions.Length; i++)
        {
            //update the osition of the screen by adding the speed
            positions[i].X += speed;
            //if the speed has the background moving th the left
            if (speed <= 0)
            {
                // check the texture is out of view then put that texture at the end of the screen
                if (positions[i].X <= -texture.Width)
                {
                    positions[i].X = texture.Width * (positions.Length - 1);
                }
            }
            //if the speed has the background moving to the right
            else
            {
                //check if the texture is out of view then position it to the start of the screen
                if (positions[i].X >= texture.Width * (positions.Length - 1))
                {
                    positions[i].X = -texture.Width;
                }
            }

        }
           

    }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < positions.Length; i++)
            {
                spriteBatch.Draw(texture, positions[i], Color.White);
            }
        }

    }
}
