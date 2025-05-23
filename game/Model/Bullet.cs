using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Model
{
    internal class Bullet
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Speed { get; } = 10f;
        public Player.Direction Direction { get; }
        public int Width { get; } = 10;
        public int Height { get; } = 5;
        public float KnockbackForce { get; } = 20f;


        public Bullet(float x, float y, Player.Direction direction)
        {
            X = direction == Player.Direction.Right ? x : x - Width;
            Y = y;
            var xOffset = 30;
            X += direction == Player.Direction.Right ? xOffset : -xOffset;
            Direction = direction;
        }

        public void Update()
        {
            X += Direction == Player.Direction.Right ? Speed : -Speed;
        }
    }
}
