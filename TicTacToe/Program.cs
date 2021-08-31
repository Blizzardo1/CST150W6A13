using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TicTacToe
{

    public enum MessageResult
    {
        Ok = 1,
        Cancel = 2,
        Abort = 3,
        Retry = 4,
        Ignore = 5,
        Yes = 6,
        No = 7,
        Close = 8,
        Help = 9,
        TryAgain = 10,
        Continue = 11,
        Null = 0
    }

    [Flags]
    public enum MessageOptions
    {
        Ok = 0,
        OkCancel = 1,
        AbortRetryIgnore = 2,
        YesNoCancel = 3,
        YesNo = 4,
        RetryCancel = 5,
        CancelTryContinue = 6,
    }

    class Player
    {
        private string _name;
        private bool Active;
        private Board.Markers pMarker;

        public string PlayerName
        {
            get
            {
                return _name;
            }
        }

        public bool IsActive
        {
            get
            {
                return Active;
            }
        }

        public Board.Markers PlayerMarker
        {
            get
            {
                return pMarker;
            }
        }

        public void SetActive()
        {
            Active = true;
        }

        public void SetInactive()
        {
            Active = false;
        }

        public Player(string Name, Board.Markers marker)
        {
            _name = Name;
            pMarker = marker;
        }
    }

    class Board
    {
        private static int col = 3;
        private static int row = 3;

        private static int Spacing = 16;

        private static int[,] slots = new int[col, row];
        public bool GameEnded = false;
        
        int counter = 0;
        
        private Player player1;
        private Player player2;

        private Player CurrentPlayer
        {
            get;
            set;
        }

        public enum Markers : int
        {
            N = (int)'*',
            O = (int)'O',
            X = (int)'X'
        }

        public Board()
        {
        }

        private void Instantiate()
        {
            counter = 0;
            for (int y = 0; y < row; y++)
            {
                for (int x = 0; x < col; x++)
                {
                    slots[x, y] = (int)Markers.N;
                }
            }
        }

        public void PrintBoard()
        {
            //Console.CursorVisible = false;
            Console.Clear();
            for (int y = 0; y < row; y++)
            {
                for (int x = 0; x < col; x++)
                {
                    SCAP(x, y, ((Markers)slots[x, y]));
                }
            }
            Console.SetCursorPosition(Spacing, 5);
            Console.Write("Player: [Row] [Column]");
            Console.SetCursorPosition(Spacing, 6);
        }

        public bool UpdateBoard(int x, int y, Markers c)
        {
            if (slots[x, y] == (int)Markers.N)
                slots[x, y] = (int)c;
            else
            {
                Console.SetCursorPosition(Spacing,7);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("That space has already been taken! Press any key to continue...");
                Console.ReadKey(true);
                Console.ResetColor();
                return false;
            }
            PrintBoard();
            return true;
        }

        private void DrawLocation(int x, int y, Markers marker)
        {
            Console.SetCursorPosition(Spacing - 6 + y + 2, x + 4);
            Console.BackgroundColor = ConsoleColor.Green;

            if (y == 0)
                Console.Write("{0}{0}{0}", (char)((int)marker));
            else
                Console.Write((char)((int)marker));
            Console.ResetColor();
        }

        string Async()
        {
            string s = string.Empty;
            while (s != "\r" )
            {

                //while (!Console.KeyAvailable)
                    Thread.Sleep(5);
                s = Console.ReadKey().KeyChar.ToString();
                //s = Console.ReadLine();

                PrintBoard();
                try
                {
                    int x = int.Parse(s.Split(' ')[0]);
                    int y = int.Parse(s.Split(' ')[1]);
                    DrawLocation(x, y, CurrentPlayer.PlayerMarker);
                }
                catch (Exception)
                {
                    try
                    {
                        int x = int.Parse(s);
                        DrawLocation(x, 0, CurrentPlayer.PlayerMarker);
                    }
                    catch (Exception)
                    {

                    }
                }
                Thread.Sleep(1);
            }
            return s;
        }

        private void SCAP(int x, int y, Markers c)
        {
            Console.SetCursorPosition(Spacing - 6, 3);
            Console.WriteLine("- 012");
            Console.SetCursorPosition(Spacing - 6, 4);
            Console.WriteLine("0");
            Console.SetCursorPosition(Spacing - 6, 5);
            Console.WriteLine("1");
            Console.SetCursorPosition(Spacing - 6, 6);
            Console.WriteLine("2");

            Console.SetCursorPosition(Spacing - 6 + y + 2, x + 4);
            Console.Write((char)((int)c));

            Console.SetCursorPosition(Spacing, 4);
            Console.Write("Pieces on board: {0}", counter);
            Console.SetCursorPosition(0, 0);
        }

        private MessageResult MessageBox(string text, MessageOptions Options)
        {
            Console.Write(text);
            string response;

            switch (Options)
            {
                case MessageOptions.AbortRetryIgnore:

                    Console.Write(" [Abort/Retry/Ignore] ");
                    response = Console.ReadLine();

                    if (response == "Abort") return MessageResult.Abort;
                    else if (response == "Retry") return MessageResult.Retry;
                    else if (response == "Ignore") return MessageResult.Ignore;

                    return MessageBox(text, Options);

                case MessageOptions.CancelTryContinue:
                    Console.WriteLine(" [Cancel/Try/Continue] ");
                    response = Console.ReadLine();

                    if (response == "Cancel") return MessageResult.Cancel;
                    else if (response == "Try") return MessageResult.TryAgain;
                    else if (response == "Continue") return MessageResult.Continue;
                    return MessageBox(text, Options);

                case MessageOptions.Ok:

                    Console.Write(" [Ok] ");
                    response = "Ok";
                    Console.ReadLine();
                    return MessageResult.Ok;

                case MessageOptions.OkCancel:

                    Console.Write(" [Ok/Cancel] ");
                    response = Console.ReadLine();

                    if (response == "Ok") return MessageResult.Ok;
                    else if (response == "Cancel") return MessageResult.Cancel;
                    return MessageBox(text, Options);

                case MessageOptions.RetryCancel:

                    Console.Write(" [Retry/Cancel] ");
                    response = Console.ReadLine();

                    if (response == "Retry") return MessageResult.Retry;
                    else if (response == "Cancel") return MessageResult.Cancel;
                    return MessageBox(text, Options);

                case MessageOptions.YesNo:

                    Console.Write(" [Yes/No] ");
                    response = Console.ReadLine();

                    if (response == "Yes") return MessageResult.Yes;
                    else if (response == "No") return MessageResult.No;
                    return MessageBox(text, Options);
                    
                case MessageOptions.YesNoCancel:

                    Console.Write(" [Yes/No/Cancel] ");
                    response = Console.ReadLine();

                    if (response == "Yes") return MessageResult.Yes;
                    else if (response == "No") return MessageResult.No;
                    else if (response == "Cancel") return MessageResult.Cancel;
                    return MessageBox(text, Options);
                    
            }
            
            return MessageResult.Null;
        }
        

        private bool CheckGame()
        {

            int [,] Winners = {
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
                if ((Winners[i, 0] == (int)CurrentPlayer.PlayerMarker) && (Winners[i, 1] == (int)CurrentPlayer.PlayerMarker) && (Winners[i, 2] == (int)CurrentPlayer.PlayerMarker))
                {
                    Console.WriteLine("{0} Wins!", CurrentPlayer.PlayerName);
                    return true;
                }
                else
                {
                    if (counter >= 8)
                    {
                        Console.WriteLine("Stalemate!");
                        Ask();
                    }
                }
            }
            return false;
        }

        public void SetPlayers(Player Player1, Player Player2)
        {
            player1 = Player1;
            player2 = Player2;
        }

        public void NewGame()
        {
            Instantiate();
            string input = string.Empty;

            Console.Clear();
            Console.SetCursorPosition(Spacing, 8);
            Console.Write("Player 1: ");
            Player p1 = new Player(Console.ReadLine(), Markers.O);

            Console.Clear();
            Console.SetCursorPosition(Spacing, 8);
            Console.Write("Player 2: ");
            Player p2 = new Player(Console.ReadLine(), Markers.X);

            SetPlayers(p1, p2);
            p1.SetActive();
            CurrentPlayer = p1;

            while (!GameEnded)
            {
                PrintBoard();
                Console.Write("{0} : ", CurrentPlayer.PlayerName);
                
                /*(new Thread(new ThreadStart(delegate
                {
                    input = Async();
                }))).Start();
                */

                input = Console.ReadLine();

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
            MessageResult mr = MessageBox("Do you want to start a new Game?", MessageOptions.YesNo);
            if(mr == MessageResult.Yes)
            {
                NewGame();
            }
            else if(mr == MessageResult.No)
            {
                Environment.Exit(0);
            }
        }

        static void Main(string[] args)
        {
            Board b = new Board();
            b.NewGame();

            //Console.ReadKey(true);
        }
    }
}
