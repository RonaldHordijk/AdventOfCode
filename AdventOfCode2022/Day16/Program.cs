using Day16;
using System.Diagnostics;
using System.Text.RegularExpressions;

//var lines = File.ReadAllLines("data.txt");
var lines = File.ReadAllLines("dataTest.txt");

var valves = new List<Valve>();

//Valve AA has flow rate=0; tunnels lead to valves DD, II, BB
var regex = new Regex(@"Valve (.+?) .*=(\d+);.* valves* (.+)");

foreach (var line in lines)
{
    Match match = regex.Match(line);

    if (match.Success)
    {
        valves.Add(new Valve
        {
            Name = match.Groups[1].Value,
            Rate = int.Parse(match.Groups[2].Value),
            Neighbours = match.Groups[3].Value.Split(", ").ToList(),
        });
    }
}

// solving
int timelimit = 30;
int maxflow = 0;
int time = 0;
int totalflow = 0;
List<SolveStep> path = new();
path.Add(new SolveStep { Valve = valves[0] });

var sw = Stopwatch.StartNew();

while (DoStep())
{
}


Console.WriteLine($"done {sw.Elapsed}");

bool DoStep()
{
    UndoLastAction();

    var current = path.Last();

    bool loop = false;

    if (path.Count > 3
        && path[path.Count - 1].Valve == path[path.Count - 3].Valve
        && path[path.Count - 2].Action >= path[path.Count - 2].Valve.Neighbours.Count)
    {
        loop = true;
    }

    // reached end
    if (current.Action >= 2 * current.Valve.Neighbours.Count - 1 || time >= timelimit || CalcPossibleMax() < maxflow || loop)
    {
        // stepback
        if (path.Count == 1)
            return false;

        path.Remove(current);

        return true;
    }

    // next step 
    current.Action++;

    if (current.Action == 0 && (current.Valve.Open || current.Valve.Rate == 0))
    {
        // skip opening
        current.Action += current.Valve.Neighbours.Count;
    }

    if (current.Action < current.Valve.Neighbours.Count)
    {
        // open 
        current.Valve.Open = true;
        time++;
        current.Flow = (30 - time) * current.Valve.Rate;
        totalflow += current.Flow;
        if (totalflow > maxflow)
        {
            maxflow = totalflow;
            ReportPath();
        }
    }

    // move to next valve
    int next = current.Action % current.Valve.Neighbours.Count;
    var newValve = valves.Find(v => v.Name == current.Valve.Neighbours[next]);
    path.Add(new SolveStep { Valve = newValve });
    time++;

    return true;
}

double CalcPossibleMax()
{
    return totalflow + (30 - time) * valves.Where(v => !v.Open).Sum(v => v.Rate);
}

void UndoLastAction()
{
    var current = path.Last();

    // only added nothing done yet
    if (current.Action < 0)
        return;

    if (current.Action < current.Valve.Neighbours.Count)
    {
        current.Valve.Open = false;
        totalflow -= current.Flow;
        time--;
    }

    time--;

}

void ReportPath()
{
    Console.WriteLine($"{time} {maxflow} {totalflow}");
    Console.WriteLine(string.Join(" ", path.Select(p => p.Valve.Name)));
}
