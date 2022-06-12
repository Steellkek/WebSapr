namespace SApr.models;

public class Graph
{
    public List<List<int>> Matrix { get; set; }
    public List<Edge> Edges = new();
    public List<Vertex> Vertexs=new();
    public int SumDegVert = 0;

    public void AddVertex(int number)
    {
        Vertexs.Add(new Vertex(number));
    }


    public void AddEdge(Vertex v1, Vertex v2, int weight)
    {
        Edges.Add(new Edge(weight,v1,v2));
    }
    

    public void WriteMatrix()
    {
        for (int i = 0; i < Matrix.Count; i++)
        {
            for (int j = 0; j < Matrix[i].Count; j++)
            {
                Console.Write("{0}\t",Matrix[i][j]);
            }
            Console.WriteLine();
        }  
    }
    
    public void WriteVertexs()
    {
        Console.WriteLine("Вершины в графе: ");
        foreach (var vertex in Vertexs)
        {
            Console.WriteLine(vertex.Number+" "+vertex.Degre);
        }
    }
    
    public void WriteEdges()
    {
        Console.Write("Вершины в графе: ");
        foreach (var edge in Edges)
        {
            
            Console.WriteLine(edge.V1.Number+" "+edge.V2.Number+" "+edge.Weight);
        }
    }

    public void AddVertDegree(Vertex v)
    {
        v.Degre = Edges.Where(edge => (edge.V1 == v) ^ (edge.V2 == v)).Sum(x => x.Weight);
    }

    public void AddAdjVerts(Edge e)
    {
        e.V1.AddAdjVert(e.V2);
        e.V2.AddAdjVert(e.V1);
    }

    public void CreateGraph()
    {
        for (int i = 0; i < Matrix.Count; i++)
        {
            AddVertex(i+1);
        }

        for (int i = 0; i < Matrix.Count; i++)
        {
            for (int j = i+1; j < Matrix.Count; j++)
            {
                if (Matrix[i][j]!=0)
                {
                    AddEdge(Vertexs[i],Vertexs[j],Matrix[i][j]);
                }
            }
        }

        foreach (var vertex in Vertexs)
        {
            AddVertDegree(vertex);
        }
        
        foreach (var edge in Edges)
        {
            AddAdjVerts(edge);
            SumDegVert += edge.Weight;
        }
        
    }
}