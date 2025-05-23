using Game.View;
using Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Remoting.Contexts;

namespace Game
{
    internal class Program
    {
        public static ApplicationContext Context { get; private set; }

        [STAThread]
        public static void Main()
        {
            Context = new ApplicationContext();
            Context.MainForm = new MenuForm();
            Application.Run(Context);
        }
    }
}
