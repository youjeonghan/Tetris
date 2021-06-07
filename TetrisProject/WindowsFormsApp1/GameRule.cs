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
        internal static int SCORE = 0;
        internal static int LIST_IDX = 1;
        internal static List<int> LIST_BlockNum = new List<int>();
        internal static List<int> LIST_Turn = new List<int>();

        public static void add() { SCORE += 12; }
        public static void reset() { SCORE = 0; }
        public static void reset_list()
        {
            Random random = new Random();
            for (int i = 0; i < 4; i++)
            {
                LIST_Turn.Add(random.Next() % 4);
                LIST_BlockNum.Add(random.Next() % 7);

            }
        }
    }
}
