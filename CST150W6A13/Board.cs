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
            g = context;
        }

        public void Initialize()
        {
            counter = 0;
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Columns; x++)
                {
                    slots[x, y].SetMarker(Marker.N);
                }
            }
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
                    g.FillRectangle(Brushes.Black, new(x, y, Square.Size, Square.Size));
                    g.DrawString(sqMsg, font, Brushes.White, new PointF(((x + Square.Size) / 2) - (sqsz.Width / 2), ((y + Square.Size) / 2) - (sqsz.Height / 2)));
                }
            }
        }

        public void Update(TimeSpan deltaTime)
        {

        }

        public bool UpdateBoard(int x, int y, Marker marker)
        {
            if (slots[x, y].Marker == Marker.N)
            {
                slots[x, y].SetMarker(marker);
            }
            else
            {
                Console.SetCursorPosition(Spacing, 7);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("That space has already been taken! Press any key to continue...");
                Console.ReadKey(true);
                Console.ResetColor();
                return false;
            }
            return true;
        }

        private void DrawLocation(int x, int y, Marker marker)
        {
            Console.SetCursorPosition(Spacing - 6 + y + 2, x + 4);
            Console.BackgroundColor = ConsoleColor.Green;

            if (y == 0)
                Console.Write("{0}{0}{0}", (char)((int)marker));
            else
                Console.Write((char)((int)marker));
            Console.ResetColor();
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
                        Ask();
                    }
                }
            }
            return false;
        }

        public void NewGame()
        {
            /// Remove block, Create form to create two players
            // TODO: Form creation; replace block, eventually clearing this entire method out in favor of new design
            Console.Clear();
            Console.SetCursorPosition(Spacing, 8);
            Console.Write("Player 1: ");
            Player p1 = new(Console.ReadLine(), Marker.O);

            Console.Clear();
            Console.SetCursorPosition(Spacing, 8);
            Console.Write("Player 2: ");
            Player p2 = new(Console.ReadLine(), Marker.X);
            ///

            p1.SetActive();
            CurrentPlayer = p1;

            while (!_gameEnded)
            {
                
                Console.Write("{0} : ", CurrentPlayer.PlayerName);

                string input = Console.ReadLine();
                try
                {
                    int x = int.Parse(input.Split(' ')[0]);
                    int y = int.Parse(input.Split(' ')[1]);
                    if (UpdateBoard(x, y, CurrentPlayer.PlayerMarker))
                        if (!CheckGame())
                        {
                            counter++;
                            if (p1.IsActive)
                            {
                                p2.SetActive();
                                p1.SetInactive();
                                CurrentPlayer = p2;
                            }
                            else if (p2.IsActive)
                            {
                                p1.SetActive();
                                p2.SetInactive();
                                CurrentPlayer = p1;
                            }
                        }
                        else
                            break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Input wasn't recognised! Rows and Columns are either 0, 1, or 2");
                }
            }

            Ask();
        }

        public void Ask()
        {
            DialogResult mr = MessageBox.Show("Do you want to start a new Game?", "Start New?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (mr == DialogResult.Yes)
            {
                NewGame();
            }
            else if (mr == DialogResult.No)
            {
                Environment.Exit(0);
            }
        }


    }
}
