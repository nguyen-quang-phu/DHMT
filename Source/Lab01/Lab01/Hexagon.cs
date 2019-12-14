using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
{
    class Hexagon : Polygon
    {
        public Hexagon()
        {
            nPoly = 6;
            init();
            //equilateral = true;
        }
        public override void set(Point pi, Point pj)
        {
            base.set(pi, pj);
            p2r.Y = p2.Y = p1.Y + (int)Math.Round((p2.X - p1.X) * Math.Sqrt(3) / 2.0);
            int rx = Math.Abs(p1.X - p2.X) / 2, ry = Math.Abs(p1.Y - p2.Y) / 2;
            int dx = rx / 2;

            nPoints[0].setPoint(-dx, ry);
            nPoints[1].setPoint(dx, ry);
            nPoints[2].setPoint(rx, 0);
            nPoints[3].setPoint(dx, -ry);
            nPoints[4].setPoint(-dx, -ry);
            nPoints[5].setPoint(-rx, 0);

        }
    }
}
