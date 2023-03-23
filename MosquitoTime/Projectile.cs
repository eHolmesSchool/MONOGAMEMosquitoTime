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
        Texture2D projectileTexture;
        Rectangle projectileRectangle;
        Controls projectileControls;
        //Bullet projectileBullet;
        Vector2 projectilePosition;
        Vector2 projectileDirection;
        float velocity;
        float bounds;

        int currentprojectileHealth;
        int maxprojectileHealth;
        float fireRate;
        int currentprojectileAmmo;
        int maxprojectileAmmo;

        //projectileUpgradeState upgradeState;
        projectileState currentprojectileState = projectileState.Alive;

        public projectile(Sprite sprite, Transform transform, Controls controls) : base(sprite, transform)// Ask Angelo how Controls work
        {
            _transform = transform;
            _sprite = sprite;
            projectileControls = controls;

            velocity = 4f;
        }

        public void Update(GameTime gameTime)
        {
            base.Update(gameTime); ///////////
            switch (currentprojectileState)
            {
                case projectileState.Alive:
                    projectileMove();
                    break;
                case projectileState.Dead:
                    break;
                default:
                    break;
            }
        }

        public void projectileMove()
        {
            _transform.TranslatePosition(new Vector2(0, velocity)); //////////////////Figuring out how to make it move

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

    public enum projectileUpgradeState
    {
        None = 0,
    }

    public enum projectileState
    {
        Alive,
        Dead
    }
}