using System.Collections.Generic;

namespace GameCommons
{
    public static class DefaultVariables
    {
        public static List<string> defaultBoardStart = new List<string>()
        {
            "15 11 12 13 16 13 12 11 15",
            "0 0 0 0 0 0 0 0 0",
            "0 14 0 0 0 0 0 14 0",
            "10 0 10 0 10 0 10 0 10",
            "0 0 0 0 0 0 0 0 0",
            "0 0 0 0 0 0 0 0 0",
            "00 0 00 0 00 0 00 0 00",
            "0 04 0 0 0 0 0 04 0",
            "0 0 0 0 0 0 0 0 0",
            "05 01 02 03 06 03 02 01 05",
        };
        public const string LoadDialogFliter = "sav files (*.sav)|*.sav|All files (*.*)|*.*";
        public const string LoadDialogTitle = "Load Save Files";

    }
}
