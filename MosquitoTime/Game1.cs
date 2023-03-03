using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MosquitoTime
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        Texture2D playerCannonTexture;

        Player playerOne;
        Controls playerControls;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            playerControls = new Controls(Keyboard.GetState(0).IsKeyDown(Keys.Left) , Keyboard.GetState(0).IsKeyDown(Keys.Right), Keyboard.GetState(0).IsKeyDown(Keys.Space));
            base.Initialize();

            playerCannonTexture = Content.Load<Texture2D>("Cannon");

            playerOne = new Player(playerCannonTexture, new Vector2(50,50), playerControls);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}