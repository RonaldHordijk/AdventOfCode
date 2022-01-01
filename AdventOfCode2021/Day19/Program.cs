var lines = File.ReadAllLines("data.txt");
var scanners = new List<Scanner>();
var beacons = new List<(int x, int y, int z)>();

foreach (var line in lines)
{
    if (string.IsNullOrEmpty(line))
        continue;

    if (line.StartsWith("---"))
    {
        scanners.Add(new Scanner());
        continue;
    }

    var coords = line.Split(",").Select(s => Int32.Parse(s)).ToList();
    scanners.Last().Positions.Add((coords[0], coords[1], coords[2]));
}



beacons = scanners[0].Positions.ToList();
var usedScanners = new List<int> { 0 };

int round = 1;
while (true)
{
    Console.WriteLine($"Round {round++}");

    int bestCount = 0;
    int bestScanner = 0;
    for (int i = 1; i < scanners.Count; i++)
    {
        if (usedScanners.Contains(i))
            continue;

        var count = scanners[i].BestMatch(beacons);
        Console.WriteLine($"{i}, {count}");

        if (count > bestCount)
        {
            bestCount = count;
            bestScanner = i;
        }

        if (count >= 12)
        {
            scanners[i].AddWorldPositions(beacons);
            usedScanners.Add(i);
        }

    }
    if (bestScanner > 0)
    {
        scanners[bestScanner].AddWorldPositions(beacons);
        usedScanners.Add((int)bestScanner);
    }
    else
    {
        break;
    }
}


Console.WriteLine(beacons.Count);

var maxDist = 0;

for (int i = 0; i < scanners.Count - 1; i++)
    for (int j = i + 1; j < scanners.Count; j++)
    {
        var dist = Math.Abs(scanners[i].WorldPosition.x - scanners[j].WorldPosition.x)
            + Math.Abs(scanners[i].WorldPosition.y - scanners[j].WorldPosition.y)
            + Math.Abs(scanners[i].WorldPosition.z - scanners[j].WorldPosition.z);

        if (dist > maxDist)
            maxDist = dist;
    }

Console.WriteLine(maxDist);




public class Scanner
{
    public List<(int x, int y, int z)> Positions { get; } = new();
    public List<(int x, int y, int z)> WorldPositions { get; private set; } = new();

    public (int x, int y, int z) WorldPosition { get; set; }

    private List<(int x, int y, int z)> _rotPositions;

    internal int BestMatch(List<(int x, int y, int z)> beacons)
    {
        int maxCount = 0;
        int maxR = 0;
        (int x, int y, int z) maxDelta = (0, 0, 0);

        for (int r = 0; r < 24; r++)
        {
            Rotate(r);

            for (int i = 0; i < _rotPositions.Count; i++)
            {
                for (int j = 0; j < beacons.Count; j++)
                {
                    var delta = (x: _rotPositions[i].x - beacons[j].x,
                        y: _rotPositions[i].y - beacons[j].y,
                        z: _rotPositions[i].z - beacons[j].z);
                    var count = CountSame(delta, beacons);
                    if (count > maxCount)
                    {
                        maxCount = count;
                        maxDelta = delta;
                        maxR = r;
                    }
                }
            }
        }

        WorldPositions = Positions
            .ConvertAll(xyz => RotateCoord(maxR, xyz.x, xyz.y, xyz.z))
            .ConvertAll(xyz => (xyz.x - maxDelta.x, xyz.y - maxDelta.y, xyz.z - maxDelta.z))
            .ToList();

        WorldPosition = (-maxDelta.x, -maxDelta.y, -maxDelta.z);
        return maxCount;

    }

    private void Rotate(int r)
    {
        _rotPositions = Positions.ConvertAll(xyz => RotateCoord(r, xyz.x, xyz.y, xyz.z)).ToList();
    }

    private (int x, int y, int z) RotateCoord(int r, int x, int y, int z) => r switch
    {
        1 => (x, y, z),
        2 => (-y, x, z),
        3 => (-x, -y, z),
        4 => (y, -x, z),
        5 => (-x, y, -z),
        6 => (y, x, -z),
        7 => (x, -y, -z),
        8 => (-y, -x, -z),
        9 => (z, y, -x),
        10 => (-y, z, -x),
        11 => (-z, -y, -x),
        12 => (y, -z, -x),
        13 => (-z, y, x),
        14 => (y, z, x),
        15 => (z, -y, x),
        16 => (-y, -z, x),
        17 => (x, z, -y),
        18 => (-z, x, -y),
        19 => (-x, -z, -y),
        20 => (z, -x, -y),
        21 => (-x, z, y),
        22 => (z, x, y),
        23 => (x, -z, y),
        0 => (-z, -x, y),
        _ => (x, y, z)
    };

    private int CountSame((int x, int y, int z) delta, List<(int x, int y, int z)> beacons)
    {
        var count = 0;

        for (int i = 0; i < Positions.Count; i++)
        {
            int x = _rotPositions[i].x - delta.x;
            int y = _rotPositions[i].y - delta.y;
            int z = _rotPositions[i].z - delta.z;

            for (int j = 0; j < beacons.Count; j++)
            {
                if (x == beacons[j].x && y == beacons[j].y && z == beacons[j].z)
                {
                    count++;
                    break;
                }
            }
        }

        return count;
    }
    internal void AddWorldPositions(List<(int x, int y, int z)> beacons)
    {
        foreach (var wp in WorldPositions)
        {
            if (!beacons.Contains(wp))
            {
                beacons.Add(wp);
            }
        }
    }
}
