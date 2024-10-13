namespace advent_of_code.days;

public class Day0222 : IDay
{
    public DateTime date { get; } = new(2022, 12, 02);

    public const int DRAW_POINTS = 3;
    public const int LOSS_POINTS = 0;
    public const int WIN_POINTS = 6;

    public readonly Shape PAPER = new(2, 'B', 'Y');
    public readonly Shape ROCK = new(1, 'A', 'X');
    public readonly Shape SCISSORS = new(3, 'C', 'Z');

    private List<(char, char)> data = new();

    public void PopulateData(string raw)
    {
        string[] lines = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < lines.Length; ++i)
        {
            string[] parts = lines[i].Split(' ');

            for (int ii = 0; ii < parts.Length; ++ii)
            {
                string part = parts[ii];
                if (part.Length != 1)
                {
                    Logger.Error($"Both parts should be exactly one: \'{part}\'");
                }
            }

            char opponent = parts[0][0];
            char response = parts[1][0];
            data.Add((opponent, response));
        }
    }

    public string SolveStarOne()
    {
        int totalScore = 0;

        foreach ((char, char) line in data)
        {
            if (line.Item1 == ROCK.Opponent)
            {
                if (line.Item2 == ROCK.Response) { totalScore += ROCK.Value + DRAW_POINTS; }
                else if (line.Item2 == PAPER.Response) { totalScore += PAPER.Value + WIN_POINTS; }
                else if (line.Item2 == SCISSORS.Response) { totalScore += SCISSORS.Value + LOSS_POINTS; }
                else { Logger.Error($"Unrecognized response \'{line.Item2}\'"); }
            }
            else if (line.Item1 == PAPER.Opponent)
            {
                if (line.Item2 == ROCK.Response) { totalScore += ROCK.Value + LOSS_POINTS; }
                else if (line.Item2 == PAPER.Response) { totalScore += PAPER.Value + DRAW_POINTS; }
                else if (line.Item2 == SCISSORS.Response) { totalScore += SCISSORS.Value + WIN_POINTS; }
                else { Logger.Error($"Unrecognized response \'{line.Item2}\'"); }
            }
            else if (line.Item1 == SCISSORS.Opponent)
            {
                if (line.Item2 == ROCK.Response) { totalScore += ROCK.Value + WIN_POINTS; }
                else if (line.Item2 == PAPER.Response) { totalScore += PAPER.Value + LOSS_POINTS; }
                else if (line.Item2 == SCISSORS.Response) { totalScore += SCISSORS.Value + DRAW_POINTS; }
                else { Logger.Error($"Unrecognized response \'{line.Item2}\'"); }
            }
            else { Logger.Error($"Unrecognized response \'{line.Item1}\'"); }
        }

        return totalScore.ToString();
    }


    public string SolveStarTwo()
    {
        int totalScore = 0;

        foreach ((char, char) line in data)
        {
            switch (line.Item2)
            {
                case 'X':
                    if (line.Item1 == ROCK.Opponent) { totalScore += LOSS_POINTS + SCISSORS.Value; }
                    else if (line.Item1 == PAPER.Opponent) { totalScore += LOSS_POINTS + ROCK.Value; }
                    else if (line.Item1 == SCISSORS.Opponent) { totalScore += LOSS_POINTS + PAPER.Value; }
                    else { Logger.Error($"Unrecognized response \'{line.Item1}\'"); }

                    break;
                case 'Y':
                    if (line.Item1 == ROCK.Opponent) { totalScore += DRAW_POINTS + ROCK.Value; }
                    else if (line.Item1 == PAPER.Opponent) { totalScore += DRAW_POINTS + PAPER.Value; }
                    else if (line.Item1 == SCISSORS.Opponent) { totalScore += DRAW_POINTS + SCISSORS.Value; }
                    else { Logger.Error($"Unrecognized response \'{line.Item1}\'"); }

                    break;
                case 'Z':
                    if (line.Item1 == ROCK.Opponent) { totalScore += WIN_POINTS + PAPER.Value; }
                    else if (line.Item1 == PAPER.Opponent) { totalScore += WIN_POINTS + SCISSORS.Value; }
                    else if (line.Item1 == SCISSORS.Opponent) { totalScore += WIN_POINTS + ROCK.Value; }
                    else { Logger.Error($"Unrecognized response \'{line.Item1}\'"); }

                    break;
                default:
                    Logger.Error($"Unrecognized response \'{line.Item2}\'");
                    break;
            }
        }

        return totalScore.ToString();
    }

    public struct Shape
    {
        public char Opponent { get; }
        public char Response { get; }
        public int Value { get; }

        public Shape(int value, char opponent, char response)
        {
            Value = value;
            Opponent = opponent;
            Response = response;
        }
    }
}
