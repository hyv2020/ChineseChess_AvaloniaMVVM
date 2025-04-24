using GameCommons.GameObjs;

namespace GameCommons
{
    [Serializable]
    public class Turn : IBoardStatus
    {
        public static string GameMode;
        public readonly int TurnNumber;
        public readonly Side WhosTurn;
        public readonly List<string> BoardState;
        public Turn(string gameMode, int turnNumber, Side whosTurn, List<string> boardState)
        {
            GameMode = gameMode;
            TurnNumber = turnNumber;
            WhosTurn = whosTurn;
            BoardState = boardState;
        }
        public Turn()
        {

        }
        public Turn(string gameMode, Side side)
        {
            GameMode = gameMode;
            TurnNumber = 1;
            WhosTurn = side;
            BoardState = DefaultVariables.ChineseChessDefaultBoardStart;
        }
        public void SaveToFile()
        {
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(FilePaths.rootTempFilePath, $"{this.TurnNumber}.txt")))
            {
                outputFile.WriteLine(GameMode);
                outputFile.WriteLine(this.WhosTurn.ToString());
                foreach (string row in BoardState)
                {
                    outputFile.WriteLine(row);
                }
            }
        }
        public override string ToString()
        {
            return "Turn " + TurnNumber.ToString();
        }
    }
}
