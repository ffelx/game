using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.Model
{
    internal class Ground
    {
        public double X;
        public double Y;
        public double Width;
        public double Height;

        public Ground(double x, double y, double width, double height) 
        {
            X = x; 
            Y = y; 
            Width = width; 
            Height = height;
        }

    }
}
