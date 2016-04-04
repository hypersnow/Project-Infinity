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
    enum BGTypes { Single, Repeating };

    class World
    {
        public static Color BGColor { get; set; }
        public static Camera2D Camera { get; set; }
        private Viewport viewport;
        private Sprite background;
        private List<GameObject> gameObjects;
        private Player player;
        private int bgType;

        public World()
        {
            viewport = new Viewport(0, 0, 640, 480);
            Camera = new Camera2D(viewport);
            BGColor = Color.White;
            background = ResourceManager.GetSprite("grid");
            bgType = (int)BGTypes.Repeating;
            gameObjects = new List<GameObject>();
            player = new Player(new Vector2(0, 64));
            AddBlock((int)BlockSizes.Normal, new Vector2(0, 128));
            AddBlock((int)BlockSizes.Normal, new Vector2(32, 128));
            AddBlock((int)BlockSizes.Normal, new Vector2(64, 128));
            AddBlock((int)BlockSizes.Normal, new Vector2(96, 128));
            AddBlock((int)BlockSizes.Normal, new Vector2(128, 128));
            AddBlock((int)BlockSizes.Normal, new Vector2(128, 96));
            AddBlock((int)BlockSizes.Normal, new Vector2(64, 64));
            AddObject(player);
        }

        private Vector2 GetMousePos(MouseState mouseState)
        {
            int mouseX = mouseState.X + (int)Camera.Pos.X - 16;
            int mouseY = mouseState.Y + (int)Camera.Pos.Y - 16;
            return new Vector2((mouseX / 8) * 8, (mouseY / 8) * 8);
        }

        public void Update(GameTime gameTime, KeyboardState keyState, MouseState mouseState)
        {
            Vector2 addPos = GetMousePos(mouseState);
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                bool exists = false;
                foreach(GameObject gameObject in gameObjects)
                {
                    if (gameObject is Block && gameObject.CollideBox.Intersects(new Rectangle((int)addPos.X, (int)addPos.Y, (int)BlockSizes.Normal, (int)BlockSizes.Normal)))
                    {
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                    AddBlock((int)BlockSizes.Normal, addPos);
            }
            else if (mouseState.RightButton == ButtonState.Pressed)
            {
                for (int i = 0; i < gameObjects.Count; i++)
                {
                    GameObject gameObject = gameObjects[i];
                    if (gameObject is Block &&
                        addPos.X >= gameObject.X - 16 && addPos.X <= gameObject.X + ((Block)gameObject).Size - 16 &&
                        addPos.Y >= gameObject.Y - 16 && addPos.Y <= gameObject.Y + ((Block)gameObject).Size - 16)
                    {
                        gameObjects.RemoveAt(i);
                        break;
                    }
                }
            }

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update(gameTime, keyState, gameObjects);
            }
            Camera.Pos = new Vector2(player.X - 320 + 16, player.Y - 240 + 16);
            Camera.Update();
        }

        private void AddBlock(int size, Vector2 position)
        {
            Block block = new Block(size, position);
            AddObject(block);
        }

        public void AddObject(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
        }

        public void Draw(SpriteBatch spriteBatch, MouseState mouseState)
        {
            if (bgType == (int)BGTypes.Single)
                spriteBatch.Draw(background.Texture, new Vector2(0, 0));
            else
            {
                for (int i = (int)((player.X - 352) / 32); i < (int)((player.X + 352) / 32); i++)
                {
                    for (int j = (int)((player.Y - 288) / 32); j < (int)((player.Y + 288) / 32); j++)
                    {
                        spriteBatch.Draw(background.Texture, new Vector2(i * 32, j * 32));
                    }
                }
            }

            Vector2 addPos = GetMousePos(mouseState);
            spriteBatch.Draw(ResourceManager.GetSprite("block").Texture, addPos, Color.White * 0.8f);
            foreach(GameObject gameObject in gameObjects)
            {
                gameObject.Draw(spriteBatch);
            }

            spriteBatch.DrawString(ResourceManager.GetFont("font"), "X: " + (int)player.X, new Vector2(Camera.Pos.X + 10, Camera.Pos.Y + 10), Color.Black);
            spriteBatch.DrawString(ResourceManager.GetFont("font"), "Y: " + (int)player.Y, new Vector2(Camera.Pos.X + 10, Camera.Pos.Y + 30), Color.Black);
        }
    }
}
