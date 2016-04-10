using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;

namespace Infinity
{
    class Level
    {
        public List<Vector2> BlockPositions { get; set; }

        public Level() { }

        public Level(List<Block> blocks)
        {
            List<Vector2> blockPositions = new List<Vector2>();
            foreach(Block block in blocks)
            {
                blockPositions.Add(new Vector2(block.X, block.Y));
            }
            BlockPositions = blockPositions;
        }

        public static List<GameObject> LoadLevel(string filepath)
        {
            Level level = JsonConvert.DeserializeObject<Level>(File.ReadAllText(filepath));
            List<GameObject> gameObjects = new List<GameObject>();
            foreach(Vector2 blockPosition in level.BlockPositions)
            {
                gameObjects.Add(new Block((int)BlockSizes.Normal, blockPosition));
            }
            return gameObjects;
        }

        public static void SaveLevel(List<Block> blocks)
        {
            Level level = new Level(blocks);
            File.WriteAllText(@"level.json", JsonConvert.SerializeObject(level, Formatting.Indented));
        }
    }
}
