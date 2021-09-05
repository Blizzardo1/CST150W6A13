using CST150W6A13;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CST150W6A13
{
    public class Board : IGameObject
    {
        private const int BorderSize = 7;
        private const int Columns = 3;
        private const int Rows = 3;

        private static int Spacing = 16;

        private static Square[,] slots = new Square[Columns, Rows];
        private bool _gameEnded = false;

        public bool GameEnded => _gameEnded;

        private BufferedGraphics bg;
        private Graphics g;

        int counter = 0;

        private Player player1;
        private Player player2;

        public Player CurrentPlayer
        {
            get;
            private set;
        }

        public Board(Graphics context)
        {
            bg = BufferedGraphicsManager.Current.Allocate(context, new Rectangle(0, 0, Columns * Square.Size, Rows * Square.Size));
            g = bg.Graphics;
        }

        public void Initialize()
        {
            counter = 0;
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    slots[x, y] = new(bg.Graphics);
                    slots[x, y].Initialize();
                    slots[x, y].SetMarker(Marker.N);
                }
            }
            player1 = new ("Player 1", Marker.O,1);
            player2 = new ("Player 2", Marker.X,2);
            player1.SetActive();
            CurrentPlayer = player1;
        }

        private string sqMsg;
        private Font font = new("Arial", 12f);
        public void Draw(TimeSpan deltaTime)
        {
            for (int y = 0; y < Rows; y += Square.Size + (BorderSize / 2))
            {
                for (int x = 0; x < Columns; x += Square.Size + (BorderSize / 2))
                {
                    // Draw Routine, check for graphics leaks
                    sqMsg = $"{x}, {y}";
                    var sqsz = g.MeasureString(sqMsg, font);
                    slots[x, y].Draw(deltaTime);
                    g.DrawString(sqMsg, font, Brushes.White, new PointF(((x + Square.Size) / 2) - (sqsz.Width / 2), ((y + Square.Size) / 2) - (sqsz.Height / 2)));
                }
            }
            g.DrawString($"{CurrentPlayer.PlayerName}'s turn", font, Brushes.White, new PointF(0, 0));
        }

        public void Update(TimeSpan deltaTime)
        {
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    if (!Cursor.Clip.IntersectsWith(slots[x, y]) && !slots[x,y].Clicked)
                        continue;

                    if (!UpdateBoard(x, y, CurrentPlayer.PlayerMarker))
                        continue;

                    if (!CheckGame())
                    {
                        counter++;
                        if (player1.IsActive)
                        {
                            player2.SetActive();
                            player1.SetInactive();
                            CurrentPlayer = player2;
                        }
                        else if (player2.IsActive)
                        {
                            player1.SetActive();
                            player2.SetInactive();
                            CurrentPlayer = player1;
                        }
                    }
                }
            }
        }

        public bool UpdateBoard(int x, int y, Marker marker)
        {
            if (slots[x, y].Marker != Marker.N)
                return false;

            slots[x, y].SetMarker(marker);
            return true;
        }

        private bool CheckGame()
        {

            Square[,] Winners = {
                {slots[0,0], slots[0,1], slots[0,2]},
                {slots[1,0], slots[1,1], slots[1,2]},
                {slots[2,0], slots[2,1], slots[2,2]},
                {slots[0,0], slots[1,0], slots[2,0]},
                {slots[0,1], slots[1,1], slots[2,1]},
                {slots[0,2], slots[1,2], slots[2,2]},
                {slots[0,0], slots[1,1], slots[2,2]},
                {slots[0,2], slots[1,1], slots[2,0]}
                                 };

            // All the possibilities of a match! If none match, it's a Stalemate and then will break.
            int tRow = 8;
            /* 0,0 0,1 0,2 *
             * 0,0 1,0 2,0 *
             * 0,0 1,1 2,2 *
             * 0,1 1,1 2,1 *
             * 0,2 1,2 2,2 *
             * 1,0 1,1 1,2 *
             * 2,0 2,1 2,2 *
             * 2,0 1,1 0,2 */

            // Begin the list of Possibilities
            for (int i = 0; i < tRow; i++)
            {
                if ((Winners[i, 0].Marker == CurrentPlayer.PlayerMarker) && (Winners[i, 1].Marker == CurrentPlayer.PlayerMarker) && (Winners[i, 2].Marker == CurrentPlayer.PlayerMarker))
                {
                    MessageBox.Show($"{CurrentPlayer.PlayerName} Wins!");
                    return true;
                }
                else
                {
                    if (counter >= 8)
                    {
                        MessageBox.Show("Stalemate!");
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
