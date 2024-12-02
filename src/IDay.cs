internal interface IDay
{
    DateTime date { get; }

    void PopulateData(string raw);

    string SolveStarOne();

    string SolveStarTwo();
}