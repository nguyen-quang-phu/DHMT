using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
{
    class Rectangle:Polygon
    {
        public Rectangle()
        {
            nPoly = 4;
            init();
        }

        public override void set(Point A, Point B)
        {
            nPoints[0].setPoint(Math.Min(A.X, B.X), Math.Min(A.Y, B.Y));
            nPoints[1].setPoint(Math.Max(A.X, B.X), Math.Min(A.Y, B.Y));
            nPoints[2].setPoint(Math.Max(A.X, B.X), Math.Max(A.Y, B.Y));
            nPoints[3].setPoint(Math.Min(A.X, B.X), Math.Max(A.Y, B.Y));
        }
    }
}
