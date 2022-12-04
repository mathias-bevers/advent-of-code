using AdventOfCode.Tools;

namespace AdventOfCode._2020
{
    public class DayTwo : Day
    {
        public override int DayNumber => 2;

        private struct PasswordData
        {
            public int MinOccurance { get; private set; }
            public int MaxOccurance { get; private set; }
            public char CharToCheck { get; private set; }
            public string Password { get; private set; }

            public PasswordData(int minOccurance, int maxOccurance, char charToCheck, string password)
            {
                MinOccurance = minOccurance;
                MaxOccurance = maxOccurance;
                CharToCheck = charToCheck;
                Password = password;
            }
        }

        private PasswordData[] passwordDatas;

        public override void Initialize()
        {
            base.Initialize();

            string[] file = DataRetriever.AsLines(this);
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


        public override string StarOne()
        {
            int rightPasswords = 0;

            for (int i = 0; i < passwordDatas.Length; i++)
            {
                int occurance = 0;

                foreach (char character in passwordDatas[i].Password)
                {
                    if (character != passwordDatas[i].CharToCheck) continue;

                    occurance++;
                }

                if (occurance < passwordDatas[i].MinOccurance || occurance > passwordDatas[i].MaxOccurance) continue;

                rightPasswords++;
            }

            return rightPasswords.ToString();
        }

        public override string StarTwo()
        {
            int rightPasswords = 0;

            for (int i = 0; i < passwordDatas.Length; i++)
            {
                int occurance = 0;
                char[] passwordAsArray = passwordDatas[i].Password.ToCharArray();

                if (passwordDatas[i].CharToCheck == passwordAsArray[passwordDatas[i].MinOccurance - 1])
                {
                    occurance++;
                }
                if (passwordDatas[i].CharToCheck == passwordAsArray[passwordDatas[i].MaxOccurance - 1])
                {
                    occurance++;
                }

                if (occurance != 1) continue;

                rightPasswords++;
            }

            return rightPasswords.ToString();
        }
    }
}