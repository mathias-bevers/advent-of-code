using System.Text.RegularExpressions;
using AdventOfCode.Tools;
using System.Collections.Generic;
using System.Linq;
using System;

namespace AdventOfCode._2020
{
    public class DayFour : Day
    {
        public override int DayNumber => 4;

        private string[] data;
        private List<string> fieldPresentPassports = new List<string>();
        private string[] eyeColors = { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

        public override void Initialize()
        {
            base.Initialize();
            data = DataRetriever.AsFile(this).Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        public override string StarOne()
        {
            foreach (string passport in data)
            {
                if (!passport.Contains("byr")) continue;
                if (!passport.Contains("iyr")) continue;
                if (!passport.Contains("eyr")) continue;
                if (!passport.Contains("hgt")) continue;
                if (!passport.Contains("hcl")) continue;
                if (!passport.Contains("ecl")) continue;
                if (!passport.Contains("pid")) continue;

                fieldPresentPassports.Add(passport);
            }


            return fieldPresentPassports.Count.ToString();
        }

        private struct Passport
        {
            public string Byr { get; private set; }
            public string Iyr { get; private set; }
            public string Eyr { get; private set; }
            public string Hgt { get; private set; }
            public string Hcl { get; private set; }
            public string Ecl { get; private set; }
            public string Pid { get; private set; }

            public Passport(string byr, string iyr, string eyr, string hgt, string hcl, string ecl, string pid)
            {
                Byr = byr;
                Iyr = iyr;
                Eyr = eyr;
                Hgt = hgt;
                Hcl = hcl;
                Ecl = ecl;
                Pid = pid;
            }
        }

        public override string StarTwo()
        {
            List<Passport> passports = new List<Passport>();
            int validPassports = 0;

            foreach (string passportString in fieldPresentPassports)
            {
                string[] fields = passportString.Split(new[] { '\n', '\r', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                string byr = string.Empty;
                string iyr = string.Empty;
                string eyr = string.Empty;
                string hgt = string.Empty;
                string hcl = string.Empty;
                string ecl = string.Empty;
                string pid = string.Empty;

                foreach (string field in fields)
                {
                    string[] split = field.Split(':');

                    switch (split[0])
                    {
                        case "byr":
                            {
                                byr = split[1];
                                break;
                            }
                        case "iyr":
                            {
                                iyr = split[1];
                                break;
                            }
                        case "eyr":
                            {
                                eyr = split[1];
                                break;
                            }
                        case "hgt":
                            {
                                hgt = split[1];
                                break;
                            }
                        case "hcl":
                            {
                                hcl = split[1];
                                break;
                            }
                        case "ecl":
                            {
                                ecl = split[1];
                                break;
                            }
                        case "pid":
                            {
                                pid = split[1];
                                break;
                            }

                    }
                }

                passports.Add(new Passport(byr, iyr, eyr, hgt, hcl, ecl, pid));
            }

            foreach (Passport passport in passports)
            {
                int byr = int.Parse(passport.Byr);
                if (byr < 1920 || byr > 2002)
                    continue;

                int iyr = int.Parse(passport.Iyr);
                if (iyr < 2010 || iyr > 2020)
                    continue;

                int eyr = int.Parse(passport.Eyr);
                if (eyr < 2020 || eyr > 2030)
                    continue;

                if (passport.Hgt.Length == 4)
                {
                    if (passport.Hgt[2] != 'i' || passport.Hgt[3] != 'n') continue;

                    int height = int.Parse(passport.Hgt.Substring(0, 2));

                    if (height < 59 || height > 76)
                        continue;
                }
                else if (passport.Hgt.Length == 5)
                {
                    if (passport.Hgt[3] != 'c' || passport.Hgt[4] != 'm') continue;

                    int height = int.Parse(passport.Hgt.Substring(0, 3));

                    if (height < 150 || height > 193)
                        continue;
                }
                else
                    continue;

                Regex regex = new Regex("^#[0-9a-f]{6}$");
                if (!regex.IsMatch(passport.Hcl))
                    continue;

                if (!eyeColors.Contains(passport.Ecl))
                    continue;

                if (passport.Pid.Length != 9)
                    continue;

                validPassports++;
            }

            return validPassports.ToString();
        }
    }
}