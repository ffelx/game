using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game.Model
{

    public class GameLoop
    {
        private Timer _timer;

        public event Action Tick;

        public GameLoop()
        {
            _timer = new Timer { Interval = 17 };
            _timer.Tick += (s, e) => Tick?.Invoke();
        }

        public void Start() => _timer.Start();
        public void Stop() => _timer.Stop();
    }

}
