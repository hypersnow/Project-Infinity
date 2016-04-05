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
    class Interval
    {
        public bool IsRunning { get { return left > 0; } }

        private float left;

        public Interval()
        {
            left = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (left > 0)
            {
                left -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (left <= 0)
                    left = 0;
            }
        }

        public void Start(float seconds)
        {
            left = seconds;
        }

        public void Reset()
        {
            left = 0;
        }
    }
}
