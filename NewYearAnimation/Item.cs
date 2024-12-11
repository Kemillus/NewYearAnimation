using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewYearAnimation
{
    internal class Item
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; set; }
        public int Step { get; set; }
        public Color Color { get; set; }

        public Item(int x, int y, int size, int step, Color color)
        {
            X = x;
            Y = y;
            Size = size;
            Step = step;
            Color = color;
        }

        public virtual void Draw(Graphics g)
        {

        }
    }
}
