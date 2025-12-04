using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day1224 : IDay
{
    public DateTime date { get; } = new DateTime(2024, 12, 12);

    private Grid<Plot> plots = new(0, 0);
    private Region[] regions = [];

    private readonly Vector2Int[] directions = [
        new ( 0, 1),
        new ( 1, 1),
        new ( 1, 0),
        new ( 1,-1),
        new ( 0,-1),
        new (-1,-1),
        new (-1, 0),
        new (-1, 1)
    ];


    public void PopulateData(string raw)
    {
        string[] rows = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        plots = new Grid<Plot>(rows.Length, rows[0].Length);

        for (int y = 0; y < plots.height; ++y)
        {
            for (int x = 0; x < plots.width; ++x)
            {
                plots[x, y] = new Plot(rows[y][x], new Vector2Int(x, y), null);
            }
        }

        List<Region> tmp = [];
        plots.Loop((plot, position) =>
        {

            if (plot.parent != null) { return; }

            tmp.Add(GenerateRegion(plot));

        });

        regions = [.. tmp];
    }

    public string SolveStarOne()
    {
        int sum = 0;
        for (int i = 0; i < regions.Length; ++i)
        {
            int area = regions[i].plots.Count;
            int perimeter = regions[i].perimeter;
            int price = area * perimeter;
            sum += price;
        }

        return sum.ToString();
    }

    public string SolveStarTwo()
    {
        int sum = 0;

        for (int i = 0; i < regions.Length; ++i)
        {
            Region region = regions[i];
            int area = region.plots.Count;
            int sides = 0;

            foreach (Plot plot in region.plots)
            {
                bool north = region.ContainsPoint(plot.location + directions[0]);
                bool northEast = region.ContainsPoint(plot.location + directions[1]);
                bool east = region.ContainsPoint(plot.location + directions[2]);
                bool southEast = region.ContainsPoint(plot.location + directions[3]);
                bool south = region.ContainsPoint(plot.location + directions[4]);
                bool southWest = region.ContainsPoint(plot.location + directions[5]);
                bool west = region.ContainsPoint(plot.location + directions[6]);
                bool northWest = region.ContainsPoint(plot.location + directions[7]);

                if (!north && !east) { ++sides; }
                if (!north && !west) { ++sides; }
                if (!south && !east) { ++sides; }
                if (!south && !west) { ++sides; }

                if (north && east && !northEast) { ++sides; }
                if (north && west && !northWest) { ++sides; }
                if (south && east && !southEast) { ++sides; }
                if (south && west && !southWest) { ++sides; }
            }

            int price = area * sides;
            sum += price;
        }

        return sum.ToString();
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

            if (!plots.InGrid(neighborLocation.x, neighborLocation.y)) { continue; }

            Plot neighbor = plots[neighborLocation.x, neighborLocation.y];

            if (neighbor.identifier != plots[location.x, location.y].identifier) { continue; }

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

        public bool ContainsPoint(Vector2Int point)
        {
            foreach (Plot plot in plots)
            {
                if (plot.location != point) { continue; }

                return true;
            }

            return false;
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