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
    class Player : GameObject
    {
        private const float moveConst = 0.01f;

        private float speed, maxSpeed, slow, jump, gravity, maxGravity;
        bool onFloor, isGravity;

        public Player(Vector2 position) : base(position)
        {
            SetSprite(ResourceManager.GetSprite("player"));
            Bounds = new Rectangle(12, 0, 8, 32);
            isGravity = true;
            speed = 500f;
            maxSpeed = 300f;
            slow = 200f;
            jump = 300f;
            gravity = 800f;
            maxGravity = 500.0f;
        }

        public override void Update(GameTime gameTime, KeyboardState keyState, List<GameObject> gameObjects)
        {
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            CheckCollisions(time, gameObjects);

            if (keyState.IsKeyDown(Keys.A) || keyState.IsKeyDown(Keys.D))
            {
                if (keyState.IsKeyDown(Keys.A))
                {
                    DX -= speed * time;
                    spriteEffects = SpriteEffects.FlipHorizontally;
                }
                if (keyState.IsKeyDown(Keys.D))
                { 
                    DX += speed * time;
                    spriteEffects = SpriteEffects.None;
                }
            }
            else 
            {
                if (DX > 0)
                    DX -= slow * time;
                else if (DX < 0)
                    DX += slow * time;
                if (Math.Abs(DX) < 10)
                    DX = 0;
            }
            if (DX > maxSpeed)
                DX = maxSpeed;
            else if (DX < -maxSpeed)
                DX = -maxSpeed;

            if (keyState.IsKeyDown(Keys.W) && onFloor)
            {
                DY = -jump;
                onFloor = false;
            }

            if (keyState.IsKeyDown(Keys.R))
            {
                X = 0;
                Y = 0;
            }

            if (isGravity)
            {
                DY += gravity * time;
                if (DY > Math.Abs(maxGravity))
                    DY = maxGravity;
            }
        }

        private void CheckCollisions(float time, List<GameObject> gameObjects)
        {
            onFloor = false;

            List<Block> blocks = new List<Block>();
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject is Block && Math.Abs(gameObject.X - X) < 33 && Math.Abs(gameObject.Y - Y) < 33)
                {
                    blocks.Add((Block)gameObject);
                }
            }

            for (float i = 0; i < Math.Abs(DY * time); i += moveConst)
            {
                bool collided = false;
                foreach (Block block in blocks)
                {
                    if (Collision.AABB(this, block))
                    {
                        collided = true;
                        while (Collision.AABB(this, block))
                        {
                            if (DY > 0)
                                Y -= moveConst;
                            else
                                Y += moveConst;
                        }
                        break;
                    }
                }
                if (!collided)
                {
                    if (DY > 0)
                        Y += moveConst;
                    else
                        Y -= moveConst;
                }
                else
                {
                    if (DY > 0)
                        onFloor = true;
                    DY = 0;
                    break;
                }
            }

            for (float i = 0; i < Math.Abs(DX * time); i += moveConst)
            {
                bool collided = false;
                foreach (Block block in blocks)
                {
                    if (Collision.AABB(this, block))
                    {
                        collided = true;
                        while (Collision.AABB(this, block))
                        {
                            if (DX > 0)
                                X -= moveConst;
                            else
                                X += moveConst;
                        }
                        break;
                    }
                }
                if (!collided)
                {
                    if (DX > 0)
                        X += moveConst;
                    else
                        X -= moveConst;
                }
                else
                {
                    DX = 0;
                    break;
                }
            }
        }
    }
}
