using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Game game;
        int bx;
        int by;
        int bwidth;
        int bheight;
        public int score;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            game = Game.Singleton;
            bx = GameRule.BOARD_X;
            by = GameRule.BOARD_Y;
            bwidth = GameRule.BLOCK_WIDTH;
            bheight = GameRule.BLOCK_HEIGHT;
            score = 0;
            SetClientSizeCore(bx * bwidth * 2, by * bheight);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawGraduation(e.Graphics);
            DrawDiagram(e.Graphics);
            DrawBoard(e.Graphics);
            WaitBlock(e.Graphics);
            DoubleBuffered = true;
        }
        private void WaitBlock(Graphics graphics)
        {
            int idx = GameRule.LIST_IDX;
            int[] bn = new int[] { GameRule.LIST_BlockNum[idx], GameRule.LIST_BlockNum[idx + 1] };
            int[] tn = new int[] { GameRule.LIST_Turn[idx], GameRule.LIST_Turn[idx + 1] };


            for (int i = 0; i < 2; i++)
            {
                for (int xx = 0; xx < 4; xx++)
                {
                    for (int yy = 0; yy < 4; yy++)
                    {
                        if (BlockValue.bvals[bn[i], tn[i], xx, yy] != 0)
                        {
                            Rectangle now_rt = new Rectangle((16 + xx) * bwidth + 2, (5 + (i * 5) + yy) * bheight + 2, bwidth - 4, bheight - 4);

                            BlockValue.bcolor(bn[i], graphics, now_rt);
                            //graphics.DrawRectangle(BlockValue.bcolor(bn), now_rt);
                            //graphics.FillRectangle(Brushes.Red, now_rt);
                        }
                    }
                }
            }
            Random random = new Random();
            GameRule.LIST_Turn.Add(random.Next() % 4);
            GameRule.LIST_BlockNum.Add(random.Next() % 7);
        }
        private void DrawBoard(Graphics graphics)
        {
            for (int xx = 0; xx < bx; xx++)
            {
                for (int yy = 0; yy < by; yy++)
                {
                    if (game[xx, yy] != 0)
                    {
                        Rectangle now_rt = new Rectangle(xx * bwidth + 2, yy * bheight + 2, bwidth - 4, bheight - 4);

                        graphics.DrawRectangle(Pens.Green, now_rt);
                        graphics.FillRectangle(Brushes.Gold, now_rt);
                    }
                }
            }
        }

        private void DrawDiagram(Graphics graphics)
        {
            //Pen bcolor;
            Point now = game.NowPosition;
            int bn = game.BlockNum;
            int tn = game.Turn;
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BlockValue.bvals[bn, tn, xx, yy] != 0)
                    {
                        Rectangle now_rt = new Rectangle((now.X + xx) * bwidth + 2, (now.Y + yy) * bheight + 2, bwidth - 4, bheight - 4);

                        BlockValue.bcolor(bn, graphics, now_rt);
                        //graphics.DrawRectangle(BlockValue.bcolor(bn), now_rt);
                        //graphics.FillRectangle(Brushes.Red, now_rt);
                    }
                }
            }
        }

        private void DrawGraduation(Graphics graphics)
        {
            DrawHorizons(graphics);
            DrawVerticals(graphics);
        }

        private void DrawVerticals(Graphics graphics)
        {
            Point st = new Point();
            Point et = new Point();
            for (int cx = 0; cx < bx + 1; cx++)
            {
                st.X = cx * bwidth;
                st.Y = 0;
                et.X = cx * bwidth;
                et.Y = by * bheight;
                graphics.DrawLine(Pens.Purple, st, et);
            }
        }

        private void DrawHorizons(Graphics graphics)
        {
            Point st = new Point();
            Point et = new Point();
            for (int cy = 0; cy < by; cy++)
            {
                st.X = 0;
                st.Y = cy * bheight;
                et.X = bx * bwidth;
                et.Y = cy * bheight;
                graphics.DrawLine(Pens.Green, st, et);
            }
        }



        private void MoveTurn()
        {
            if (game.MoveTurn())
            {
                Region rg = MakeRegion();
                Invalidate(rg);
            }
        }

        private void MoveDown()
        {
            label2.Text = GameRule.SCORE.ToString();
            if (game.MoveDown())
            {
                Region rg = MakeRegion(0, -1);
                Invalidate(rg);
            }
            else
            {
                GameRule.LIST_IDX += 1;
                EndingCheck();
            }
        }
        private void EndingCheck()
        {
            if (game.Next())
            {
                Invalidate();
            }
            else
            {
                timer_down.Enabled = false;

                if (DialogResult.Yes == MessageBox.Show($"Score {GameRule.SCORE}\nAgain?", "Game Over", MessageBoxButtons.YesNo))
                {
                    game.ReStart();
                    GameRule.reset();
                    timer_down.Enabled = true;
                    Invalidate();
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void MoveLeft()
        {
            if (game.MoveLeft())
            {
                Region rg = MakeRegion(1, 0);
                Invalidate(rg);
            }
        }

        private void MoveRight()
        {
            if (game.MoveRight())
            {
                Region rg = MakeRegion(-1, 0);
                Invalidate(rg);
            }
        }

        private Region MakeRegion(int cx, int cy)
        {
            Point now = game.NowPosition;

            int bn = game.BlockNum;
            int tn = game.Turn;
            Region region = new Region();
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BlockValue.bvals[bn, tn, xx, yy] != 0)
                    {
                        Rectangle rect1 = new Rectangle((now.X + xx) * bwidth + 2, (now.Y + yy) * bheight + 2, bwidth - 4, bheight - 4);
                        Rectangle rect2 = new Rectangle((now.X + cx + xx) * bwidth, (now.Y + cy + yy) * bheight, bwidth, bheight);
                        Region rg1 = new Region(rect1);
                        Region rg2 = new Region(rect2);
                        region.Union(rg1);
                        region.Union(rg2);
                    }
                }
            }
            return region;
        }
        private Region MakeRegion()
        {
            Point now = game.NowPosition;
            int bn = game.BlockNum;
            int tn = game.Turn;
            int oldtn = (tn + 3) % 4;
            Region region = new Region();
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BlockValue.bvals[bn, tn, xx, yy] != 0)
                    {
                        Rectangle rect1 = new Rectangle((now.X + xx) * bwidth + 2, (now.Y + yy) * bheight + 2, bwidth - 4, bheight - 4);
                        Region rg1 = new Region(rect1);
                        region.Union(rg1);
                    }
                    if (BlockValue.bvals[bn, oldtn, xx, yy] != 0)
                    {
                        Rectangle rect1 = new Rectangle((now.X + xx) * bwidth + 2, (now.Y + yy) * bheight + 2, bwidth - 4, bheight - 4);
                        Region rg1 = new Region(rect1);
                        region.Union(rg1);
                    }
                }
            }
            return region;
        }

        private void timer_down_Tick(object sender, EventArgs e)
        {
            MoveDown();

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right: MoveRight(); return;
                case Keys.Left: MoveLeft(); return;
                case Keys.Space: MoveDown(); return;
                case Keys.Down: MoveDown(); return;
                case Keys.Up: MoveTurn(); return;
            }
        }

    }
}
