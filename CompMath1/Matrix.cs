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
        public int Rows;
        public int Columns;



        public Matrix(int Quantity)
        {
            InputMatrix = new double[Quantity, Quantity + 1];
            Rows = Quantity;
            Columns = Quantity + 1;
        }
        public void Print()
        {
            int ElementLength = 0;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (InputMatrix[i, j].ToString().Length > ElementLength)
                    {
                        ElementLength = InputMatrix[i, j].ToString().Length;
                    }
                }
            }
            string format = "{0," + ElementLength + "}";
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Console.Write(String.Format(format, InputMatrix[i, j]) + " ");
                }
                Console.WriteLine();
            }


        }

    }
}
