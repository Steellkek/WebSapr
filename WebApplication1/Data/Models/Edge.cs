namespace SApr.models;

public class Edge
{
    public Vertex V1 { get; set; }
    public Vertex V2 { get; set; }
    public int Weight { get; set; }

    public Edge(int weight, Vertex v1, Vertex v2)
    {
        V1 = v1;
        V2 = v2;
        Weight = weight;
    }
}