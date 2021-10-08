using System;
using System.Timers;

namespace Tetris
{
    class Program
    {
        private static Game _game;
        private static ConsoleDrawing _drawer;
        private static Timer _gameTimer;
        private static int _timerCounter = 0;
        private static readonly int _timerStep = 10;

        static void Main(string[] args)
        {
            _drawer = new ConsoleDrawing();
            ConsoleDrawing.ShowControls();
            Console.ReadKey();
            Console.Clear();
            _game = new Game();
            _game.Start();
            _gameTimer = new Timer(800);
            _gameTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            _gameTimer.Start();
            _drawer.DrawScene(_game);
            while (_game.Status != Game.GameStatus.Finished)
            {
                if (Console.KeyAvailable)
                {
                    KeyPressedHandler(Console.ReadKey(true));
                    _drawer.DrawScene(_game);
                    _gameTimer.Enabled = true;
                }
            }
            _gameTimer.Stop();
            _drawer.ShowGameOver(_game);
            Console.ResetColor();
            Console.CursorVisible = true;
        }

        private static void KeyPressedHandler(ConsoleKeyInfo input_key)
        {
            switch (input_key.Key)
            {
                case ConsoleKey.LeftArrow:
                    _game.MoveLeft();
                    break;
                case ConsoleKey.RightArrow:
                    _game.MoveRight();
                    break;
                case ConsoleKey.UpArrow:
                    _game.Rotate();
                    break;
                case ConsoleKey.DownArrow:
                    _game.MoveDown();
                    _gameTimer.Enabled = false;
                    break;
                case ConsoleKey.Spacebar:
                    _game.SmashDown();
                    break;
                case ConsoleKey.Escape:
                    _game.GameOver();
                    break;
            }
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (_game.Status != Game.GameStatus.Finished)
            {
                _timerCounter += _timerStep;
                _game.MoveDown();
                if (_game.Status == Game.GameStatus.Finished)
                {
                    _gameTimer.Stop();
                }
                else
                {
                    _drawer.DrawScene(_game);
                    if (_timerCounter >= (1000 - (_game.Lines * 10)))
                    {
                        _gameTimer.Interval -= 50;
                        _timerCounter = 0;
                    }
                }
            }
        }
    }
}