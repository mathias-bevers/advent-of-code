using advent_of_code.utils;

namespace advent_of_code.days;

public class Day0822 : IDay
{
    public DateTime date { get; } = new(2022, 12, 08);

    // private Tree[,] trees = new Tree[0, 0];
    private Grid<Tree> trees = new(0, 0);

    public void PopulateData(string raw)
    {
        string[] rowsAsStrings = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);

        trees = new Grid<Tree>(rowsAsStrings[0].Length, rowsAsStrings.Length);

        for (int y = 0; y < trees.height; y++)
        {
            for (int x = 0; x < trees.width; x++)
            {
                int height = (int)char.GetNumericValue(rowsAsStrings[y][x]);
                trees[x, y] = new Tree(height, x, y);
            }
        }
    }

    public string SolveStarOne()
    {
        int visibleTreeCount = 0;

        trees.Loop((tree, position) =>
        {
            SetTreeVisibilityAndScore(tree);

            if (!tree.IsVisible) { return; }

            ++visibleTreeCount;
        });

        return visibleTreeCount.ToString();
    }


    public string SolveStarTwo()
    {
        IEnumerable<Tree> sortedByScore = trees.As1D().ToList().OrderByDescending(t => t.ScenicScore);
        return sortedByScore.First().ScenicScore.ToString();
    }

    private void SetTreeVisibilityAndScore(Tree tree)
    {
        Tree.Direction visibleSides = Tree.Direction.ALL;
        int[] scores = { 0, 0, 0, 0 };

        if (tree.PositionX != trees.width - 1)
        {
            for (int x = tree.PositionX + 1; x < trees.width; ++x)
            {
                ++scores[0];
                if (tree.Height > trees[x, tree.PositionY].Height) { continue; }

                visibleSides &= ~Tree.Direction.Right;
                break;
            }
        }

        if (tree.PositionX != 0)
        {
            for (int x = tree.PositionX - 1; x >= 0; --x)
            {
                ++scores[1];
                if (tree.Height > trees[x, tree.PositionY].Height) { continue; }

                visibleSides &= ~Tree.Direction.Left;
                break;
            }
        }

        if (tree.PositionY != trees.height - 1)
        {
            for (int y = tree.PositionY + 1; y < trees.height; ++y)
            {
                ++scores[2];
                if (tree.Height > trees[tree.PositionX, y].Height) { continue; }

                visibleSides &= ~Tree.Direction.Bottom;
                break;
            }
        }

        if (tree.PositionY != 0)
        {
            for (int y = tree.PositionY - 1; y >= 0; --y)
            {
                ++scores[3];
                if (tree.Height > trees[tree.PositionX, y].Height) { continue; }

                visibleSides &= ~Tree.Direction.Top;
                break;
            }
        }

        tree.VisibleSides = visibleSides;
        tree.ScenicScore = scores.Aggregate(1, (current, t) => current * t);
    }

    private class Tree
    {
        [Flags]
        public enum Direction
        {
            NONE = 0,
            Top = 1,
            Right = 2,
            Bottom = 4,
            Left = 8,
            ALL = Top | Right | Bottom | Left
        }

        public bool IsVisible => VisibleSides != Direction.NONE;
        public Direction VisibleSides { get; set; }
        public int Height { get; }
        public int PositionX { get; }
        public int PositionY { get; }
        public int ScenicScore { get; set; }

        public Tree(int height, int x, int y)
        {
            Height = height;
            PositionX = x;
            PositionY = y;
            VisibleSides = Direction.ALL;
        }

        public override string ToString()
        {
            string[] parts =
            {
                $"({PositionX},{PositionY})'s visible sides: {VisibleSides,-25}",
                $"is {Height} tall".PadRight(10),
                $"and has a score of {ScenicScore}"
            };


            return string.Concat(parts);
        }
    }
}
