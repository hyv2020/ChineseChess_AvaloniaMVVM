using System.Windows.Forms;

namespace ChineseChess_Avalonia
{
    public class ValidMove
    {
        private bool isValid = false;
        public bool Valid
        {
            get { return this.isValid; }
        }
        public PictureBox ValidMovePicBox = new PictureBox();

        public ValidMove(int x, int y)
        {
            isValid = false;
            ValidMovePicBox = DrawFunctions.DrawLegalMoveIndictor(x, y);
        }
        public void IsValidMove()
        {
            isValid = true;
            ValidMovePicBox.Visible = true;
        }
        public void NotValidMove()
        {
            isValid = false;
            ValidMovePicBox.Visible = false;
        }
        public override string ToString()
        {
            return typeof(ValidMove).ToString();
        }
    }
}
