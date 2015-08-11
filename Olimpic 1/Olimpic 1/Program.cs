using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olimpic_1
{
    class Program
    {
        private static void Main(string[] args)
        {
            int N = Convert.ToInt32(Console.ReadLine());
            string[] _stringArray = new string[2*N];
            for (int i = 0; i < _stringArray.Length; i++)
                _stringArray[i] = Console.ReadLine();
            string[][] _strArrayNumers = new string[2*N][];
            for (int i = 0; i < _strArrayNumers.Length; i++)
                _strArrayNumers[i] = _stringArray[i].Split(' ');
            double[,] _numers = new double[2*N, 2*N];
            for (int i = 0; i < _strArrayNumers.Length; i++)
                for (int j = 0; j < _strArrayNumers.Length; j++)
                    _numers[i, j] = Convert.ToDouble(_strArrayNumers[i][j]);
            double _buf1, _buf2;
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                {
                    _buf1 = _numers[i, j + N];
                    _numers[i, j + N] = _numers[i, j];
                    _buf2 = _numers[i + N, j + N];
                    _numers[i + N, j + N] = _buf1;
                    _numers[i, j] = _numers[i + N, j];
                    _numers[i + N, j] = _buf2;
                }
            for (int i = 0; i < 2*N; i++)
                for (int j = 0; j < 2*N; j++)
                {
                    Console.Write(_numers[i, j]);
                    if (j != 2*N - 1)
                        Console.Write(" ");
                }
                Console.WriteLine();
        }
    }
}
