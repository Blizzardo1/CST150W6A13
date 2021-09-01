using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST150W6A13
{

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
}
