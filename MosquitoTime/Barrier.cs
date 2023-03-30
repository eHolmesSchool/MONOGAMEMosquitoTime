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
    public class Barrier : GameObject
    {
        public Barrier(Sprite sprite, Transform transform) : base(sprite, transform)
        {
            _sprite = sprite;
            _transform = transform;
        }
    }
}