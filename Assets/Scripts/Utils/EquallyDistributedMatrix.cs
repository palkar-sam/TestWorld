namespace Utils
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class EquallyDistributedMatrix
    {
        public static int[,] GetMatrix(int size)
        {
            int matrixSize = size;

            int totalCells = matrixSize * matrixSize;
            int numValues = matrixSize; // Values will be {0, 1, ..., N-1}

            // Make sure total cells can be evenly divided by value count
            if (totalCells % numValues != 0)
            {
                Debug.LogError($"Matrix size {matrixSize}x{matrixSize} can't evenly distribute {numValues} values. Default created 2x2");
                return new int[,] { { 0,1}, { 1, 1} };
            }

            int countPerValue = totalCells / numValues;
            List<int> values = new List<int>();

            // Generate value set: {0, 1, ..., N-1}
            for (int v = 0; v < numValues; v++)
            {
                values.AddRange(Enumerable.Repeat(v, countPerValue));
            }

            // Shuffle values randomly
            values = values.OrderBy(_ => Random.value).ToList();

            // Fill the NxN matrix
            int[,] matrix = new int[matrixSize, matrixSize];
            int index = 0;

            for (int row = 0; row < matrixSize; row++)
            {
                for (int col = 0; col < matrixSize; col++)
                {
                    matrix[row, col] = values[index++];
                }
            }

            return matrix;

            // Print the matrix
            //Debug.Log($"Random {matrixSize}x{matrixSize} Matrix:");
            //for (int row = 0; row < matrixSize; row++)
            //{
            //    string line = "";
            //    for (int col = 0; col < matrixSize; col++)
            //    {
            //        line += matrix[row, col] + " ";
            //    }
            //    Debug.Log(line);
            //}
        }
    }
}
