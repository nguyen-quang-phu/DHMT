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
            base.set(A, B);
            int rx = Math.Abs(p1.X - p2.X) / 2, ry = Math.Abs(p1.Y - p2.Y) / 2;

            //Tọa độ các đỉnh
            nPoints[0].setPoint(-rx, -ry);
            nPoints[1].setPoint(rx, -ry);
            nPoints[2].setPoint(rx, ry);
            nPoints[3].setPoint(-rx, ry);
        }
    }
}
