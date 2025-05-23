using System;
using System.Drawing;
using System.Windows.Forms;
using Game.Model;
using Game.View;

namespace Game
{
    internal class GameForm : Form
    {
        private GameModel _game;
        private GameRenderer _renderer;
        private TextureManager _textures;

        private long _lastFrameTimeMs = 0;
        private Font _debugFont = new Font("Arial", 12);

        private int _winPlayer = 0;

        public GameForm(bool isGameBot = false)
        {
            DoubleBuffered = true;
            
            InitializeForm();

            _game = new GameModel(isGameBot);
            _textures = new TextureManager();
            _renderer = new GameRenderer(_textures);

            _game.Updated += Update;
            SetKeys();
        }

        private void InitializeForm()
        {
            Size = new Size(1280, 720);
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void SetKeys()
        {
            KeyPreview = true;
            KeyDown += (s, e) => _game.PressKey(e.KeyCode);
            KeyUp += (s, e) => _game.ReleaseKey(e.KeyCode);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            base.OnPaint(e);

            var g = e.Graphics;
            g.Clear(Color.Black);

            float scale = GetScale();
            _renderer.Render(g, _game, this, scale);

            RenderScore(g);

            /////////
            stopwatch.Stop();
            _lastFrameTimeMs = stopwatch.ElapsedMilliseconds;

            g.ResetTransform();
            g.DrawString($"Frame time: {_lastFrameTimeMs} ms", _debugFont, Brushes.White, new PointF(10, 10));
        }

        private void RenderScore(Graphics g)
        {
            using (var scoreFont = new Font("Arial", 24, FontStyle.Bold))
            {
                string scoreText = $"{_game.FirstPlayerScore} : {_game.SecondPlayerScore}";
                var textSize = g.MeasureString(scoreText, scoreFont);
                var originalTransform = g.Transform;

                g.ResetTransform();

                float scoreX = (ClientSize.Width - textSize.Width) / 2;
                float scoreY = 0; 

                g.DrawString(scoreText, scoreFont, Brushes.Black, scoreX + 2, scoreY + 2);
                g.DrawString(scoreText, scoreFont, Brushes.White, scoreX, scoreY);

                g.Transform = originalTransform;
            }
        }

        private float GetScale()
        {
            float scaleX = (float)ClientSize.Width / GameModel.Width;
            float scaleY = (float)ClientSize.Height / GameModel.Height;
            return Math.Min(scaleX, scaleY);
        }

        void Update()
        {
            Invalidate();
            if (_game.IsGameOver(out int winPlayer))
            {
                _winPlayer = winPlayer;
                Close();
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            _textures.Dispose();

            if (_winPlayer != 0)
            {
                var gameEndForm = new GameEndForm(_winPlayer);  
                gameEndForm.Width = Width;
                gameEndForm.Height = Height;
                Program.Context.MainForm = gameEndForm;
                gameEndForm.Show();
            }
        }
    }
}
