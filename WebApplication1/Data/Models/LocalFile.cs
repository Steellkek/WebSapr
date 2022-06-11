using System.Net.Mime;
using System.Xml;

namespace SApr.models;

public class LocalFile
{
    public List<List<int>> ReadGraph()
    {
        List<List<int>> matrix = new();
        int L=1;
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load("Files/File.xml");
        var xRoot = xDoc.SelectSingleNode("root/graph");
        if (xRoot != null)
        {
            // обходим все дочерние узлы элемента user
            foreach (XmlNode childnode in xRoot.ChildNodes)
            {
                if (childnode.Name=="n")
                {
                    L = Int32.Parse(childnode.InnerText);

                }

                if (childnode.Name=="matrix")
                {
                    matrix = childnode.InnerText
                        .Split(Array.Empty<string>(), StringSplitOptions.RemoveEmptyEntries)
                        .Select((s, i) => new { N = int.Parse(s), I = i})
                        .GroupBy(at => at.I/L, at => at.N, (k, g) => g.ToList())
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
        xDoc.Load("Files/File.xml");
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
        var x = new BuildRanMatrix().BuildMatrix(n);
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
                // обходим все дочерние узлы элемента user
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

        xDoc.Save("File.xml");
    }

    public List<double> ReadGenAlg()
    {
        List<double> split = new();
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load("Files/File.xml");
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
}