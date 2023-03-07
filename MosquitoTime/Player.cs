using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MosquitoTime
{

    public class Player
    {

        Texture2D playerTexture;
        Rectangle playerRectangle;
        Controls playerControls;
        //Bullet playerBullet;
        Vector2 playerPosition;
        Vector2 playerDirection;
        float velocity;
        float bounds;

        int currentPlayerHealth;
        int maxPlayerHealth;
        float fireRate;
        int currentPlayerAmmo;
        int maxPlayerAmmo;

        //PlayerUpgradeState upgradeState;
        PlayerState currentplayerState = PlayerState.Alive;



        public Player(Texture2D texture, Vector2 position, Controls controls /*Bullet */)
        {
            this.playerTexture = texture;
            this.playerPosition = position;
            this.playerControls = controls;

            playerRectangle = this.playerTexture.Bounds;
        }

        public void Update(GameTime gameTime)
        {
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

        public void PlayerMove()
        {
            if (playerControls.positiveDirection)
            {
                //move right
            } 
            if (playerControls.negativeDirection)
            {
                //move left
            }
        }

        public void PlayerFire()
        {
            if (playerControls.wasFirePressedThisFrame)
            {
                //fire Bullet
            }
        }



    }

    public struct Controls
    {
        public Controls(bool pos, bool neg, bool fire)
        {
            this.positiveDirection = pos;
            this.negativeDirection = neg;
            this.wasFirePressedThisFrame = fire;
        }
        public bool positiveDirection;
        public bool negativeDirection;
        public bool wasFirePressedThisFrame;
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