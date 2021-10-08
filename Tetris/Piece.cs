using System;

namespace Tetris
{
    public class Piece
    {
        private int[,] _piece;
        private int _initPosX, _initPosY;

        public Piece(int[,] p)
        {
            if (p == null)
            {
                throw new ArgumentNullException();
            }
            _piece = (int[,])p.Clone();
            _initPosY = (p.GetUpperBound(0) + 1) * -1;
            _initPosX = 0;
        }

        public int Height => _piece.GetUpperBound(0) + 1;

        public int Width => _piece.GetUpperBound(1) + 1;

        public int InitPosX => _initPosX;

        public int InitPosY => _initPosY;

        public Piece RotateRight()
        {
            int[,] rotated = new int[this.Width, this.Height];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    rotated[j, Height - i - 1] = _piece[i, j];
                }
            }
            return new Piece(rotated);
        }

        public void MakeItShadow()
        {
            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    if (this[i, j] != 0)
                    {
                        _piece[i, j] = 8;
                    }
                }
            }
        }

        public int[,] ToArray() => _piece;

        public int this[int h, int w] => (h < 0) || (h >= this.Height) || (w < 0) || (w >= this.Width) ? throw new IndexOutOfRangeException("Index is out of range!") : _piece[h, w];

        public object Clone() => new Piece(this._piece);
    }
}