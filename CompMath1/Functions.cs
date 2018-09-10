using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompMath1
{
    static class Functions
    {
        public static bool FileInput()
        {
            Console.Clear();
            var FileToOpen = new OpenFileDialog
            {
                Filter = "Текстовый файл(*.txt)|*.txt"
            };
            FileToOpen.ShowDialog();
            if (FileToOpen.FileName == "")
            {
                return true;
            }
            string Path = FileToOpen.FileName;
            using (StreamReader SR = File.OpenText(Path))
            {
                if (!Int32.TryParse(SR.ReadLine(), out int Quantity) || Quantity < 3
                    || Quantity > 20)
                {
                    Console.WriteLine("The file is wrong: quantity is set wrong");
                    return true;
                }
                string[] Elements = SR.ReadLine().Split(' ');
                int QuantityInFile = 0;
                foreach (var str in Elements)
                {
                    QuantityInFile++;
                    if (!Double.TryParse(str, out double useless))
                    {
                        Console.WriteLine("The file is wrong: one of the elements can't be parsed to Double");
                        return true;
                    }
                }
                if (QuantityInFile != Quantity * (Quantity + 1))
                {
                    Console.WriteLine("The file is wrong: quantity of elements doesn't match the one set in file");
                    return true;
                }
                Matrix Matrix = new Matrix(Quantity);
                
                int ElementNumber = 0;
                for (int i = 0; i < Quantity; i++)
                {
                    for (int j = 0; j < Quantity + 1; j++)
                    {
                        Matrix.InputMatrix[i, j] = Convert.ToDouble(Elements[ElementNumber]);
                        ElementNumber++;
                    }
                }


                double[,] mat = new double[Quantity, Quantity];
                for (int i = 0; i < Quantity; i++)
                {
                    for (int j = 0; j < Quantity; j++)
                    {
                        mat[i, j] = Matrix.InputMatrix[i, j];
                    }
                }
                
            }

            return true;
        }
        public static bool KeyboardInput()
        {
            Console.Clear();
            Console.Write("Enter the quantity of equatations:");
            int Quantity;
            while (!Int32.TryParse(Console.ReadLine(), out Quantity) || Quantity < 3
                || Quantity > 20)
            {
                Console.Write("Wrong input, try again : ");
            }
            Matrix Matrix = new Matrix(Quantity);
            for (int i = 0; i < Quantity; i++)
            {
                for (int j = 0; j < Quantity + 1; j++)
                {
                    Console.WriteLine($"Input the element №{j} on the string №{i}");
                    double Element;
                    while (!Double.TryParse(Console.ReadLine(), out Element))
                    {
                        Console.WriteLine("Wrong input, try again : ");
                    }
                    Matrix.InputMatrix[i, j] = Element;
                }
            }




            Matrix.Print();
            
            return true;
        }
        public static bool RandomInput()
        {
            Console.Clear();
            Random rnd = new Random();
            Console.WriteLine("Enter the quantity of equatations:");
            int Quantity;
            
            while (!Int32.TryParse(Console.ReadLine(), out Quantity) || Quantity < 3
                || Quantity > 20)
            {
                Console.Write("Wrong input, try again : ");
            }
            Matrix Matrix = new Matrix(Quantity);
            for (int i = 0; i < Quantity; i++)
            {
                for (int j = 0; j < Quantity + 1; j++)
                {
                    Matrix.InputMatrix[i, j] = rnd.Next(-20, 20);
                }
            }
            Matrix.Print();
            return true;
        }
        public static bool Quit()
        {
            Console.Clear();
            Console.WriteLine("GOODBYE");
            return false;
        }
    }
}
