using Microsoft.AspNetCore.Mvc;
using SApr.models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GraphController : Controller
{
    [HttpGet("graph")]
    public Task<ActionResult<Graph>> Graph()
    {
        LocalFile lf = new();
        Graph graph = new Graph();
        graph.Matrix = lf.ReadGraph();
        graph.CreateGraph();
        return Task.FromResult<ActionResult<Graph>>(graph);
    }
    
    [HttpGet("gengraph")]
    public Task<ActionResult<GenAlg>> GenGraph()
    {
        LocalFile lf = new();
        Graph graph = new Graph
        {
            Matrix = lf.ReadGraph()
        };
        graph.CreateGraph();
        GenAlg genAlg = new GenAlg(graph);
        genAlg.Go();
        if (genAlg.BestGen.Gen!=null)
        {
            lf.WriteResult(genAlg);
        }
        return Task.FromResult<ActionResult<GenAlg>>(genAlg);
    }
    
    [HttpGet("{number}")]
    public Task<ActionResult<Graph>> NewGraph(int number)
    {
        LocalFile lf = new();
        lf.WriteMatix(number);
        Graph graph = new Graph();
        graph.Matrix = lf.ReadGraph();
        graph.CreateGraph();
        return Task.FromResult<ActionResult<Graph>>(graph);
    }
}