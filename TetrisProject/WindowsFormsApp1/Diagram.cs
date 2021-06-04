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

        internal void Reset()
        {
            X = GameRule.START_X;
            Y = GameRule.START_Y;
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
    }
}
