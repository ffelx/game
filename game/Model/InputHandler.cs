using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game.Model
{
    internal class InputHandler
    {
        private HashSet<Keys> _pressedKeys = new HashSet<Keys>();
        public void PressKey(Keys key) => _pressedKeys.Add(key);
        public void ReleaseKey(Keys key) => _pressedKeys.Remove(key);
        public bool IsKeyPressed(Keys key) => _pressedKeys.Contains(key);
    }
}
