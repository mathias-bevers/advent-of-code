using System.Text.RegularExpressions;

namespace advent_of_code.days;

public class Day0420 : IDay
{
    public DateTime date { get; } = new(2020, 12, 04);

    private readonly List<string> fieldPresentPassports = [];
    private string[] data = [];
    private readonly string[] eyeColors = ["amb", "blu", "brn", "gry", "grn", "hzl", "oth"];

    public void PopulateData(string raw)
    {
        data = raw.Split(Utils.NEW_CHUNKS, StringSplitOptions.RemoveEmptyEntries);
    }

    public string SolveStarOne()
    {
        foreach (string passport in data)
        {
            if (!passport.Contains("byr")) { continue; }

            if (!passport.Contains("iyr")) { continue; }

            if (!passport.Contains("eyr")) { continue; }

            if (!passport.Contains("hgt")) { continue; }

            if (!passport.Contains("hcl")) { continue; }

            if (!passport.Contains("ecl")) { continue; }

            if (!passport.Contains("pid")) { continue; }

            fieldPresentPassports.Add(passport);
        }


        return fieldPresentPassports.Count.ToString();
    }

    public string SolveStarTwo()
    {
        List<Passport> passports = [];
        char[] separator = ['\n', '\r', ' ', '\t'];
        int validPassports = 0;

        foreach (string passportString in fieldPresentPassports)
        {
            string[] fields = passportString.Split(separator, StringSplitOptions.RemoveEmptyEntries);

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

        Regex regex = new("^#[0-9a-f]{6}$");
        foreach (Passport passport in passports)
        {
            int byr = int.Parse(passport.Byr);
            if (byr < 1920 || byr > 2002) { continue; }

            int iyr = int.Parse(passport.Iyr);
            if (iyr < 2010 || iyr > 2020) { continue; }

            int eyr = int.Parse(passport.Eyr);
            if (eyr < 2020 || eyr > 2030) { continue; }

            if (passport.Hgt.Length == 4)
            {
                if (passport.Hgt[2] != 'i' || passport.Hgt[3] != 'n') { continue; }

                int height = int.Parse(passport.Hgt[..2]);

                if (height < 59 || height > 76) { continue; }
            }
            else if (passport.Hgt.Length == 5)
            {
                if (passport.Hgt[3] != 'c' || passport.Hgt[4] != 'm') { continue; }

                int height = int.Parse(passport.Hgt[..3]);

                if (height < 150 || height > 193) { continue; }
            }
            else { continue; }

            if (!regex.IsMatch(passport.Hcl)) { continue; }

            if (!eyeColors.Contains(passport.Ecl)) { continue; }

            if (passport.Pid.Length != 9) { continue; }

            validPassports++;
        }

        return validPassports.ToString();
    }

    private struct Passport(string byr, string iyr, string eyr, string hgt, string hcl, string ecl, string pid)
    {
        public string Byr { get; private set; } = byr;
        public string Ecl { get; private set; } = ecl;
        public string Eyr { get; private set; } = eyr;
        public string Hcl { get; private set; } = hcl;
        public string Hgt { get; private set; } = hgt;
        public string Iyr { get; private set; } = iyr;
        public string Pid { get; private set; } = pid;
    }
}