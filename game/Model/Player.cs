using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.Model
{
    internal class Player
    {
        public float X { get; set; }
        public float Y { get; set; }
        
        private float _velocityX;
        public float VelocityX
        {
            get => _velocityX;
            set
            {
                if ((Math.Abs(value) < MaxSpeed)
                  || Math.Abs(_velocityX) > MaxSpeed && Math.Abs(_velocityX) > Math.Abs(value))
                {
                    _velocityX = value;
                    return;
                }
            }
        }
        public float VelocityY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public float MaxSpeed { get; set; }
        public float JumpForce { get; set; }
        public float Acceleration { get; set; } = 0.5f;

        public Player(float positionX, float positionY) 
        {
            X = positionX;
            Y = positionY;
            Width = 50;
            Height = 120;
            MaxSpeed = 4;
            JumpForce = 3;
        }

        public void ApplyGravity(float g)
        {
            VelocityY += g;
        }
        public void UpdatePosition()
        {
            X += VelocityX;
            Y += VelocityY;
        }

        public void Stop()
        {
            int sign = _velocityX < 0 ? -1 : 1;
            var resultSpeed = Math.Abs(_velocityX) - Acceleration;
            resultSpeed = resultSpeed < 0 ? 0 : resultSpeed;
            _velocityX = resultSpeed * sign;
        }

    }
}
