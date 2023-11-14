
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

public class FileData
{

    public static List<int> genData(int n)
    {
        Random r = new Random();
        List<int> data = Enumerable.Range(1,n).ToList();
        return data;
    } 

    public static List<int> genNumbers(int n) //using RandomNumberGenerator
    {
        List<int> res = new List<int>();
        RandomNumberGenerator.Create();
        for (int i=0; i<n; i++)
        {
            double x = RandomNumberGenerator.GetInt32(2,20);
            Console.WriteLine(x);
        }
        return res;
    }


    public static List<Double> genDoubleData(int n)
    {
        List<Double> data = new List<Double>();
        for(int i=0; i<n; i++)
        {
            Random r = new Random();
            double x = Math.Round(r.NextDouble()*100,3);

            if(!data.Contains(x))
                data.Add(x);

        }

        return data;
    }

    public static void writeFile<T>(List<T> data, string filename)
    {
        using (StreamWriter sw = new StreamWriter(filename))
        {
            for(int i=0; i<data.Count; i++)
            {
                sw.Write(data[i]);
                if(i<data.Count-1)
                    sw.Write(",");
            }
        }
    }

    public static List<Double> readData(string filename)
    {
        List<Double> res = new List<double>();

        string [] x = System.IO.File.ReadAllLines(filename);
        var raw = x[0].Split(',');
        // Console.WriteLine(raw.Length);
        foreach(var item in raw)
        {
            res.Add(Convert.ToDouble(item));
        }
        return res;
    }

     public static void printList<T>(List<T> list)
     {
            
        foreach (T i in list)
        {
            Console.WriteLine(i);
        }
    }

    public static void writeFitnessResults<T>(T [,] results, string filename)
    {
        using (StreamWriter sw = new StreamWriter(filename))
        {
            sw.Write("iter,current_fitness,new_fitness");
            sw.WriteLine();
            for (int i=0; i<results.GetLength(0); i++)
            {
                for (int j=0; j<results.GetLength(1); j++)
                {
                    // Console.WriteLine(results[i,j]);
                    sw.Write(results[i,j]);
                    if (j<results.GetLength(1)-1)
                        sw.Write(",");
                }
                sw.Write("\n");
            }
        }
    }

    public static void writeSolutions(List<List<int>> solutions, string filename)
    {
        using(StreamWriter sw = new StreamWriter(filename))
        {
            sw.Write("iter,solution");
            sw.WriteLine();
            for(int i=0; i<solutions.Count; i++)
            {
                sw.Write(i+",");
                for(int j=0; j<solutions[i].Count; j++)
                {
                    sw.Write(solutions[i][j]);
                    sw.Write(" ");
                }
                sw.WriteLine();
            }
        }
    }


}