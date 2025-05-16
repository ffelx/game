using System.Drawing;
using game.Model;

namespace game.View
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

            GameObjectsRenderer(g, game);
        }

        private void GameObjectsRenderer(Graphics g, GameModel game)
        {
            foreach (var ground in game.Grounds)
            {
                //g.FillRectangle(Brushes.Red, ground.X, ground.Y, ground.Width, ground.Height);
            }

            foreach (var player in game.Players)
            {
                var texture = _textures.GetPlayerTexture(player.CurrentDirection);
                g.DrawImage(texture, (float)player.X, (float)player.Y, player.Width, player.Height);
            }
        }
    }
}
