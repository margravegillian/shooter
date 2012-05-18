using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input.Touch;

namespace Shooter
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Represents the player 
        Player player;
        //keyboard states used to determine key presses
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;
        //gamepad states
        GamePadState currentGamePadState;
        GamePadState previousGamePadState;

        // a movement speed for the player
        float playerMoveSpeed;
        // image used to display teh static background
        Texture2D mainBackground;
        //paralaxing layers
        ParallaxingBackground bgLayer1;
        ParallaxingBackground bgLayer2;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //Initialize the player class
            player = new Player();
            //set a constante player move speed
            playerMoveSpeed = 8.0f;
            //enable freedrag gesture
            TouchPanel.EnabledGestures = GestureType.FreeDrag;
            //load the paralaxing background
            bgLayer1 = new ParallaxingBackground();
            bgLayer2 = new ParallaxingBackground();
            



            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load the player resources            
            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            Animation playerAnimation = new Animation();
            Texture2D playerTexture = Content.Load<Texture2D>("shipAnimation");
            playerAnimation.Initialize(playerTexture, Vector2.Zero, 115, 69, 8, 30, Color.White, 1f, true);
            player.Initialize(playerAnimation, playerPosition);
            bgLayer1.Initialize(Content, "bgLayer1", GraphicsDevice.Viewport.Width, -1);
            bgLayer2.Initialize(Content, "bgLayer2", GraphicsDevice.Viewport.Width, -2);
            mainBackground = Content.Load<Texture2D>("mainbackground");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            // save the previous state of the keyboard and game pad so we can determine key/button presses
            previousGamePadState = currentGamePadState;
            previousKeyboardState = currentKeyboardState;

            //read the current state of the keyboard and gamepad and store it
            currentKeyboardState = Keyboard.GetState();
            currentGamePadState = GamePad.GetState(PlayerIndex.One);

            // call the updateplayermethod
            UpdatePlayer(gameTime);
            //update the paraalaxing background
            bgLayer1.Update();
            bgLayer2.Update();


            base.Update(gameTime);
        }

        //update player method
        private void UpdatePlayer(GameTime gametime)
        {
            //send gametime to player to send to animation
            player.Update(gametime);
            //windows phone controls
            while (TouchPanel.IsGestureAvailable)
            {
                GestureSample gesture = TouchPanel.ReadGesture();
                if (gesture.GestureType == GestureType.FreeDrag)
                {
                    player.Position += gesture.Delta;
                }
            }
            //get thumbstick controls  testing just test one change
            player.Position.X += currentGamePadState.ThumbSticks.Left.X * playerMoveSpeed;
            player.Position.Y -= currentGamePadState.ThumbSticks.Left.Y * playerMoveSpeed;


            //use the keyboard / dpad
            if (currentKeyboardState.IsKeyDown(Keys.Left) ||
                currentGamePadState.DPad.Left == ButtonState.Pressed)
            {
                player.Position.X -= playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Right) ||
                currentGamePadState.DPad.Right == ButtonState.Pressed)
            {
                player.Position.X += playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Up) ||
                currentGamePadState.DPad.Up == ButtonState.Pressed)
            {
                player.Position.Y -= playerMoveSpeed;
            }
            if (currentKeyboardState.IsKeyDown(Keys.Down) ||
                currentGamePadState.DPad.Down == ButtonState.Pressed)
            {
                player.Position.Y += playerMoveSpeed;
            }
            //make sure that player does not go out of bounds
            player.Position.X = MathHelper.Clamp(player.Position.X, 0, GraphicsDevice.Viewport.Width - player.Width);
            player.Position.Y = MathHelper.Clamp(player.Position.Y, 0, GraphicsDevice.Viewport.Height - player.Health);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Start drawing
            spriteBatch.Begin();
            // draw the moving paralaxing background
            spriteBatch.Draw(mainBackground, Vector2.Zero, Color.White);
            bgLayer1.Draw(spriteBatch);
            bgLayer2.Draw(spriteBatch);

            // Draw the Player
            player.Draw(spriteBatch);

            //Stop drawing
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
