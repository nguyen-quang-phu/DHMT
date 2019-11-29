using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
{
    class Triangle : Polygon
    {
        public Triangle()
        {
            nPoly = 3;
            init();
        }

        public override void set(Point A, Point B)
        {
            base.set(A, B);


            p2.Y = p1.Y + (int)Math.Round((p2.X - p1.X) / 2.0 * Math.Sqrt(3));

            //Tọa độ các đỉnh            
            nPoints[0].setPoint((p1.X + p2.X) / 2, p2.Y);
            nPoints[1].setPoint(p1.X, p1.Y);
            nPoints[2].setPoint(p2.X, p1.Y);
        }
    }
}
