﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFormsApp1
{
    class Board
    {
        internal static Board GameBoard
        {
            get;
            private set;
        }
        static Board()
        {
            GameBoard = new Board();
        }
        Board()
        {
        }

        int[,] board = new int[GameRule.BOARD_X, GameRule.BOARD_Y];

        internal int this[int x, int y]
        {
            get
            {
                return board[x, y];
            }
        }
        internal bool MoveEnable(int bn, int tn, int x, int y)
        {
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BlockValue.bvals[bn, tn, xx, yy] != 0)
                    {
                        if (board[x + xx, y + yy] != 0)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        internal void Store(int bn, int turn, int x, int y)
        {
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (((x + xx) >= 0) && (x + xx < GameRule.BOARD_X) && (y + yy >= 0) && (y + yy < GameRule.BOARD_Y))
                    {

                        board[x + xx, y + yy] += BlockValue.bvals[bn, turn, xx, yy];
                    }
                }
            }
            CheckLines(y + 3);
        }
        private void CheckLines(int y)
        {
            int yy = 0;
            for (yy = 0; (yy < 4); yy++)
            {
                if (y - yy < GameRule.BOARD_Y)
                {
                    if (CheckLine(y - yy))
                    {
                        ClearLine(y - yy);
                        y++;
                    }
                }
            }
        }
        private void ClearLine(int y)
        {
            for (; y > 0; y--)
            {
                for (int xx = 0; xx < GameRule.BOARD_X; xx++)
                {
                    board[xx, y] = board[xx, y - 1];
                }
            }
        }

        private bool CheckLine(int y)
        {
            for (int xx = 0; xx < GameRule.BOARD_X; xx++)
            {
                if (board[xx, y] == 0)
                {
                    return false;
                }
            }
            return true;
        }

        internal void ClearBoard()
        {
            for (int xx = 0; xx < GameRule.BOARD_X; xx++)
            {
                for (int yy = 0; yy < GameRule.BOARD_Y; yy++)
                {
                    board[xx, yy] = 0;
                }
            }
        }
    }
}
