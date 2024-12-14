# Advent Of Code

[<img src="https://github.com/mathias-bevers/advent-of-code/blob/main/images/1424.png?raw=true" alt="x-mas tree" width="303" align=right>](https://github.com/mathias-bevers/advent-of-code/blob/main/src/days/2024/Day1424.cs)

In this repository, you will find all my advent of code solutions written in C#. I have been participating since 2019, but I still need to earn a lot of stars.

<br clear="right">

## Running the project
To prevent my input from getting published, I am using a web request to get my puzzle input. To run this project, you can run it in example mode (see arguments), or you will need to get your session ID. I found my session ID by using an extension to gather my cookie data. Once you have found your cookie, put it in a file called `aoc.cookies`. The project is running the `net8.0` framework.

### Arguments
With out passing any arguments it will be the same as providing `-y <year based on computer>`. 

| Short Hand | Full | Description |
| --- | --- | --- |
| -a | --all | Run all solutions in the project |
| -t | --today | Runs the solution of today based on the computer's date. |
| -e | --example | Run the project in example mode. In example mode the input will be read from a local file with the naming scheme `examples/year/day.example`. If the file does not exist, it will be created | 
| -y | --year | Runs all solutions of a specific year. Unless it is combined with the `-d` argument, in which case it will run the solution of a specific year and day |
| -d | --day | Runs the solution of the specified day of the current year. Unless it is combined with the `-y`, in which case it will run the solution of a specific year and day |
