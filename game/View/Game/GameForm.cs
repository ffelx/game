using System;
using System.Drawing;
using System.Windows.Forms;
using Game.Model;
using Game.View;

namespace Game
{
    public class GameForm : Form
    {
        public GameForm()
        {
            InitializeForm();
        }

        private void InitializeForm()
        {
            DoubleBuffered = true;
            KeyPreview = true;
            ClientSize = new Size(1280, 720);
            StartPosition = FormStartPosition.CenterScreen;
        }
    }
}
