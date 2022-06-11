namespace SApr.models;

public class Population
{
    public List<Genome> population = new();
    public List<Genome> Parents;
    public double SumFitness;
    public Graph _graph;

    public Population(Graph graph)
    {
        _graph = graph;
    }

    public void CreateStartedPopulation(int N)
    {
        for (int i = 0; i < N; i++)
        {
            Genome gen = new Genome(_graph);
            population.Add(gen);
        }
        CalculateAverFitness();
    }

    public void CreateNewParents()
    {
        Parents = new List<Genome>();
        Random ran = new();
        var chanes = new double[population.Count];
        chanes[0] = population[0].Fitness / SumFitness;
        for (int i = 1; i < population.Count; i++)
        {
            chanes[i] =chanes[i-1] +population[i].Fitness / SumFitness;
        }

        for (int i = 0; i < population.Count; i++)
        {
            var y = ran.NextDouble();
            if (y<chanes[0])
            {
                Parents.Add(population[0]);
                continue;
            }

            for (int j = 1; j < chanes.Length; j++)
            {
                if ((y<chanes[j])&(y>chanes[j-1]))
                {
                    Parents.Add(population[j]);
                    break;
                }
            }
            //Console.WriteLine(5);

        }
        //Console.WriteLine(5);
    }

    public void CalculateAverFitness()
    {
        SumFitness = population.Sum(x => x.Fitness);
    }
    
    public void Crossover(int index1,int index2)
    {
        var rand = new Random();
        Genome child1 = new Genome();
        Genome child2 = new Genome();
        var j = rand.Next(2,Parents[index1].Gen.Count-2);
        child1.Gen = Parents[index1].Gen.GetRange(0, j);
        child2.Gen = Parents[index2].Gen.GetRange(0, j);
        child1.GetChild(child1,Parents[index2],_graph);
        child2.GetChild(child2,Parents[index1],_graph);
        //Console.WriteLine(5);
        population[index1].Gen = child1.Gen.GetRange(0,child1.Gen.Count);
        population[index2].Gen = child2.Gen.GetRange(0,child2.Gen.Count);
        //Console.WriteLine(5);
        population[index1].DetermineFitnes(_graph);
        population[index2].DetermineFitnes(_graph);
        //Console.WriteLine(5);
    }

    public void FaildCrossover(int index1,int index2)
    {
        population[index1].Gen = Parents[index1].Gen.GetRange(0,Parents[index1].Gen.Count);
        population[index2].Gen = Parents[index2].Gen.GetRange(0,Parents[index2].Gen.Count);
        population[index1].DetermineFitnes(_graph);
        population[index2].DetermineFitnes(_graph);
    }
    public void Mutation(int index)
    {
        var Ran = new Random();
        var p1 = Ran.Next(0,population[index].Gen.Count);
        var p2 = Ran.Next(0,population[index].Gen.Count);
        while (p1 == p2)
        {
            p2=Ran.Next(population[index].Gen.Count);
        }

        (population[index].Gen[p1], population[index].Gen[p2]) = (population[index].Gen[p2], population[index].Gen[p1]);
        population[index].DetermineFitnes(_graph);
    }

    public void Inversion(int index)
    {
        //Console.WriteLine(5);
        Random ran = new();
        int side = ran.Next(0, 2);
        var point = ran.Next(population[index].Gen.Count);
        if (side==0)
        {
            population[index].Gen.Reverse(0,point+1);
        }
        if (side==1)
        {
            population[index].Gen.Reverse(point, population[index].Gen.Count-point);
        }
        population[index].DetermineFitnes(_graph);
        //Console.WriteLine(5);
    }
}