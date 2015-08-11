using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olimpic_4
{
    internal class Chislo
    {
        private byte[] BigByteArray;
        private byte[] StartValue;
        private int BuffUP;
        private byte[][] BuffArray;
        private int PositionStart;

        public Chislo(int a)
        {
            BuffUP = 0;
            if (Math.Abs(a) < 10) // Однозначное число
            {
                PositionStart = 1;
                StartValue = new byte[2];
                StartValue[1] = Convert.ToByte(Math.Abs(a));
                BuffArray = new byte[2][];
                for (int i = 0; i < BuffArray.Length; i++)
                    BuffArray[i] = new byte[203];
            }
            else if (Math.Abs(a) < 100) // Двухзначное число
            {
                PositionStart = 2;
                StartValue = new byte[3];
                StartValue[1] = Convert.ToByte(Math.Abs(a%10));
                a /= 10;
                StartValue[2] = Convert.ToByte(Math.Abs(a%10));
                BuffArray = new byte[3][];
                for (int i = 0; i < BuffArray.Length; i++)
                    BuffArray[i] = new byte[203];
            }
            else // Сотня
            {
                PositionStart = 3;
                StartValue = new byte[4];
                StartValue[1] = StartValue[2] = 0;
                StartValue[3] = 1;
            }
            //if (a >= 0)
            if(Math.Sign(a) == 1 || Math.Sign(a) == 0)
                StartValue[0] = 1;
            else
                StartValue[0] = 0;

            BigByteArray = new byte[203];
        }

        public void Pow(int pow)
        {
            if (pow == 0)
            {
                if (PositionStart == 3 || PositionStart == 2)
                    PositionStart = 1;
                BigByteArray[0] = BigByteArray[1] = 1;
                BigByteArray[2] = 11;
                return;
            }

            if (PositionStart == 3)
            {
                //if (pow == 1)
                //{
                //    BigByteArray[3]
                //    return;
                //}
                for (int i = 1; i < pow; i++)
                {
                    BigByteArray[2*i - 1] = 0;
                    BigByteArray[2*i] = 0;
                }
                BigByteArray[2*pow + 1] = 1;
                BigByteArray[2*pow + 2] = 11;
                PositionStart = 2*pow + 1;
                if (StartValue[0] == 0 && pow%2 == 1)
                {
                    StartValue[0] = 0;
                    return;
                }
                BigByteArray[0] = 1;
                return;
            }

            if (StartValue.Length == 2)
            {
                if (pow == 1)
                {
                    for (int i = 0; i < StartValue.Length; i++,BigByteArray[i + 1] = 11)
                        BigByteArray[i] = StartValue[i];
                    return;
                }

                for (int i = 1; i <= pow; i++)
                {
                    if (i == 1)
                    {
                        for (int j = 0; j < StartValue.Length; BigByteArray[j + 1] = 11, j++)
                            BigByteArray[j] = StartValue[j];
                        continue;
                    }

                    int end = 0;

                    for (int j = 1; BigByteArray[j] != 11; j++)
                    {
                        if (BuffUP == 0)
                        {
                            if (BigByteArray[j]*StartValue[1] < 10)
                            {
                                BuffArray[0][j] = Convert.ToByte(BigByteArray[j]*StartValue[1]);
                                end = j;
                            }
                            else
                            {
                                BuffArray[0][j] = Convert.ToByte(BigByteArray[j]*StartValue[1]%10);
                                BuffUP = (BigByteArray[j]*StartValue[1])/10;
                                end = j;
                            }
                        }
                        else
                        {
                            if ((BigByteArray[j]*StartValue[1]) + BuffUP < 10)
                            {
                                BuffArray[0][j] = Convert.ToByte((BigByteArray[j]*StartValue[1]) + BuffUP);
                                BuffUP = 0;
                                end = j;
                            }
                            else
                            {

                                BuffArray[0][j] = Convert.ToByte(((BigByteArray[j]*StartValue[1]) + BuffUP)%10);
                                BuffUP = ((BigByteArray[j]*StartValue[1]) + BuffUP)/10;
                                end = j;
                            }
                        }
                    }
                    if (BuffUP != 0)
                    {
                        BuffArray[0][++end] = Convert.ToByte(BuffUP);
                        BuffUP = 0;
                    }
                    BuffArray[0][end + 1] = 11;
                    PositionStart = end;
                    for (int k = 0; BuffArray[0][k] != 11; BigByteArray[k + 1] = 11, k++)
                        BigByteArray[k] = BuffArray[0][k];
                }
            }

            if (StartValue.Length == 3)
            {
                for (int i = 1; i <= pow; i++)
                {
                    if (i == 1)
                    {
                        for (int j = 0; j < StartValue.Length; BigByteArray[j + 1] = 11, j++)
                            BigByteArray[j] = StartValue[j];
                        continue;
                    }


                    for (int buf = 0; buf < 2; buf++)
                    {
                        int end = 0;
                        for (int j = 1; BigByteArray[j] != 11; j++)
                        {

                            if (BuffUP == 0)
                            {
                                if (BigByteArray[j]*StartValue[buf + 1] < 10)
                                {
                                    BuffArray[buf][j] = Convert.ToByte(BigByteArray[j]*StartValue[buf + 1]);
                                    end = j;
                                }
                                else
                                {
                                    BuffArray[buf][j] = Convert.ToByte(BigByteArray[j]*StartValue[buf + 1]%10);
                                    BuffUP = (BigByteArray[j]*StartValue[buf + 1])/10;
                                    end = j;
                                }
                            }
                            else
                            {
                                if ((BigByteArray[j]*StartValue[buf + 1]) + BuffUP < 10)
                                {
                                    BuffArray[buf][j] = Convert.ToByte((BigByteArray[j]*StartValue[buf + 1]) + BuffUP);
                                    BuffUP = 0;
                                    end = j;
                                }
                                else
                                {

                                    BuffArray[buf][j] =
                                        Convert.ToByte(((BigByteArray[j]*StartValue[buf + 1]) + BuffUP)%10);
                                    BuffUP = ((BigByteArray[j]*StartValue[buf + 1]) + BuffUP)/10;
                                    end = j;
                                }
                            }
                        }
                        if (BuffUP != 0)
                        {
                            BuffArray[buf][++end] = Convert.ToByte(BuffUP);
                            BuffUP = 0;
                        }
                        BuffArray[buf][end + 1] = 11;
                        PositionStart = end;
                        //for (int k = 0; BuffArray[0][k] != 11; BigByteArray[k + 1] = 11, k++)
                        //    BigByteArray[k] = BuffArray[0][k];
                    }
                    BuffUP = 0;
                    bool finish1, finish2;
                    finish1 = finish2 = false;
                    for (int j = 1; !finish1 && !finish2; j++)
                    {
                        if (j == 1)
                        {
                            BigByteArray[j] = BuffArray[0][j];
                            continue;
                        }
                        if (BuffArray[0][j] == 11)
                            finish1 = true;
                        if (BuffArray[1][j - 1] == 11)
                            finish2 = true;
                        if (!finish1 && !finish2)
                        {
                            if (BuffUP == 0)
                            {
                                if (BuffArray[0][j] + BuffArray[1][j - 1] < 10)
                                {
                                    BigByteArray[j] = Convert.ToByte(BuffArray[0][j] + BuffArray[1 - 1][j]);
                                    BigByteArray[j + 1] = 11;
                                    PositionStart = j;
                                    continue;
                                }
                                else
                                {
                                    BigByteArray[j] = Convert.ToByte((BuffArray[0][j] + BuffArray[1][j - 1])%10);
                                    BuffUP = (BuffArray[0][j] + BuffArray[1][j - 1])/10;
                                    BigByteArray[j + 1] = 11;
                                    PositionStart = j;
                                    continue;
                                }
                            }
                            else
                            {
                                if (BuffArray[0][j] + BuffArray[1][j - 1] + BuffUP < 10)
                                {
                                    BigByteArray[j] = Convert.ToByte(BuffArray[0][j] + BuffArray[1][j - 1] + BuffUP);
                                    BuffUP = 0;
                                    BigByteArray[j + 1] = 11;
                                    PositionStart = j;
                                    continue;
                                }
                                else
                                {
                                    BigByteArray[j] = Convert.ToByte((BuffArray[0][j] + BuffArray[1][j - 1] + BuffUP)%10);
                                    BuffUP = (BuffArray[0][j] + BuffArray[1][j - 1] + BuffUP)/10;
                                    BigByteArray[j + 1] = 11;
                                    PositionStart = j;
                                    continue;
                                }
                            }
                        }
                        if (finish1)
                        {
                            if (BuffUP == 0)
                            {
                                BigByteArray[j] = BuffArray[1][j - 1];
                                if (BuffArray[1][j + 1] == 11)
                                {
                                    BigByteArray[j + 1] = BuffArray[1][j];
                                    BigByteArray[j + 2] = 11;
                                    PositionStart = j + 1;
                                }
                                else
                                {
                                    BigByteArray[j + 1] = 11;
                                    PositionStart = j;
                                }
                                continue;
                            }
                            else
                            {
                                if (BuffArray[1][j - 1] + BuffUP < 10)
                                {
                                    BigByteArray[j] = Convert.ToByte(BuffArray[1][j - 1] + BuffUP);
                                    BuffUP = 0;
                                    if (BuffArray[1][j+ 1] == 11)
                                    {
                                        BigByteArray[j + 1] = BuffArray[1][j];
                                        BigByteArray[j + 2] = 11;
                                        PositionStart = j + 1;
                                    }
                                    else
                                    {
                                        BigByteArray[j + 1] = 11;
                                        PositionStart = j;
                                    }
                                    continue;
                                }
                                else
                                {
                                    BigByteArray[j] = Convert.ToByte((BuffArray[1][j - 1] + BuffUP)%10);
                                    BuffUP = (BuffArray[1][j] + BuffUP)/10;
                                    BigByteArray[j + 1] = 11;
                                    PositionStart = j;
                                    continue;
                                }
                            }
                        }
                        if (finish2)
                        {
                            if (BuffUP == 0)
                            {
                                BigByteArray[j] = BuffArray[0][j];
                                BigByteArray[j + 1] = 11;
                                PositionStart = j;
                                continue;
                            }
                            else
                            {
                                if (BuffArray[0][j] + BuffUP < 10)
                                {
                                    BigByteArray[j] = Convert.ToByte(BuffArray[0][j] + BuffUP);
                                    BuffUP = 0;
                                    BigByteArray[j + 1] = 11;
                                    PositionStart = j;
                                    continue;
                                }
                                else
                                {
                                    BigByteArray[j] = Convert.ToByte((BuffArray[0][j] + BuffUP)%10);
                                    BuffUP = (BuffArray[0][j] + BuffUP)/10;
                                    BigByteArray[j + 1] = 11;
                                    PositionStart = j;
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
            if (StartValue[0] == 0 && pow%2 == 1)
            {
                BigByteArray[0] = 0;
                return;
            }
            BigByteArray[0] = 1;
        }

        public void Show()
        {
            if (BigByteArray[0] == 0)
                Console.Write("-");
            for (int i = PositionStart; i > 0; i--)
                Console.Write(BigByteArray[i]);
            Console.WriteLine();
        }
    }

    internal class Program
    {

        private static void Main(string[] args)
        {
        //    Chislo[] Array = new Chislo[300];
        //    for (int i = 0; i < 201; i++)
        //        Array[i] = new Chislo(i - 100);
        //    for (int i = 0; i < 201; i++)
        //        Array[i].Pow(2);
        //    for (int i = 0; i < 201; i++)
        //        Array[i].Show();
        //    Console.ReadKey();

            Chislo a = new Chislo(89);
            a.Pow(2);
            a.Show();
        }
    }
}
