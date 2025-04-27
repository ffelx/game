using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace game.Model
{

    internal class GameModel
    {
        private HashSet<Keys> _pressedKeys = new HashSet<Keys>();
        
        public const int Width = 1280;
        public const int Height = 720;

        private const float g = -0.1f; 
        private Timer _timer;
        public Player[] Players { get; private set; }
        public Ground[] Grounds { get; private set; }
        
        public event Action Updated;
        public GameModel()
        {
            SetPlayers();
            SetGrounds();

            _timer = new Timer();
            _timer.Interval = 17;
            _timer.Tick += Update;
            _timer.Start();
        }

        private void SetPlayers()
        {
            Players = new Player[1] { new Player(80, 500) };
        }

        private void SetGrounds()
        {
            Grounds = new Ground[2]
            {
                 new Ground(100, 100, 1000, 30),
                 new Ground(20, 200, 1000, 10),
            };
        }

        private void Update(object sender, EventArgs e)
        {
            foreach (var player in Players)
            {
                bool isLeft = _pressedKeys.Contains(Keys.A);
                bool isRight = _pressedKeys.Contains(Keys.D);
                if (isLeft)
                {
                    player.VelocityX -= player.Acceleration;
                }
                if (isRight)
                {
                    player.VelocityX += player.Acceleration;
                }
                if (!isLeft && !isRight)
                {
                    player.Stop();
                }

                bool isGround = TryLandOnGround(player);
                if (_pressedKeys.Contains(Keys.W) && isGround)
                {
                    player.VelocityY += player.JumpForce;
                }

                if (!isGround)
                {
                    player.ApplyGravity(g);
                }
               
                player.UpdatePosition();
               
            }
            Updated.Invoke();
        }

        private bool TryLandOnGround(Player player)
        {
            foreach (var ground in Grounds)
            {
                var tempPlayer = new Player(player.X, player.Y + player.VelocityY);
                if (IsOnGround(tempPlayer, ground))
                {
                    player.Y = ground.Y + ground.Height;
                    player.VelocityY = 0;
                    return true;
                }
            }
            return false;
        }

        private static bool AreIntersected(Player player, Ground ground)
        {
            return !(player.X + player.Width < ground.X ||
                 ground.X + ground.Width < player.X ||
                 player.Y > ground.Y + ground.Height ||
                 player.Y + player.Height < ground.Y);
        }


        private static bool IsPointInside(Ground ground, float x, float y)
        {
            return x >= ground.X &&
                   x <= ground.X + ground.Width &&
                   y >= ground.Y &&
                   y <= ground.Y + ground.Height;
        }

        private static bool IsOnGround(Player player, Ground ground)
        {
            return IsPointInside(ground, player.X, player.Y) 
                || IsPointInside(ground, player.X + player.Width, player.Y);
        }

        public void PressKey(Keys key)
        {
            //MessageBox.Show("");
            _pressedKeys.Add(key);
        }

        public void ReleaseKey(Keys key)
        {
           // MessageBox.Show("");
            _pressedKeys.Remove(key);
        }
    }
}
