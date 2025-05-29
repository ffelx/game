using System.Collections.Generic;
using System.Windows.Forms;

namespace Game.Model
{
    internal class InputHandler
    {
        private HashSet<Keys> _pressedKeysPlayer1 = new HashSet<Keys>();
        private HashSet<Keys> _pressedKeysPlayer2 = new HashSet<Keys>();

        public void PressKey(Keys key)
        {
            if (IsPlayer1Key(key))
            {
                _pressedKeysPlayer1.Add(key);
            }
            else if (IsPlayer2Key(key))
            {
                _pressedKeysPlayer2.Add(key);
            }
        }
        public void ReleaseKey(Keys key)
        {
            if (IsPlayer1Key(key))
            {
                _pressedKeysPlayer1.Remove(key);
            }
            else if (IsPlayer2Key(key))
            {
                _pressedKeysPlayer2.Remove(key);
            }
        }
        public bool IsKeyPressed(Keys key, int playerNumber)
        {
            return playerNumber == 1
                ? _pressedKeysPlayer1.Contains(key)
                : _pressedKeysPlayer2.Contains(key);
        }

        private bool IsPlayer1Key(Keys key)
        {
            return key == Keys.W 
                || key == Keys.A 
                || key == Keys.D 
                || key == Keys.S 
                || key == Keys.Space 
                || key == Keys.E;
        }

        private bool IsPlayer2Key(Keys key)
        {
            return key == Keys.Up 
                || key == Keys.Left 
                || key == Keys.Right 
                || key == Keys.Down 
                || key == Keys.ControlKey 
                || key == Keys.Enter;
        }
    }
}
