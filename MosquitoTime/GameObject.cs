﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace MosquitoTime
{
    public class GameObject
    {
        public Sprite _sprite;
        public Transform _transform;

        public List<GameObject> _collidableObjects;

        public ObjectState currentState = ObjectState.None;

        public GameObject(Sprite sprite, Transform transform)
        {
            _sprite = sprite;
            _transform = transform;

            _collidableObjects = new List<GameObject>();
        }

        public bool OnCollide(Rectangle otherBounds)
        {
            return _sprite.Bounds.Intersects(otherBounds);
        }

        public void Move(Vector2 offset)
        {
            _transform.TranslatePosition(offset);
            _sprite.UpdateBounds(_transform);
        }

        public void Update(GameTime gameTime)
        {
            //_transform.CheckBounds(_sprite);

            if(currentState == ObjectState.Dead)
            {
                _transform.Position = new Vector2(-100, -100);
            }

            _sprite.UpdateBounds(_transform); 
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            _sprite.Draw(spriteBatch);
        }
        public virtual void Collision()
        {
            currentState = ObjectState.Dead;
        }

        public enum ObjectState
        {
            None,
            Alive,
            Hit,
            Dying,
            Dead
        }
    }

    public struct Sprite
    {
        public Sprite(Texture2D texture, Rectangle bounds, float scale)
        {
            this.SpriteSheet = texture;
            this.Bounds = bounds;
            this.Scale = scale;
        }

        public Texture2D SpriteSheet;
        public Rectangle Bounds;
        public float Scale;

        public void UpdateBounds(Transform transform)
        {   //Changes the location of the visual sprite based on a passed-in rectangle   (?)
            Bounds = new Rectangle(transform.Position.ToPoint(), Bounds.Size);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpriteSheet, Bounds, Color.White);
        }
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

        public bool CheckBounds(Sprite sprite)
        {  
            return false;
        }
    }
}
