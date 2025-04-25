using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace game.Model
{

    internal class GameModel
    {
        private const double g = 0.1; 
        private Timer _timer;
        public Player[] Players { get; private set; }
        public Ground[] Grounds { get; private set; }
        
        public event Action Updated;
        public GameModel()
        {
            SetPlayers();
            SetGrounds();

            int n = 0;
            var number = n % 2 != 0 ? 1 : 2;

            _timer = new Timer();
            _timer.Interval = 17;
            _timer.Tick += Update;
            _timer.Start();
        }

        private void SetPlayers()
        {
            Players = new Player[1] { new Player(80, 100) };
        }

        private void SetGrounds()
        {
            Grounds = new Ground[2]
            {
                 new Ground(100, 100, 1000, 10),
                 new Ground(20, 200, 1000, 10),
            };
        }

        private void Update(object sender, EventArgs e)
        {
            foreach (var player in Players)
            {
                
                bool isGround = false;
                foreach (var ground in Grounds)
                {
                    var tempPlayer = new Player(player.X, player.Y + player.VelocityY);
                    if (AreIntersected(tempPlayer, ground))
                    {
                        player.Y = ground.Y - player.Height;
                        player.VelocityY = 0;
                        isGround = true;
                        break;
                    }
                }
                if (isGround) continue;
                
                player.UpdatePosition();
                player.ApplyGravity(g);
            }
            Updated.Invoke();
        }

        public static bool AreIntersected(Player player, Ground ground)
        {
            return !(player.X + player.Width < ground.X ||
                     ground.X + ground.Width < player.X ||
                     player.Y + player.Height < ground.Y ||
                     ground.Y + ground.Height < player.Y);
        }



    }
}
