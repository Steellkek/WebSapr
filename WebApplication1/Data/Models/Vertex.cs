namespace SApr.models;

public class Vertex
{
    public int Number { get; set; }

    public List<Vertex> AdjVert = new List<Vertex>();

    public int Degre = 0;
    public Vertex(int number)
    {
        Number = number;
    }
}