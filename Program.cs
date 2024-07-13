using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Day04
{
    internal class Program
    {

        static void Main2(string[] args)
        {
            int[] array;
            array = new int[5];
            array[0] = 1;
            array[1] = 2;
            array[2] = 3;
            array[3] = 4;
            array[4] = 5;

            //for (int i = 0; i < array.length; i++)
            //{
            //    Console.WriteLine(); (array[i]);
            //}

            foreach (int item in array)//更加简单的输出方法（有局限） 依次读取 读取全部
            {
                Console.WriteLine(item);
            }







        }//主函数
        static void Main1(string[] args)
        {
            //float[] scoreArray = CreateScoreArray();
            //float max = GetMax(scoreArray);
            //Console.WriteLine("最高分为："+max);
            Console.WriteLine("这一年已经过了："+GetTotaDays(2024, 6, 21)); 


        }
        static float[] CreateScoreArray()
        {
            Console.WriteLine("请输入需要录入的学生人数：");
            string StudentNum = Console.ReadLine();
            int StudentNumber = int.Parse(StudentNum);
            float[] scoreArray = new float[StudentNumber];
            for(int i=0;i< scoreArray.Length;)
            {
                Console.WriteLine("请输入第{0}个学生的成绩：",i+1);
                float score = float.Parse(Console.ReadLine());
                

                if (score >= 0 && score <= 100)
                {
                    scoreArray[i++] = score;//重点i++放置的位置
                }
                else
                {
                    Console.WriteLine("您的输入有误，请重新输入");
                }
            }

            return scoreArray;

        }//输入学生人数以及成绩
        private static float GetMax(float[] array)//求最高分
        {
            float max = array[0];
            for(int i = 0; i < array.Length;i++)
            {
                if (max < array[i])
                {
                    max = array[i];
                }
            }
            return max;
        }
        private static int GetTotaDays(int year,int month,int day)
        {
            int[] daysofmonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            if(lsLeapYear(year))
            {
                daysofmonth[1] = 29;
            }
            int days = 0;
            for(int i=0;i<month-1;i++)
            {
                days += daysofmonth[i];
            }
            days += day;
            return days;
        }//输出年月日
        private static bool lsLeapYear(int year)//判断闰年
        {
            return (year%4==0&&year%100!=0) || year % 400 == 0;
        }

        //彩票

        static void Main3(string[] args)
        {
            int[] PlayerTicket = BuyTicket();
            Console.WriteLine("您购买的彩票是：");
            for (int i = 0; i < 7; i++)
            {
                Console.Write(PlayerTicket[i] + " ");
            }
            Console.WriteLine("\n");
            int[] RandomTicket = CreateRandomTicket();
            Console.WriteLine("今日中奖号码为：");
            for (int i = 0; i < 7; i++)
            {
                Console.Write(RandomTicket[i] + " ");
            }
            Console.WriteLine("\n");
            int level = TicketEquals(PlayerTicket, RandomTicket);
            if (level != 0)
            {
                Console.WriteLine("恭喜您中{0}等奖", level);
            }
            else
            {
                Console.WriteLine("抱歉，此次你没能中奖，请下次努力");
            }
        }
        private static int[] BuyTicket()
        {
            int[] ticket = new int[7];
            for(int i=0;i<6;)
            {
                Console.WriteLine("请输入第{0}个红球的号码：",i+1);
                int redNumber = int.Parse(Console.ReadLine());
                if(redNumber<1||redNumber>33)
                {
                    Console.WriteLine("购买的号码有误");
                }
                else if(Array.IndexOf(ticket,redNumber)>=0 )//数组的索引，意思大概为在ticket中寻找redNumber，
                                                                                         //当索引到redNumber数值>=0,
                                                                                         //说明redNumber在数组ticket中出现过了
                                                                                         //例如有一个数组a[]={0,1,9,3,4,5}
                                                                                         //往里面查找9则会输出2，如果查找8则会输出-1
                {
                    Console.WriteLine("号码已经存在");
                }
                else
                {
                    ticket[i++] = redNumber;
                }


            }


            while (true)
            {
                Console.WriteLine("请输入蓝球号码：");
                int blueNumber = int.Parse(Console.ReadLine());
                if (blueNumber >= 1 && blueNumber <= 16)
                {
                    ticket[6] = blueNumber;
                    break;
                }
                else
                {
                    Console.WriteLine("号码超出范围");
                }
            }
            return ticket;
        }//买彩票

        static Random random = new Random();
        private static int[] CreateRandomTicket()
        {
            int[] ticket = new int[7];
            for(int i=0;i<6;i++)
            {
                int redNumber = random.Next(1, 34);
                if(Array.IndexOf(ticket, redNumber)<0)//判断随机生成的数是否重复
                {
                    ticket[i] = redNumber;
                }

            }
            ticket[6] = random.Next(1, 17);
            //红球排序(冒泡方法)//更简洁的Array.Sort(ticket,0,6)
            for (int i = 0; i < ticket.Length - 2; i++)//本来这里ticket.Length是-1，但是我们不想蓝球也跟着排序，我们只要红球排序，所以-2
            {
                for (int j = 0; j < ticket.Length - 2 - i; j++)
                {
                    if (ticket[j] > ticket[j + 1])
                    {
                        // 交换元素  
                        int temp = ticket[j];
                        ticket[j] = ticket[j + 1];
                        ticket[j + 1] = temp;
                    }
                }
            }
            return ticket;
        }//输出随机中奖号码

        private static int TicketEquals(int[] myTicket, int[]randomTicket)
        {
            int blueCount = myTicket[6] == randomTicket[6] ? 1 : 0;
            int RedCount = 0;
            for (int i=0;i<6;i++)
            {
                if (Array.IndexOf(randomTicket, myTicket[i],0,6) >= 0)//拿我们的第一个号码跟中将号码中的六个比较，消除排序影响（号码只需要有就行而不是一一对应）
                {
                    
                    RedCount++;
                }
            }
            int level=0;
            if(blueCount+RedCount==7)
            {
                level = 1;
            }
            else if(RedCount==6)
            {
                level = 2;
            }
            else if(blueCount+RedCount==6)
            {
                level = 3;
            }
            else if(RedCount+blueCount==5)
            {
                level = 4;
            }
            else if(RedCount+blueCount==4)
            {
                level = 5;
            }
            else if(blueCount==1)
            {
                level = 6;
            }
            else
            {
                level = 0;
            }
            

            return level;
        }//判断中奖


        //二维数组
        static void Main4(string[] args)
        {

            //for (int i = 0; i < 4; i++)
            //{
            //    for (int j = 0; j <= i; j++)
            //    {
            //        Console.Write("#");
            //    }
            //    Console.WriteLine();
            //}



            for (int h = 0; h < 100; h++)
            {
                for (int k = 0; k < h; k++)
                {
                    Console.Write(" ");
                }
                for (int j = 100; j > h; j--)
                {
                    Console.Write("#");
                }
                Console.WriteLine();
            }


            int[,] arrayone = new int[5, 3];//定义int[,]多一个逗号
            arrayone[0, 2] = 3;
            arrayone[1, 0] = 13;
            arrayone[3, 1] = 5;


            //Array.GetLength(0)  行数
            //Array.GetLength(1)  列数
            for (int r = 0; r < arrayone.GetLength(0); r++)
            {
                for(int c=0;c<arrayone.GetLength(1);c++)
                {
                    Console.Write(arrayone[r,c]+"\t");
                }
                Console.WriteLine();
            }

    }

        //二维数组练习
        static void Main5(string[] args)
        {
            float[,] arraytwo = Createarraytwo();
            PrintDoubleArray(arraytwo);
        }
        private static float[,] Createarraytwo()//将数据输入二维数组
        {
            Console.WriteLine("请输入学生总数");
            int sumNumberofstudent = int.Parse(Console.ReadLine());
            Console.WriteLine("请输入科目总数");
            int sumNumberofsubject = int.Parse(Console.ReadLine());
            float[,] arraytwo = new float[(sumNumberofstudent), (sumNumberofsubject)];


            //将输入的数据填入表格中
            for (int i = 0; i < arraytwo.GetLength(0); i++)
            {
                for (int j = 0; j < arraytwo.GetLength(1); j++)
                {
                    Console.WriteLine("请输入第{0}个学生的第{1}门成绩", i + 1, j + 1);//{0}为占位符{1}为新的占位符，占位符写的一样输出结果一样
                    arraytwo[i, j] = int.Parse(Console.ReadLine());
                }
            }
            return arraytwo;
        }
        private static void PrintDoubleArray(float[,]arraytwo)//输出表格
        {
            // 打印一个制表符以对齐标题
            Console.Write("\t");
            for (int j = 0; j < arraytwo.GetLength(0); j++)
            {
                Console.Write("科目{0}\t", j + 1);
            }
            Console.WriteLine();

            //输出最后的表格
            string studentNumberFormat = "学生{0,10}\t";
            for (int i = 0; i < arraytwo.GetLength(1); i++)
            {
                if (i < arraytwo.GetLength(1))
                {
                    Console.Write("学生{0}", i + 1);
                }

                for (int j = 0; j < arraytwo.GetLength(0); j++)
                {
                    Console.Write("{0,3}\t", arraytwo[i, j] + "\t");
                }
                Console.WriteLine();
            }
            //           科目1       科目2    科目3    科目4
            //学生1   [0,0]        [0,1]     [0,2]     [0,3]
            //学生2   [1,0]        [1,1]     [1,2]     [1,3]
            //学生3   [2,0]        [2,1]     [2,2]     [2,3]
            //学生4   [3,0]        [3,1]     [3,2]     [3,3]
        }



        //******************************2048游戏思路*****************************//
        /* 需求分析（重点）
         * 合并数据：
         * 上移：
         * 1.去零：将0元素移动到末尾
         * 2.相邻相同相加（后一个加前一个，并将后一个元素清零）
         * 下移:
         */
       
        static void Main6()
        {
            int[,] map = new int[4, 4];

            // 使用循环来初始化数组  
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    // 假设你有以下值要设置  
                    // 第一行: 2, 2, 4, 8  
                    // 第二行: 2, 4, 4, 4  
                    // 第三行: 0, 8, 4, 0  
                    // 第四行: 2, 4, 0, 4  
                    switch (i)
                    {
                        case 0:
                            map[i, j] = (j == 0 || j == 1) ? 2 : (j == 2) ? 4 : 8;
                            break;
                        case 1:
                            map[i, j] = (j == 0) ? 2 : 4;
                            break;
                        case 2:
                            map[i, j] = (j == 0) ? 0 : (j == 1) ? 8 : (j == 2) ? 4 : 0;
                            break;
                        case 3:
                            map[i, j] = (j == 0 || j == 1) ? 2 : (j == 2) ? 0 : 4;
                            break;
                    }
                }
            }

            // 打印数组以验证  
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j] + " ");
                }
                Console.WriteLine();
            }
            PrintDoubleArray(map);
            Console.WriteLine("上移");
            map = MoveUp(map);
            PrintDoubleArray(map);

            Console.WriteLine("下移");
            map = MoveDown(map);
            PrintDoubleArray(map);

            Console.WriteLine("左移");
            map = MoveLeft(map);
            PrintDoubleArray(map);

            Console.WriteLine("右移");
            map=MoveRight(map);
            PrintDoubleArray(map);


        }
        //去零（将零在移位之前就将其放到最后一位）
        private static int[] RemoveZero(int[] array)
        {
            int[] newArray = new int[array.Length];
            int index = 0;
            //将非零元素依次赋值给新数组
            for(int i=0;i<array.Length;i++)
            {
                if (array[i]!=0)
                {
                    newArray[index++] = array[i];
                }
            }
            return newArray;
        }

       //相同的项相加
        private static int[] Merge(int[]array)
        {
            array = RemoveZero(array);
            for(int i=0;i<array.Length-1;i++)//前面三个和它的后一位比就行
            {
                if (array[i]!=0&&array[i] == array[i+1])//加array[i]!=0加不加不影响结果，但是会影响后面的动画，所以要加
                {
                    array[i] += array[i + 1];
                    array[i + 1] = 0;
                }
            }
            array = RemoveZero(array);
            return array;
        }

        //*上移* 
        private static int[,] MoveUp(int[,]map)
        {
            int[] mergearray = new int[map.GetLength(0)];
            for (int c = 0; c < map.GetLength(1); c++)
            {
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    mergearray[i] = map[i, c];
                }
                mergearray = Merge(mergearray);
                for (int i = 0; i < map.GetLength(0); i++)
                {
                    map[i, c] = mergearray[i];
                }
            }
            return map;
        }//(将二维数组里的数据一条一条抽取出来，计算完上移位之后再将它还回去)

        //*下移*
        private static int[,] MoveDown(int[,]map)
        {
            int[] mergearray = new int[map.GetLength(0)];
            for (int c = 0; c < map.GetLength(1); c++)
            {
                for(int r=map.GetLength(0)-1;r>=0;r--)
                {
                    mergearray[3 - r] = map[c, r];//关键点
                }
                mergearray = Merge(mergearray);
                for (int r = map.GetLength(0)-1; r >= 0; r--)
                {
                     map[r, c]=mergearray[3 - r];
                }
            }
            return map;
        }//（拿数据、计算、还数据）

        //*左移*
        private static int[,] MoveLeft(int[,] map)
        {
            int[] mergearray = new int[map.GetLength(0)];

            for (int r = 0; r < map.GetLength(0); r++)
            {
                for (int c = 0; c < map.GetLength(1) ; c++)
                {
                    mergearray[c] = map[r, c];
                }
                mergearray = Merge(mergearray);
                for (int c = 0; c < map.GetLength(1); c++)
                {
                    map[r, c] = mergearray[c];
                }
            }
            return map;
            //[0,0]     [0,1]     [0,2]     [0,3]    2       2        4         8        |       4       4       8      0        
            //[1,0]     [1,1]     [1,2]     [1,3]    2       4        4         4        |       2       8       4      0
            //[2,0]     [2,1]     [2,2]     [2,3]    0       8        4         0        |       8       4       0      0
            //[3,0]     [3,1]     [3,2]     [3,3]    2       2        0         4        |       4       4       0      0
        }

        //*右移*
        private static int[,] MoveRight(int[,] map)
        {
            int[] mergearray = new int[map.GetLength(0)];
            for (int r = 0; r < map.GetLength(0); r++)
            {
                for (int c = map.GetLength(1)-1; c >= 0; c--)
                {
                    mergearray[3-c] = map[r, c];
                }
                mergearray = Merge(mergearray);
                for (int c = map.GetLength(1)-1; c >= 0; c--)
                {
                    map[r, c] = mergearray[3-c];
                }
            }
            return map;
        }

        //打印
        private static void PrintDoubleArray(Array array)
        {
            for(int r=0;r<array.GetLength(0);r++)
            {
                for(int c=0;c<array.GetLength(1);c++)
                {
                    Console.Write(array.GetValue(r,c)+"\t");
                }
                Console.WriteLine();
            }
        }

        //不规则用交错数组
        static void Main()
        {
            //int[][] array01;
            //array01 = new int[4][];//创建4行不规则数组
            //array01[0] = new int[3];// 交错数组的第一个元素 第一行有三个数
            //array01[0][0] = 1;


            //object o1 = 1;
            //object o2 = o1;
            //o1 = 2;
            //Console.WriteLine(o2);


            //单词反转
            string originalString = "Hello world this is a test";

            // 使用空格分割字符串为单词数组  
            string[] words = originalString.Split(' ');

            // 反转单词数组  
            Array.Reverse(words);

            // 使用空格将反转后的单词重新组合成字符串  
            string reversedString = string.Join(" ", words);

            // 输出结果  
            Console.WriteLine(reversedString);




            //字符反转
            
                string originalStringtwo = "Hello, World!";
                char[] charArray = originalString.ToCharArray();
                int length = charArray.Length;

                // 使用循环来反转字符数组  
                for (int i = 0; i < length / 2; i++)
                {
                    char temp = charArray[i];
                    charArray[i] = charArray[length - i - 1];
                    charArray[length - i - 1] = temp;
                }

                // 将反转后的字符数组转换回字符串  
                string reversedStringtwo = new string(charArray);

                // 输出结果  
                Console.WriteLine(reversedString); // 输出: !dlroW ,olleH  


            string input = "hello world, hello universe!";
            string result = RemoveDuplicateChars(input);
            Console.WriteLine(result);

        }

        static string RemoveDuplicateChars(string input)
        {
            var charCounts = new Dictionary<char, int>();
            foreach (char c in input)
            {
                if (charCounts.ContainsKey(c))
                {
                    charCounts[c]++;
                }
                else
                {
                    charCounts[c] = 1;
                }
            }

            // 假设我们想要保留第一个出现的字符  
            StringBuilder sb = new StringBuilder();
            foreach (char c in input)
            {
                if (charCounts[c] == 1)
                {
                    sb.Append(c);
                }
                else if (charCounts[c] > 1 && sb.Length == 0 || sb[sb.Length - 1] != c)
                {
                    // 如果字符重复，但之前未添加到结果中，则添加一次  
                    sb.Append(c);
                }
                // 如果字符已经添加到结果中，则不再添加  
            }

            return sb.ToString();
        }


    }
    
}
