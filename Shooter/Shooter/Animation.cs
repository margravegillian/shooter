using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace Shooter
{
    class Animation
    {
        // the image representing the colleciton of images used for animation
        Texture2D spriteStrip;
        
        //the scale used to diplay sprite strip
        float scale;
        // the time since we last updated the frame
        int elapsedTime;
        //the time we display a frame until the next one
        int frameTime;
        //the number of frames that the animation contains
        int frameCount;
        // the index of the current frame we are displaying
        int currentFrame;
        // the color of the frame we will be displaying
        Color color;
        // the area of the image strip we want to display
        Rectangle sourceRect = new Rectangle();
        // the area where we want to display the image strip in the game
        Rectangle destinationRect = new Rectangle();
        //width of a given frame
        public int FrameWidth;
        // heit of a given frame
        public int FrameHeight;
        // the state of the animation
        public bool Active;
        // determines if the animation will keep playing or deactivate after one run
        public bool Looping;
        // position of given frame
        public Vector2 Position;
        public void Initialize(Texture2D texture, Vector2 position,int frameWidth, int frameHeight, int frameCount, int frameTime, Color color, float scale, bool looping)
        {
            //keep a local copy of the values passed in
            this.color = color;
            this.FrameHeight = frameHeight;
            this.FrameWidth = frameWidth;
            this.frameCount = frameCount;
            this.frameTime = frameTime;
            this.scale = scale;
            Looping = looping;
            Position = position;
            spriteStrip = texture;
            //set the time to zero;
            elapsedTime = 0;
            currentFrame = 0;
            //set the animation to active by default
            Active = true;





        }
        public void Update(GameTime gameTime)
        {
            // do not update the game if we are not active
            if (Active == false)
                return;

            // update the elapsed time
            elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            // if the elapsed time is larger than the frame time we need to switch frames
            if (elapsedTime > frameTime)
            {
                //move to the next frame
                currentFrame++;

                // if the current frame is dqual to frameCount reset currentframe to zerio
                if (currentFrame == frameCount)
                {
                    currentFrame = 0;
                    //if we are not loooping deactivate the animation
                    if (Looping == false)
                        Active = false;
                }
                //reset the elapsed time to zerio
                elapsedTime = 0;
            }
            // grab the correct frame in the image strip by multiplying the currentFrame index by the frame width
            sourceRect = new Rectangle(currentFrame * FrameWidth, 0, FrameWidth, FrameHeight);
            //grab the correct frame in the image strip by multiplying the currentFrame index by the frame width
            destinationRect = new Rectangle((int)Position.X - (int)(FrameWidth * scale) / 2, (int)Position.Y - (int)(FrameHeight * scale) / 2, (int)(FrameWidth * scale), (int)(FrameHeight * scale));





        }

       
        public void Draw(SpriteBatch spriteBatch)
        {
            // only draw the animation when we are active
            if(Active)
            {
                spriteBatch.Draw(spriteStrip, destinationRect,sourceRect,color);
            }
        
        

        }
    }
}
