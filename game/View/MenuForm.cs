using System;
using System.Drawing;
using System.Windows.Forms;

namespace Game.View
{
    internal class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeForm();
            InitializeControls();
        }

        private void InitializeForm()
        {
            this.Text = "Game Menu";
            this.Size = new Size(1280, 720);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(350, 300);
            this.BackColor = Color.FromArgb(240, 240, 240);
            this.FormBorderStyle = FormBorderStyle.Sizable;
        }

        private void InitializeControls()
        {
            var containerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };
            this.Controls.Add(containerPanel);

            var buttonTable = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 1,
                RowCount = 3
            };

            buttonTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));
            buttonTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));
            buttonTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));

            var btnNewGame = CreateMenuButton("НОВАЯ ИГРА", Color.LightSkyBlue);
            btnNewGame.Click += (s, e) => MessageBox.Show("Новая игра!");
            buttonTable.Controls.Add(btnNewGame, 0, 0);

            var btnSettings = CreateMenuButton("НАСТРОЙКИ", Color.LightGray);
            btnSettings.Click += (s, e) => MessageBox.Show("Настройки");
            buttonTable.Controls.Add(btnSettings, 0, 1);

            var btnExit = CreateMenuButton("ВЫХОД", Color.LightCoral);
            btnExit.Click += (s, e) => this.Close();
            buttonTable.Controls.Add(btnExit, 0, 2);

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