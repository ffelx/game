using Game.Model;
using System;

namespace game.Model
{
    internal class BotController
    {
        private GameModel _gameModel;
        private Player _bot;
        private Player _target;
        private Random _random = new Random();
        private Physics _physics;

        private float _decisionTimeCoefficient = 0.001f;
        private float _decisionTime;
        private float _currentDecisionTime;

        private float _centerX;

        public BotController(GameModel gameModel, Physics physics)
        {
            _gameModel = gameModel;
            _bot = gameModel.Players[1]; 
            _target = gameModel.Players[0]; 
            _physics = physics;
            _centerX = GameModel.Width / 2;
        }

        public void Update()
        {
            float deltaTime = GameLoop.Interval / 1000f;
            _currentDecisionTime += deltaTime;
            
            if (_currentDecisionTime >= _decisionTime)
            {
                MakeDecision();
                _currentDecisionTime = 0;
                _decisionTime = _random.Next(2, 5) * _decisionTimeCoefficient;
            }

            //var currentDistance = Math.Abs(_target.X - _bot.X);
            //if (Math.Abs(_bot.VelocityX) > _bot.MaxSpeed
            //    || _oldDistanceToPlayer < currentDistance)
            //{
            //    //_bot.Stop();
            //    _oldDistanceToPlayer = currentDistance;
            //}

            _bot.Shoot();
            UpdateBotMovement();
        }

        private void MakeDecision()
        {
            bool isGround = _physics.TryLandOnGround(_bot, _gameModel.Grounds, out var ground);

            var newDirection = _target.X > _bot.X ?
                Player.Direction.Right :
                Player.Direction.Left;

            if (newDirection != _bot.CurrentDirection)
            {
                _bot.CurrentDirection = newDirection;
                _bot.VelocityX = 0; 
            }

            _bot.CurrentDirection = newDirection;

           
                _bot.VelocityX += _centerX > _bot.X ? _bot.Acceleration : -_bot.Acceleration;

            
            if (isGround && _random.NextDouble() < 0.1)
            {
                _bot.Jump();
            }

            if (_random.NextDouble() < 0.1
                && isGround
                && ground.CanDropDown
                && _bot.Y > _target.Y
                && !_bot.IgnorePlatformCollision)
            {
                _bot.StartDropDown();
            }

            if (_bot.IgnorePlatformCollision)
            {
                _bot.UpdateDropDown(GameLoop.Interval / 1000f);
            }

        }

        private void UpdateBotMovement()
        {
            bool isGround = _physics.TryLandOnGround(_bot, _gameModel.Grounds, out var ground);
            if (!isGround)
            {
                _physics.ApplyGravity(_bot);
            }
            _bot.UpdatePosition();
        }
    }
}
