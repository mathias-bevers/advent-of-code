using System.Collections.Generic;
using AdventOfCode.Tools;
using System;

namespace AdventOfCode._2020
{
    public class DayFive : Day
    {
        public override int DayNumber => 5;

        private string[] data;
        private List<BoardingPass> boardingPasses = new List<BoardingPass>();

        public override void Initialize()
        {
            base.Initialize();
            data = DataRetriever.AsLines(this);
        }

        private struct BoardingPass
        {
            public int Row { get; private set; }
            public int Column { get; private set; }
            public int Id { get; private set; }

            public BoardingPass(int row, int column)
            {
                Row = row;
                Column = column;

                Id = row * 8 + column;
            }
        }

        public override string StarOne()
        {
            int FindRow(string row)
            {
                int min = 0;
                int max = 127;

                foreach (char character in row)
                {
                    int difference = max - min;

                    if (character == 'F')
                    {
                        max = min + ((int)(Math.Floor(difference * 0.5f)));
                        continue;
                    }

                    if (character == 'B')
                    {
                        min += ((int)(Math.Ceiling(difference * 0.5f)));
                        continue;
                    }

                    Debug.LogError($"{character} is not a valid in put!");
                }

                if (min != max) Debug.LogError($"{min} != {max}");

                return max;
            }

            int FindColumn(string column)
            {
                int min = 0;
                int max = 7;

                foreach (char character in column)
                {
                    int difference = max - min;

                    if (character == 'L')
                    {
                        //Floor
                        max = min + ((int)(Math.Floor(difference * 0.5f)));
                        continue;
                    }

                    if (character == 'R')
                    {
                        //Ceiling
                        min += ((int)(Math.Ceiling(difference * 0.5f)));
                        continue;
                    }

                    Debug.LogError($"{character} is not a valid in put!");
                }

                if (min != max) Debug.LogError($"{min} != {max}");

                return max;
            }

            foreach (string line in data)
            {
                int row = FindRow(line.Substring(0, 7));
                int column = FindColumn(line.Substring(7, 3));

                boardingPasses.Add(new BoardingPass(row, column));
            }

            int highestId = 0;

            foreach (BoardingPass boardingPass in boardingPasses)
            {
                if (boardingPass.Id <= highestId) continue;

                highestId = boardingPass.Id;
            }

            return highestId.ToString();
        }

        public override string StarTwo()
        {
            List<int> boardingPassIds = boardingPasses.ConvertAll(boardingPass => boardingPass.Id);
            int seatId = 0;

            for (int i = 0; i < boardingPassIds.Count; i++)
            {
                if (boardingPassIds.Contains(boardingPasses[i].Id + 1) || !boardingPassIds.Contains(boardingPasses[i].Id)) continue;

                seatId = boardingPasses[i].Id + 1;
            }

            return seatId.ToString();
        }
    }
}