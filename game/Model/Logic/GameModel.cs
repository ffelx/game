using game.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Game.Model
{
    internal class GameModel
    {
        private InputHandler _inputHandler = new InputHandler();
        private Physics _physics = new Physics();
        private GameLoop _loop = new GameLoop();
        private BotController _botController;
        private bool _isBotGame = false;

        public int FirstPlayerScore { get; set; }
        public int SecondPlayerScore { get; set; }
        private int _maxScore = 5;
        private bool isEndGame = false;

        public const int Width = 1280;
        public const int Height = 720;

        public List<Player> Players { get; private set; }
        public Ground[] Grounds { get; private set; }
        

        public event Action Updated;
        public event Action<int> GameOver;

        public GameModel(bool isBotGame = false)
        {
            SetGrounds();          
            SetPlayers();
            SetBotIfBotGame(isBotGame);

            _loop.Tick += Update;
            _loop.Start();
        }

        private void SetBotIfBotGame(bool isBotGame)
        {
            _isBotGame = isBotGame;
            if (isBotGame)
            {
                _botController = new BotController(this, _physics);      
                Players[1].JumpForce += 0.1f;
            }
        }

        private void SetPlayers()
        {
            Players = new List<Player>()
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
            if (isEndGame) return;
            for (int i = 0; i < Players.Count; i++)
            {
                int playerNumber = i + 1;
                var player = Players[i];

                PlayerOrBotUpdate(player, playerNumber);
                BulletsUpdate(player, playerNumber);
                PlayerStateUpdate(player, playerNumber);
            }
            Updated?.Invoke();
        }

        private void PlayerStateUpdate(Player player, int playerNumber)
        {
            if (player.Y < -player.Height)
            {
                if (playerNumber == 1)
                {
                    SecondPlayerScore += 1;
                }
                else
                {
                    FirstPlayerScore += 1;
                }
                player.VelocityY = 0;
                player.X = player.StartPoint.X;
                player.Y = player.StartPoint.Y;
                TryGameOver();
            }
        }

        public void TryGameOver()
        {
            if (FirstPlayerScore >= _maxScore)
            {
                isEndGame = true;
                GameOver?.Invoke(1);
                
            }
            else if (SecondPlayerScore >= _maxScore)
            {
                isEndGame = true;
                GameOver?.Invoke(2);
            }
        }

        private void PlayerOrBotUpdate(Player player, int playerNumber)
        {
            if (!_isBotGame || playerNumber == 1)
            {
                PlayerMoveUpdate(player, playerNumber);
            }
            else
            {
                _botController.Update();
            }
        }

        private void PlayerMoveUpdate(Player player, int playerNumber)
        {
            MoveLeftOrRight(player, playerNumber);
            MoveUpDown(player, playerNumber);
            player.UpdatePosition();
        }


        private void MoveLeftOrRight(Player player, int playerNumber)
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
        }
        private void MoveUpDown(Player player, int playerNumber)
        {
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
        }

        private void BulletsUpdate(Player player, int playerNumber)
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

            if (_inputHandler.IsKeyPressed(playerNumber == 1 ? Keys.E : Keys.ControlKey, playerNumber))
            {
                player.Shoot();
            }
        }
    }
}
