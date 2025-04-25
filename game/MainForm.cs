using game.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace game
{
    internal class MainForm : Form
    {
        private Panel _playerPanel;
        private GameModel _game;
        public MainForm()
        {
            _game = new GameModel();
            _game.Updated += Update;
            SetPlayer();
            SetGrounds();

        }

        public void SetPlayer()
        {
            var player = _game.Players[0];
            _playerPanel = new Panel();
            _playerPanel.BackColor = Color.Blue;
            _playerPanel.Size = new Size(player.Width, player.Height);
            _playerPanel.Location = new Point((int)player.X, (int)player.Y);
            Controls.Add(_playerPanel);
        }
        public void SetGrounds()
        {
            foreach (var ground in _game.Grounds)
            {
                var groundPanel = new Panel();
                groundPanel.BackColor = Color.Red;
                groundPanel.Size = new Size((int)ground.Width, (int)ground.Height);
                groundPanel.Location = new Point((int)ground.X, (int)ground.Y);
                Controls.Add(groundPanel);
            }
        }

        void Update()
        {
            var player = _game.Players[0];
            _playerPanel.Location = new Point((int)player.X,(int)player.Y);
        }

        public static void Main()
        {
            Application.Run(new MainForm());
        }
    }
}
