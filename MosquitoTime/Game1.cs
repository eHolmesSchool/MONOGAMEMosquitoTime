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

            testTransform = new Transform(new Vector2(30, 10), Vector2.Zero, 0, 1);
            testSprite = new Sprite(playerCannonTexture, playerCannonTexture.Bounds, 1);
            testObject = new Player(testSprite, testTransform);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            playerCannonTexture = Content.Load<Texture2D>("Cannon");
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

            testObject.Draw(_spriteBatch);///////////////////////////////////

            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}