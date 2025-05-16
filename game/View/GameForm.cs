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

        public GameForm()
        {
            DoubleBuffered = true;
            Width = 1280;
            Height = 720;

            _game = new GameModel();

            _textures = new TextureManager();
            _renderer = new GameRenderer(_textures);

            _game.Updated += Update;
            SetKeys();
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
            float offsetX = (ClientSize.Width - GameModel.Width * scale) / 2;
            float offsetY = (ClientSize.Height - GameModel.Height * scale) / 2;

            _renderer.Render(g, _game, scale, offsetX, offsetY);

            stopwatch.Stop();
            _lastFrameTimeMs = stopwatch.ElapsedMilliseconds;

            g.ResetTransform();
            g.DrawString($"Frame time: {_lastFrameTimeMs} ms", _debugFont, Brushes.White, new PointF(10, 10));
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
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            _textures.Dispose();
        }

        
    }
}
