using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompMath1
{
    public class Matrix
    {
        public double[,] InputMatrix;
        public int Size;

        public double[,] ValuesMatrix;
        public double[] FreeTerms;
        public double[] Discrepancies;
        public double[] Roots;
        public double Determinant;

        public Matrix(int Quantity)
        {
            InputMatrix = new double[Quantity, Quantity + 1];
            Size = Quantity;
        }
        public void Print()
        {
            Console.WriteLine("\nMatrix:");
            for (int i = 0; i < Size; ++i)
            {
                for (int j = 0; j < Size; ++j)
                {
                    Console.Write("{0:0.##}\t", ValuesMatrix[i, j]);
                }
                Console.Write(" || {0:0.##}\t", FreeTerms[i]);
                Console.WriteLine("\n");
            }
        }
        public void PrintResult()
        {
            Print();
            int length;
            Console.WriteLine("\nA = {0}\n", Determinant);
            for (length = 0; length < Size; length++)
                Console.WriteLine("X{0} = {1:0.#####}", length, Roots[length]);
            Console.WriteLine("\nDiscrepancies:");
            for (length = 0; length < Size; length++)
                Console.WriteLine("U{0} = {1}", length, Discrepancies[length]);
        }

        public void GenerateWorkingMatrix()
        {
            ValuesMatrix = new double[Size, Size];
            FreeTerms = new double[Size];
            Roots = new double[Size];
            Discrepancies = new double[Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size + 1; j++)
                {
                    if (j < Size)
                    {
                        ValuesMatrix[i, j] = InputMatrix[i, j];
                    }
                    else
                    {
                        FreeTerms[i] = InputMatrix[i, j];
                    }
                }
            }
        }

    }
}
