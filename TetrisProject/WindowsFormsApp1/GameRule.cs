using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFormsApp1
{
    class GameRule
    {
        internal const int BLOCK_WIDTH = 30;
        internal const int BLOCK_HEIGHT = 30;
        internal const int BOARD_X = 12;
        internal const int BOARD_Y = 20;
        internal const int START_X = 4;
        internal const int START_Y = 0;
        internal static int SCORE = 12;
        public static void add()
        {
            SCORE += 12;
        }
    }
}
