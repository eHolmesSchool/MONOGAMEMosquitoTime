using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MosquitoTime
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D backgroundTexture;
        private Texture2D playerCannonTexture;
        private Texture2D playerProjectileTexture;
        private Texture2D enemyTexture;
        private Texture2D enemyProjectileTexture;

        GameState currentGameState = GameState.Playing;
        Level currentLevel = Level.Level1;

        Transform playerTransform;
        Sprite playerSprite;
        Player playerObject;

        Transform enemyTransform;
        Sprite enemySprite;
        Enemy enemyObject;

        Sprite playerProjectileSprite;

        Sprite enemyProjectileSprite;


        List<Projectile> PlayerBulletList = new List<Projectile>(); //Remember, at GameStateInitialize, run the loop that fills this list
        int playerBulletCount = 4;

        List<Projectile> EnemyBulletList = new List<Projectile>();
        int enemyBulletCount = 4;

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

            playerSprite = new Sprite(playerCannonTexture, playerCannonTexture.Bounds, 1);
            playerTransform = new Transform(new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight - playerSprite.Bounds.Height-26), Vector2.Zero, 0, 1);
            playerObject = new Player(playerSprite, playerTransform);

            enemySprite = new Sprite(enemyTexture, enemyTexture.Bounds, 1);
            enemyTransform = new Transform(new Vector2(20,20), Vector2.Zero, 0, 1);
            enemyObject = new Enemy(enemySprite, enemyTransform);


            for (int bulletIndex = 0 ; bulletIndex < playerBulletCount; bulletIndex++) //Player Bullets
            {
                PlayerBulletList.Add(new Projectile(playerProjectileSprite, new Transform(Vector2.Zero, Vector2.Zero, 0f, 1f)));
            }

            for (int bulletIndex = 0; bulletIndex < enemyBulletCount; bulletIndex++) //Enemy Bullets
            {
                EnemyBulletList.Add(new Projectile(enemyProjectileSprite, new Transform(Vector2.Zero, Vector2.Zero, 0f, 1f)));
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            playerCannonTexture = Content.Load<Texture2D>("Cannon");
            backgroundTexture = Content.Load<Texture2D>("Background");
            enemyTexture = Content.Load<Texture2D>("Mosquito");
            playerProjectileTexture = Content.Load<Texture2D>("CannonBall");
            enemyProjectileTexture = Content.Load<Texture2D>("Fireball");
            // TODO: use this.Content to load your game content here
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {Exit();}


            switch (currentGameState)
            {
                case GameState.Start:
                    break;
                case GameState.InitLevel:
                    break;
                case GameState.Playing:
                    break;
                case GameState.Paused:
                    break;
                case GameState.GameOver:
                    break;
                default:
                    break;
            }


            // TODO: Add your update logic here
            base.Update(gameTime);


            playerObject.Update(gameTime);

            //foreach Enemy in Enemy List
            enemyObject.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            _spriteBatch.Draw(backgroundTexture, new Vector2(0,0), Color.White);


            playerObject.Draw(_spriteBatch);///////////////////////////////////

            enemyObject.Draw(_spriteBatch);

            /*foreach Projetile in Enemy Projectiles
             * if ProjectileStatus = Alive
             * projectile.Draw()
             */
            /*foreach Projetile in Player Projectiles
            * if ProjectileStatus = Alive
            * projectile.Draw()
            */


            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }


        public enum GameState
        {
            Start,
            InitLevel,
            Playing,
            Paused,
            GameOver,
        }

        public enum Level
        {
            Level1,
            Level2,
        }

    }
}