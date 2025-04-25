using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.Model
{
    internal class Player
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double VelocityX { get; set; }
        public double VelocityY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Player(double positionX, double positionY) 
        {
            X = positionX;
            Y = positionY;
            Width = 7;
            Height = 16;
        }

        public void ApplyGravity(double g)
        {
            VelocityY += g;
        }
        public void UpdatePosition()
        {
            X += VelocityX;
            Y += VelocityY;
        }

    }
}
