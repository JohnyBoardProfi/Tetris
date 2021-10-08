using System;

namespace Tetris
{
    public class ConsoleDrawing
    {
        private static readonly int[,] clearBlock = { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } };

        public void DrawScene(Game game)
        {
            lock (game)
            {
                int posX = Console.CursorLeft, posY = Console.CursorTop;
                ShowLines(game);
                ShowScore(game);
                Console.SetCursorPosition(posX, posY);
                DrawArray(game.ActualBoard.ToArray(), true);
                Console.CursorLeft = game.ActualBoard.Width + 4;
                Console.CursorTop = 2;
                DrawArray(clearBlock, false);
                if (game.NextPieceMode)
                {
                    Console.CursorLeft = game.ActualBoard.Width + 5;
                    Console.CursorTop = 3;
                    DrawArray(game.NextPiece.ToArray(), false);
                }
                Console.SetCursorPosition(posX, posY);
            }
        }

        public static void ShowControls()
        {
            Console.WriteLine("Tetris");
            Console.WriteLine("[→] [←] [↓] [↑]");
            Console.WriteLine("[SPACE] - сбросить блок");
            Console.WriteLine("[ESC] - выход");
            Console.WriteLine("Нажмите любую клавишу");
        }

        public void ShowGameOver(Game game)
        {
            Console.Clear();
            Console.Write("Ваш счёт: " + game.Score);
        }

        private static void ShowLines(Game game) => Print(game.ActualBoard.Width + 4, 7, "Ряд: " + game.Lines);

        private static void ShowScore(Game game) => Print(game.ActualBoard.Width + 4, 9, "Счёт: " + game.Score);

        private static void Print(int a, int b, string s)
        {
            Console.SetCursorPosition(a, b);
            Console.Write(s);
        }

        private void DrawArray(int[,] a, bool border)
        {
            int x = Console.CursorLeft;
            for (int i = 0; i <= a.GetUpperBound(0); i++)
            {
                if (border)
                {
                    Console.Write("█");
                }
                for (int j = 0; j <= a.GetUpperBound(1); j++)
                {
                    switch (a[i, j])
                    {
                        case 1:
                            DrawColor("█", ConsoleColor.White);
                            break;
                        case 2:
                            DrawColor("█", ConsoleColor.Magenta);
                            break;
                        case 3:
                            DrawColor("█", ConsoleColor.Blue);
                            break;
                        case 4:
                            DrawColor("█", ConsoleColor.Green);
                            break;
                        case 5:
                            DrawColor("█", ConsoleColor.Yellow);
                            break;
                        case 6:
                            DrawColor("█", ConsoleColor.Red);
                            break;
                        case 7:
                            DrawColor("█", ConsoleColor.Cyan);
                            break;
                        case 8:
                            DrawColor("█", ConsoleColor.Gray);
                            break;
                        default:
                            Console.Write(" ");
                            break;
                    }
                }
                Console.WriteLine(border ? "█"  : "");
                Console.CursorLeft = x;
            }
            if (border)
            {
                for (int q = 0; q <= a.GetUpperBound(1) + 2; q++)
                {
                    Console.Write("█");
                }
            }
        }

        private static void DrawColor(string a, ConsoleColor b)
        {
            ConsoleColor c = Console.ForegroundColor;
            Console.ForegroundColor = b;
            Console.Write(a);
            Console.ForegroundColor = c;
        }      
    }
}