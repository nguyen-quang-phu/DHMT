using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;

namespace Lab01
{
    class Polygon : Shape
    {
        public int nPoly=0;
        public Point[] nPoints;
        protected Point p1r = new Point(), p2r = new Point();
        protected void init()
        { 
            nPoints = new Point[nPoly];
            for (int i = 0; i < nPoly; i++)
                nPoints[i] = new Point();
        }
        public override void set(Point A, Point B)
        {
            base.set(A, B);
            p1r.setPoint(p1);
            p2r.setPoint(p2);
        }


        public override void Draw(OpenGL gl)
        {
            Line li = new Line();
            li.LineWidth = LineWidth;
            li.LineColor = LineColor;
            int cx = (p1.X + p2.X) / 2, cy = (p1.Y + p2.Y) / 2;
            gl.PushMatrix();
            gl.Translate(cx, cy, 0.0);
            gl.Rotate(Angle, 0.0, 0.0, 1.0);
            gl.Scale((double)(p2.X - p1.X) / (p2r.X - p1r.X), (double)(p2.Y - p1.Y) / (p2r.Y - p1r.Y), 0.0);

            //Vẽ từng cạnh bằng các nối lần lược các đỉnh
            Point start = new Point(), end = new Point();

            start.setPoint(nPoints[nPoly - 1]);
            for (int i = 0; i < nPoly; i++)
            {
                end.setPoint(nPoints[i]);
                li.set(start, end);
                li.Draw(gl);
                start.setPoint(end);
            }
            gl.PopMatrix();
        }
        public override void Fill(OpenGL gl, bool mode)
        {
            if (mode)
            {
                base.Fill(gl, mode);
            }
            else //scanline
            {
                if (nPoly < 3) return;
                ScanFill fillPolygon = new ScanFill();
                List<Point> p = new List<Point>();
                for (int i = 0; i < nPoly; i++)
                {
                    p.Add(nPoints[i]);
                }
                int cx = (p1.X + p2.X) / 2, cy = (p1.Y + p2.Y) / 2;
                gl.PushMatrix();
                gl.Translate(cx, cy, 0.0);
                gl.Rotate(Angle, 0.0, 0.0, 1.0);
                gl.Scale((double)(p2.X - p1.X) / (p2r.X - p1r.X), (double)(p2.Y - p1.Y) / (p2r.Y - p1r.Y), 0.0);
                gl.Color(FillColor.getR(), FillColor.getG(), FillColor.getB());
                fillPolygon.setFill(p);
                fillPolygon.initEdges();
                fillPolygon.scanlineFill(gl);
                gl.PopMatrix();
            }
        }

    }
}
