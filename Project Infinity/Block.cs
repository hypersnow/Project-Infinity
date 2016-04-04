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
    enum BlockSizes { Normal = 32, Small = 16, Tiny = 8 };

    class Block : GameObject
    {
        public int Size { get { return size; } }

        private Sprite dot;
        private int size;

        public Block(int size, Vector2 position) : base(position)
        {
            SetSprite(ResourceManager.GetSprite("block"));
            dot = ResourceManager.GetSprite("dot");
            this.size = size;
            Bounds = new Rectangle(0, 0, size, size);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite.Texture, new Rectangle((int)X, (int)Y, size, size), new Rectangle(0, 0, size, size), Color.White);
            spriteBatch.Draw(dot.Texture, new Rectangle((int)X, (int)Y, size, 1), Color.White);
            spriteBatch.Draw(dot.Texture, new Rectangle((int)X, (int)Y, 1, size), Color.White);
            spriteBatch.Draw(dot.Texture, new Rectangle((int)X + size - 1, (int)Y, 1, size), Color.White);
            spriteBatch.Draw(dot.Texture, new Rectangle((int)X, (int)Y + size - 1, size, 1), Color.White);
        }
    }
}
