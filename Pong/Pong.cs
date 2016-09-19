using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Pong : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private int paddleWidth, paddleHeight, ballWidth, ballHeight;
        private Texture2D player1, player2, ball, background;
        private Color[] paddleData, ballData, blackColorData;
        private Vector2 player1Pos, player2Pos, ballPos;
        private float ballVelX, ballVelY;

        public Pong()
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
            // TODO: Add your initialization logic here

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

            // width and height of the paddles
            paddleWidth = 20;
            paddleHeight = 150;

            // size of the ball
            ballWidth = 20;
            ballHeight = 20;

            // starting velocity of the ball
            ballVelX = 2;
            ballVelY = 0;

            // initialize the color for the textures
            //paddle
            paddleData = new Color[paddleWidth * paddleHeight];
            for (int i = 0; i < paddleData.Length; i++)
            {
                paddleData[i] = Color.White;
            }

            //ball
            ballData = new Color[ballWidth * ballHeight];
            for (int i = 0; i < ballData.Length; i++)
            {
                ballData[i] = Color.White;
            }


            //black
            blackColorData = new Color[GraphicsDevice.Viewport.Width * GraphicsDevice.Viewport.Height];
            for (int i = 0; i < blackColorData.Length; i++)
            {
                blackColorData[i] = Color.Black;
            }

            player1Pos = new Vector2(0, GraphicsDevice.Viewport.Height / 2 - paddleHeight/2);
            player2Pos = new Vector2(GraphicsDevice.Viewport.Width - paddleWidth, GraphicsDevice.Viewport.Height / 2 - paddleHeight/2);
            ballPos = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);


            // create rectangles for each texture
            // player 1's paddle
            player1 = new Texture2D(GraphicsDevice, paddleWidth, paddleHeight);
            player1.SetData(paddleData);

            //player 2's paddle
            player2 = new Texture2D(GraphicsDevice, paddleWidth, paddleHeight);
            player2.SetData(paddleData);

            //ball
            ball = new Texture2D(GraphicsDevice, ballWidth, ballHeight);
            ball.SetData(ballData);

            //background
            background = new Texture2D(GraphicsDevice, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            background.SetData(blackColorData);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            base.UnloadContent();
            spriteBatch.Dispose();
            player1.Dispose();
            player2.Dispose();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            // paddle collision
            if (ballPos.X + ballWidth > player2Pos.X && ballPos.X + ballWidth < player2Pos.X + paddleHeight)
            {
                ballVelX = -ballVelX;
            }
            else if (ballPos.Y > player1Pos.Y && ballPos.Y < player1Pos.Y + paddleHeight && ballPos.X > player1Pos.X && ballPos.X < player1Pos.X + ballWidth)
            {
                ballVelX = -ballVelX;
            }

                // top and bottom wall collision
                if (ballPos.Y == 0 || ballPos.Y + ballHeight == GraphicsDevice.Viewport.Height)
            {
                ballVelY = -ballVelY;
            }

            //update the ball's position
            ballPos.X += ballVelX;
            ballPos.Y += ballVelY;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.Draw(background, new Vector2(), Color.White);
            spriteBatch.Draw(player1, player1Pos, Color.White);
            spriteBatch.Draw(player2, player2Pos, Color.White);
            spriteBatch.Draw(ball, ballPos, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
