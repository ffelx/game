using Game.View;

namespace game.Controller
{
    internal class MenuController
    {
        private MenuForm _menuView;

        public MenuController(MenuForm menuView)
        {
            _menuView = menuView;
            _menuView.OnNewGameClicked += StartNewGame;
            _menuView.OnExitClicked += ExitGame;
        }

        private void StartNewGame()
        {
            var newGameForm = new NewGameForm();
            var newGameController = new NewGameController(newGameForm);
            newGameForm.Size = _menuView.Size;
            _menuView.Hide(); 
            newGameForm.ShowDialog(); 
            _menuView.Close(); 
        }

        private void ExitGame()
        {
            _menuView.Close(); 
        }
    }
}
