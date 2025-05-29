using Game.View;
using Game;

namespace game.Controller
{
    internal class NewGameController
    {
        private NewGameForm _view;
        private bool _isBotMode;

        public NewGameController(NewGameForm view)
        {
            _view = view;
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _view.OnTwoPlayersClicked += () => StartGame(isBot: false);
            _view.OnBotGameClicked += () => StartGame(isBot: true);
        }

        private void StartGame(bool isBot)
        {
            _isBotMode = isBot;

            var gameForm = new GameForm();
            var gameController = new GameController(gameForm, _isBotMode);
            gameForm.Size = _view.Size;
            _view.Hide();
            gameForm.ShowDialog(); 
            _view.Close(); 
        }
    }
}
