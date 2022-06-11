using System.Xml;

namespace SApr.models;

public class BuildRanMatrix
{
    Random ran = new Random();

    public int[,] BuildMatrix(int N)
    {
        int[,] matrix = new int[N, N];
        for (int i = 0; i < N; i++)
        {
            matrix[i, i] = 0;
            for (int j = i + 1; j < N; j++)
            {
                matrix[i, j] = ran.Next(0, 10);
                matrix[j, i] = matrix[i, j]; // обратный порядок индексов
            }
        }
        return matrix;
    }

    
}