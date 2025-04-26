using System.ComponentModel;

namespace ChessModelLib
{
    public interface ICreateBoardCommand
    {
        string Name { get; }
        ChessBoardBase Execute(PropertyChangedEventHandler postChessPieceMove);

    }
}
