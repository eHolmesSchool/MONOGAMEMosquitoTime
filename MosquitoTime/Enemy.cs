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
    public class Enemy : GameObject
    {
        //Projectile enemyBullet;

        float velocityX = 4f;
        float velocityY = 4f;

        float rightmostWall = 600;

        //EnemyUpgradeState currentEnemyUpgradeState = EnemyUpgradeState.None;
        EnemyMovementState currentMovementState = EnemyMovementState.Right;

        public Enemy(Sprite sprite, Transform transform) : base(sprite, transform) 
        {
            _sprite = sprite;
            _transform = transform;

            currentState = ObjectState.Alive;
        }


        public new void Update(GameTime gameTime)
        {
            base.Update(gameTime); ///////////


            switch (currentState)
            {
                case ObjectState.Alive:
                    EnemyMove();
                    EnemyFire();
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
            else if (currentMovementState == EnemyMovementState.Left)
            {
                if (_sprite.Bounds.X! >= 0)
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
