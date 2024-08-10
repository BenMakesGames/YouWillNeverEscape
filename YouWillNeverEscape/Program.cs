using System.Diagnostics;

// Please remember to build in RELEASE mode - debug mode will run slower!

const int defaultNumberOfAttempts = 1_000_000_000;
const int numberOfTurns = 231;

// Parse the number of attempts from the command line arguments, or use the default value
var numberOfAttempts = args.Length > 0 && int.TryParse(args[0], out var arg)
    ? arg
    : defaultNumberOfAttempts;

Console.WriteLine($"Number of combat attempts: {numberOfAttempts}");

var stopwatch = new Stopwatch();
stopwatch.Start();

var bestAttempt = Enumerable.Range(0, numberOfAttempts)
    .AsParallel() // I hope you didn't have other plans for your CPU...
    .Select(_ => ParalysisProcCount())
    .Max();

stopwatch.Stop();

Console.WriteLine($"Done! Ran for {stopwatch.Elapsed}");
Console.WriteLine($"Highest number of paralysis procs in {numberOfTurns} turns: {bestAttempt}");

return;

int ParalysisProcCount()
{
    var successes = 0;

    for (var turn = 0; turn < numberOfTurns; turn++)
    {
        if (Random.Shared.Next(4) == 0)
            successes++;
    }

    return successes;
}
