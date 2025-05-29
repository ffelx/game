using System.Drawing;
using Game.Model;

namespace Game.View
{
    internal class GameRenderer
    {
        private readonly TextureManager _textures;
        private readonly Font _scoreFont;
        private readonly StringFormat _scoreFormat;

        public GameRenderer(TextureManager textures)
        {
            _textures = textures;
            _scoreFont = new Font("Arial", 24, FontStyle.Bold);
            _scoreFormat = new StringFormat();
        }

        public void Render(Graphics g, GameModel game, GameForm gameForm, float scale)
        {
            float offsetX = (gameForm.ClientSize.Width - GameModel.Width * scale) / 2;
            float offsetY = (gameForm.ClientSize.Height - GameModel.Height * scale) / 2;

            var background = _textures.GetBackground(scale);
            if (background != null)
            {
                g.DrawImage(background, offsetX, offsetY);
            }

            g.TranslateTransform(offsetX, offsetY + GameModel.Height * scale);
            g.ScaleTransform(scale, -scale);

            RenderGameObjects(g, game);
        }

        private void RenderGameObjects(Graphics g, GameModel game)
        {
            foreach (var player in game.Players)
            {
                var texture = _textures.GetPlayerTexture(player.CurrentDirection);
                g.DrawImage(texture, (float)player.X, (float)player.Y, player.Width, player.Height);

                RenderGunAndBullets(g, player);
            }
        }

        private void RenderGunAndBullets(Graphics g, Player player)
        {
            foreach (var bullet in player.Bullets)
            {
                g.FillRectangle(Brushes.Yellow, bullet.X, bullet.Y, bullet.Width, bullet.Height);
            }

            var gunTexture = _textures.GetGunTexture(player.CurrentDirection);

            var gunWidth = 32;
            var gunHeight = 20;

            var xOffset = 20;
            var gunX = player.X + (player.CurrentDirection == Player.Direction.Right
                    ? player.Width - gunWidth + xOffset : -xOffset);
            var gunY = player.Y + 33;

            g.DrawImage(gunTexture, gunX, gunY, gunWidth, gunHeight);
        }

        public void RenderScore(Graphics g, GameModel game, GameForm gameForm)
        {
            string scoreText = $"{game.SecondPlayerScore} : {game.FirstPlayerScore}";
            var textSize = g.MeasureString(scoreText, _scoreFont);
            var originalTransform = g.Transform;

            g.ResetTransform();

            float scoreX = (gameForm.ClientSize.Width - textSize.Width) / 2;
            float scoreY = 0;

            g.DrawString(scoreText, _scoreFont, Brushes.Black, scoreX + 2, scoreY + 2, _scoreFormat);
            g.DrawString(scoreText, _scoreFont, Brushes.White, scoreX, scoreY, _scoreFormat);

            g.Transform = originalTransform;
        }
    }
}