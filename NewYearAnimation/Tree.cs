using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewYearAnimation
{

    internal class Tree : Item
    {
        public Tree(int x, int y, Color color, int size, int step) : base(x, y, size, step, color)
        {

        }

        public override void Draw(Graphics g)
        {
            Pen trunkPen = new Pen(Color.FromArgb(100, 100, 100));
            SolidBrush trunkBrush = new SolidBrush(Color.FromArgb(180, 110, 30));
            SolidBrush[] leavesBrushes = new SolidBrush[] {
        new SolidBrush(Color.FromArgb(80, 170, 20)),
        new SolidBrush(Color.FromArgb(150, 200, 30)),
        new SolidBrush(Color.FromArgb(180, 230, 40))
    };

            float trunkWidth = 0.1f * Size;
            float trunkHeight = 0.25f * Size;
            RectangleF trunkRect = new RectangleF(X - trunkWidth / 2, Y - trunkHeight, trunkWidth, trunkHeight);
            g.FillRectangle(trunkBrush, trunkRect);
            g.DrawRectangle(trunkPen, trunkRect.X, trunkRect.Y, trunkRect.Width, trunkRect.Height);

            float leafSegmentHeight = 0.3f * Size;
            float leafMaxWidth = Size;

            for (int i = 0; i < 3; i++)
            {
                float leafWidth = leafMaxWidth * (1f - i * 0.25f);
                float leafLeft = X - leafWidth / 2f;
                float leafTop = Y - trunkHeight - (i + 1) * leafSegmentHeight;
                PointF[] trianglePoints = new PointF[] {
            new PointF(leafLeft, leafTop + leafSegmentHeight),
            new PointF(leafLeft + leafWidth, leafTop + leafSegmentHeight),
            new PointF(X, leafTop)
            };

                g.FillPolygon(leavesBrushes[i], trianglePoints);
            }

            trunkPen.Dispose();
            trunkBrush.Dispose();
            foreach (SolidBrush brush in leavesBrushes)
            {
                brush.Dispose();
            }
        }
    }
}

