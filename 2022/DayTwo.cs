using AdventOfCode.Tools;

namespace AdventOfCode._2022
{
	public class DayTwo : Day
	{
		public const int DRAW_POINTS = 3;
		public const int LOSS_POINTS = 0;
		public const int WIN_POINTS = 6;

		public readonly Shape PAPER = new(2, 'B', 'Y');
		public readonly Shape ROCK = new(1, 'A', 'X');
		public readonly Shape SCISSORS = new(3, 'C', 'Z');

		public override int DayNumber { get; } = 2;
		private List<(char, char)> data = new();
		
		public override double Initialize()
		{
			stopwatch.Start();
			
			foreach (string line in DataRetriever.AsLines(this))
			{
				string[] parts = line.Split(' ');
				foreach (string part in parts)
				{
					if (part.Length != 1) { Debug.LogError($"Both parts should be exactly one: \'{part}\'"); }
				}

				char opponent = parts[0][0];
				char response = parts[1][0];
				data.Add((opponent, response));
			}

			stopwatch.Stop();
			return stopwatch.ElapsedMilliseconds;
		}

		public override string StarOne()
		{
			int totalScore = 0;

			foreach ((char, char) line in data)
			{
				if (line.Item1 == ROCK.Opponent)
				{
					if (line.Item2 == ROCK.Response) { totalScore += ROCK.Value + DRAW_POINTS; }
					else if (line.Item2 == PAPER.Response) { totalScore += PAPER.Value + WIN_POINTS; }
					else if (line.Item2 == SCISSORS.Response) { totalScore += SCISSORS.Value + LOSS_POINTS; }
					else { Debug.LogError($"Unrecognized response \'{line.Item2}\'"); }
				}
				else if (line.Item1 == PAPER.Opponent)
				{
					if (line.Item2 == ROCK.Response) { totalScore += ROCK.Value + LOSS_POINTS; }
					else if (line.Item2 == PAPER.Response) { totalScore += PAPER.Value + DRAW_POINTS; }
					else if (line.Item2 == SCISSORS.Response) { totalScore += SCISSORS.Value + WIN_POINTS; }
					else { Debug.LogError($"Unrecognized response \'{line.Item2}\'"); }
				}
				else if (line.Item1 == SCISSORS.Opponent)
				{
					if (line.Item2 == ROCK.Response) { totalScore += ROCK.Value + WIN_POINTS; }
					else if (line.Item2 == PAPER.Response) { totalScore += PAPER.Value + LOSS_POINTS; }
					else if (line.Item2 == SCISSORS.Response) { totalScore += SCISSORS.Value + DRAW_POINTS; }
					else { Debug.LogError($"Unrecognized response \'{line.Item2}\'"); }
				}
				else { Debug.LogError($"Unrecognized response \'{line.Item1}\'"); }
			}

			return totalScore.ToString();
		}


		public override string StarTwo()
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
						else { Debug.LogError($"Unrecognized response \'{line.Item1}\'"); }

						break;
					case 'Y':
						if (line.Item1 == ROCK.Opponent) { totalScore += DRAW_POINTS + ROCK.Value; }
						else if (line.Item1 == PAPER.Opponent) { totalScore += DRAW_POINTS + PAPER.Value; }
						else if (line.Item1 == SCISSORS.Opponent) { totalScore += DRAW_POINTS + SCISSORS.Value; }
						else { Debug.LogError($"Unrecognized response \'{line.Item1}\'"); }

						break;
					case 'Z':
						if (line.Item1 == ROCK.Opponent) { totalScore += WIN_POINTS + PAPER.Value; }
						else if (line.Item1 == PAPER.Opponent) { totalScore += WIN_POINTS + SCISSORS.Value; }
						else if (line.Item1 == SCISSORS.Opponent) { totalScore += WIN_POINTS + ROCK.Value; }
						else { Debug.LogError($"Unrecognized response \'{line.Item1}\'"); }

						break;
					default:
						Debug.LogError($"Unrecognized response \'{line.Item2}\'");
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
}