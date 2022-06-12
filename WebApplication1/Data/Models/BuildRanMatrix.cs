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
                var check = ran.Next(0, 2);
                var el = 0;
                if (check==1)
                {
                    el=ran.Next(1, 10);
                }

                matrix[i, j] = el;
                matrix[j, i] = matrix[i, j]; // обратный порядок индексов
            }
        }
        return matrix;
    }

    
}