using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
{
    class Line : Object
    {
        public Line() { }
        public Line(Point s, Point e)
        {
            p1.setPoint(s);
            p2.setPoint(e);
        }
        public override void set(Point A, Point B)
        {
            p1.setPoint(A.X, A.Y);
            p2.setPoint(B.X, B.Y);
        }
        public override void Draw(OpenGL gl)
        {
            gl.Color(LineColor.R, LineColor.G, LineColor.B);
            gl.LineWidth(LineWidth);

            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex2sv(new short[] { (short)p1.X, (short)p1.Y });
            gl.Vertex2sv(new short[] { (short)p2.X, (short)p2.Y });
            gl.End();
        }
    }
}
