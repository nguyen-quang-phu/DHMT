using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
{
    class Color
    {
        public float R { get; set; }    //Kênh màu red
        public float G { get; set; }    //Kênh màu green
        public float B { get; set; }    //Kênh màu blue

        public Color(float r = 0, float g = 0, float b = 0)
        {
            R = r;
            G = g;
            B = b;
        }
        public Color(Color c)
        {
            R = c.R;
            G = c.G;
            B = c.B;
        }
        public void setColor(float r, float g, float b)
        {
            R = r;
            G = g;
            B = b;
        }
        public float getR() { return R; }
        public float getG() { return G; }
        public float getB() { return B; }
    }
}
