using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Infinity
{
    class Sprite
    {
        public Rectangle Bounds { get; set; }
        public Texture2D Texture { get { return texture; } }
        public Vector2 Origin { get { return origin; } }

        private Texture2D texture;
        private Vector2 origin;

        public Sprite(Texture2D texture)
        {
            this.texture = texture;
            origin = new Vector2(0, 0);
        }

        public Sprite(Texture2D texture, Vector2 origin)
        {
            this.texture = texture;
            this.origin = origin;
        }
    }
}
