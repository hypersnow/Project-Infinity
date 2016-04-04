using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Infinity
{
    static class Collision
    {
        public static bool AABB(GameObject object1, GameObject object2)
        {
            return object1.CollideBox.Intersects(object2.CollideBox);
        }
    }
}
