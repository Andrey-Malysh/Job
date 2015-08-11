using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olimpic_2
{
    internal class Program
    {
        private static int _buf;
        private static string _str1;

        private static bool TryParse(int index, ref int count)
        {
            if (!Int32.TryParse(_str1[index].ToString(), out _buf))
            {
                count = 0;
                return false;
            }
            else if (!Int32.TryParse(_str1[index].ToString() + _str1[index + 1].ToString(), out _buf))
            {
                Int32.TryParse(_str1[index].ToString(), out _buf);
                count = 1;
                return true;
            }else if (
                !Int32.TryParse(
                    _str1[index].ToString() + _str1[index + 1].ToString() + _str1[index + 2].ToString(), out _buf))
            {
                Int32.TryParse(_str1[index].ToString() + _str1[index + 1].ToString(), out _buf);
                count = 2;
                return true;
            }
            else
            {
                Int32.TryParse(
                    _str1[index].ToString() + _str1[index + 1].ToString() + _str1[index + 2].ToString(), out _buf);
                count = 3;
                return true;
            }
        }

        private static void Main(string[] args)
        {
            //string a = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
            //Console.WriteLine(a.Length);
            //Console.ReadKey();


            int count = 0;
            _str1 = Console.ReadLine();
            string _strAnswer = "";
            for (int i = 0; i < _str1.Length; i++)
            {
                if (TryParse(i, ref count))
                {
                    for (int j = 0; j < _buf; j++)
                        _strAnswer += _str1[i + count];
                    i += count;
                }
                else
                    _strAnswer += _str1[i];
            }
            if (_strAnswer.Length > 40)
            {
                for(int i = 0; i < 40; i++)
                    Console.Write(_strAnswer[i]);
                Console.WriteLine();
                for (int i = 40; i <_strAnswer.Length ; i++)
                Console.Write(_strAnswer[i]);
            }
            else
                Console.WriteLine(_strAnswer);
            }
    }
}
