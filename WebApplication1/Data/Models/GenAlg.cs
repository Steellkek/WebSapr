using System.Diagnostics;

namespace SApr.models;

public class GenAlg
{
    public int CountGenome;
    public int Iteration;
    public double ChanseCrosover;
    public double ChanseMutation;
    public double ChanseInversion;
    public Graph _graph;
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
        for (int i = 0; i < Iteration; i++)
        {
            Crossingover();
            Mutations();
            Inversions();
            _population.CalculateAverFitness();
            GetBestGen(stopwatch);
        }
        stopwatch.Stop();
    time=stopwatch.ElapsedMilliseconds;
    }

    public void GetBestGen(Stopwatch stopwatch)
    {
        var VarBestGen = _population.population.OrderBy(x => x.Fitness).Last();
        if (VarBestGen.Fitness>BestGen.Fitness)
        {
            BestGen.Fitness = VarBestGen.Fitness;
            BestGen.Gen = VarBestGen.Gen.GetRange(0,VarBestGen.Gen.Count);
            BestGen.time = stopwatch.ElapsedMilliseconds;
        }
    }
    public void Inversions()
    {
        Random ran = new();
        var chanse = ran.NextDouble();
        for (int i = 0; i < _population.population.Count; i++)
        {
            if (chanse<ChanseMutation)
            {
                _population.Inversion(i);
            }
        }
        
    }

    public void Mutations()
    {
        Random ran = new();
        var chanse = ran.NextDouble();
        for (int i = 0; i < _population.population.Count; i++)
        {
            if (chanse < ChanseInversion)
            {
                _population.Mutation(i);
            }
        }
    }

    public void Crossingover()
    {
        Random ran = new();
        var chanse = ran.NextDouble();
        _population.CreateNewParents();

        for (int j = 0; j < _population.population.Count-1; j+=2)
        {
            if (chanse<ChanseCrosover)
            {
                _population.Crossover(j,j+1);
            }
            else
            {
                _population.FaildCrossover(j,j+1);
            }

            chanse = ran.NextDouble();
        }

        if (_population.population.Count % 2 == 1)
        {
             _population.population[_population.population.Count-1] = _population.Parents[_population.population.Count-1];
        }
    }
}