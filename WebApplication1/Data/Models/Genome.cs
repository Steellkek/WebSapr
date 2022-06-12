namespace SApr.models;

public class Genome
{
    public List<int> Split { get; }
    public List<Vertex> Gen;
    public int Fitness;
    public long time;
    public static Graph _graph;

    public Genome()
    {
        Split=new LocalFile().ReadSplit();
    }
    public Genome(Graph g)
    {
        _graph = g;
        Split=new LocalFile().ReadSplit();
        Gen = Shuffle(g);
        DetermineFitnes(g);
    }


    private static List<Vertex> Shuffle(Graph g)
    {
        Random rand = new Random();

        List<Vertex> list = g.Vertexs.GetRange(0, g.Vertexs.Count);

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

        int l = 0;
        foreach (var x in Split)
        {
            var t = new List<Vertex>();
            var y = Gen.GetRange(l,  x);
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

            l += x;
            //Console.WriteLine(5);
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