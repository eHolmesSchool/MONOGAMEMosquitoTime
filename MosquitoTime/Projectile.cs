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
    public class Projectile : GameObject
    {

        float _velocityX;
        float _velocityY;
        float bounds;

        //projectileUpgradeState upgradeState;

        public Projectile(Sprite sprite, Transform transform, float velocityX, float velocityY) : base(sprite, transform)// Ask Angelo how Controls work
        {
            _transform = transform;
            _sprite = sprite;
            _velocityX = velocityX;
            _velocityY = velocityY;

            currentState = ObjectState.Dead; //Defaults to Inactive
        }

        public void Update(GameTime gameTime, Vector2 windowDimensions)
        {
            base.Update(gameTime); ///// We call the base GameObject update As Well as our Child Shenanigans /////

            switch (currentState)
            {
                case ObjectState.Alive:
                    _transform.TranslatePosition(new Vector2(_velocityX, _velocityY));
                    OutOfBoundsCheck(windowDimensions);
                    break;
                case ObjectState.Dead:
                    //Make It NOT Draw while dead and DO draw while alive       DONE
                    break;
                default:
                    break;
            }
        }

        private void OutOfBoundsCheck(Vector2 windowDimensions)
        {
            if(_sprite.Bounds.X <= 0 || _sprite.Bounds.Y <=0 || _sprite.Bounds.X + _sprite.Bounds.Width >= windowDimensions.X || _sprite.Bounds.Y + _sprite.Bounds.Height >= windowDimensions.Y)
            {
                this.currentState = ObjectState.Dead;
            }
        }

        public void Initialize()             //Initialize the projectiles in GameState.Init
        {
            currentState = ObjectState.Dead;
            _transform.Position = Vector2.Zero;
        }

        public void Activate(Vector2 Position)
        {
            _transform.Position = Position;
            currentState = ObjectState.Alive;
        }
    }
}