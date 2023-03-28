using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MosquitoTime
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D playerCannonTexture;
        private Texture2D backgroundTexture;

        Player testObject;
        Transform testTransform;
        Sprite testSprite;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {// TODO: Add your initialization logic here
            base.Initialize(); //Runs the LoadContent() function       

            _graphics.PreferredBackBufferWidth = 600;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();

           
            testSprite = new Sprite(playerCannonTexture, playerCannonTexture.Bounds, 1);
            testTransform = new Transform(new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight - testSprite.Bounds.Height-26), Vector2.Zero, 0, 1);
            testObject = new Player(testSprite, testTransform);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            playerCannonTexture = Content.Load<Texture2D>("Cannon");
            backgroundTexture = Content.Load<Texture2D>("Background");

            // TODO: use this.Content to load your game content here
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {Exit();}


            // TODO: Add your update logic here
            base.Update(gameTime);


            testObject.Update(gameTime);

        }



        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            _spriteBatch.Draw(backgroundTexture, new Vector2(0,0), Color.White);


            testObject.Draw(_spriteBatch);///////////////////////////////////

            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}