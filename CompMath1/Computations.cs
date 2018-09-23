using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompMath1
{
    public  class Computations
    {
        public bool success;
        public int swapsCount;
        public bool Straight;

        public void Swap(Matrix workMatrix, int swapLineIndex)
        {
            double[] buffer = new double[workMatrix.Size];
            double RightPart;
            int columnIndex;
            for (columnIndex = 0; columnIndex < workMatrix.Size; columnIndex++)
            {
                buffer[columnIndex] = workMatrix.ValuesMatrix[swapLineIndex, columnIndex];
            }

            int necessaryLineIndex;
            for (necessaryLineIndex = swapLineIndex + 1; necessaryLineIndex < workMatrix.Size; necessaryLineIndex++)
            {
                if (workMatrix.ValuesMatrix[necessaryLineIndex, swapLineIndex] != 0)
                    break;
            }
            for (columnIndex = 0; columnIndex < workMatrix.Size; columnIndex++)
            {
                workMatrix.ValuesMatrix[swapLineIndex, columnIndex] = workMatrix.ValuesMatrix[necessaryLineIndex, columnIndex];
                workMatrix.ValuesMatrix[necessaryLineIndex, columnIndex] = buffer[columnIndex];
            }
            RightPart = workMatrix.FreeTerms[swapLineIndex];
            workMatrix.FreeTerms[swapLineIndex] = workMatrix.FreeTerms[necessaryLineIndex];
            workMatrix.FreeTerms[necessaryLineIndex] = RightPart;
            swapsCount = necessaryLineIndex - swapLineIndex;
            success = true;
        }

        public void Solve(Matrix workMatrix)
        {
            Straight = true;
            double leadingСoefficient;
            while (Straight)
            {
                int swapLineIndex = 0;
                try
                {
                    // Прямой ход

                    for (int diagonalIndex = 0; diagonalIndex < workMatrix.Size; diagonalIndex++)
                    {
                        //проверка на 0 
                        if (workMatrix.ValuesMatrix[diagonalIndex, diagonalIndex] == 0)
                        {
                            int zeroCount = 0;
                            for (int i = diagonalIndex; i < workMatrix.Size; i++)
                            {

                                if (workMatrix.ValuesMatrix[i, diagonalIndex] == 0)
                                    zeroCount++;
                            }
                            if (zeroCount == workMatrix.Size - diagonalIndex)
                                continue;
                            swapLineIndex = diagonalIndex;
                            throw new DivideByZeroException();
                        }

                        //эта часть делает единицы в диагонали
                        leadingСoefficient = 1 / workMatrix.ValuesMatrix[diagonalIndex, diagonalIndex];
                        workMatrix.ValuesMatrix[diagonalIndex, diagonalIndex] = 1;

                        int lineIndex;
                        for (lineIndex = diagonalIndex + 1; lineIndex < workMatrix.Size; lineIndex++)
                        {
                            workMatrix.ValuesMatrix[diagonalIndex, lineIndex] *= leadingСoefficient;
                        }
                        workMatrix.FreeTerms[diagonalIndex] *= leadingСoefficient;

                        //не забываем про то, что определитель изменился
                        workMatrix.Determinant /= leadingСoefficient;

                        //"обнуляем" последующие значения столбца
                        for (int columnIndex = diagonalIndex + 1; columnIndex < workMatrix.Size; columnIndex++)
                        {
                            leadingСoefficient = workMatrix.ValuesMatrix[columnIndex, diagonalIndex];
                            workMatrix.ValuesMatrix[columnIndex, diagonalIndex] = 0;
                            for (lineIndex = diagonalIndex + 1; lineIndex < workMatrix.Size; lineIndex++)
                                workMatrix.ValuesMatrix[columnIndex, lineIndex] -= workMatrix.ValuesMatrix[diagonalIndex, lineIndex] * leadingСoefficient;
                            workMatrix.FreeTerms[columnIndex] -= workMatrix.FreeTerms[diagonalIndex] * leadingСoefficient;
                        }
                    }
                    success = true;
                    Straight = false;

                }
                catch (DivideByZeroException)
                {
                    //пытаемся найти строчку с ненулевым значением и поменять их местами
                    Swap(workMatrix, swapLineIndex);

                    if (success)
                    {
                        workMatrix.Determinant *= Math.Pow(-1, swapsCount);
                    }
                    else
                    {
                        success = false;
                        Straight = false;
                    }
                }
            }

            if (!success)
                return;

            //Обратный ход

            for (int lineIndex = workMatrix.Size - 1; lineIndex >= 0; lineIndex--)
            {
                if (workMatrix.ValuesMatrix[lineIndex, lineIndex] == 0 && workMatrix.FreeTerms[lineIndex] != 0)
                {
                    double rootSum = 0;
                    for (int i = workMatrix.Size - 1; i > lineIndex; i--)
                    {
                        rootSum += workMatrix.Roots[i] * workMatrix.ValuesMatrix[lineIndex, i];
                    }

                    if (rootSum != workMatrix.FreeTerms[lineIndex])
                    {
                        success = false;
                        return;
                    }
                }
                leadingСoefficient = workMatrix.FreeTerms[lineIndex];
                for (int columnIndex = lineIndex + 1; columnIndex < workMatrix.Size; columnIndex++)
                    leadingСoefficient -= workMatrix.ValuesMatrix[lineIndex, columnIndex] * workMatrix.Roots[columnIndex];
                workMatrix.Roots[lineIndex] = leadingСoefficient;
            }

            //Невязки
            double sum;
            for (int lineIndex = 0; lineIndex < workMatrix.Size; lineIndex++)
            {
                sum = 0;
                for (int columnIndex = 0; columnIndex < workMatrix.Size; columnIndex++)
                    sum += workMatrix.ValuesMatrix[lineIndex, columnIndex] * workMatrix.Roots[columnIndex];
                workMatrix.Discrepancies[lineIndex] = workMatrix.FreeTerms[lineIndex] - sum;
            }

            success = true;
        }
    }
}
