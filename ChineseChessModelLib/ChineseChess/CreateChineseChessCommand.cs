using ChessModelLib;
using System.ComponentModel;

namespace ChineseChessModelLib.ChineseChess
{
    public class CreateChineseChessCommand : ICreateBoardCommand
    {
        public string Name => "Chinese Chess";

        public ChessBoardBase Execute(PropertyChangedEventHandler postChessPieceMove)
        {
            return new ChineseChessBoard(postChessPieceMove);
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
