using System.Numerics;

namespace advent_of_code.utils;

internal record Interval<T>(T start, T end) : Interval where T : INumber<T>
{
    public T start { get; } = start;
    public T end { get; } = end;
    public T difference { get; } = end - start;

    public bool Contains(T value)
    {
        if (value < start)
        {
            return false;
        }

        if (value > end)
        {
            return false;
        }

        return true;
    }

    public bool Encapsules(Interval<T> other)
    {
        if (start > other.start)
        {
            return false;
        }

        if (end < other.end)
        {
            return false;
        }

        return true;
    }

    public override string ToString()
    {
        return string.Concat('(', start, " <-> ", end, ')');
    }
}

internal record Interval
{
    public static IEnumerable<Interval<T>> Collapse<T>(IList<Interval<T>> collection)
    where T : INumber<T>
    {
        List<Interval<T>> collapsed = [.. collection.OrderBy(i => i.end)];
        
        for (int i = collapsed.Count - 1; i > 0; i--)
        {
            Interval<T> a = collapsed[i];
            Interval<T> b = collapsed[i - 1];

            if (a.Encapsules(b))
            {
                collapsed.RemoveAt(i - 1);
            }
            else if (a.Contains(b.end)) // meaning they overlap
            {
                collapsed.RemoveAt(i);
                collapsed.RemoveAt(i - 1);
                collapsed.Insert(i - 1, Union(a, b));
            }
        }


        return collapsed;
    }

    public static Interval<T> Union<T>(Interval<T> a, Interval<T> b) where T : INumber<T>
    {
        // math.min and math.max are not happy with T : INumber
        return new Interval<T>(a.start < b.start ? a.start : b.start,
                               a.end > b.end ? a.end : b.end);
    }
}