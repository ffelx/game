using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace game.Model
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
                new Player(100, 500),
                new Player(700, 500)
            };

        }

        private void SetGrounds()
        {
            Grounds = new Ground[3]
            {
                 new Ground(242, 84, 797, 30),
                 new Ground(242, 228, 318, 10),
                 new Ground(720, 228, 318, 10),
            };
        }

        public void PressKey(Keys key) => _inputHandler.PressKey(key);
        public void ReleaseKey(Keys key) => _inputHandler.ReleaseKey(key);

        private void Update()
        {
            foreach (var player in Players)
            {
                PlayerMoveUpdate(player);

                bool isGround = _physics.TryLandOnGround(player, Grounds);
                if (_inputHandler.IsKeyPressed(Keys.W) && isGround)
                {
                    player.VelocityY += player.JumpForce;
                }

                if (!isGround)
                {
                    _physics.ApplyGravity(player);
                }
               
                player.UpdatePosition();
            }
            Updated.Invoke();
        }

        private void PlayerMoveUpdate(Player player)
        {
            bool isLeft = _inputHandler.IsKeyPressed(Keys.A);
            bool isRight = _inputHandler.IsKeyPressed(Keys.D);
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
    }
}
