using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication7
{
    class board
    {
        public int[,] b1 = new int[,]
        {   {0,0,0,0},
            {0,0,0,0},
            {1,1,1,1},
            {0,0,0,0}   };
        public int[,] b2 = new int[,]
        {
            {0,0,0,0},
            {0,2,2,0},
            {0,2,2,0},
            {0,0,0,0}    };
        public int[,] b3 = new int[,]
        {
            {0,3,0,0},
            {0,3,3,0},
            {0,0,3,0},
            {0,0,0,0}   };
        public int[,] b4=new int[,]
        {
            {0,0,4,0},
            {0,4,4,0},
            {0,4,0,0},
            {0,0,0,0}   };
        public int[,] b5= new int[,]
        {
            {0,5,5,0},
            {0,0,5,0},
            {0,0,5,0},
            {0,0,0,0}   };
        public int[,] b6 = new int[,]
        {
            {0,6,6,0},
            {0,6,0,0},
            {0,6,0,0},
            {0,0,0,0}   };
    }
    class Mainfunction
    {
        
        public int[,] mainB = new int[4, 4];
        public int i, j;
        board temp = new board();

        public Mainfunction()
        {
            i = 0; j = 3;
            Random random = new Random();
            int k = random.Next(1, 7);
            
            switch (k)
            {
                case 1:
                    mainB = temp.b1;
                    break;
                case 2:
                    mainB = temp.b2;
                    break;
                case 3:
                    mainB = temp.b3;
                    break;
                case 4:
                    mainB = temp.b4;
                    break;
                case 5:
                    mainB = temp.b5;
                    break;
                case 6:
                    mainB = temp.b6;
                    break;
                default:
                    break;
            }
        }
        public void put(ref int[,] map)
        {
            for (int a = 0; a < 4; a++)
            {
                for (int b = 0; b < 4; b++)
                {
                    if (i + a< 0 || i + a > 15 || j + b < 0 || j + b > 10) { continue; }
                    map[i + a, j + b] += mainB[a, b];
                }
            }
        }
        public void move(int k)
        {
            if (k == 3)
            {
                int[,] temp= new int[4,4];
                for (int a = 0; a < 4; a++)
                {
                    for (int b = 0; b < 4; b++)
                    {
                        temp[a, b] = mainB[3-b,a];
                    }
                }
                mainB = temp;
            }

            switch(k)
            {
                case 0:
                    j--;
                    break;
                case 1:
                    j++;
                    break;
                case 2:
                    i++;
                    break;
                default:
                    break;
            }
        }
        public int check(int[,] map, int time, int move, int bottom)
        {
            int check = 0;

            for (int a = 0; a < 10; a++)
            {
                if (map[time - bottom, a] != 0)
                {
                    check = 1;
                }
            }

            if (bottom > 3)
            {
                check = 2;
                return check;
            }

            if (check == 1)
            {

                for (int a = 0; a < 4; a++)
                {


                    //Console.WriteLine(mainB[3 - bottom,i]);
                    for (int b = 0; b <= bottom; b++)
                    {

                        if (time > 16)
                        {
                            if (map[time - bottom,move +a ] != 0 && mainB[3 - bottom,a ] != 0)
                            {
                                check = 2;
                                return check;
                            }
                        }
                        else if (map[time - b,move +a ] != 0 && mainB[3 - b, a] != 0)
                        {
                            check = 2;
                            return check;
                        }
                    }


                }


            }

            return check;
        }
        
    }
}
