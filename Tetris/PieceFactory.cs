using System;
using System.Collections.Generic;

namespace Tetris
{
    public class PieceFactory
    {
        private static List<Piece> _pieces;

        static PieceFactory() => Initialize();

        public static Piece GetPiecebyId(int id) => _pieces.Count > id && id >= 0 ? _pieces[id] : null;

        public static Piece GetRandomPiece(Random r) => GetPiecebyId(r.Next(_pieces.Count));

        public static int Count => _pieces.Count;

        public static void Initialize()
        {
            _pieces = new List<Piece>()
            {
                new Piece(new int[,] { { 1, 1, 1, 1 } }),
                new Piece(new int[,] { { 2, 2 }, { 2, 2 } }),
                new Piece(new int[,] { { 0, 0, 3 }, { 3, 3, 3 } }),
                new Piece(new int[,] { { 4, 0, 0 }, { 4, 4, 4 } }),
                new Piece(new int[,] { { 0, 5, 5 }, { 5, 5, 0 } }),
                new Piece(new int[,] { { 6, 6, 0 }, { 0, 6, 6 } }),
                new Piece(new int[,] { { 6, 6, 0 }, { 0, 6, 6 } }),
                new Piece(new int[,] { { 7, 7, 7 }, { 0, 7, 0 } })
            };
        }
    }
}