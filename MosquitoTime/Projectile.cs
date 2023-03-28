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
    public class Projectile: GameObject
    {
        Texture2D projectileTexture;
        Rectangle projectileRectangle;
        
        Vector2 projectilePosition;
        Vector2 projectileDirection;
        float velocityX;
        float velocityY;
        float bounds;

        //projectileUpgradeState upgradeState;
        ProjectileState currentprojectileState = ProjectileState.Alive;

        public Projectile(Sprite sprite, Transform transform) : base(sprite, transform)// Ask Angelo how Controls work
        {
            _transform = transform;
            _sprite = sprite;


        }

        public new void Update(GameTime gameTime)
        {
            base.Update(gameTime); ///////////

            switch (currentprojectileState)
            {
                case ProjectileState.Alive:
                    ProjectileMove();
                    break;
                case ProjectileState.Dead:
                    //ASK ANGELO HOW TO GET AN OBJECT TO REMOVE ITSELF IN MONOGAME
                    break;
                default:
                    break;
            }
        }

        public new void ProjectileMove()
        {
            _transform.TranslatePosition(new Vector2(velocityX, velocityY)); 
        }
        //_transform.TranslatePosition(new Vector2(velocity, 0)); //////////////////Figuring out how to make it move          FIGURED OUT
    }

    public enum ProjectileState
    {
        None,
        Alive,
        Dead,
    }
}