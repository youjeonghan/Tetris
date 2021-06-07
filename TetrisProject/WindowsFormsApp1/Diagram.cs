using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFormsApp1
{
    class Diagram
    {
        // Define Tetris Brick
        internal int X
        {
            get;
            private set;
        }
        internal int Y
        {
            get;
            private set;
        }
        internal Diagram()
        {
            Reset();
        }
        internal int Turn
        {
            get;
            private set;
        }
        internal int BlockNum
        {
            get;
            private set;
        }

        internal void Reset()
        {
            Random random = new Random();
            X = GameRule.START_X;
            Y = GameRule.START_Y;
            //Turn = random.Next() % 4;
            //BlockNum = random.Next() % 7;
            Turn = GameRule.LIST_Turn[GameRule.LIST_IDX-1];
            BlockNum = GameRule.LIST_BlockNum[GameRule.LIST_IDX-1];
        }
        internal void MoveLeft()
        {
            X--;
        }
        internal void MoveRight()
        {
            X++;
        }
        internal void MoveDown()
        {
            Y++;
        }
        internal void MoveTurn()
        {
            Turn = (Turn + 1) % 4;
        }
    }
}
