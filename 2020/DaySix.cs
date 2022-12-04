using System.Collections.Generic;
using AdventOfCode.Tools;
using System.Linq;
using System.IO;
using System;

namespace AdventOfCode._2020
{
    public class DaySix : Day
    {
        public override int DayNumber => 6;
        private string[] data;


        public override void Initialize()
        {
            base.Initialize();
            data = DataRetriever.AsFile(this).Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        public override string StarOne()
        {
            int sumOfYes = 0;

            foreach (string group in data)
            {
                string trimmedGroup = String.Concat(group.Where(c => !Char.IsWhiteSpace(c)));

                sumOfYes += trimmedGroup.Distinct().Count();
            }

            return sumOfYes.ToString();
        }

        public override string StarTwo()
        {
            int sumOfSimmilarAwnsers = 0;

            foreach (string group in data)
            {
                string[] persons = group.Split(new[] { '\n', '\r', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                Dictionary<char, int> awnsers = new Dictionary<char, int>();

                foreach (string person in persons)
                {
                    int simmilarAwnsers = 0;

                    foreach (char awnser in person)
                    {
                        if (awnsers.ContainsKey(awnser))
                        {
                            awnsers[awnser]++;
                            continue;
                        }

                        awnsers.Add(awnser, 1);
                        continue;
                    }

                    foreach (KeyValuePair<char, int> keyValuePair in awnsers)
                    {
                        if (keyValuePair.Value != persons.Count()) continue;

                        simmilarAwnsers++;
                    }

                    sumOfSimmilarAwnsers += simmilarAwnsers;
                }
            }

            return sumOfSimmilarAwnsers.ToString();
        }
    }
}