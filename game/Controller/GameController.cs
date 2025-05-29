using Game.Model;
using Game.View;
using Game;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace game.Controller
{
    public class GameController
    {
        private GameForm _view;
        private GameModel _model;
        private TextureManager _textures;
        private GameRenderer _renderer;

        public GameController(GameForm view, bool isBotMode)
        {
            _view = view;
            _model = new GameModel(isBotMode);
            _textures = new TextureManager();
            _renderer = new GameRenderer(_textures);

            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _view.KeyDown += (s, e) => _model.PressKey(e.KeyCode);
            _view.KeyUp += (s, e) => _model.ReleaseKey(e.KeyCode);
            _view.Paint += RenderGame;
            _model.Updated += () => _view.Invalidate();

            _view.FormClosed += (s, e) => HandleGameClosed();
            _model.GameOver += HandleGameOver;
        }

        private void RenderGame(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            float scale = Math.Min(
                (float)_view.ClientSize.Width / GameModel.Width,
                (float)_view.ClientSize.Height / GameModel.Height
            );

            _renderer.Render(e.Graphics, _model, _view, scale);
            RenderScore(e.Graphics);
        }

        private void RenderScore(Graphics g)
        {
            using (var scoreFont = new Font("Arial", 24, FontStyle.Bold))
            {
                string scoreText = $"{_model.FirstPlayerScore} : {_model.SecondPlayerScore}";
                var textSize = g.MeasureString(scoreText, scoreFont);
                var originalTransform = g.Transform;

                g.ResetTransform();

                float scoreX = (_view.ClientSize.Width - textSize.Width) / 2;
                float scoreY = 0;

                g.DrawString(scoreText, scoreFont, Brushes.Black, scoreX + 2, scoreY + 2);
                g.DrawString(scoreText, scoreFont, Brushes.White, scoreX, scoreY);

                g.Transform = originalTransform;
            }
        }

        private void HandleGameOver(int winner)
        {
            _view.Invoke((MethodInvoker)delegate
            {
                var endForm = new GameEndForm(winner);
                var controller = new GameEndController(endForm);
                endForm.Size = _view.Size;
                _view.Hide();
                endForm.ShowDialog();
                _view.Close();
            });
        }

        private void HandleGameClosed()
        {
            _textures.Dispose();
        }
    }
}
