using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewYearAnimation
{
    internal class Human
    {
        public int X {  get; set; }
        public int Y { get; set; }
        public int Size { get; set; }
        public int Step { get; set; }
        public Image Image { get; set; }

        Human(int x, int y, int size, Image image) 
        {
            X = x;
            Y = y;
            Size = size;
            Image = image;
        }
    }
}
