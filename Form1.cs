using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication7
{
    public partial class Form1 : Form
    {
        private Mainfunction block = new Mainfunction();
       
        private int[,] map = new int[17,10]
        {
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {7,7,7,7,7,7,7,7,7,7}
        };

        private Bitmap[] tiles = new Bitmap[]
        {
            Properties.Resources.그림1,
            Properties.Resources.그림2,
            Properties.Resources.그림3,
            Properties.Resources.그림4,
            Properties.Resources.그림5,
            Properties.Resources.그림6,
            Properties.Resources.그림7,
            Properties.Resources.그림8
        };

        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void Form1_Paint_1(object sender, PaintEventArgs e)
        {
            //배경
            for (int row = 0; row < map.GetLength(0); row++)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {
                    e.Graphics.DrawRectangle(new Pen(Brushes.DeepSkyBlue), 20, 20, 120, 190);

                    e.Graphics.DrawImage(tiles[map[row, col]],
                        new Rectangle(col * 12 + 20, row * 12 + 20, 12, 12));
                }
            }
            //타일
                for (int a = 0; a < 4; a++)
                {
                    for (int b = 0; b < 4; b++)
                    {
                        if (block.mainB[a, b] == 0) { continue; }
                        e.Graphics.DrawImage(tiles[block.mainB[a, b]],
                            new Rectangle((block.j + b) * 12 + 20,(block.i+a) * 12 + 20, 12, 12));
                    }
                }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            block.i += 1;
            blockCheck1();
            Invalidate();
            
        }


      //  private void button1_Click(object sender, EventArgs e)
      //  {  timer1.Start();  }

      //  private void button2_Click(object sender, EventArgs e)
      //  { timer1.Stop();  }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int k=4;
            if (e.KeyCode == Keys.Left&& block.j!=0) {  k = 0; }
            if (e.KeyCode == Keys.Right&&block.j!=10) { k = 1; }
            if (e.KeyCode == Keys.Down) { k = 2; }
            if (e.KeyCode == Keys.Up) { k = 3; }
            block.move(k);
           
            e.Handled = true;
            
            blockCheck1();
            Invalidate();
        }
        private void blockCheck1()
        {
            int[] checkfour={3,3,3,3};
            for (int num = 0; num <4; num++)
            {
                int k=3;
                do
                {
                    if (k<0) { break; }
                    if (block.mainB[k,num] == 0)
                    {
                        k--;
                        checkfour[num] = k;
                    }
                    else { break; }
                } while (true);
            }
            for (int num = 0; num < 4; num++)
            {
                if (checkfour[num] < 0) { continue; }
                if (map[block.i + checkfour[num], block.j + num]!= 0)
                {
                    block.i -= 1;
                    block.put(ref map);
                    block = new Mainfunction();
                }
            }
        }
    }
}
