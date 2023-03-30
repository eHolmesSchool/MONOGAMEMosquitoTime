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
    public class Player : GameObject
    {
        int currentPlayerHealth; //Not doing in this project
        int maxPlayerHealth; //Not doing in this project
        float fireRate; //Not doing in this project


        Controls playerControls = new Controls();
        float velocity; //shouldve been covered by GameObject but we did movement in Player this time

        float rightmostWall = 600;

        KeyboardState kbs;
        KeyboardState prevKbs; //used to fire a Single bullet per keypress


        //        int currentPlayerAmmo; 
        List<Projectile> _playerBullets;
        int maxPlayerAmmo;

        //PlayerUpgradeState upgradeState;
        PlayerState currentplayerState = PlayerState.Alive;


        public Player(Sprite sprite, Transform transform, List<Projectile> playerBullets) : base(sprite, transform)  // Ask Angelo how Controls work SOLVED ON MY OWN HELL YEAH
        {
            _sprite = sprite;
            _transform = transform;
            _playerBullets = playerBullets;


            velocity = 4f;

            playerControls = new Controls(Keys.D, Keys.A, Keys.W); //no multiplayer (Right, Left, Fire)
        }

        public new void Update(GameTime gameTime, List<Projectile> playerBullets)
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

            prevKbs = Keyboard.GetState();
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
                if (_sprite.Bounds.X + _sprite.Bounds.Width! <= rightmostWall)
                {
                    _transform.TranslatePosition(new Vector2(velocity, 0)); //////////////////Figuring out how to make it move          FIGURED OUT
                }
            }
            if (kbs.IsKeyDown(playerControls.negativeDirection))
            {//move left
                if (_transform.Position.X - velocity! >= 0)
                {
                    _transform.TranslatePosition(new Vector2(-velocity, 0));
                }
            }
        }

        public void PlayerFire()
        {
            if (kbs.IsKeyDown(playerControls.fireGunButton) && !prevKbs.IsKeyDown(playerControls.fireGunButton))
            {//Instantiate a moving Bullet
                foreach (Projectile bullet in _playerBullets)
                {
                    if (bullet.currentProjectileState == Projectile.ProjectileState.Dead)
                    {
                        bullet.Activate(new Vector2(_sprite.Bounds.X + (_sprite.Bounds.Width / 2) - 2, _transform.Position.Y));
                        break;
                    }
                }
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