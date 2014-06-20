using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;

namespace WindowsFormsApplication7
{
    public partial class Form1 : Form
    {
		private Random random;
        private SoundPlayer soundPlay1;
        private void bitsound(int k)
        {
            switch (k)
            {
                case 0://멈춤
                    soundPlay1 = new SoundPlayer(Properties.Resources.pause1);
                    soundPlay1.Play();

                    break;
                case 1://멈췄다 실행
                    //soundPlay1 = new SoundPlayer(Properties.Resources.pause2);
                    //soundPlay1.Play();
                    soundbackg.PlayLooping();
                    break;
                /*case 2://한줄 제거
                    soundPlay1 = new SoundPlayer(Properties.Resources.rm);
                    soundPlay1.Play();
                    
                    break
                case 3://쌓을때
                    soundPlay1 = new SoundPlayer(Properties.Resources.stack);
                    soundPlay1.Play();
                    soundbackg.PlayLooping();
                    break;
                  */
                case 4://stageup
                    soundPlay1 = new SoundPlayer(Properties.Resources.stack);
                    soundPlay1.Play();
                    break;
                default:
                    break;
            }

        }
        private SoundPlayer soundbackg;

        int bk = 0;
        int lastbk = 0;

		private Mainfunction block;
		private Mainfunction nextblock;
       
        private int[,] map = new int[,]
        {
            {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
            {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
            {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
            {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
            {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
            {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
            {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
            {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
            {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
            {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
            {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
            {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
            {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
            {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
            {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
            {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
            {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
            {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
            {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
			{7,7,0,0,0,0,0,0,0,0,0,0,7,7},
            {7,7,7,7,7,7,7,7,7,7,7,7,7,7},
            {7,7,7,7,7,7,7,7,7,7,7,7,7,7}
        };

        private Bitmap[] tiles = new Bitmap[]
        {
            Properties.Resources.block1,
            Properties.Resources.block2,
            Properties.Resources.block3,
            Properties.Resources.block4,
            Properties.Resources.block5,
            Properties.Resources.block6,
            Properties.Resources.block7,
            Properties.Resources.block8
        };
        private Bitmap[] background = new Bitmap[]//0진행중 //1시작화면 //2일시정지화면
        {
            Properties.Resources._base, 
            Properties.Resources.start,
            Properties.Resources.pause
        };
        private Bitmap[] stageImage = new Bitmap[]
        {
            Properties.Resources.stage2,
            Properties.Resources.stage3,
            Properties.Resources.stage4,
            Properties.Resources.stage5,
            Properties.Resources.stage6,
            Properties.Resources.stage7,
            Properties.Resources.stage8
        };
        private Bitmap[] stageBar = new Bitmap[]
        {
            Properties.Resources.stagebar1,
            Properties.Resources.stagebar2,
            Properties.Resources.stagebar3,
            Properties.Resources.stagebar4,
            Properties.Resources.stagebar5,
            Properties.Resources.stagebar6,
            Properties.Resources.stagebar7
        };


		int stage = 1;
		int score = 0;

		public Lists list = new Lists();

        public Form1()
        {
            InitializeComponent();

			random = new Random();
			
			nextblock = new Mainfunction(random);
			block = nextblock;
			nextblock = new Mainfunction(random);
			
			label3.Text = stage.ToString();
            label4.Text = score.ToString();
            
        }
        private void InitializeSound()
        {
            soundbackg = new SoundPlayer(Properties.Resources.bgm);
            soundbackg.PlayLooping();
        }
        private void EndSound()
        { soundbackg.Stop(); }

        private void Form1_Paint_1(object sender, PaintEventArgs e)
        {
            //토토로 배경
            if (bk == 0)//처음 화면
            {
                
                e.Graphics.DrawImage(background[1], new Rectangle(0, 0, 454, 486));
                return;
            }
            else if (bk == 1)//실행중
            {

                timer1.Start();
                
                //soundbackg = new SoundPlayer(Properties.Resources.bgm);

                e.Graphics.DrawImage(background[0], new Rectangle(0, 0, 454, 486));
            }
            else if (bk == 2)//p
            {
                if (lastbk == bk)
                    
                {   timer1.Start();
                    bitsound(1);
                    bk=1;
                }
                else 
                {   
                    timer1.Stop();
                    bitsound(0);
                    e.Graphics.DrawImage(background[2], new Rectangle(0, 0, 454, 486));
                    return;
                    
                }
            }
            else if (bk == 3)//q
            {
				
				timer1.Stop();
                EndSound();

                soundbackg = new SoundPlayer(Properties.Resources.end);
                soundbackg.Play();
				scoreboard();
				
				Form2 form = new Form2(list);

				form.ShowDialog();
				bk = 0;
				mapReset();

				Invalidate();
				
                return;

            }
            else if (bk == 4)//n
            {
                if (lastbk == bk)
                {
                    timer1.Start();
                    soundbackg.PlayLooping();
                    bk = 1;
					lastbk = 0;
                }
                else
                {
                    timer1.Stop();
                    soundbackg.Stop(); bitsound(4);
                    e.Graphics.DrawImage(background[0], new Rectangle(0, 0, 454, 486));
                    e.Graphics.DrawImage(stageImage[(stage+5)%7], new Rectangle(20, 20, 280, 440));
                    e.Graphics.DrawString(" stage: " + stage.ToString(),
                        new Font("Arial", 24), new SolidBrush(Color.White), 85, 210);
                    e.Graphics.DrawString(" press'n'", new Font("Arial", 24), new SolidBrush(Color.White), 85, 250);
                    return;

                }
 
            }
            //background
            e.Graphics.DrawImage(background[0], new Rectangle(0,0,454,486));
            

			//타일 배경
            for (int row = 0; row < map.GetLength(0); row++)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {
                    //e.Graphics.DrawRectangle(new Pen(Brushes.DeepSkyBlue), 44, 56, 96, 192);
                    if (map[row, col] == 0) { continue; }
                    e.Graphics.DrawImage(tiles[map[row, col]],
                        new Rectangle(col * 20 + 20, row * 20 + 20, 20, 20));
                }
            }
            //타일
                for (int a = 0; a < 4; a++)
                {
                    for (int b = 0; b < 4; b++)
                    {
                        if (block.mainB[a, b] == 0) { continue; }
                        e.Graphics.DrawImage(tiles[block.mainB[a, b]],
                            new Rectangle((block.j + b) * 20 + 20,(block.i+a) * 20 + 20, 20, 20));
                    }
                }

			//Next tile
				for (int a = 0; a < 4; a++)
				{
					for (int b = 0; b < 4; b++)
					{
						if (nextblock.mainB[a, b] != 0)
						{
							e.Graphics.DrawImage(tiles[nextblock.mainB[a, b]],
							new Rectangle((16+b)*20+20, (4+a)*20+4, 20, 20));
						}
					}
				}
            //stageBar
            e.Graphics.DrawImage(stageBar[(stage+6) % 7], new Rectangle(20, 20, 280, 60));
            e.Graphics.DrawString(" stage: " + stage.ToString(),
                        new Font("Arial", 24), new SolidBrush(Color.White), 85, 30);
            string think="";
            for(int temp=0;temp<stage;temp++)
            {
                think+="!";
                if (temp % 7 == 6) { think += "\n"; }
            }
            e.Graphics.DrawString(think, new Font("Arial", 10), new SolidBrush(Color.Black), 340-stage, 340);
            
            
        }
		private void timer1_Tick(object sender, EventArgs e)
        {
            block.i += 1;
            blockCheck2(2);
            mapcheck3();
            
            //score+=500;

			label4.Text = score.ToString();

			if (score > stage * 2000)
			{
                
				stage++;
				stageUp();
                bk = 4;
				label3.Text = stage.ToString();
                if (500 - 20 * stage > 0)
                {
                    timer1.Interval = 500 - 20 * stage;
                }
				timer1.Stop();
			}

            Invalidate();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

			if (bk == 0)
			{
                if (e.KeyCode == Keys.S) { bk = 1; InitializeSound(); }
			}
            else if (bk == 4)
            {
                if (e.KeyCode == Keys.N)
                {
                    bk = 1;
                }
            }
            else
            {
                if (e.KeyCode == Keys.P&&(bk==1||bk==2)) { 
                    lastbk = bk; 
                    bk = 2;
                }
                
                if (e.KeyCode == Keys.Q&&(bk==1)) { bk = 3; }

                int myKey = 8;

                if (e.KeyCode == Keys.Left) { myKey = 0; }
                if (e.KeyCode == Keys.Right) { myKey = 1; }
                if (e.KeyCode == Keys.Down) { myKey = 2; }
                if (e.KeyCode == Keys.Space) { myKey = 4; }//바로 내려가기
                if (e.KeyCode == Keys.Up) { myKey = 3; }//회전

                block.move(myKey);
                blockCheck2(myKey);

                while (myKey == 4 && blockCheck2(2) == 0)
                {
                    block.move(2);
                }

                e.Handled = true;

                mapcheck3();

            }

			Invalidate();
        }
        private int blockCheck2(int k)//블록이 가도 되는지 체크
        {
            int returnint = 0;
            for (int a = 0; a < 4; a++)
            {
                for (int b = 0; b < 4; b++)
                {
                    if (block.mainB[a, b] != 0 && map[block.i + a, block.j + b] != 0)
                    {

                        block.backmove(k);
                        if (k == 2)
                        {
                            returnint = 1;
                            block.put(ref map);
                            bitsound(3);
                            score += 10;

                            block = nextblock;
                            nextblock = new Mainfunction(random);

                            if (map[2,7]!=0)
                            {
                                bk = 3;
                            }
                        }
                    }
                }
            }
            return returnint;
        }
        private void mapcheck3()
        {
            for (int row1 = 0; row1 < map.GetLength(0)-2; row1++)
            {
                int zerosum = row1;
                for (int col1 = 2; col1 < map.GetLength(1)-2; col1++)
                {
                    if (map[row1, col1] == 0)
                    {  
                        zerosum=-1; 
                        break;
                    }
                }
                if (zerosum == row1)
                {
                    for (int row2=zerosum; row2 > 0; row2--)
                    {
                        for (int col2=2; col2 < map.GetLength(1)-2; col2++)
                        {
                            map[row2, col2] = map[row2 - 1, col2];
                        }
                    }

					score = score + 100;
                    bitsound(2);
                    if (timer1.Interval > 30)
                    {
                        timer1.Interval = timer1.Interval - 20;
                    }
                }

            }
            Invalidate();   
        }//한줄 없애기
        private void mapReset()
        {
			stageUp();
            stage = 1;
            score = 0;

			timer1.Interval = 500;

			list.first = null;
            

            nextblock = new Mainfunction(random);
            block = nextblock;
            nextblock = new Mainfunction(random);

            label3.Text = stage.ToString();
            label4.Text = score.ToString();
            
        }//끝날때 맵 리셋
		public void stageUp()
		{
            block = nextblock;
            nextblock = new Mainfunction(random);
            
			map = new int[,]
            {
                
                {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
                {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
                {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
                {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
                {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
                {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
                {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
                {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
                {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
                {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
                {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
                {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
                {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
                {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
                {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
                {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
                {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
                {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
                {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
			    {7,7,0,0,0,0,0,0,0,0,0,0,7,7},
                {7,7,7,7,7,7,7,7,7,7,7,7,7,7},
                {7,7,7,7,7,7,7,7,7,7,7,7,7,7}
            };
		}//스테이지 업
		public void scoreboard()
		{
			string name;
			MessageBox.Show("Score: " + score);

			if (File.Exists("score.data"))
			{
				BinaryReader br = new BinaryReader(new FileStream("score.data", FileMode.Open));
				while (br.PeekChar() != -1)
				{
					Member t = new Member(br.ReadString(), br.ReadInt32());
					list.Add(t);
				}
				br.Close();
			}

			Member last = null;
			int membernum = 0;

			for (Member t = list.first; t != null; t = t.next)
			{
				membernum++;

				if (t.next == null)
				{
					last = t;
				}
			}

			if (last == null || last.score < score || membernum < 30)
			{

				name = Microsoft.VisualBasic.Interaction.InputBox("닉네임을 작성해주세요\n\n한번 저장된 점수는 수정이 불가능합니다\n\n획득한 점수 : " + score + "점", "점수 기록", "");

				Member new_mem = new Member(name, score);

				list.Add(new_mem);
			}

			if (membernum > 30)
			{
				for (Member t = list.first; t != null; t = t.next)
				{
					if (t.next.next== null)
					{
						t.next = null;
					}
				}
			}

			BinaryWriter bs = new BinaryWriter(new FileStream("score.data", FileMode.Create));

			for (Member t = list.first; t != null; t = t.next)
			{
				bs.Write(t.name);
				bs.Write(t.score);

			}

			bs.Close();

		}//점수판
        
    }
	
}
