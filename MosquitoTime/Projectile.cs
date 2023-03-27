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
        float velocity;
        float bounds;

        //projectileUpgradeState upgradeState;

        public Projectile(Sprite sprite, Transform transform) : base(sprite, transform)// Ask Angelo how Controls work
        {
            _transform = transform;
            _sprite = sprite;

            velocity = 4f;
        }
    }
}