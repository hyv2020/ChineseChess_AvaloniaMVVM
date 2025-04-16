using System;

namespace GameCommons
{
    public static class FilePaths
    {
        //set the root location of chess piece images
        public static string rootChessImageFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Images\chess piece images\");
        //set the root location of board images
        public static string rootBoardImageFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Images\board images\");
        public static string rootSaveFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Saves\");
        public static string rootTempFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Saves\temp\");


    }
}
