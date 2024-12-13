using System.Runtime.CompilerServices;
using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day1324 : IDay
{
    public DateTime date { get; } = new(2024, 12, 13);

    private const int BUTTON_A_COST = 3;
    private const int MAX_BUTTON_PRESSES = 100;

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
            Vector2Long buttonA = new(long.Parse(stringA[0].Replace("X", string.Empty)),
                                     long.Parse(stringA[1].Replace("Y", string.Empty)));

            string[] stringB = lines[1].Replace("Button B: ", string.Empty).Split(", ");
            Vector2Long buttonB = new(long.Parse(stringB[0].Replace("X", string.Empty)),
                                     long.Parse(stringB[1].Replace("Y", string.Empty)));

            string[] stringT = lines[2].Replace("Prize: ", string.Empty).Split(", ");
            Vector2Long target = new(long.Parse(stringT[0].Replace("X=", string.Empty)),
                                    long.Parse(stringT[1].Replace("Y=", string.Empty)));

            games[i] = new Game(buttonA, buttonB, target);
        }
    }

    public string SolveStarOne()
    {
        long totalNeededTokens = 0;

        for (int i = 0; i < games.Length; ++i)
        {
            Game game = games[i];

            long neededTokens = GetTargetCost(1, game);

            if (neededTokens < 0) { continue; }

            totalNeededTokens += neededTokens;
        }

        return totalNeededTokens.ToString();
    }

    public string SolveStarTwo()
    {
        long totalNeededTokens = 0;

        for (int i = 0; i < games.Length; ++i)
        {
            Game game = games[i];

            long neededTokens = GetTargetCost(2, game);

            if (neededTokens < 0) { continue; }

            totalNeededTokens += neededTokens;
        }

        return totalNeededTokens.ToString();
    }

    private long GetTargetCost(byte part, Game game)
    {
        Vector2Long target = part == 2 ? game.targetTwo : game.targetOne;

        long i = target.Determinant(game.buttonB) / game.buttonA.Determinant(game.buttonB);
        long j = game.buttonA.Determinant(target) / game.buttonA.Determinant(game.buttonB);

        if (i < 0 || j < 0) { return -1; }

        Vector2Long result = game.buttonA * i + game.buttonB * j;

        if (result != target) { return -1; }

        return i * BUTTON_A_COST + j;
    }

    private record Game(Vector2Long a, Vector2Long b, Vector2Long t)
    {
        public Vector2Long buttonA { get; } = a;
        public Vector2Long buttonB { get; } = b;
        public Vector2Long targetOne { get; } = t;
        public Vector2Long targetTwo { get; } = t + new Vector2Long(10_000_000_000_000);

        public override string ToString() =>
            string.Concat(buttonA, ", ", buttonB, ", ", targetOne);
    }
}