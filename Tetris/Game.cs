using System;

namespace Tetris
{
    public class Game
    {
        private const int defaultBoardWidth = 10, defaultBoardHeight = 22;

        public enum GameStatus { ReadyToStart, InProgress, Finished }
        
        private Board gameBoard;
        private GameStatus status;
        private Piece currPiece, nextPiece;
        private Random rnd;
        private int posX, posY, lines, score;

        public Game()
        {
            gameBoard = new Board(defaultBoardWidth, defaultBoardHeight);
            currPiece = null;
            nextPiece = null;
            status = GameStatus.ReadyToStart;
            ShadowPieceMode = true;
            NextPieceMode = true;
            rnd = new Random();
            posX = posY = 0;
            lines = 0;
            score = 0;
        }

        public void Start()
        {
            if (this.status != GameStatus.ReadyToStart)
            {
                throw new InvalidOperationException("Only game with status 'ReadyToStart' can be started");
            }
            this.status = GameStatus.InProgress;
            DropNewPiece();
        }

        public void GameOver()
        {
            if ((this.status != GameStatus.InProgress))
            {
                throw new InvalidOperationException("Only game with status 'InProgress' or 'Pause'  can be finished");
            }
            status = GameStatus.Finished;
        }

        public int PosX => this.posX;

        public int PosY => this.posY;

        public Board ActualBoard
        {
            get
            {
                if (this.Status == GameStatus.ReadyToStart)
                {
                    return this.gameBoard;
                }
                Board tmp_board = (Board)gameBoard.Clone();
                Piece tmp_piece = (Piece)currPiece.Clone();
                if (ShadowPieceMode)
                {
                    Piece shadow_piece = (Piece)currPiece.Clone();
                    tmp_board.FixShadowPiece(shadow_piece, posX, posY);
                }
                tmp_board.FixPiece(tmp_piece, posX, posY);
                return tmp_board;
            }
        }

        public Piece NextPiece => nextPiece;

        public Piece CurrPiece => currPiece;

        public GameStatus Status => this.status;

        public int Lines => lines;

        public int Score => score;        

        private void Step()
        {
            posY += Convert.ToInt32(this.Status == GameStatus.InProgress && gameBoard.CanPosAt(currPiece, posX, posY + 1));
            if (this.Status == GameStatus.InProgress && !gameBoard.CanPosAt(currPiece, posX, posY + 1))
            {
                gameBoard.FixPiece(currPiece, posX, posY);
                int currLinesMade = gameBoard.CheckLines();
                lines += currLinesMade;
                score += Convert.ToInt32(currLinesMade == 1) * 40;
                score += Convert.ToInt32(currLinesMade == 2) * 100;
                score += Convert.ToInt32(currLinesMade == 3) * 300;
                score += Convert.ToInt32(currLinesMade == 4) * 1200;
                if (gameBoard.IsTopReached())
                {
                    GameOver();
                }
                else
                {
                    DropNewPiece();
                }                
            }
        }

        private void DropNewPiece()
        {
            rnd = new Random(DateTime.Now.Millisecond);
            currPiece = (nextPiece != null) ? nextPiece : PieceFactory.GetRandomPiece(rnd);
            posY = currPiece.InitPosY;
            posX = ((gameBoard.Width - 1) / 2) + currPiece.InitPosX;
            nextPiece = PieceFactory.GetRandomPiece(rnd);
        }

        public void MoveRight() => posX += Convert.ToInt32(gameBoard.CanPosAt(currPiece, posX + 1, posY));

        public void MoveLeft() => posY -= Convert.ToInt32(gameBoard.CanPosAt(currPiece, posX - 1, posY));

        public void MoveDown() => Step();

        public void SmashDown()
        {
            while (gameBoard.CanPosAt(currPiece, posX, posY + 1))
            {
                Step();
            }
            MoveDown();
        }

        public void Rotate()
        {
            Piece tmp_piece = currPiece.RotateRight();
            if (gameBoard.CanPosAt(tmp_piece, posX, posY))
            {
                currPiece = tmp_piece;
            }
        }       

        public bool NextPieceMode { get; set; }

        public bool ShadowPieceMode { get; set; }
    }
}