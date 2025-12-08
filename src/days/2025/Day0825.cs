using System.Net;
using advent_of_code.utils;
using Connection = (int from, int to, double distance); // index, index, distance

namespace advent_of_code.days;

internal class Day0825 : IDay
{
    public DateTime date { get; } = new(2025, 12, 08);

    private Vector3Int[] junctionBoxes = [];
    private Connection[] connections = [];
    private int pairsNeeded = -1;

    public void PopulateData(string raw)
    {
        string[] lines = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        junctionBoxes = new Vector3Int[lines.Length];

        // parse input to an vector 3 int array.
        for (int i = 0; i < junctionBoxes.Length; ++i)
        {
            string[] values = lines[i].Split(',', StringSplitOptions.RemoveEmptyEntries);
            int x = int.Parse(values[0]);
            int y = int.Parse(values[1]);
            int z = int.Parse(values[2]);

            junctionBoxes[i] = new Vector3Int(x, y, z);
        }

        // pre compute all the distances between the junctions.
        List<Connection> tmp = [];
        for (int i = 0; i < junctionBoxes.Length; ++i)
        {
            Vector3Int from = junctionBoxes[i];
            for (int ii = i + 1; ii < junctionBoxes.Length; ++ii)
            {
                Vector3Int to = junctionBoxes[ii];
                double distance = Vector3Int.Distance(from, to);

                tmp.Add((i, ii, distance));
            }
        }

        // order precomputed connection by distance.
        connections = [.. tmp.OrderBy(c => c.distance)];

        // set the needed pairs dynamiclly to based on the input's line count.
        pairsNeeded = lines.Length == 20 ? 10 : 1000;
    }

    public string SolveStarOne()
    {
        // create two arrays where circuits acts as a map for the sizes.
        int[] circuits = new int[junctionBoxes.Length];
        int[] circuitSizes = new int[junctionBoxes.Length];

        // init the arrays with their defualt values.
        for (int i = 0; i < junctionBoxes.Length; ++i)
        {
            circuits[i] = i;
            circuitSizes[i] = 1;
        }

        // loop for the needed pairs.
        for (int i = 0; i < pairsNeeded; ++i)
        {
            // get the map indexes for the current connection.
            Connection connection = connections[i];
            int a = circuits[connection.from];
            int b = circuits[connection.to];

            if (a == b)
            {
                continue;
            }

            // loop though all the junctions to find the to connection.
            for (int ii = 0; ii < junctionBoxes.Length; ii++)
            {
                if (circuits[ii] != b)
                {
                    continue;
                }

                // point the to connection to the to connection.
                circuits[ii] = a;
            }

            // add the from circuit size to the to circuit size and set to zero.  
            circuitSizes[a] += circuitSizes[b];
            circuitSizes[b] = 0;
        }

        // sort the array and reverse to get the sizes decending.
        Array.Sort(circuitSizes);
        Array.Reverse(circuitSizes);

        long result = circuitSizes[0] * circuitSizes[1] * circuitSizes[2];
        return result.ToString();
    }

    public string SolveStarTwo()
    {
        throw new NotImplementedException();
    }
}