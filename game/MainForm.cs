using game.Model;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;


namespace game
{
    internal class MainForm : Form
    {
        private GameModel _game;
        public MainForm()
        {
            DoubleBuffered = true;
            Width = 1280;
            Height = 720;
            _game = new GameModel();
            _game.Updated += Update;

            KeyPreview = true;
            KeyDown += (s, e) => _game.PressKey(e.KeyCode);
            KeyUp += (s, e) => _game.ReleaseKey(e.KeyCode);

            MouseMove += OnMouseMove; //
        }

        private void DrawGame(Graphics g)
        {
            foreach (var ground in _game.Grounds)
            {
                g.FillRectangle(Brushes.Red, (float)ground.X, (float)ground.Y, (float)ground.Width, (float)ground.Height);
            }

            foreach (var player in _game.Players)
            {
                g.FillRectangle(Brushes.Blue, (float)player.X, (float)player.Y, player.Width, player.Height);
            }

            Coords(g);
        }

        void Update()
        {
            Invalidate();
        }

        private float GetScale()
        {
            float scaleX = (float)ClientSize.Width / GameModel.Width;
            float scaleY = (float)ClientSize.Height / GameModel.Height;
            return Math.Min(scaleX, scaleY);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;

            float scale = GetScale();
            float offsetX = (ClientSize.Width - GameModel.Width * scale) / 2;
            float offsetY = (ClientSize.Height - GameModel.Height * scale) / 2;

            g.TranslateTransform(offsetX, offsetY + GameModel.Height * scale);
            g.ScaleTransform(scale, -scale);

            DrawGame(g);
        }

        public static void Main()
        {
            Application.Run(new MainForm());
        }


        private PointF mouseModelCoords;
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            float scale = GetScale();
            float offsetX = (ClientSize.Width - GameModel.Width * scale) / 2;
            float offsetY = (ClientSize.Height - GameModel.Height * scale) / 2;

            float x = (e.X - offsetX) / scale;
            float y = GameModel.Height - (e.Y - offsetY) / scale;

            mouseModelCoords = new PointF(x, y);
            Invalidate(); 
        }


        private void Coords(Graphics g)
        {
            g.ResetTransform(); 
            g.DrawString(
                $"Cursor: X={mouseModelCoords.X:F1}, Y={mouseModelCoords.Y:F1}",
                new Font("Arial", 12),
                Brushes.Black,
                new PointF(10, 10)
            );
        }
    }
}
