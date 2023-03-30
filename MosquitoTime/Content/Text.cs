using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace MosquitoTime.Content
{
    public class Text
    {
        public SpriteFont _font;
        public string _text;
        public Transform _transform; //includes: position, direction, rotation, scale


        public Text(SpriteFont font, string text, Transform transform)
        {
            _font = font;
            _text = text;
            _transform = transform;
        }
        

        public struct Transform
        {
            public Vector2 Position;
            public Vector2 Direction;
            public float Rotation;
            public float Scale;

            public Transform(Vector2 position, Vector2 direction, float rotation, float scale)
            { //Constructor
                this.Position = position;
                this.Direction = direction;
                this.Rotation = rotation;
                this.Scale = scale;
            }

            public void TranslatePosition(Vector2 offset) //Accidentally wrote a different function to cover movemetn in some of the child classes
            {
                Position += offset;
            }
            public void TransformPosition(Vector2 newPos)
            {
                Position = newPos;
            }
        }
    }
}
