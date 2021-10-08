using System;

namespace Tetris
{
    public class Board
    {
        private int[,] _mBoard;

        public Board(int width, int height)
        {
            if ((width >= 10) && (height >= 20))
            {
                _mBoard = new int[height, width];
                for (int i = 0; i < _mBoard.GetUpperBound(0) + 1; i++)
                {
                    for (int j = 0; j < _mBoard.GetUpperBound(1) + 1; ++j)
                    {
                        _mBoard[i, j] = 0;
                    }
                }
            }
            else
            {
                throw new ArgumentException("Board must be at least 10x20");
            }
        }        

        public void FixPiece(Piece piece, int x, int y)
        {
            if ((x >= 0) && ((x + piece.Width) <= this.Width) && ((y + piece.Height) <= this.Height))
            {
                for (int i = 0; i < piece.Width; i++)
                {
                    for (int j = 0; j < piece.Height; j++)
                    {
                        if ((piece[j, i] != 0) && (y + j >= 0))
                        {
                            _mBoard[y + j, x + i] = piece[j, i];
                        }
                    }
                }
            }
        }

        public int CheckLines()
        {
            for (int i = 0; i < this.Height; i++)
            {
                bool isFullLine = true;
                for (int j = 0; j < this.Width; j++)
                {
                    isFullLine = (isFullLine) && (_mBoard[i, j] != 0);
                }
                if (isFullLine)
                {
                    this.RemoveLine(i--);
                    return this.CheckLines() + 1;
                }
            }
            return 0;
        }

        public bool CanPosAt(Piece piece, int x, int y)
        {
            if ((x >= 0) && ((x + piece.Width) <= this.Width) && ((y + piece.Height) <= this.Height))
            {
                for (int i = 0; i < piece.Width; i++)
                {
                    for (int j = 0; j < piece.Height; j++)
                    {
                        if ((piece[j, i] != 0) && (y + j >= 0) && !this.IsFreePos(y + j, x + i))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            return false;
        }

        public bool IsTopReached()
        {
            for (int i = 0; i < this.Width; i++)
            {
                if (!this.IsFreePos(0, i))
                {
                    return true;
                }
            }
            return false;
        }

        public int[,] ToArray() => _mBoard;

        public void FixShadowPiece(Piece piece, int posX, int posY)
        {
            piece.MakeItShadow();
            int add = 0;
            while (this.CanPosAt(piece, posX, posY + add))
            {
                add++;
            }
            if (posY + add - 1 > 0)
            {
                this.FixPiece(piece, posX, posY + add - 1);
            }
        }

        public int Height => _mBoard.GetUpperBound(0) + 1;

        public int Width => _mBoard.GetUpperBound(1) + 1;

        private bool IsFreePos(int pX, int pY) => this[pX, pY] == 0;

        private void RemoveLine(int index)
        {
            for (int i = index; i > 0; i--)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    _mBoard[i, j] = _mBoard[i - 1, j];
                }
            }
            for (int j = 0; j < this.Width; j++)
            {
                _mBoard[0, j] = 0;
            }
        }

        public int this[int h, int w] => (h < 0) || (h >= this.Height) || (w < 0) || (w >= this.Width) ? throw new IndexOutOfRangeException("Index is out of range!") : _mBoard[h, w];

        public object Clone()
        {
            Board temp = new Board(this.Width, this.Height);
            temp._mBoard = (int[,])_mBoard.Clone();
            return temp;
        }
    }
}