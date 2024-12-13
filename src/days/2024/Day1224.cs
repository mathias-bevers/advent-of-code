
using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day1224 : IDay
{
    public DateTime date { get; } = new DateTime(2024, 12, 12);

    private int width = 0;
    private int height = 0;
    private Plot[,] grid = new Plot[0, 0];

    public void PopulateData(string raw)
    {
        string[] rows = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        grid = new Plot[rows.Length, rows[0].Length];
        width = grid.GetLength(0);
        height = grid.GetLength(1);

        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                grid[x, y] = new Plot(rows[y][x], new Vector2Int(x, y), null);
            }
        }
    }

    public string SolveStarOne()
    {
        List<Region> regions = [];

        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                Plot plot = grid[x, y];

                if (plot.parent != null) { continue; }

                regions.Add(GenerateRegion(plot));
            }
        }

        int sum = 0;
        for (int i = 0; i < regions.Count; ++i)
        {
            int area = regions[i].plots.Count;
            int perimeter = regions[i].perimeter;
            int cost = area * perimeter;
            sum += cost;
        }

        return sum.ToString();
    }

    public string SolveStarTwo()
    {
        throw new NotImplementedException();
    }

    private Region GenerateRegion(Plot initialPlot)
    {
        Region region = new(initialPlot.identifier);
        Queue<Plot> q = new();
        q.Enqueue(initialPlot);

        while (q.Count > 0)
        {
            Plot plot = q.Dequeue();

            List<Plot> neighbors = FindNeighbors(plot.location);
            for (int i = 0; i < neighbors.Count; ++i)
            {
                if (region.plots.Contains(plot)) { continue; }
                q.Enqueue(neighbors[i]);
            }

            plot.perimeter = 4 - neighbors.Count;

            region.AddPlot(plot);
        }

        return region;
    }

    private List<Plot> FindNeighbors(Vector2Int location)
    {
        List<Plot> neighbors = new(4);
        Vector2Int dir = new(0, 1);

        for (int i = 0; i < 4; ++i)
        {
            dir.RotateDegrees(-90);
            Vector2Int neighborLocation = location + dir;

            if (neighborLocation.x < 0 || neighborLocation.y < 0) { continue; }
            if (neighborLocation.x >= width || neighborLocation.y >= height) { continue; }

            Plot neighbor = grid[neighborLocation.x, neighborLocation.y];

            if (neighbor.identifier != grid[location.x, location.y].identifier) { continue; }

            neighbors.Add(neighbor);
        }

        return neighbors;
    }

    private class Region(char identifier)
    {
        public char identifier { get; } = identifier;
        public int perimeter { get; private set; } = 0;
        public HashSet<Plot> plots { get; } = [];

        public void AddPlot(Plot plot)
        {
            if (!plots.Add(plot)) { return; }

            plot.parent = this;
            perimeter += plot.perimeter;
        }
        
        public override string ToString() =>
            string.Concat(identifier, ": ", string.Join(',', plots));
    }

    private class Plot(char identifier, Vector2Int location, Region? parent)
    {
        public char identifier { get; } = identifier;
        public Vector2Int location { get; } = location;
        public Region? parent { get; set; } = parent;
        public int perimeter { get; set; } = -1;

        public override string ToString() => location.ToString();
    }
}