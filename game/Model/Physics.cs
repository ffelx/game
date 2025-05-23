using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Model
{
    internal class Physics
    {
        private const float Gravity = -0.05f;
        public bool TryLandOnGround(Player player, Ground[] grounds, out Ground value)
        {
            value = null;
            foreach (var ground in grounds)
            {
                var tempPlayer = new Player(player.X, player.Y + player.VelocityY);
                if (IsOnGround(tempPlayer, ground) 
                    && player.VelocityY <= 0
                    && !player.IgnorePlatformCollision)
                {
                    player.Y = ground.Y + ground.Height;
                    player.VelocityY = 0;
                    value = ground;
                    return true;
                }
            }
            return false;
        }

        private static bool IsOnGround(Player player, Ground ground)
        {
            return IsPointInside(ground, player.X, player.Y)
                || IsPointInside(ground, player.X + player.Width, player.Y);
        }

        private static bool IsPointInside(Ground ground, float x, float y)
        {
            return x >= ground.X &&
                   x <= ground.X + ground.Width &&
                   y >= ground.Y &&
                   y <= ground.Y + ground.Height;
        }


        private static bool AreIntersected(Player player, Ground ground)
        {
            return !(player.X + player.Width < ground.X ||
                 ground.X + ground.Width < player.X ||
                 player.Y > ground.Y + ground.Height ||
                 player.Y + player.Height < ground.Y);
        }

        public void ApplyGravity(Player player)
        {
            player.ApplyGravity(Gravity);
        }

        public bool TryHitPlayerWithBullet(Bullet bullet, Player target)
        {
            bool isHit = bullet.X < target.X + target.Width &&
                        bullet.X + bullet.Width > target.X &&
                        bullet.Y < target.Y + target.Height &&
                        bullet.Y + bullet.Height > target.Y;

            if (isHit)
            {
                target.ApplyKnockback(bullet.Direction, bullet);
            }

            return isHit;
        }
    }
}
