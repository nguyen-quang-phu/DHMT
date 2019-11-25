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
            //if (equilateral)
            //{
            //    p2.Y = p1.Y + (p2.X - p1.X);
            //}
            p2.Y = p1.Y + (p2.X - p1.X);
            int xc = (p1.X + p2.X) / 2,
                yc = (p1.Y + p2.Y) / 2,
                rx = xc - p1.X,
                ry = yc - p1.Y;

            int dx = (int)Math.Round(rx / 2.0),
                dy = (int)Math.Round(ry * Math.Sqrt(3) / 2.0);

            nPoints[0].setPoint(xc - dx, yc + dy);
            nPoints[1].setPoint(xc + dx, yc + dy);
            nPoints[2].setPoint(xc + rx, yc);
            nPoints[3].setPoint(xc + dx, yc - dy);
            nPoints[4].setPoint(xc - dx, yc - dy);
            nPoints[5].setPoint(xc - rx, yc);
        }
    }
}
