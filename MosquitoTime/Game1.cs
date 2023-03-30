using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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
        private Texture2D barrierTexture;

        GameState currentGameState = GameState.Start;
        Level currentLevel = Level.None; ///            /////////                //////

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

        Transform barrierTransform;
        Sprite barrierSprite;


        public List<Projectile> PlayerProjectileList = new List<Projectile>(); //Remember, at GameState.Initialize, run the loop that fills this list
        int playerProjectileCount = 4;

        public List<Projectile> EnemyProjectileList = new List<Projectile>();
        int enemyProjectileCount = 6;

        public List<Enemy> EnemyList = new List<Enemy>();
        int enemyCount = 10;

        public List<Barrier> BarrierList = new List<Barrier>();
        int barrierCount = 2;


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

            barrierSprite = new Sprite(barrierTexture, barrierTexture.Bounds, 1);
            barrierTransform = new Transform(new Vector2(75, 350), Vector2.Zero, 0, 1);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            playerCannonTexture = Content.Load<Texture2D>("Cannon");
            backgroundTexture = Content.Load<Texture2D>("Background");
            enemyTexture = Content.Load<Texture2D>("Mosquito");
            playerProjectileTexture = Content.Load<Texture2D>("CannonBall");
            enemyProjectileTexture = Content.Load<Texture2D>("Fireball");
            barrierTexture = Content.Load<Texture2D>("pile-of-bricks");
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

                    InitAllLists();

                    currentGameState = GameState.Playing;
                    break;
                case GameState.Playing:

                    playerObject.Update(gameTime); /////We only have to pass in the list once at the beginning

                    AllTheUpdates(gameTime);


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

        private void InitAllLists()
        {
            //add nested switch case that takes in Level1, Level2 etc.
            for (int projectileIndex = 0; projectileIndex < playerProjectileCount; projectileIndex++) //Player Projectiles
            {
                PlayerProjectileList.Add(new Projectile(playerProjectileSprite, new Transform(Vector2.Zero, Vector2.Zero, 0f, 1f), playerProjectileVeloX, playerProjectileVeloY));
            }

            for (int projectileIndex = 0; projectileIndex < enemyProjectileCount; projectileIndex++) //Enemy Projectiles
            {
                EnemyProjectileList.Add(new Projectile(enemyProjectileSprite, new Transform(Vector2.Zero, Vector2.Zero, 0f, 1f), enemyProjectileVeloX, enemyProjectileVeloY));
            }

            for (int enemyIndex = 0; enemyIndex < enemyCount; enemyIndex++) //Enemies
            {
                EnemyList.Add(new Enemy(enemySprite, new Transform(new Vector2(enemyTransform.Position.X + (enemyIndex * 30), enemyTransform.Position.Y + (enemyIndex * 30)), Vector2.Zero, 0f, 1f)));
            }

            for (int barrierIndex = 0; barrierIndex < barrierCount; barrierIndex++)
            {
                BarrierList.Add(new Barrier(barrierSprite, new Transform(new Vector2(barrierTransform.Position.X + (barrierIndex * 300), barrierTransform.Position.Y), Vector2.Zero, 0f, 1f)));
            }
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

                    AllTheDrawing(_spriteBatch);

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


        private void AllTheDrawing(SpriteBatch spriteBatch)
        {
            playerObject.Draw(spriteBatch);///////////////////////////////////

            foreach (Enemy enemy in EnemyList)
            {
                if (enemy.currentState == Enemy.EnemyState.Alive)
                {
                    enemy.Draw(spriteBatch);
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
                    playerProjectile.Draw(spriteBatch);
                }
            }

            foreach (Barrier bar in BarrierList)
            {
                bar.Draw(spriteBatch);
            }
        }

        private void AllTheUpdates(GameTime gameTime)
        {
            foreach (Enemy enemy in EnemyList)
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

            foreach (Barrier bar in BarrierList)
            {
                bar.Update(gameTime);
            }
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
            None,
            Level1,
            Level2,
        }

    }
}