namespace SApr.models;

public class Vertex
{
    public int Number { get; set; }

    private List<Vertex> AdjVert = new List<Vertex>();


    public Vertex(int number)
    {
        Number = number;
    }
    public void AddAdjVert(Vertex v)
    {
        AdjVert.Add(v);
    }

    public List<Vertex> GetAdjVerts()
    {
        return AdjVert;
    }
}