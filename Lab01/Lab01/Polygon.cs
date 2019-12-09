﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;

namespace Lab01
{
    class Polygon : Shape
    {
        public int nPoly;
        public Point[] nPoints;

        protected void init()
        {
            nPoints = new Point[nPoly];
            for (int i = 0; i < nPoly; i++)
                nPoints[i] = new Point();
        }
        public override void set(Point A, Point B)
        {
            base.set(A, B);
        }


        public override void Draw(OpenGL gl)
        {
            Line li = new Line();
            for (int i = 0; i < nPoly; i++)
            {
                if (i == nPoly - 1)
                    li = new Line(nPoints[nPoly - 1], nPoints[0]);
                else
                    li = new Line(nPoints[i], nPoints[i+1]);

                li.LineColor = LineColor;
                li.LineWidth = LineWidth;
                li.Draw(gl);
            }
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
                gl.Color(FillColor.getR(), FillColor.getG(), FillColor.getB());
                fillPolygon.setFill(p);
                fillPolygon.initEdges();
                fillPolygon.scanlineFill(gl);
            }
        }

    }
}
