using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewYearAnimation
{
    internal class Snowflake : Item
    {
        public Snowflake(int x, int y, Color color, int size, int step) : base(x, y, size, step, color)
        {

        }

        public override void Draw(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color), X, Y, Size, Size);
        }
    }
}
