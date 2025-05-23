using System;
using System.Windows.Forms;

namespace Game.Model
{
    public class GameLoop
    {
        public const int Interval = 15;
        private Timer _timer;
        public event Action Tick;

        public GameLoop()
        {
            _timer = new Timer { Interval = Interval };
            _timer.Tick += (s, e) => Tick?.Invoke();
        }

        public void Start() => _timer.Start();
        public void Stop() => _timer.Stop();
    }

}
