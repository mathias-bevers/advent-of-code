namespace advent_of_code.days;

public class Day0220 : IDay
{
    public DateTime date { get; } = new(2020, 12, 02);

    private PasswordData[] passwordDatas = [];

    public void PopulateData(string raw)
    {
        string[] file = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        passwordDatas = new PasswordData[file.Length];

        for (int i = 0; i < file.Length; i++)
        {
            string[] splitLine = file[i].Split(' ');

            string[] numbers = splitLine[0].Split('-');
            int minOccurance = int.Parse(numbers[0]);
            int maxOccurance = int.Parse(numbers[1]);

            string[] chars = splitLine[1].Split(':');
            char charToCheck = chars[0].ToCharArray()[0];

            string password = splitLine[2];

            passwordDatas[i] = new PasswordData(minOccurance, maxOccurance, charToCheck, password);
        }
    }


    public string SolveStarOne()
    {
        int rightPasswords = 0;

        for (int i = 0; i < passwordDatas.Length; i++)
        {
            int occurance = 0;

            foreach (char character in passwordDatas[i].Password)
            {
                if (character != passwordDatas[i].CharToCheck) { continue; }

                occurance++;
            }

            if (occurance < passwordDatas[i].MinOccurance || occurance > passwordDatas[i].MaxOccurance) { continue; }

            rightPasswords++;
        }

        return rightPasswords.ToString();
    }

    public string SolveStarTwo()
    {
        int rightPasswords = 0;

        for (int i = 0; i < passwordDatas.Length; i++)
        {
            int occurance = 0;
            char[] passwordAsArray = passwordDatas[i].Password.ToCharArray();

            if (passwordDatas[i].CharToCheck == passwordAsArray[passwordDatas[i].MinOccurance - 1]) { occurance++; }

            if (passwordDatas[i].CharToCheck == passwordAsArray[passwordDatas[i].MaxOccurance - 1]) { occurance++; }

            if (occurance != 1) { continue; }

            rightPasswords++;
        }

        return rightPasswords.ToString();
    }

    private struct PasswordData(int minOccurance, int maxOccurance, char charToCheck, string password)
    {
        public char CharToCheck { get; private set; } = charToCheck;
        public int MaxOccurance { get; private set; } = maxOccurance;
        public int MinOccurance { get; private set; } = minOccurance;
        public string Password { get; private set; } = password;
    }
}
