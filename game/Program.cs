using Game.View;
using System;
using System.Windows.Forms;
using game.Controller;

namespace Game
{
    internal class Program
    {
        public static ApplicationContext Context { get; set; }

        [STAThread]
        public static void Main()
        {
            Context = new ApplicationContext();
            Context.MainForm = new MenuForm();

            var menuForm = new MenuForm();
            var menuController = new MenuController(menuForm);

            Application.Run(menuForm);
        }
    }
}
