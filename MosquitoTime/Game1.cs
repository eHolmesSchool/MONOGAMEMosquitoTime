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

        Point clickableRectStart;
        Point clickableRectEnd;
        Clickable StartButton;

        Clickable GameOverButton;



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


            clickableRectStart = new Point((_graphics.PreferredBackBufferWidth / 2), (_graphics.PreferredBackBufferHeight / 2));
            clickableRectEnd = new Point(73,35);
            StartButton = new Clickable(new Rectangle(clickableRectStart, clickableRectEnd), $"Click Here\nTo Start");

            GameOverButton = new Clickable(new Rectangle(new Point(clickableRectStart.X - 100, clickableRectStart.Y - 100), clickableRectEnd), $"Go Back\nTo Menu");
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
                    //Display Starter screen  DONE
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        if (StartButton.WasClicked(new Vector2(Mouse.GetState().X, Mouse.GetState().Y)))
                        {
                            currentGameState = GameState.InitLevel;
                            currentLevel = Level.Level1;
                        }
                    }
                    break;
                case GameState.InitLevel:
                    UnInitAll();
                    switch (currentLevel)
                    {
                        //InitAll(playerProjectileNumb, enemyProjectileNumb, enemyNumb, enemySpacing(vector2), barrierNumb, barrierSpacing(Vector2))
                        case Level.Level1:
                            playerObject.currentPlayerHealth = playerObject.maxPlayerHealth;

                            playerProjectileCount = 4;
                            enemyProjectileCount = 5;
                            enemyCount = 5;
                            barrierCount = 2;

                            InitAll(playerProjectileCount, enemyProjectileCount, enemyCount, new Vector2(30, 30), barrierCount, new Vector2(300, 0));

                            break;

                        case Level.Level2:
                            playerObject.currentPlayerHealth = playerObject.maxPlayerHealth;

                            playerProjectileCount = 2;
                            enemyProjectileCount = 10;
                            enemyCount = 10;
                            barrierCount = 0;

                            InitAll(playerProjectileCount, enemyProjectileCount, enemyCount, new Vector2(-15, -15), barrierCount, new Vector2(0, 0));

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

                    if (playerObject.currentPlayerHealth <= 0)
                    {
                        currentLevel = Level.Level1;
                        currentGameState = GameState.GameOver;
                    }
                    if (AliveEnemyCount(EnemyList)<=0)
                    {
                            Debug.Write("WAWAWEEWA ");

                    //    if (currentLevel == Level.Level1)
                    //    {
                    //        currentLevel = Level.Level2;
                    //        currentGameState = GameState.InitLevel;
                    //    } else if (currentLevel == Level.Level2)
                    //    {
                    //        currentLevel = Level.Level1;
                    //        currentGameState = GameState.Start;
                    //    }
                    }

                        break;
                case GameState.Paused:
                    break;
                case GameState.GameOver:
                    playerObject.currentPlayerHealth = playerObject.maxPlayerHealth;
                    if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        if (GameOverButton.WasClicked(new Vector2(Mouse.GetState().X, Mouse.GetState().Y)))
                        {
                            currentGameState = GameState.Start;
                            currentLevel = Level.Level1;
                        }
                    }
                    break;
                default:
                    break;
            }

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        private int AliveEnemyCount(List<Enemy> enemylist )
        {
            int Count = 0;
            
            foreach (Enemy enemy in enemylist)
            {
                Debug.WriteLine(enemy.currentState);
                if (enemy.currentState == GameObject.ObjectState.Dead)
                {
                    Count++;
                }
            }
            return Count;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            // TODO: Add your drawing code here
            switch (currentGameState)
            {
                case GameState.Start:

                    StartMenuDrawing(_spriteBatch, StartButton);/////     /////

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

                    StartGameOverDrawing(_spriteBatch, GameOverButton);

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
            playerObject.PlayerInitializeDefaults();
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

        private void UnInitAll()
        {

            PlayerProjectileList.Clear();
            EnemyProjectileList.Clear();
            EnemyList.Clear();
            BarrierList.Clear();
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


        private void StartMenuDrawing(SpriteBatch spriteBatch, Clickable StartButton)
        {
            spriteBatch.DrawString(arial, "BUG GAMEING", new Vector2(100, 100), Color.Black);
            StartButton.Draw(spriteBatch, arial);
        }

        private void StartGameOverDrawing(SpriteBatch spriteBatch, Clickable gameOverButton)
        {
            spriteBatch.DrawString(arial, "YOU LOST HAHA", new Vector2(400, 100), Color.Black);
            gameOverButton.Draw(spriteBatch, arial);
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