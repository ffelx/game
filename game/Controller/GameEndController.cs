using Game;
using Game.View;

namespace game.Controller
{
    internal class GameEndController
    {
        private GameEndForm _gameEndView;

        public GameEndController(GameEndForm gameEndView)
        {
            _gameEndView = gameEndView;
            _gameEndView.OnMenuClicked += ReturnToMenu;
        }

        private void ReturnToMenu()
        {
            var menuForm = new MenuForm();
            var menuController = new MenuController(menuForm);
            menuForm.Size = _gameEndView.Size;
            _gameEndView.Hide();
            menuForm.ShowDialog();
            _gameEndView.Close();
        }
    }
}
