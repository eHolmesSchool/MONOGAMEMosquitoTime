using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MosquitoTime
{
    public class Player: GameObject
    {
        Texture2D playerTexture; //covered by GameObject
        Rectangle playerRectangle; //covered by GameObject
        Vector2 playerPosition; //covered by GameObject
        Vector2 playerDirection; //covered by GameObject


        Controls playerControls = new Controls();
        //Projectile playerBullet;
        float velocity; //shouldve been covered by GameObject but we did movement in Player this time

        float rightmostWall = 600; 

        KeyboardState kbs;
        KeyboardState prevKbs; //used for Single Fire 

        int currentPlayerHealth;
        int maxPlayerHealth;
        float fireRate;
        int currentPlayerAmmo;
        int maxPlayerAmmo;

        //PlayerUpgradeState upgradeState;
        PlayerState currentplayerState = PlayerState.Alive;


        public Player(Sprite sprite, Transform transform) : base(sprite, transform)  // Ask Angelo how Controls work SOLVED ON MY OWN HELL YEAH
        {
            _sprite = sprite;
            _transform = transform;

            velocity = 4f;

            playerControls = new Controls(Keys.D, Keys.A, Keys.W); //no multiplayer (Right, Left, Fire)
        }

        public new void Update(GameTime gameTime)
        {
            base.Update(gameTime); ///////////

            kbs = Keyboard.GetState(); 

            switch (currentplayerState)
            {
                case PlayerState.Alive:
                    PlayerMove();
                    PlayerFire();
                    break;
                case PlayerState.Hit:
                    break;
                case PlayerState.Dying:
                    break;
                case PlayerState.Dead:
                    break;
                default:
                    break;
            }
        } 
        
        /*
         * 
         * 
         * 
         *
         *
         *
         */


        public void PlayerMove()
        {
            if (kbs.IsKeyDown(Keys.D))
            {//move right
                if (_sprite.Bounds.X + _sprite.Bounds.Width  !<= rightmostWall)
                {
                    _transform.TranslatePosition(new Vector2(velocity, 0)); //////////////////Figuring out how to make it move          FIGURED OUT
                }
            } 
            if (kbs.IsKeyDown(playerControls.negativeDirection))
            {//move left
                if (_transform.Position.X - velocity !>= 0)
                {
                    _transform.TranslatePosition(new Vector2(-velocity, 0));
                }
            }
        }

        public void PlayerFire()
        {
            if (kbs.IsKeyDown(playerControls.fireGunButton))
            {//Instantiate a moving Bullet
                //Google this later
            }
        }
    }

    public struct Controls
    {
        public Keys positiveDirection;
        public Keys negativeDirection;
        public Keys fireGunButton;

        public Controls(Keys posEnum, Keys negEnum, Keys fire)
        {
            this.positiveDirection = posEnum;
            this.negativeDirection = negEnum;
            this.fireGunButton = fire;
        }
    }

    public enum PlayerUpgradeState
    {
        None = 0,
    }

    public enum PlayerState
    {
        Alive,
        Hit,
        Dying,
        Dead
    }
}