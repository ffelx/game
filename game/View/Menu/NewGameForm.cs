using System;
using System.Drawing;
using System.Windows.Forms;

namespace Game.View
{
    public class NewGameForm : Form
    {
        public event Action OnTwoPlayersClicked;
        public event Action OnBotGameClicked;

        public NewGameForm()
        {
            InitializeForm();
            InitializeControls();
        }

        private void InitializeForm()
        {
            BackColor = Color.FromArgb(195, 195, 196);
            Size = new Size(1280, 720);
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void InitializeControls()
        {
            var containerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.Transparent
            };
            
            Controls.Add(containerPanel);

            var buttonTable = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 1,
                RowCount = 2,
                BackColor = Color.Transparent
            };

            buttonTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));
            buttonTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));

            var btnTwoPlayers = CreateMenuButton("Два игрока", Color.LightSkyBlue);
            btnTwoPlayers.Click += (s, e) => OnTwoPlayersClicked?.Invoke(); 
            buttonTable.Controls.Add(btnTwoPlayers, 0, 0);

            var btnBotGame = CreateMenuButton("С ботом", Color.Gray);
            btnBotGame.Click += (s, e) => OnBotGameClicked?.Invoke(); 
            buttonTable.Controls.Add(btnBotGame, 0, 1);

            containerPanel.Controls.Add(buttonTable);
            buttonTable.Location = new Point(
                (containerPanel.ClientSize.Width - buttonTable.Width) / 2,
                (containerPanel.ClientSize.Height - buttonTable.Height) / 2
            );

            containerPanel.Resize += (s, e) =>
            {
                buttonTable.Location = new Point(
                    (containerPanel.ClientSize.Width - buttonTable.Width) / 2,
                    (containerPanel.ClientSize.Height - buttonTable.Height) / 2
                );
            };
        }

        private Button CreateMenuButton(string text, Color backColor)
        {
            return new Button
            {
                Text = text,
                Size = new Size(300, 70),
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = backColor,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.None,
                Margin = new Padding(10)
            };
        }
    }
}