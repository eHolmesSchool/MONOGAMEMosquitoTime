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

        GameState currentGameState = GameState.Start;
        Level currentLevel = Level.Level1; ///            /////////                //////

        Transform playerTransform;
        Sprite playerSprite;
        Player playerObject;

        Transform enemyTransform;
        Sprite enemySprite;

        Sprite playerProjectileSprite;
        float playerProjectileVeloX;
        float playerProjectileVeloY;

        Sprite enemyProjectileSprite;
        float enemyProjectileVeloX;
        float enemyProjectileVeloY;
        float specialEnemyProjectileVeloX;
        float specialEnemyProjectileVeloY;

        public List<Projectile> PlayerProjectileList = new List<Projectile>(); //Remember, at GameState.Initialize, run the loop that fills this list
        int playerProjectileCount = 4;

        public List<Projectile> EnemyProjectileList = new List<Projectile>();
        int enemyProjectileCount = 4;

        public List<Enemy> EnemyList = new List<Enemy>();

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
            playerObject = new Player(playerSprite, playerTransform, PlayerProjectileList); /////

            enemySprite = new Sprite(enemyTexture, enemyTexture.Bounds, 1);
            enemyTransform = new Transform(new Vector2(20,20), Vector2.Zero, 0, 1);

            playerProjectileSprite = new Sprite(playerProjectileTexture, playerProjectileTexture.Bounds, 1);
            playerProjectileVeloX = 0f;// negative because these move upwards
            playerProjectileVeloY = -2f;

            enemyProjectileSprite = new Sprite(enemyProjectileTexture, enemyProjectileTexture.Bounds, 1);
            enemyProjectileVeloX = 0f; // positive because these move downwards
            enemyProjectileVeloY = 2f;
            specialEnemyProjectileVeloX = 1.5f; // Special enemies bullets move diagonally. May not use when I hand this in the first time but will use
            specialEnemyProjectileVeloY = 1.5f; // if I am able to hand this in again for bonus marks later
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
                    currentGameState = GameState.InitLevel;
                    break;
                case GameState.InitLevel:

                    for (int ProjectileIndex = 0; ProjectileIndex < playerProjectileCount; ProjectileIndex++) //Player Projectiles
                    {
                        PlayerProjectileList.Add(new Projectile(playerProjectileSprite, new Transform(Vector2.Zero, Vector2.Zero, 0f, 1f), playerProjectileVeloX, playerProjectileVeloY));
                    }

                    for (int ProjectileIndex = 0; ProjectileIndex < enemyProjectileCount; ProjectileIndex++) //Enemy Projectiles
                    {
                        EnemyProjectileList.Add(new Projectile(enemyProjectileSprite, new Transform(Vector2.Zero, Vector2.Zero, 0f, 1f), enemyProjectileVeloX, enemyProjectileVeloY));
                    }

                    for (int EnemyIndex = 0; EnemyIndex < enemyProjectileCount; EnemyIndex++) //Enemies
                    {
                        EnemyList.Add(new Enemy(enemySprite, enemyTransform));
                    }

                    currentGameState = GameState.Playing;
                    break;
                case GameState.Playing:

                    playerObject.Update(gameTime, PlayerProjectileList); /////We may only have to pass in the list once at the beginning, or we may have to do it every frame

                    
                    foreach(Enemy enemy in EnemyList)
                    {
                        enemy.Update(gameTime);
                    }

                    //foreach Player / Enemy Projectile
                    foreach (Projectile projectile in PlayerProjectileList)
                    {
                        projectile.Update(gameTime, new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight));
                    }

                    foreach (Projectile projectile in EnemyProjectileList)
                    {
                        projectile.Update(gameTime);
                    }


                    //If caps button pressed, go to pause
                    //If player is hit, go to Game Over
                    //If player kills all enemies, set current Level to 2 and go to Init level
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



        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            // TODO: Add your drawing code here


            switch (currentGameState)
            {
                case GameState.Start:
                    break;
                case GameState.InitLevel:
                    break;
                case GameState.Playing:

                    _spriteBatch.Draw(backgroundTexture, new Vector2(0, 0), Color.White);

                    playerObject.Draw(_spriteBatch);///////////////////////////////////

                    foreach (Enemy enemy in EnemyList)
                    {
                        if (enemy.currentState == Enemy.EnemyState.Alive)
                        {
                            enemy.Draw(_spriteBatch);
                        }
                    }

                    /*foreach Projetile in Enemy Projectiles
                     * if ProjectileStatus = Alive
                     * projectile.Draw()
                     */

                    foreach (Projectile playerProjectile in PlayerProjectileList)
                    {
                        if (playerProjectile.currentProjectileState == Projectile.ProjectileState.Alive)
                        {
                            playerProjectile.Draw(_spriteBatch);
                        }
                    }

                    break;
                case GameState.Paused:
                    break;
                case GameState.GameOver:
                    break;
                default:
                    break;
            }


            _spriteBatch.End();
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