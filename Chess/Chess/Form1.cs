using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Chess
{
    public partial class Form1 : Form
    {
        private Chessboard mainMap;
        private Graphics board;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(@"img\chessboardPicture.jpg");
            board = Graphics.FromImage(pictureBox1.Image);

            mainMap = new Chessboard();
            renderBoard(mainMap.chessboardMap);
        }

        /// <summary>
        /// Funkcja przelicza wspolrzedne kursora w pikselach na wspolrzedne konkretnego pola na planszy
        /// </summary>
        /// <param name="pixelsPoint">Wspolrzedne kursora w pikselach</param>
        /// <returns>Zwraca Point ze wspolrzednymi na planszy</returns>
        private Point cursorPositionToCoord(Point pixelsPoint)
        {
            Point coordPoint = new Point();

            if(pixelsPoint.X < 44 || pixelsPoint.X > 8*76 + 44 || pixelsPoint.Y < 44 || pixelsPoint.Y > 8*76 + 44)
            {
                coordPoint.X = -1;
                coordPoint.Y = -1;
                return coordPoint;
            }

            coordPoint.X = (int)((pixelsPoint.X - 44) / 76);
            coordPoint.Y = (int)((pixelsPoint.Y - 44) / 76);

            return coordPoint;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Point currentTile = cursorPositionToCoord(e.Location);

            //currentTile przechowuje wspolrzedne wlasnie kliknietego pola na planszy
            // Nie jest jeszcze mega dokladny!

            label1.Text = "x: " + currentTile.X + "y: " + currentTile.Y;
            List<Point> movesList = mainMap.possibleMoves(currentTile);
            Image token = Image.FromFile("img/greenToken.png");


            if(movesList != null)
            {
                for (int i = 0; i < movesList.Count; i++)
                {
                    board.DrawImage(token, movesList[i].X * 76 + 44, movesList[i].Y * 76 + 44);
                    Console.WriteLine("token printed on tile x: {0} and y: {1}", movesList[i].X, movesList[i].Y);
                }

                Console.WriteLine();
            }
        }

        private void renderBoard(Piece[,] chessBoard)
        {
            Point currentTile = new Point();

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (chessBoard[col, row].type != PieceType.Empty)
                    {
                        currentTile.X = col * 76 + 44;
                        currentTile.Y = row * 76 + 44;

                        board.DrawImage(chessBoard[col, row].pieceImg, currentTile.X, currentTile.Y);
                    }
                }
            }
        }
    }
}
