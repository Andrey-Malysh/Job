using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olimpiada_0
{
    class Program
    {
        private static void Main(string[] args)
        {
            int[] _intArrayAnswer = new int[2];
            string _str1 = Console.ReadLine();
            string[] _strArray = _str1.Split(' ');
            int[] _intArray = new int[2];
            for (int i = 0; i < 2; i++)
                _intArray[i] = Convert.ToInt32(_strArray[i]);
            if (_intArray[0] > _intArray[1])
            {
                int buff = _intArray[0];
                _intArray[0] = _intArray[1];
                _intArray[1] = buff;
            }
            _intArrayAnswer[0] = _intArray[1] - _intArray[0] - 1;
            _intArrayAnswer[1] = 10 + _intArray[0] - _intArray[1];
            if (_intArrayAnswer[0] == 0)
                Console.WriteLine("9" + _intArrayAnswer[1]);
            else
                Console.WriteLine(_intArrayAnswer[0] + "9" + _intArrayAnswer[1]);
        }
    }
}
