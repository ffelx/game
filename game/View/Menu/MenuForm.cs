using System;
using System.Drawing;
using System.Windows.Forms;

namespace Game.View
{
    public class MenuForm : Form
    {
        public event Action OnNewGameClicked;
        public event Action OnExitClicked;

        public MenuForm()
        {
            InitializeForm();
            InitializeControls();
        }

        private void InitializeForm()
        {
            Size = new Size(1280, 720);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(195, 195, 196);
        }

        private void InitializeControls()
        {
            var containerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.FromArgb(0, 0, 0, 0),
            };
            this.Controls.Add(containerPanel);

            var buttonTable = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 1,
                RowCount = 2
            };

            buttonTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));
            buttonTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));

            var btnNewGame = CreateMenuButton("Новая игра", Color.LightSkyBlue);
            btnNewGame.Click += (s, e) => OnNewGameClicked?.Invoke(); 
            buttonTable.Controls.Add(btnNewGame, 0, 0);

            var btnExit = CreateMenuButton("Выход", Color.LightCoral);
            btnExit.Click += (s, e) => OnExitClicked?.Invoke(); 
            buttonTable.Controls.Add(btnExit, 0, 1);

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