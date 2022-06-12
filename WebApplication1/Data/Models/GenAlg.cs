using System.Diagnostics;

namespace SApr.models;

public class GenAlg
{
    public int CountGenome { get; set; }
    public int Iteration{ get; set; }
    public double ChanseCrosover{ get; set; }
    public double ChanseMutation{ get; set; }
    public double ChanseInversion{ get; set; }
    public Graph _graph{ get; set; }
    public long time;
    public Genome BestGen = new();
    private Population _population;

    public GenAlg(Graph graph)
    {
        List<double> parameters = new LocalFile().ReadGenAlg();
        CountGenome = (int) parameters[0];
        Iteration = (int) parameters[1];
        ChanseCrosover = parameters[2];
        ChanseMutation = parameters[3];
        ChanseInversion = parameters[4];
        _graph =graph;
    }

    public void Go()
    {
        Stopwatch stopwatch = new();
        stopwatch.Start();
        _population = new Population(_graph);
        _population.CreateStartedPopulation(CountGenome);
        //Console.WriteLine(5);
        for (int i = 0; i < Iteration; i++)
        {
            Crossingover();
            Mutations();

            Inversions();

            _population.CalculateAverFitness();
            GetBestGen(stopwatch);
            //Console.WriteLine(5);

        }

        if (BestGen==null)
        {
            BestGen = _population.population[0];
        }
        stopwatch.Stop();
    
        time=stopwatch.ElapsedMilliseconds;
    }

    public void GetBestGen(Stopwatch stopwatch)
    {
        var b = _population.population.OrderBy(x => x.Fitness).Last();
        if (b.Fitness>BestGen.Fitness)
        {
            //Console.WriteLine(5);
            BestGen.Fitness = b.Fitness;
            BestGen.Gen = b.Gen.GetRange(0,b.Gen.Count);
            BestGen.time = stopwatch.ElapsedMilliseconds;
        }
    }
    public void Inversions()
    {
        Random ran = new();
        var y = ran.NextDouble();
        for (int i = 0; i < _population.population.Count; i++)
        {
            if (y<ChanseMutation)
            {
                _population.Inversion(i);
            }
        }
        
    }

    public void Mutations()
    {
        Random ran = new();
        var y = ran.NextDouble();
        for (int i = 0; i < _population.population.Count; i++)
        {
            if (y < ChanseInversion)
            {
                _population.Mutation(i);
            }
        }
    }

    public void Crossingover()
    {
        Random ran = new();
        var y = ran.NextDouble();
        _population.CreateNewParents();

        for (int j = 0; j < _population.population.Count-1; j+=2)
        {
            if (y<ChanseCrosover)
            {
                _population.Crossover(j,j+1);
            }
            else
            {
                _population.FaildCrossover(j,j+1);
            }

            y = ran.NextDouble();
        }

        if ((_population.population.Count % 2 == 1) & (y<ChanseCrosover))
        {
             _population.population[_population.population.Count-1] = _population.Parents[_population.population.Count-1];
        }
    }
}