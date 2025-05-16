using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Model
{
    internal class Ground
    {
        public float X;
        public float Y;
        public float Width;
        public float Height;
        public bool CanDropDown { get; set; } = true;

        public Ground(float x, float y, float width, float height) 
        {
            X = x; 
            Y = y; 
            Width = width; 
            Height = height;
        }

    }
}
