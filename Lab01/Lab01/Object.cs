using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
{
    class Object
    {
        public Color LineColor { get; set; } = new Color();
        public Color FillColor { get; set; } = new Color();
        //Độ dày viền
        public int LineWidth { get; set; } = 1;
        //Điểm chặn trên trái
        protected Point p1 = new Point();
        //Điểm chặn dưới phải
        protected Point p2 = new Point();
        public virtual void set(Point A, Point B)
        {
            p1.setPoint(Math.Min(A.X, B.X), Math.Min(A.Y, B.Y));
            p2.setPoint(Math.Max(A.X, B.X), Math.Max(A.X, B.X));
        }
        public virtual void Draw(OpenGL gl) { }
    }
}
