using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace Game.Model
{

    internal class GameModel
    {
        private InputHandler _inputHandler = new InputHandler();
        private Physics _physics = new Physics();
        private GameLoop _loop = new GameLoop();

        public const int Width = 1280;
        public const int Height = 720;

        public Player[] Players { get; private set; }
        public Ground[] Grounds { get; private set; }
        
        public event Action Updated;
        public GameModel()
        {
            SetPlayers();
            SetGrounds();

            _loop.Tick += Update;
            _loop.Start();
        }

        private void SetPlayers()
        {
            Players = new Player[2] 
            { 
                new Player(350, 720),
                new Player(870, 720) { CurrentDirection = Player.Direction.Left}
            };
        }

        private void SetGrounds()
        {
            Grounds = new Ground[3]
            {
                 new Ground(242, 83, 797, 30)  { CanDropDown = false },
                 new Ground(242, 229, 318, 10) { CanDropDown = true },
                 new Ground(720, 229, 318, 10) { CanDropDown = true },
            };
        }

        public void PressKey(Keys key) => _inputHandler.PressKey(key);
        public void ReleaseKey(Keys key) => _inputHandler.ReleaseKey(key);

        private void Update()
        {
            for (int i = 0; i < Players.Length; i++)
            {
                int playerNumber = i + 1;
                var player = Players[i];
                PlayerMoveUpdate(player, playerNumber);
                BulletsUpdate(player);
            }
            Updated?.Invoke();
        }

        private void PlayerMoveUpdate(Player player, int playerNumber)
        {
            bool isLeft = _inputHandler.IsKeyPressed(playerNumber == 1 ? Keys.A : Keys.Left, playerNumber);
            bool isRight = _inputHandler.IsKeyPressed(playerNumber == 1 ? Keys.D : Keys.Right, playerNumber);

            if (isLeft)
            {
                player.VelocityX -= player.Acceleration;
                player.CurrentDirection = Player.Direction.Left;
            }
            if (isRight)
            {
                player.VelocityX += player.Acceleration;
                player.CurrentDirection = Player.Direction.Right;
            }
            if (!isLeft && !isRight)
            {
                player.Stop();
            }
            if (_inputHandler.IsKeyPressed(playerNumber == 1 ? Keys.E : Keys.ControlKey, playerNumber))
            {
                player.Shoot();
            }

            bool isGround = _physics.TryLandOnGround(player, Grounds, out var ground);

            if (_inputHandler.IsKeyPressed(playerNumber == 1 ? Keys.W : Keys.Up, playerNumber) && isGround)
            {
                player.Jump();
            }

            if (!isGround)
            {
                _physics.ApplyGravity(player);
            }

            var downKey = playerNumber == 1 ? Keys.S : Keys.Down;
            if (isGround
                && _inputHandler.IsKeyPressed(downKey, playerNumber) 
                && ground.CanDropDown
                && !player.IgnorePlatformCollision)
            {
                player.StartDropDown();
            }

            if (player.IgnorePlatformCollision)
            {
                player.UpdateDropDown(GameLoop.Interval / 1000f);
            }

            player.UpdatePosition();
        }

        private void BulletsUpdate(Player player)
        {
            float deltaTime = GameLoop.Interval / 1000f;
            player.UpdateCooldown(deltaTime);

            foreach (var bullet in player.Bullets.ToList())
            {
                bullet.Update();

                Player target = Players.FirstOrDefault(p => p != player);
                if (target != null && _physics.TryHitPlayerWithBullet(bullet, target))
                {
                    player.Bullets.Remove(bullet);
                    continue;
                }

                if (bullet.X < 0 || bullet.X > Width)
                {
                    player.Bullets.Remove(bullet);
                }
            }
        }


    }
}
