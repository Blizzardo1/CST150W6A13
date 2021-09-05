using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CST150W6A13
{
    public class Game
    {
        private Control parent;
        private Graphics g;
        private Board board;
        private DateTime _lastTime;  // The beginning
        private int _framesRendered; // Frame counter
        private int _fps;            // Frames per Second

        public int FramesPerSecond => _fps;

        private bool _gameStarted;

        public Game(Control parent)
        {
            this.parent = parent;
            g = parent.CreateGraphics();
            board = new Board(g);
        }

        private void Loop()
        {
            _lastTime = DateTime.Now;
            board.Initialize();
            while(_gameStarted)
            {
                _framesRendered++;
                var delta = DateTime.Now - _lastTime;
                if(delta.TotalSeconds >=1)
                {
                    _fps = _framesRendered;
                    _framesRendered = 0;
                    _lastTime = DateTime.Now;
                }
                board.Draw(delta);
                board.Update(delta);
                Application.DoEvents();
            }
        }

        public void Start()
        {
            _gameStarted = true;
            Task.Run(() => Loop()).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
