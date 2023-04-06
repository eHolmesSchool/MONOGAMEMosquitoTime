using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MosquitoTime.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MosquitoTime
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SpriteFont arial;

        private Texture2D backgroundTexture;
        private Texture2D playerCannonTexture;
        private Texture2D playerProjectileTexture;
        private Texture2D enemyTexture;
        private Texture2D enemyProjectileTexture;
        private Texture2D barrierTexture;

        GameState currentGameState = GameState.Start;
        Level currentLevel = Level.None;/////     /////     /////     /////     /////     /////

        Transform playerTransform;
        Sprite playerSprite;
        Controls playerControls;
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

        //All the Texts are Here. Not gonna organize these. Just letting them be the Ugly Ducklings of my code rn
        Text.Transform playerLifeCounterTransform;
        string playerLifeCounterString;
        Vector2 playerLifeCounterOffset = new Vector2(45, 30);


        public Text playerLifeCounter;

        public List<Projectile> PlayerProjectileList = new List<Projectile>(); //Remember, at GameState.Initialize, run the loop that fills this list
        int playerProjectileCount = 4;

        public List<Projectile> EnemyProjectileList = new List<Projectile>();
        int enemyProjectileCount = 5;

        public List<Enemy> EnemyList = new List<Enemy>();
        int enemyCount = 5;

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

            //LEVEL SWITCH STATEMENT HERE 

            _graphics.PreferredBackBufferWidth = 600;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();

            playerSprite = new Sprite(playerCannonTexture, playerCannonTexture.Bounds, 1);
            playerTransform = new Transform(new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight - playerSprite.Bounds.Height - 26), Vector2.Zero, 0, 1);
            playerControls = new Controls(Keys.D, Keys.A, Keys.W);
            playerObject = new Player(playerSprite, playerTransform, PlayerProjectileList, playerControls); ///// This is here because we Dont have a list of Players


            enemySprite = new Sprite(enemyTexture, enemyTexture.Bounds, 1);
            enemyTransform = new Transform(new Vector2(20, 20), Vector2.Zero, 0, 1); //change starting pos of enemies in diff levels

            playerProjectileSprite = new Sprite(playerProjectileTexture, playerProjectileTexture.Bounds, 1);
            playerProjectileVeloX = 0f; 
            playerProjectileVeloY = -4f; // negative because these move upwards

            enemyProjectileSprite = new Sprite(enemyProjectileTexture, enemyProjectileTexture.Bounds, 1);
            enemyProjectileVeloX = 0f; 
            enemyProjectileVeloY = 2f; // positive because these move downwards
            specialEnemyProjectileVeloX = 1.5f; // Special enemies bullets move diagonally. May not use when I first hand in
            specialEnemyProjectileVeloY = 1.5f;

            barrierSprite = new Sprite(barrierTexture, barrierTexture.Bounds, 1);
            barrierTransform = new Transform(new Vector2(75, 350), Vector2.Zero, 0, 1);

            playerLifeCounterTransform = new Text.Transform(new Vector2(75, 30), Vector2.Zero, 0, 1);
            playerLifeCounterString = playerObject.currentPlayerHealth.ToString();
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
            arial = Content.Load<SpriteFont>("Arial");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            { Exit(); }

            switch (currentGameState)
            {
                case GameState.Start:
                    //Display Starter screen



                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {



                        currentGameState = GameState.InitLevel;
                        currentLevel = Level.Level1;
                        //Check if CLICKABLE Pressed
                    }

                    //Wait until correct player Input
                    
                    
                    break;
                case GameState.InitLevel:
                    //ADD SWITCH STATEMENT that covers each of the 2 levels
                    switch (currentLevel)
                    {
                        //InitAll(playerProjectileNumb, enemyProjectileNumb, enemyNumb, enemySpacing(vector2), barrierNumb, barrierSpacing(Vector2))
                        case Level.Level1:

                            InitAll(playerProjectileCount, enemyProjectileCount, enemyCount, new Vector2(30, 30), barrierCount, new Vector2(300, 0));

                            break;

                        case Level.Level2:

                            break;

                        default:
                            InitAll(playerProjectileCount, enemyProjectileCount, enemyCount, new Vector2(30, 30), barrierCount, new Vector2(300, 0));

                            break;
                    }

                    AddListsOfCollidableObjectsToEachObject();

                    currentGameState = GameState.Playing;
                    break;
                case GameState.Playing:

                    playerObject.Update(gameTime);

                    AllTheUpdates(gameTime);
                    AllTheCollisionChecks();

                    //If caps button pressed, go to paused
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

                    StartMenuDrawing(_spriteBatch);

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








        private void InitAll(int playerProjectileCount, int enemyProjectileCount, int enemyCount, Vector2 enemySpacing, int barrierCount, Vector2 barrierSpacing)
        {
            playerLifeCounter = new Text(arial, playerLifeCounterString, playerLifeCounterTransform);

            //Case Level1 InitAll(playerLife, playerProjectileNumb, enemyProjectileNumb, enemyNumb, enemySpacing(vector2), barrierNumb, barrierSpacing(Vector2))

            //add nested switch case that takes in Level1, Level2 etc. COVERED IN THE VARIABLES GIVEN IN AN EARLIER SWITCH
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
                EnemyList.Add(new Enemy(enemySprite, new Transform(enemyTransform.Position + (enemySpacing*enemyIndex), Vector2.Zero, 0f, 1f), EnemyProjectileList));
            }

            for (int barrierIndex = 0; barrierIndex < barrierCount; barrierIndex++)
            {
                BarrierList.Add(new Barrier(barrierSprite, new Transform(barrierTransform.Position + (barrierSpacing*barrierIndex) , Vector2.Zero, 0f, 1f)));
            }
        }

        private void AddListsOfCollidableObjectsToEachObject()
        {
            //add nested switch case that takes in Level1, Level2 etc.

            foreach (GameObject obj in EnemyProjectileList)
            {
                playerObject._collidableObjects.Add(obj);
            }

            for (int listIndex = 0; listIndex < PlayerProjectileList.Count; listIndex++)
            {//Player Projectiles have a reaction when they collide with Enemies and Barriers. 
                foreach (GameObject obj in EnemyList)
                {
                    PlayerProjectileList[listIndex]._collidableObjects.Add(obj);
                }
                foreach (GameObject obj in BarrierList)
                {
                    PlayerProjectileList[listIndex]._collidableObjects.Add(obj);
                }
            }

            for (int listIndex = 0; listIndex < EnemyProjectileList.Count; listIndex++)
            {//Enemy Projectiles have a reaction when they collide with the Player and Barriers

                EnemyProjectileList[listIndex]._collidableObjects.Add(playerObject);
                foreach (GameObject obj in BarrierList)
                {
                    EnemyProjectileList[listIndex]._collidableObjects.Add(obj);
                }
            }

            for (int listIndex = 0; listIndex < EnemyList.Count; listIndex++)
            {//Enemies have a reaction when they collide with the Player Projectiles
                foreach (GameObject obj in PlayerProjectileList)
                {
                    EnemyList[listIndex]._collidableObjects.Add(obj);
                }
            }

            for (int listIndex = 0; listIndex < BarrierList.Count; listIndex++)
            {//Barriers have no reaction to any collision for now so no code is needed here
                //foreach (GameObject obj in  ...List)
                //{
                //    BarrierList[listIndex]._collidableObjects.Add(obj);
                //}
            }
        }

        private void AllTheCollisionChecks()
        {
            List<GameObject> AllList = new List<GameObject>();

            AllList.Add(playerObject);
            AllList.AddRange(EnemyList);
            AllList.AddRange(EnemyProjectileList);
            AllList.AddRange(PlayerProjectileList);


            foreach (GameObject OGobj in AllList)
            {
                foreach (GameObject collisionCheckedObj in OGobj._collidableObjects)
                {
                    if (OGobj.currentState == GameObject.ObjectState.Alive)
                    {
                        if (OGobj.OnCollide(collisionCheckedObj._sprite.Bounds))
                        {
                            OGobj.Collision();
                        }
                    }
                }
            }
        }

        private void AllTheDrawing(SpriteBatch spriteBatch)
        {

            if (playerObject.currentState == GameObject.ObjectState.Alive)
            {
                playerObject.Draw(spriteBatch);///////////////////////////////////
            }
            //Drawing playerLifeCounter on near the player, offset by a set amount
            spriteBatch.DrawString(playerLifeCounter._font, playerLifeCounter._text, playerObject._transform.Position + playerLifeCounterOffset, Color.Black);

            foreach (Enemy enemy in EnemyList)
            {
                if (enemy.currentState == Enemy.ObjectState.Alive)
                {
                    enemy.Draw(spriteBatch);
                }
            }

            foreach (Projectile enemyProjectiles in EnemyProjectileList)
            {
                if (enemyProjectiles.currentState == GameObject.ObjectState.Alive)
                {
                    enemyProjectiles.Draw(spriteBatch);
                }
            }

            foreach (Projectile playerProjectile in PlayerProjectileList)
            {
                if (playerProjectile.currentState == GameObject.ObjectState.Alive)
                {
                    playerProjectile.Draw(spriteBatch);
                }
            }

            foreach (Barrier bar in BarrierList)
            {
                bar.Draw(spriteBatch);
            }
        }


        private void StartMenuDrawing(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(arial, "GAMEING", new Vector2(100, 100), Color.Black);
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
                projectile.Update(gameTime, new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight));
            } //You HAVE to put in the extra Vector2 Parameter into your projectiles, even if you dont use it
              //Otherwise, it will use the Base GameObject Update since the parameters match up.
              //Thus, we will never run the TranslatePosition part of the Projectiles Update function.

            foreach (Barrier bar in BarrierList)
            {
                bar.Update(gameTime);
            }

            //foreach (Text text in TextList)
            playerLifeCounter._text = playerObject.currentPlayerHealth.ToString();
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