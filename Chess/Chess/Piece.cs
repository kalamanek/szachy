using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Chess
{
    enum PieceColor { White, Black };
    enum PieceType { King, Queen, Rook, Bishop, Knight, Pawn, Empty };

    /* Pieces names:
         * K for king (krol),
         * Q for queen (krolowa),
         * R for rook (wieza),
         * B for bishop (goniec),
         * N for knight (kon),
         * P for pawn (pionek),
         * X for empty
         */

    class Piece
    {
        public Image pieceImg;
        public PieceColor color = PieceColor.White;
        public PieceType type = PieceType.Empty;

        public Piece() { }

        public Piece(PieceType type)
        {
            this.type = type;
        }

        public Piece(PieceType type, PieceColor color)
        {
            this.type = type;

            if (color == PieceColor.Black)
            {
                this.color = PieceColor.Black;

                if (type == PieceType.King)
                {
                    this.pieceImg = Image.FromFile("img/BlackKing.png");
                }

                else if (type == PieceType.Queen)
                {
                    this.pieceImg = Image.FromFile("img/BlackQueen.png");
                }

                else if (type == PieceType.Rook)
                {
                    this.pieceImg = Image.FromFile("img/BlackRook.png");
                }

                else if (type == PieceType.Bishop)
                {
                    this.pieceImg = Image.FromFile("img/BlackBishop.png");
                }

                else if (type == PieceType.Knight)
                {
                    this.pieceImg = Image.FromFile("img/BlackKnight.png");
                }

                else if (type == PieceType.Pawn)
                {
                    this.pieceImg = Image.FromFile("img/BlackPawn.png");
                }
            }
            else if (color == PieceColor.White)
            {
                this.color = PieceColor.White;

                if (type == PieceType.King)
                {
                    this.pieceImg = Image.FromFile("img/WhiteKing.png");
                }

                else if (type == PieceType.Queen)
                {
                    this.pieceImg = Image.FromFile("img/WhiteQueen.png");
                }

                else if (type == PieceType.Rook)
                {
                    this.pieceImg = Image.FromFile("img/WhiteRook.png");
                }

                else if (type == PieceType.Bishop)
                {
                    this.pieceImg = Image.FromFile("img/WhiteBishop.png");
                }

                else if (type == PieceType.Knight)
                {
                    this.pieceImg = Image.FromFile("img/WhiteKnight.png");
                }

                else if (type == PieceType.Pawn)
                {
                    this.pieceImg = Image.FromFile("img/WhitePawn.png");
                }
            }
        }

        public Piece(PieceType type, PieceColor color, string imgPath)
        {
            this.type = type;

            if (color == PieceColor.Black)
            {
                this.color = PieceColor.Black;
            }

            this.pieceImg = Image.FromFile(imgPath);
        }
    }
}
