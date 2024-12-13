using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day1324 : IDay
{
    public DateTime date { get; } = new(2024, 12, 13);

    private Game[] games = [];

    public void PopulateData(string raw)
    {
        string[] rawGames = raw.Split(Utils.NEW_CHUNKS, StringSplitOptions.RemoveEmptyEntries);
        games = new Game[rawGames.Length];

        for (int i = 0; i < games.Length; ++i)
        {
            string[] lines = rawGames[i].Split(Utils.NEW_LINES,
                                               StringSplitOptions.RemoveEmptyEntries);

            string[] stringA = lines[0].Replace("Button A: ", string.Empty).Split(", ");
            Vector2Int buttonA = new(int.Parse(stringA[0].Replace("X", string.Empty)),
                                     int.Parse(stringA[1].Replace("Y", string.Empty)));

            string[] stringB = lines[1].Replace("Button B: ", string.Empty).Split(", ");
            Vector2Int buttonB = new(int.Parse(stringB[0].Replace("X", string.Empty)),
                                     int.Parse(stringB[1].Replace("Y", string.Empty)));

            string[] stringT = lines[2].Replace("Prize: ", string.Empty).Split(", ");
            Vector2Int target = new(int.Parse(stringT[0].Replace("X=", string.Empty)),
                                    int.Parse(stringT[1].Replace("Y=", string.Empty)));

            games[i] = new Game(buttonA, buttonB, target);
        }

        Logger.Info('\n' + string.Join('\n', games));
    }

    public string SolveStarOne()
    {
        throw new NotImplementedException();
    }

    public string SolveStarTwo()
    {
        throw new NotImplementedException();
    }

    private struct Game(Vector2Int a, Vector2Int b, Vector2Int t)
    {
        public Vector2Int buttonA { get; } = a;
        public Vector2Int buttonB { get; } = b;
        public Vector2Int target { get; } = t;

        public override string ToString() => string.Concat(buttonA, ", ", buttonB, ", ", target);
    }
}