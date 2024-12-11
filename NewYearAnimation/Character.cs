using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NewYearAnimation
{
    internal class Character : Item
    {
        public Image Image { get; set; }
        public int Direction { get; set; }

        public Character(int x, int y, int size, int step, Image image) : base(x, y, size, step, Color.White)
        {
            X = x;
            Y = y;
            Size = size;
            Image = image;
            Direction = 1;
        }

        public override void Draw(Graphics g)
        {
            int drawX = X - Size / 2;
            int drawY = Y - Size;

            if (Image != null)
            {
                g.DrawImage(Image, drawX, drawY, Size, Size);

            }
        }

        public void Move(int width)
        {
            X += Step * Direction;

            if (X + Size / 2 > width || X - Size / 2 < 0)
            {
                Direction *= -1;
                Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
        }
    }
}
