using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MosquitoTime.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using static MosquitoTime.GameObject;

namespace MosquitoTime
{
    public class Clickable
    {
        public Rectangle _rect;
        public string _text;

        public Clickable(Rectangle rect, string text)
        {
            _rect = rect;
            _text = text;
        }

        public bool WasClicked(Vector2 clickArea)
        {
            return _rect.Contains(clickArea);
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {

            spriteBatch.DrawString(font, _text, _rect.Center.ToVector2(), Color.Red);
        }

    }
}
