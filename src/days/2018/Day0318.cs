using System.ComponentModel;
using advent_of_code.utils;

namespace advent_of_code.days;

public class DayThree : IDay
{
    public DateTime date { get; } = new(2018, 12, 03);

    // private readonly char[,] grid = new char[999, 997];
    private readonly Grid<char> grid = new (999, 997);

    private ElfClaim[] elfClaims = [];

    public void PopulateData(string raw)
    {
        string[] lines = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        elfClaims = new ElfClaim[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            string[] mainParts = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            int id = int.Parse(mainParts[0].TrimStart('#'));

            string[] positionAsStrings = mainParts[2].TrimEnd(':').Split(',');
            Vector2Int position = new(int.Parse(positionAsStrings[0]), int.Parse(positionAsStrings[1]));

            string[] sizeAsStrings = mainParts[3].Split('x');
            Vector2Int size = new(int.Parse(sizeAsStrings[0]), int.Parse(sizeAsStrings[1]));

            elfClaims[i] = new ElfClaim(id, position, size);
        }
    }

    public string SolveStarOne()
    {
        foreach (ElfClaim elfClaim in elfClaims)
        {
            for (int x = elfClaim.Position.x; x < elfClaim.Position.x + elfClaim.Size.x; ++x)
                for (int y = elfClaim.Position.y; y < elfClaim.Position.y + elfClaim.Size.y; ++y)
                {
                    switch (grid[x, y])
                    {
                        case 'x': break;
                        case '*':
                            grid[x, y] = 'x';
                            break;
                        default:
                            grid[x, y] = '*';
                            break;
                    }
                }
        }

        int occupiedMultipleTimesCount = 0;

        grid.Loop((c, position) => {
            if(c != 'x') 
            {
                return;
            }

            ++occupiedMultipleTimesCount;
        });

        return occupiedMultipleTimesCount.ToString();
    }

    public string SolveStarTwo()
    {
        foreach (ElfClaim elfClaim in elfClaims)
        {
            if (IsOverlapping(elfClaim)) { continue; }

            return elfClaim.ID.ToString();
        }

        return "FAILED";
    }

    private bool IsOverlapping(ElfClaim elfClaim)
    {
        for (int x = elfClaim.Position.x; x < elfClaim.Position.x + elfClaim.Size.x; ++x)
            for (int y = elfClaim.Position.y; y < elfClaim.Position.y + elfClaim.Size.y; ++y)
            {
                if (grid[x, y] == 'x') { return true; }
            }

        return false;
    }

    private readonly struct ElfClaim(int id, Vector2Int position, Vector2Int size)
    {
        public int ID { get; } = id;
        public Vector2Int Position { get; } = position;
        public Vector2Int Size { get; } = size;
    }
}