using domineering_game.game;
using System;
using System.Collections.Generic;

namespace domineering_game.game
{
    public class AlBePruningAi
    {
        int _level;
        readonly IPlayer _currPlayer;
        readonly IPlayer _enemyPlayer;
        public int Level { get => _level; set { _level = value; } }

        public AlBePruningAi(int level, IPlayer currentPlayer, IPlayer enemyPlayer)
        {
            _level = level;
            _currPlayer = currentPlayer;
            _enemyPlayer = enemyPlayer;
        }

        public int CalcBestMove(Board board, out int h, out int w)
        {
            int result = MiniMax(board, _level, int.MinValue, int.MaxValue,out (int,int) pos);
            h = pos.Item1;
            w = pos.Item2;
            return result;
        }

        public int MiniMax(Board board, int depth, int alpha, int beta, out (int, int) pos)
        {
            pos = (int.MinValue, int.MinValue);
            if (depth == 0)
                return Heuristic(board, _currPlayer.MoveType);

            int best = int.MinValue;

                var moves = board.HaveAvailableCells(_currPlayer.MoveType);
                if (moves.Count == 0)
                    return Heuristic(board, _currPlayer.MoveType);

                foreach (var move in moves)
                {
                    Board childBoard = (Board)board.Clone();

                    childBoard.Move(_currPlayer.Id, move.Item1, move.Item2, _currPlayer.MoveType);

                    int value = MaxiMin(childBoard, depth - 1, alpha, beta, out (int, int) childPos);

                    if (value > best)
                    {
                        pos.Item1 = move.Item1;
                        pos.Item2 = move.Item2;
                        best = value;
                    }
                    alpha = Math.Max(alpha, best);

                    if (beta <= alpha)
                        break;
                }
            return best;
        }
        public int MaxiMin(Board board, int depth, int alpha, int beta, out (int, int) pos)
        {
            pos = (int.MinValue, int.MinValue);
            if (depth == 0)
                return Heuristic(board, _currPlayer.MoveType);

            int best = int.MaxValue;

                var moves = board.HaveAvailableCells(_enemyPlayer.MoveType);
                if (moves.Count == 0)
                    return Heuristic(board, _currPlayer.MoveType);

                foreach (var move in moves)
                {
                    Board childBoard = (Board)board.Clone();

                    childBoard.Move(_enemyPlayer.Id, move.Item1, move.Item2, _enemyPlayer.MoveType);

                    int value = MiniMax(childBoard, depth - 1, alpha, beta, out (int, int) childPos);

                    if (value < best)
                    {
                        pos.Item1 = move.Item1;
                        pos.Item2 = move.Item2;
                        best = value;
                    }
                    beta = Math.Min(beta, best);
                    if (beta <= alpha)
                        break;
                }
            return best;
        }

        public static int Heuristic(Board bState, MoveType move)
        {
            int result = 0;
            int iH, iW;
            (iH, iW) = bState.BoardSize;
            for (int h = 0; h < iH; h++)
                for (int w = 0; w < iW; w++)
                    if (bState.Cells[h][w] ==0)
                    {
                        if ((h != iH - 1) && bState.Cells[h + 1][w] == 0) 
                            result++;
                        if ((w != iW - 1) && bState.Cells[h][w + 1] == 0) 
                            result--;
                    }

            return move == MoveType.Vertical ? result: -result;
        }
    }
}