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

    internal static class Program
    {
        private static void Main(string[] args)
        {
            Board b = new Board();
            b.NewGame();

            //Console.ReadKey(true);
        }
    }
}
