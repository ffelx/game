using System;
using System.Drawing;
using System.Windows.Forms;

namespace Game
{
    public class GameEndForm : Form
    {
        public event Action OnMenuClicked;
        public GameEndForm(int winnerNumber)
        {
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(195, 195, 196);
            
            var menuButton = new Button
            {
                Text = "Главное меню",
                Size = new Size(150, 40),
                Location = new Point(10, 10), 
                FlatStyle = FlatStyle.Flat,
                
            };
            menuButton.FlatAppearance.BorderSize = 0;
            menuButton.Click += (s, e) => OnMenuClicked?.Invoke();

            var titleLabel = new Label
            {
                Text = $"Игрок {winnerNumber} победил!",
                Font = new Font("Arial", 24, FontStyle.Bold),
                BackColor = Color.FromArgb(0,0, 0, 0),
                AutoSize = true 
            };
        
            titleLabel.Location = new Point(
                (ClientSize.Width - titleLabel.Width) / 2,
                (ClientSize.Height - titleLabel.Height) / 2
            );
          
            this.Resize += (s, e) =>
            {
                titleLabel.Location = new Point(
                    (ClientSize.Width - titleLabel.Width) / 2,
                    (ClientSize.Height - titleLabel.Height) / 2
                );
            };

            Controls.Add(titleLabel);
            Controls.Add(menuButton);
        }
    }
}