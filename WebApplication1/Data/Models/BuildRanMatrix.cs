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
                var y = ran.Next(0, 2);
                var x = 0;
                if (y==1)
                {
                    x=ran.Next(1, 10);
                }

                matrix[i, j] = x;
                matrix[j, i] = matrix[i, j]; // обратный порядок индексов
            }
        }
        return matrix;
    }

    
}