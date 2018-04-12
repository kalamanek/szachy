using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Chess
{
    class Chessboard
    {
        /* Chessmen's names:
         * K for king (krol),
         * Q for queen (krolowa),
         * R for rook (wieza),
         * B for bishop (goniec),
         * N for knight (kon),
         * P for pawn (pionek),
         * X for empty
         * 
         * Zakladamy, ze biale sa na dole planszy.
         */

        public Piece[,] chessboardMap
        {
            get;
        }

        /// <summary>
        /// Klasa zawierajaca reprezentacje planszy do szachow, czyli tablice dwuwymiarowa 8x8
        /// </summary>
        public Chessboard()
        {
            // Wspolrzedne podawane sa w formacie [x, y]
            chessboardMap = new Piece[8, 8];

            for(int row = 2; row <= 5; row++)
            {
                for(int column = 0; column < 8; column++)
                {
                    chessboardMap[column, row] = new Piece(PieceType.Empty);
                }
            }

            for(int column = 0; column < 8; column++)
            {
                chessboardMap[column, 6] = new Piece(PieceType.Pawn, PieceColor.White);
                chessboardMap[column, 1] = new Piece(PieceType.Pawn, PieceColor.Black);
            }

            chessboardMap[0, 7] = new Piece(PieceType.Rook, PieceColor.White);
            chessboardMap[7, 7] = new Piece(PieceType.Rook, PieceColor.White);
            chessboardMap[1, 7] = new Piece(PieceType.Knight, PieceColor.White);
            chessboardMap[6, 7] = new Piece(PieceType.Knight, PieceColor.White);
            chessboardMap[2, 7] = new Piece(PieceType.Bishop, PieceColor.White);
            chessboardMap[5, 7] = new Piece(PieceType.Bishop, PieceColor.White);
            chessboardMap[3, 7] = new Piece(PieceType.Queen, PieceColor.White);
            chessboardMap[4, 7] = new Piece(PieceType.King, PieceColor.White);

            chessboardMap[0, 0] = new Piece(PieceType.Rook, PieceColor.Black);
            chessboardMap[7, 0] = new Piece(PieceType.Rook, PieceColor.Black);
            chessboardMap[1, 0] = new Piece(PieceType.Knight, PieceColor.Black);
            chessboardMap[6, 0] = new Piece(PieceType.Knight, PieceColor.Black);
            chessboardMap[2, 0] = new Piece(PieceType.Bishop, PieceColor.Black);
            chessboardMap[5, 0] = new Piece(PieceType.Bishop, PieceColor.Black);
            chessboardMap[3, 0] = new Piece(PieceType.Queen, PieceColor.Black);
            chessboardMap[4, 0] = new Piece(PieceType.King, PieceColor.Black);
        }

        /// <summary>
        /// Przeklada figure ze wspolrzednych "start" na wspolrzedne "destiny"
        /// UWAGA! Funkcja nic nie sprawdza, jedynie przepisuje wartość do drugiego pola, walidacja musi odbyć się wcześniej
        /// </summary>
        /// <param name="start">Lokalizacja z której figura zostanie przeniesiona</param>
        /// <param name="destiny">Lokalizacja do której figura zostanie zapisana</param>
        public void movePiece(Point start, Point destiny)
        {
            chessboardMap[destiny.X, destiny.Y] = chessboardMap[start.X, start.Y];
            chessboardMap[start.X, start.Y].type = PieceType.Empty;
        }

        /// <summary>
        /// To samo co funkcja wyżej.
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="destinyX"></param>
        /// <param name="destinyY"></param>
        public void movePiece(int startX, int startY, int destinyX, int destinyY)
        {
            chessboardMap[destinyX, destinyY] = chessboardMap[startX, startY];
            chessboardMap[startX, startY].type = PieceType.Empty;
        }

        /// <summary>
        /// Funkcja sprawdza, czy wspolrzedne podane w argumencie znajduja sie na planszy, czyli czy nie "wystaja" poza plansze.
        /// </summary>
        /// <param name="coord"></param>
        /// <returns>Zwraca: true jesli znajduja sie na mapie
        ///             zwraca: false jesli nie znajduje sie na mapie</returns>
        private static bool inRange(Point coord)
        {
            if (coord.X < 0 || coord.X > 7 || coord.Y < 0 || coord.Y > 7)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static bool inRange(int x, int y)
        {
            if (x < 0 || x > 7 || y < 0 || y > 7)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Funkcja szuka ruchow mozliwych do wykonania przez dana figure.
        /// </summary>
        /// <param name="coord">Wspolrzedne figury do sprawdzenia</param>
        /// <returns>Zwraca liste typu Point ze wspolrzednymi okreslajacymi mozliwe</returns>
        public List<Point> possibleMoves(Point coord)
        {
            if(!inRange(coord))
            {
                throw new Exception("PossibleMoves: Given coordinates are out of chess board.");
            }

            switch(chessboardMap[coord.X, coord.Y].type)
            {
                case PieceType.Empty:
                    return null;
                    break;

                case PieceType.King:
                    return possibleKingsMoves(coord);
                    break;

                case PieceType.Queen:
                    return possibleQueenMoves(coord);
                    break;

                case PieceType.Bishop:
                    return possibleBishopMoves(coord);
                    break;

                case PieceType.Knight:
                    return possibleKnightMoves(coord);
                    break;

                case PieceType.Rook:
                    return possibleRookMoves(coord);
                    break;

                case PieceType.Pawn:
                    return possiblePawnMoves(coord);
                    break;

                default:
                    throw new Exception("Unrecognized piece");
            }
        }

        private List<Point> possiblePawnMoves(Point coord)
        {
            List<Point> moves = new List<Point>();

            Piece currentPiece = chessboardMap[coord.X, coord.Y];
            if(currentPiece.color == PieceColor.White)
            {
                for(int row = coord.Y; row > coord.Y-2; row--)
                {
                    if(inRange(coord.X, row) && chessboardMap[coord.X, row].type == PieceType.Empty)
                    {
                        moves.Add(new Point(coord.X, row));
                    }
                }

                if (inRange(coord.X + 1, coord.Y - 1) && chessboardMap[coord.X+1, coord.Y-1].color != currentPiece.color)
                {
                    moves.Add(new Point(coord.X + 1, coord.Y - 1));
                }

                if (inRange(coord.X - 1, coord.Y - 1) && chessboardMap[coord.X - 1, coord.Y - 1].color != currentPiece.color)
                {
                    moves.Add(new Point(coord.X + 1, coord.Y - 1));
                }
            }
            else
            {
                for (int row = coord.Y; row < coord.Y + 2; row++)
                {
                    if (inRange(coord.X, row) && chessboardMap[coord.X, row].type == PieceType.Empty)
                    {
                        moves.Add(new Point(coord.X, row));
                    }
                }

                if (inRange(coord.X + 1, coord.Y + 1) && chessboardMap[coord.X + 1, coord.Y + 1].color != currentPiece.color)
                {
                    moves.Add(new Point(coord.X + 1, coord.Y + 1));
                }

                if (inRange(coord.X - 1, coord.Y + 1) && chessboardMap[coord.X - 1, coord.Y + 1].color != currentPiece.color)
                {
                    moves.Add(new Point(coord.X + 1, coord.Y + 1));
                }
            }

            return moves;
        }

        private List<Point> possibleRookMoves(Point coord)
        {
            List<Point> moves = new List<Point>();

            for(int col = coord.X; col <= 7; col++)
            {
                if(chessboardMap[col, coord.Y].type != PieceType.Empty)
                {
                    if(chessboardMap[col, coord.Y].color != chessboardMap[coord.X, coord.Y].color)
                    {
                        moves.Add(new Point(col, coord.Y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    moves.Add(new Point(col, coord.Y));
                }
            }
            for (int col = coord.X; col >= 0; col--)
            {
                if (chessboardMap[col, coord.Y].type != PieceType.Empty)
                {
                    if (chessboardMap[col, coord.Y].color != chessboardMap[coord.X, coord.Y].color)
                    {
                        moves.Add(new Point(col, coord.Y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    moves.Add(new Point(col, coord.Y));
                }
            }

            for (int row = coord.Y; row <= 7; row++)
            {
                if (chessboardMap[coord.X, row].type != PieceType.Empty)
                {
                    if (chessboardMap[coord.X, row].color != chessboardMap[coord.X, coord.Y].color)
                    {
                        moves.Add(new Point(coord.X, row));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    moves.Add(new Point(coord.X, row));
                }
            }
            for (int row = coord.Y; row >= 0; row--)
            {
                if (chessboardMap[coord.X, row].type != PieceType.Empty)
                {
                    if (chessboardMap[coord.X, row].color != chessboardMap[coord.X, coord.Y].color)
                    {
                        moves.Add(new Point(coord.X, row));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    moves.Add(new Point(coord.X, row));
                }
            }

            return moves;
        }

        private List<Point> possibleKnightMoves(Point coord)
        {
            List<Point> moves = new List<Point>();

            int x = coord.X + 1;
            int y = coord.Y + 2;
            if(inRange(x, y))
            {
                if(chessboardMap[x, y].type == PieceType.Empty || chessboardMap[x, y].color != chessboardMap[coord.X, coord.Y].color)
                {
                    moves.Add(new Point(x, y));
                }
            }

            y = coord.Y - 2;
            if (inRange(x, y))
            {
                if (chessboardMap[x, y].type == PieceType.Empty || chessboardMap[x, y].color != chessboardMap[coord.X, coord.Y].color)
                {
                    moves.Add(new Point(x, y));
                }
            }

            x = coord.X - 1;
            if (inRange(x, y))
            {
                if (chessboardMap[x, y].type == PieceType.Empty || chessboardMap[x, y].color != chessboardMap[coord.X, coord.Y].color)
                {
                    moves.Add(new Point(x, y));
                }
            }

            y = coord.Y + 2;
            if (inRange(x, y))
            {
                if (chessboardMap[x, y].type == PieceType.Empty || chessboardMap[x, y].color != chessboardMap[coord.X, coord.Y].color)
                {
                    moves.Add(new Point(x, y));
                }
            }

            int[,] temp = new int[,] { { -1, -2 }, { 1, 2 } };

            for(int k = 0; k <= 1; k++)
            {
                for(int l = 0; l <= 1; l++)
                {
                    x = coord.X + temp[k, l];
                    if (inRange(x, y))
                    {
                        if (chessboardMap[x, y].type == PieceType.Empty || chessboardMap[x, y].color != chessboardMap[coord.X, coord.Y].color)
                        {
                            moves.Add(new Point(x, y));
                        }
                    }
                }
            }

            return moves;
        }

        private List<Point> possibleBishopMoves(Point coord)
        {
            List<Point> moves = new List<Point>();

            int x = coord.X + 1;
            int y = coord.Y + 1;
            while (inRange(x, y))
            {
                if (chessboardMap[x, y].type != PieceType.Empty)
                {
                    if (chessboardMap[x, y].color != chessboardMap[x, y].color)
                    {
                        moves.Add(new Point(x, y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    moves.Add(new Point(x, y));
                }

                x += 1;
                y += 1;
            }

            x = coord.X - 1;
            y = coord.Y + 1;
            while (inRange(x, y))
            {
                if (chessboardMap[x, y].type != PieceType.Empty)
                {
                    if (chessboardMap[x, y].color != chessboardMap[x, y].color)
                    {
                        moves.Add(new Point(x, y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    moves.Add(new Point(x, y));
                }

                x -= 1;
                y += 1;
            }

            x = coord.X + 1;
            y = coord.Y - 1;
            while (inRange(x, y))
            {
                if (chessboardMap[x, y].type != PieceType.Empty)
                {
                    if (chessboardMap[x, y].color != chessboardMap[x, y].color)
                    {
                        moves.Add(new Point(x, y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    moves.Add(new Point(x, y));
                }

                x += 1;
                y -= 1;
            }

            x = coord.X - 1;
            y = coord.Y - 1;
            while (inRange(x, y))
            {
                if (chessboardMap[x, y].type != PieceType.Empty)
                {
                    if (chessboardMap[x, y].color != chessboardMap[x, y].color)
                    {
                        moves.Add(new Point(x, y));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    moves.Add(new Point(x, y));
                }

                x += 1;
                y += 1;
            }

            return moves;
        }

        private List<Point> possibleQueenMoves(Point coord)
        {
            List<Point> moves = new List<Point>();

            moves = possibleBishopMoves(coord);
            moves.AddRange(possibleRookMoves(coord));

            return moves;
        }

        private List<Point> possibleKingsMoves(Point coord)
        {
            List<Point> moves = new List<Point>();

            for(int column = coord.X-1; column < coord.X+1; column++)
            {
                for (int row = coord.Y - 1; row < coord.Y + 1; row++)
                {
                    if (inRange(column, row))
                    {
                        if(chessboardMap[coord.X, coord.Y].type == PieceType.Empty || chessboardMap[coord.X, coord.Y].color != chessboardMap[column, row].color)
                        {
                            moves.Add(new Point(column, row));
                        }
                    }
                }
            }

            return moves;
        }

    }
}
