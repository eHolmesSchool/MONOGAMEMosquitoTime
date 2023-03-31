﻿using System;
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
        public int maxPlayerHealth;
        public int currentPlayerHealth;   
        float fireRate;             //Not used in this version

        Controls playerControls = new Controls();
        float velocity; //shouldve been covered by GameObject but we did movement in Player this time

        float rightmostWall = 600;

        KeyboardState kbs;
        KeyboardState prevKbs; //used to fire a Single bullet per keypress

        List<Projectile> _playerBullets;
        int maxPlayerAmmo;          //Not used in this version
        int currentAmmo;            //Not used in this version

        //PlayerUpgradeState upgradeState;

        public Player(Sprite sprite, Transform transform, List<Projectile> playerBullets) : base(sprite, transform)  // Ask Angelo how Controls work SOLVED ON MY OWN HELL YEAH
        {
            _sprite = sprite;
            _transform = transform;
            _playerBullets = playerBullets;
            
            ///// move everything below this line into an Initialize() function called by the constructor and the Init gamestate
            velocity = 4f;

            currentState = ObjectState.Alive;
            playerControls = new Controls(Keys.D, Keys.A, Keys.W); //no multiplayer so just standard controls (Right, Left, Fire)

            maxPlayerHealth = 3;
            currentPlayerHealth = maxPlayerHealth;
            /////
        }

        public new void Update(GameTime gameTime)//"new" keyword required since this and Base.Update have identical parameters
        {
            base.Update(gameTime); ///////////

            kbs = Keyboard.GetState();

            switch (currentState)
            {
                case ObjectState.Alive:
                    PlayerMove();
                    PlayerFire();
                    break;
                case ObjectState.Hit:
                    break;
                case ObjectState.Dying:
                    break;
                case ObjectState.Dead:
                    break;
                default:
                    break;
            }

            prevKbs = kbs;
        }

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
                    if (bullet.currentState == ObjectState.Dead)
                    {
                        bullet.Activate(new Vector2(_sprite.Bounds.X + (_sprite.Bounds.Width / 2) - 2, _transform.Position.Y));
                        break;
                    }
                }
            }
        }

        public override void Collision()
        {
            
            currentPlayerHealth--;
            Debug.WriteLine(currentPlayerHealth);
            if (currentPlayerHealth <= 0)
            {
                currentState = ObjectState.Dead;
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
}