using System.Xml;

namespace SApr.models;

public class LocalFile
{
    private string FileWay = "Files/File.xml";
    private string ResultWay = "Files/Result.xml";
    public List<List<int>> ReadGraph()
    {
        List<List<int>> matrix = new();
        int length=1;
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load(FileWay);
        var xRoot = xDoc.SelectSingleNode("root/graph");
        if (xRoot != null)
        {
            // обходим все дочерние узлы элемента user
            foreach (XmlNode childnode in xRoot.ChildNodes)
            {
                if (childnode.Name=="n")
                {
                    length = Int32.Parse(childnode.InnerText);
                }

                if (childnode.Name=="matrix")
                {
                    matrix = childnode.InnerText
                        .Split(Array.Empty<string>(), StringSplitOptions.RemoveEmptyEntries)
                        .Select((s, i) => new { N = int.Parse(s), I = i})
                        .GroupBy(at => at.I/length, at => at.N, (k, g) => g.ToList())
                        .ToList();;   
                }
            }
        }
        return matrix;
    }
    public List<int> ReadSplit()
    {
        List<int> split;
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load(FileWay);
        var xRoot = xDoc.SelectSingleNode("root/split");
        split = xRoot
            .InnerText
            .Split(Array.Empty<string>(), StringSplitOptions.RemoveEmptyEntries)
            .Select(x => int.Parse(x))
            .ToList();
        return split;
    }
    public void WriteMatix(int n)
    {
        Random ran = new();
        var x = BuildMatrix(n);
        string matrix = "";
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                matrix += x[i, j] + " ";
            }
            matrix += "\n";
        }  
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load("Files/File.xml");
        XmlElement? xRoot = xDoc.DocumentElement;
        if (xRoot != null)
        {
            foreach (XmlElement xnode in xRoot)
            {
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name=="n")
                    {
                        childnode.InnerText = n.ToString();
                    }
                    if (childnode.Name=="matrix")
                    {
                        childnode.InnerText = matrix;
                    }
                }
            }
        }

        var split = "";
        while (n!=0)
        {
            var random = ran.Next(1, n);
            split += random + " ";
            n -= random;
        }
        var xNode = xDoc.SelectSingleNode("root/split");
        xNode.InnerText = split;
        xDoc.Save("Files/File.xml");
    }
    private int[,] BuildMatrix(int N)
    {
        Random ran = new Random();
        int[,] matrix = new int[N, N];
        for (int i = 0; i < N; i++)
        {
            matrix[i, i] = 0;
            for (int j = i + 1; j < N; j++)
            {
                var check = ran.Next(0, 3);
                var el = 0;
                if (check==2)
                {
                    el=ran.Next(1, 20);
                }


                matrix[i, j] = el;
                matrix[j, i] = matrix[i, j]; 
            }
        }
        return matrix;
    }
    public List<double> ReadGenAlg()
    {
        List<double> split = new();
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load(FileWay);
        var xRoot = xDoc.SelectSingleNode("root/GenAlg");
        if (xRoot != null)
        {
            foreach (XmlNode childnode in xRoot.ChildNodes)
            {
                split.Add(double.Parse(childnode.InnerText));
            }
        }
        return split;
    }

    public void WriteResult(GenAlg genAlg)
    {
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load(ResultWay);
        var xRoot = xDoc.SelectSingleNode("root/Result");
        if (xRoot != null)
        {
            foreach (XmlNode childnode in xRoot.ChildNodes)
            {
                if (childnode.Name=="Split")
                {
                    var Split = "|";
                    var m = 1;
                    var n = 0;
                    foreach (var vertex in genAlg.BestGen.Gen)
                    {
                        Split += "v" + vertex.Number + " ";
                        n += 1;
                        if (genAlg.BestGen.Split[m - 1] == n) {
                            n = 0;
                            m += 1;
                            Split += "|";
                        }
                    }

                    childnode.InnerText = Split;
                }

                if (childnode.Name=="Fitness")
                {
                    childnode.InnerText = genAlg.BestGen.Fitness.ToString();
                }

                if (childnode.Name=="TimeBestGen")
                {
                    childnode.InnerText = genAlg.BestGen.time.ToString();
                }
                if (childnode.Name=="Time")
                {
                    childnode.InnerText = genAlg.time.ToString();
                }

                if (childnode.Name=="Matrix")
                {
                    childnode.InnerText = genAlg._graph.WriteMatrix();
                }
            }
        }
        xDoc.Save("Files/Result.xml");
    }
}