using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Infinity
{
    class GameObject
    {
        public Sprite Sprite { get { return sprite; } }
        public Rectangle CollideBox { get; set; }
        public Rectangle Bounds { get { return bounds; } set { bounds = value; CollideBox = new Rectangle(bounds.X + (int)X, bounds.Y + (int)Y, bounds.Width, bounds.Height); } }
        public Vector2 Position { get; set; }
        public Vector2 Motion { get; set; }
        public int CollideX { get { return CollideBox.X; } set { CollideBox = new Rectangle(value, CollideY, bounds.Width, bounds.Height); } }
        public int CollideY { get { return CollideBox.Y; } set { CollideBox = new Rectangle(CollideX, value, bounds.Width, bounds.Height); } }
        public float X { get { return Position.X; } set { Position = new Vector2(value, Position.Y); CollideX = bounds.X + (int)value; } }
        public float Y { get { return Position.Y; } set { Position = new Vector2(Position.X, value); CollideY = bounds.Y + (int)value; } }
        public float DX { get { return Motion.X; } set { Motion = new Vector2(value, Motion.Y); } }
        public float DY { get { return Motion.Y; } set { Motion = new Vector2(Motion.X, value); } }

        protected Sprite sprite;
        protected Rectangle bounds;
        protected SpriteEffects spriteEffects;
        protected float animSpeed;
        private Interval animTimer;

        public GameObject(Vector2 position)
        {
            Position = position;
            Bounds = new Rectangle(0, 0, 0, 0);
            spriteEffects = SpriteEffects.None;
            animSpeed = 0.1f;
            animTimer = new Interval();
        }

        public GameObject(Sprite sprite, Vector2 position)
        {

            Position = new Vector2(0, 0);
            SetSprite(sprite);
            spriteEffects = SpriteEffects.None;
            animSpeed = 1;
        }

        public void SetSprite(Sprite sprite)
        {
            this.sprite = sprite;
        }

        public void SetSpriteResource(string name)
        {
            this.sprite = ResourceManager.GetSprite(name);
        }

        protected void Animate(GameTime gameTime)
        {
            animTimer.Update(gameTime);
            if (sprite.MaxFrames > 1 && !animTimer.IsRunning)
            {
                if (animSpeed > 0)
                    sprite.Frame++;
                if (sprite.Frame >= sprite.MaxFrames)
                    sprite.Frame = 0;
                animTimer.Start(animSpeed);
            }
        }

        protected void SetAnimSpeed(float seconds)
        {
            animSpeed = seconds;
        }

        public virtual void Update(GameTime gameTime, KeyboardState keyState, List<GameObject> gameObjects) 
        {
            Position += Motion * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Animate(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite.Texture, Position, new Rectangle((sprite.Frame + sprite.AnimBegin) * sprite.SourceWidth, 0, sprite.SourceWidth, sprite.Texture.Height), Color.White, 0f, new Vector2(0f, 0f), new Vector2(1f, 1f), spriteEffects, 0f);
        }
    }
}
