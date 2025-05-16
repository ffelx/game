using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game.Model
{
    
    internal class Player
    {
        public Direction CurrentDirection { get; set; } = Direction.Right;

        public float X { get; set; }
        public float Y { get; set; }
        
        public int Width { get; set; }
        public int Height { get; set; }
        public float MaxSpeed { get; set; }
        public float JumpForce { get; set; }
        public float Acceleration { get; set; } = 0.5f;
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

        public List<Bullet> Bullets { get; } = new List<Bullet>();
        
        
        private float _shootCooldown = 0.5f; 
        private float _currentCooldown = 0;
        public float KnockbackForce { get; } = 15f;

        public bool IgnorePlatformCollision { get; private set; }
        private float _ignoreCollisionTimer = 0;
        private float _dropDownTime = 0.5f;

        public Player(float positionX, float positionY) 
        {
            X = positionX;
            Y = positionY;
            Width = 50;
            Height = 95;
            MaxSpeed = 4;
            JumpForce = 7.0f;
        }

        
        public void ApplyGravity(float g)
        {
            VelocityY += g;
        }

        public void ApplyKnockback(Direction bulletDirection)
        {
            _velocityX -= bulletDirection == Direction.Right
                ? -KnockbackForce
                : KnockbackForce;
            
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

        public void Shoot()
        {
            if (_currentCooldown <= 0)
            {
                Bullets.Add(new Bullet(X + Width / 2, Y + Height / 2, CurrentDirection));
                _currentCooldown = _shootCooldown;
            }
        }

        public void UpdateCooldown(float deltaTime)
        {
            if (_currentCooldown > 0)
            {
                _currentCooldown -= deltaTime;
            }
        }

        public void Jump()
        {
            VelocityY += JumpForce;
        }

        public void StartDropDown()
        {
            //MessageBox.Show("StartDropDown");
            IgnorePlatformCollision = true;
            _ignoreCollisionTimer = _dropDownTime;
        }

        public void UpdateDropDown(float time)
        {
            //MessageBox.Show("StartDropDown");
            if (IgnorePlatformCollision)
            {
                _ignoreCollisionTimer -= time;
                if (_ignoreCollisionTimer <= 0)
                {
                    IgnorePlatformCollision = false;
                }
            }
            
        }

        public enum Direction
        {
            Left,
            Right
        }
    }

    
}
