namespace SApr.models;

public class Genome
{
    public List<int> Split;
    public List<Vertex> Gen;
    public int Fitness;
    public long time;
    private Graph _graph;

    public Genome()
    {
        Split=new LocalFile().ReadSplit();
    }
    public Genome(Graph graph)
    {
        _graph = graph;
        Split=new LocalFile().ReadSplit();
        Gen = Shuffle(_graph);
        DetermineFitnes(graph);
    }


    private List<Vertex> Shuffle(Graph graph)
    {
        Random rand = new Random();
        List<Vertex> list = graph.Vertexs.GetRange(0, graph.Vertexs.Count);
        for (int i = list.Count - 1; i >= 1; i--)
        {
            int j = rand.Next(i + 1);
            (list[j], list[i]) = (list[i], list[j]);
        }
        return list;
    }

    public void DetermineFitnes(Graph g)
    {
        Fitness = 0;
        int length = 0;
        foreach (var x in Split)
        {
            var y = Gen.GetRange(length,  x);
            foreach (var j in y)
            {
                var v = j.GetAdjVerts().GetRange(0,j.GetAdjVerts().Count).Intersect(y).ToList();
                foreach (var vertex in v)
                {
                    Fitness += g.Edges
                        .Where(x => ((x.V1 == vertex) ^ (x.V2 == vertex))&((x.V1 == j) ^ (x.V2 == j)))
                        .Sum(x => x.Weight);
                }
            }
            length += x;
        }
        Fitness = Fitness / 2;
    }

    public void GetChild(Genome child, Genome parent, Graph graph)
    {
        foreach (var vertex in parent.Gen)
        {
            if (!child.Gen.Contains(vertex))
            {
                child.Gen.Add(vertex);
            }
        }
        DetermineFitnes(graph);
    }
    
}