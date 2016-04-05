using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Infinity
{
    class ResourceManager
    {
        private static Dictionary<string, Sprite> sprites;
        private static Dictionary<string, SpriteFont> fonts;

        public ResourceManager()
        {
            sprites = new Dictionary<string, Sprite>();
            fonts = new Dictionary<string, SpriteFont>();
        }

        public void LoadResources(ContentManager content)
        {
            sprites.Add("player", new Sprite(content.Load<Texture2D>("char"), 1, 32));
            sprites.Add("player_run", new Sprite(content.Load<Texture2D>("char"), 2, 32));
            sprites.Add("player_jump", new Sprite(content.Load<Texture2D>("char"), 1, 32, 2));
            sprites.Add("grid", new Sprite(content.Load<Texture2D>("grid")));
            sprites.Add("block", new Sprite(content.Load<Texture2D>("block")));
            sprites.Add("dot", new Sprite(content.Load<Texture2D>("dot")));
            fonts.Add("font", content.Load<SpriteFont>("font"));
        }

        public static Sprite GetSprite(string name)
        {
            if (sprites.ContainsKey(name))
                return sprites[name];
            else
                return null;
        }

        public static SpriteFont GetFont(string name)
        {
            if (fonts.ContainsKey(name))
                return fonts[name];
            else
                return null;
        }
    }
}
