using System;
using System.Collections.Generic;
using System.IO;

namespace GameCommons
{
    [Serializable]
    public class Turn
    {
        public readonly int TurnNumber;
        public readonly Side WhosTurn;
        public readonly List<string> BoardState;
        public Turn(int turnNumber, Side whosTurn, List<string> boardState)
        {
            TurnNumber = turnNumber;
            WhosTurn = whosTurn;
            BoardState = boardState;
        }
        public Turn()
        {

        }
        public Turn(Side side)
        {
            TurnNumber = 1;
            WhosTurn = side;
            BoardState = DefaultVariables.defaultBoardStart;
        }
        public void SaveToFile()
        {
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(FilePaths.rootTempFilePath, $"{this.TurnNumber}.txt")))
            {
                outputFile.WriteLine(this.WhosTurn.ToString());
                foreach (string row in BoardState)
                {
                    outputFile.WriteLine(row);
                }
            }
        }
    }
}
