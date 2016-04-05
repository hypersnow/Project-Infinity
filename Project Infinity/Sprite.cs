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
        public int SourceWidth { get; set; }
        public int AnimBegin { get; set; }
        public int Frame { get; set; }
        public int MaxFrames { get; set; }

        private Texture2D texture;
        private Vector2 origin;

        public Sprite(Texture2D texture)
        {
            this.texture = texture;
            origin = new Vector2(0, 0);
            Frame = 0;
            MaxFrames = 1;
            SourceWidth = texture.Width;
            AnimBegin = 0;
        }

        public Sprite(Texture2D texture, int maxFrames, int sourceWidth)
        {
            this.texture = texture;
            origin = new Vector2(0, 0);
            Frame = 0;
            MaxFrames = maxFrames;
            SourceWidth = sourceWidth;
            AnimBegin = 0;
        }

        public Sprite(Texture2D texture, int maxFrames, int sourceWidth, int animBegin)
        {
            this.texture = texture;
            origin = new Vector2(0, 0);
            Frame = 0;
            MaxFrames = maxFrames;
            SourceWidth = sourceWidth;
            AnimBegin = animBegin;
        }

        public Sprite(Texture2D texture, Vector2 origin, int maxFrames, int sourceWidth)
        {
            this.texture = texture;
            this.origin = origin;
            Frame = 0;
            MaxFrames = maxFrames;
            SourceWidth = sourceWidth;
            AnimBegin = 0;
        }
    }
}
