using System.Drawing;
using Game.Model;

namespace Game.View
{
    internal class GameRenderer
    {
        private readonly TextureManager _textures;

        public GameRenderer(TextureManager textures)
        {
            _textures = textures;
        }

        public void Render(Graphics g, GameModel game, float scale, float offsetX, float offsetY)
        {
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
            foreach (var ground in game.Grounds)
            {
               //g.FillRectangle(Brushes.Red, ground.X, ground.Y, ground.Width, ground.Height);
            }

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
    }
}
