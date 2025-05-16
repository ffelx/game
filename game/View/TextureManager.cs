using game.Model;
using System;
using System.Drawing;

namespace game.View
{
    internal class TextureManager : IDisposable
    {
        private Bitmap _playerTextureRight;
        private Bitmap _playerTextureLeft;
        private Bitmap _backgroundOriginal;
        private Bitmap _backgroundCache;
        private float _cachedScale = -1f;

        public TextureManager()
        {
            LoadTextures();
        }

        private void LoadTextures()
        {
            _playerTextureRight = new Bitmap("Images/player2.png");
            _playerTextureLeft = new Bitmap(_playerTextureRight);
            _playerTextureLeft.RotateFlip(RotateFlipType.RotateNoneFlipX);

            _backgroundOriginal = new Bitmap("Images/location.jpg");
        }

        public Bitmap GetPlayerTexture(Player.Direction direction)
        {
            return direction == Player.Direction.Left ? _playerTextureLeft : _playerTextureRight;
        }

        public Bitmap GetBackground(float scale)
        {
            if (_backgroundOriginal == null) return null;

            if (_backgroundCache != null && Math.Abs(_cachedScale - scale) < 0.01f)
            {
                return _backgroundCache;
            }

            _cachedScale = scale;

            int bmpWidth = (int)(GameModel.Width * scale);
            int bmpHeight = (int)(GameModel.Height * scale);

            _backgroundCache?.Dispose();
            _backgroundCache = new Bitmap(bmpWidth, bmpHeight);

            using (var g = Graphics.FromImage(_backgroundCache))
            {
                g.Clear(Color.Black);
                g.TranslateTransform(0, bmpHeight);
                g.ScaleTransform(scale, -scale);
                g.DrawImage(_backgroundOriginal, 0, 0, GameModel.Width, GameModel.Height);
            }

            return _backgroundCache;
        }

        public void Dispose()
        {
            _playerTextureLeft?.Dispose();
            _playerTextureRight?.Dispose();
            _backgroundOriginal?.Dispose();
            _backgroundCache?.Dispose();
        }
    }
}
