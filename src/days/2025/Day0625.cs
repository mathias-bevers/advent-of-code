using System.Diagnostics;
using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day0625 : IDay
{
    public DateTime date { get; } = new(2025, 12, 06);

    private Grid<short> problemsS1 = new(0, 0);
    private Grid<byte> problemsS2 = new(0, 0);
    private byte[] columnDigids = [];

    private char[] operators = [];

    public void PopulateData(string raw)
    {
        string[] lines = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);

        // add the operators to their own char array.
        string[] stringOperators = lines[^1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
        operators = new char[stringOperators.Length];
        for (int i = 0; i < operators.Length; i++)
        {
            operators[i] = stringOperators[i][0];
        }

        problemsS1 = new Grid<short>(operators.Length, lines.Length - 1);
        columnDigids = new byte[operators.Length];
        // skip the last line since it holds the opertators.
        for (int y = 0; y < lines.Length - 1; ++y)
        {
            string[] numbers = lines[y].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            for (int x = 0; x < numbers.Length; ++x)
            {
                problemsS1[x, y] = short.Parse(numbers[x]);

                if (numbers[x].Length <= columnDigids[x]) { continue; }

                columnDigids[x] = (byte)numbers[x].Length;
            }
        }

        // format the lines so the extra spaces are replaced with 0s.
        string[] formattedLines = new string[lines.Length - 1];
        for (int y = 0; y < formattedLines.Length; y++)
        {
            string formattedLine = string.Empty;
            int start = 0;
            for (int i = 0; i < columnDigids.Length; ++i)
            {
                string substr = lines[y].Substring(start, columnDigids[i]);
                substr = substr.Replace(' ', '0');
                formattedLine += substr;
                start += columnDigids[i] + 1;
            }

            formattedLines[y] = formattedLine;
        }

        // convert the formatted lines into a byte grid.
        problemsS2 = new Grid<byte>(formattedLines[0].Length, formattedLines.Length);
        for (int y = 0; y < problemsS2.height; y++)
        {
            for (int x = 0; x < problemsS2.width; x++)
            {
                problemsS2[x, y] = (byte)char.GetNumericValue(formattedLines[y][x]);
            }
        }
    }

    public string SolveStarOne()
    {
        long result = 0;

        for (int x = 0; x < problemsS1.width; x++)
        {
            short[] column = problemsS1.GetColumn(x);
            long columnResult = column[0];
            for (int y = 1; y < column.Length; y++)
            {
                if (operators[x] == '+')
                {
                    columnResult += column[y];
                }
                else
                {
                    columnResult *= column[y];
                }
            }

            result += columnResult;
        }

        return result.ToString();
    }

    public string SolveStarTwo()
    {
        long result = 0;
        int operatorIndex = 0;
        int digidLimit = columnDigids[0];
        List<long> columnValues = []; ;

        for (int x = 0; x < problemsS2.width; ++x)
        {
            if (x == digidLimit)
            {
                result += operators[operatorIndex] == '+' ? columnValues.Sum() 
                                                          : columnValues.Product();

                columnValues.Clear();
                ++operatorIndex;
                digidLimit += columnDigids[operatorIndex];
            }

            byte[] column = problemsS2.GetColumn(x);
            int columnValue = 0;
            for (int y = 0; y < column.Length; y++)
            {
                if (column[y] == 0)
                {
                    continue;
                }

                columnValue = (columnValue * 10) + column[y];
            }

            columnValues.Add(columnValue);
        }

        // repeat one more time for the last column
        result += operators[operatorIndex] == '+' ? columnValues.Sum() : columnValues.Product();

        return result.ToString();
    }
}