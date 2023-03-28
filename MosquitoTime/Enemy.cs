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
    public class Enemy: GameObject
    {

        Texture2D playerTexture; //covered by GameObject
        Rectangle playerRectangle; //covered by GameObject
        Vector2 playerPosition; //covered by GameObject
        Vector2 playerDirection; //covered by GameObject


        //Projectile enemyBullet;

        float velocityX = 4f;
        float velocityY = 4f;

        float rightmostWall = 600;

        int currentPlayerHealth;
        int maxPlayerHealth;
        float fireRate;
        int currentPlayerAmmo;
        int maxPlayerAmmo;


        /// //////////////////////////////////////////////EnemyUpgradeState currentEnemyUpgradeState = EnemyUpgradeState.None;
        EnemyState currentState = EnemyState.Alive;
        EnemyMovementState currentMovementState = EnemyMovementState.Right;

        public Enemy(Sprite sprite, Transform transform) : base(sprite, transform)  // Ask Angelo how Controls work SOLVED ON MY OWN HELL YEAH
        {
            _sprite = sprite;
            _transform = transform;
        }


        public new void Update(GameTime gameTime)
        {
            base.Update(gameTime); ///////////


            switch (currentState)
            {
                case EnemyState.Alive:
                    EnemyMove();
                    EnemyFire();
                    break;
                case EnemyState.Hit:
                    break;
                case EnemyState.Dying:
                    break;
                case EnemyState.Dead:
                    break;
                default:
                    break;
            }
        }



        public void EnemyMove()
        {
            if (currentMovementState == EnemyMovementState.Right)
            {
                if (_sprite.Bounds.X + _sprite.Bounds.Width! <= rightmostWall)
                {
                    _transform.TranslatePosition(new Vector2(velocityX, 0)); 
                }
                else
                {
                    currentMovementState = EnemyMovementState.Left;
                }
            }
            else if(currentMovementState == EnemyMovementState.Left)
            {
                if (_sprite.Bounds.X + _sprite.Bounds.Width! >= 0)
                {
                    _transform.TranslatePosition(new Vector2(-velocityX, 0));
                }
                else
                {
                    currentMovementState = EnemyMovementState.Right;
                }
            }
            else
            {
                //dont move
            }
        }
        public void EnemyFire()
        {
            //Instantiate an Enemy Projectile
        }












        public enum EnemyState
        {
            None,
            Alive,
            Hit,
            Dying,
            Dead,
        }

        public enum EnemyUpgradeState
        {//0, 1, 2, 4, 8, 16   this is done so we can have the ability to represent combinations of states as additions of multiple numbers together
            None = 0, //Never will we have a situation where we add a number more than once. So any derivitave number (ex.7) can only be accomplished one way (7 = 1 + 2 + 4)
        }

        public enum EnemyMovementState
        {
            None,
            Left,
            Right,
        }
    }
}
