var lines = File.ReadAllLines("data.txt");

bool[,,] map = new bool[101, 101, 101];

var cubes = new List<Cube>();

foreach (var line in lines)
{
    var w = line.Split(' ');
    bool on = w[0] == "on";
    var c = w[1].Split(',');
    var mm = c[0].Split("=")[1].Split("..");
    int minx = int.Parse(mm[0]);
    int maxx = int.Parse(mm[1]);
    mm = c[1].Split("=")[1].Split("..");
    int miny = int.Parse(mm[0]);
    int maxy = int.Parse(mm[1]);
    mm = c[2].Split("=")[1].Split("..");
    int minz = int.Parse(mm[0]);
    int maxz = int.Parse(mm[1]);

    var newCube = new Cube
    {
        MinX = minx,
        MinY = miny,
        MaxX = maxx,
        MaxY = maxy,
        MinZ = minz,
        MaxZ = maxz
    };
    if (on)
        AddCube(newCube);
    else
        RemoveCube(newCube);

    for (int x = Math.Max(-50, minx); x <= Math.Min(50, maxx); x++)
        for (int y = Math.Max(-50, miny); y <= Math.Min(50, maxy); y++)
            for (int z = Math.Max(-50, minz); z <= Math.Min(50, maxz); z++)
            {
                map[x + 50, y + 50, z + 50] = on;
            }

}

long sum = 0;

for (int x = 0; x < 101; x++)
    for (int y = 0; y < 101; y++)
        for (int z = 0; z < 101; z++)
        {
            if (map[x, y, z])
                sum++;
        }

Console.WriteLine(sum);

sum = 0;
foreach (var cube in cubes)
{
    if (cube.Positive)
        sum += cube.Count;
    else
        sum -= cube.Count;
}

Console.WriteLine(sum);


void RemoveCube(Cube removeCube)
{
    if (cubes.Count == 0)
        return;

    int end = cubes.Count;
    for (int i = 0; i < end; i++)
    {
        if (!cubes[i].HasOverlap(removeCube))
            continue;

        cubes.Add(cubes[i].GetOverlap(removeCube));
        cubes.Last().Positive = !cubes[i].Positive;
    }
}

void AddCube(Cube newCube)
{
    int end = cubes.Count;
    cubes.Add(newCube);

    for (int i = 0; i < end; i++)
    {
        if (!cubes[i].HasOverlap(newCube))
            continue;

        cubes.Add(cubes[i].GetOverlap(newCube));
        if (cubes[i].Positive)
            cubes.Last().Positive = false;
    }

}




class Cube
{
    public int MinX { get; set; }
    public int MaxX { get; set; }
    public int MinY { get; set; }
    public int MaxY { get; set; }
    public int MinZ { get; set; }
    public int MaxZ { get; set; }

    public bool Positive { get; set; } = true;

    public List<Cube> SplitX(List<Cube> cubes, int newX)
    {
        var result = new List<Cube>();
        foreach (Cube c in cubes)
        {
            if (c.MinX >= newX || c.MaxX < newX)
            {
                result.Add(c);
            }
            else
            {

                result.Add(new Cube
                {
                    MinX = c.MinX,
                    MaxX = newX - 1,
                    MinY = c.MinY,
                    MaxY = c.MaxY,
                    MinZ = c.MinZ,
                    MaxZ = c.MaxZ,
                });

                result.Add(new Cube
                {
                    MinX = newX,
                    MaxX = c.MaxX,
                    MinY = c.MinY,
                    MaxY = c.MaxY,
                    MinZ = c.MinZ,
                    MaxZ = c.MaxZ,
                });
            }
        }

        return result;
    }

    public List<Cube> SplitY(List<Cube> cubes, int newY)
    {
        var result = new List<Cube>();
        foreach (Cube c in cubes)
        {
            if (c.MinY >= newY || c.MaxY < newY)
            {
                result.Add(c);
            }
            else
            {
                result.Add(new Cube
                {
                    MinX = c.MinX,
                    MaxX = c.MaxX,
                    MinY = newY - 1,
                    MaxY = c.MaxY,
                    MinZ = c.MinZ,
                    MaxZ = c.MaxZ,
                });

                result.Add(new Cube
                {
                    MinX = c.MinX,
                    MaxX = c.MaxX,
                    MinY = newY,
                    MaxY = c.MaxY,
                    MinZ = c.MinZ,
                    MaxZ = c.MaxZ,
                });
            }
        }

        return result;
    }

    public List<Cube> SplitZ(List<Cube> cubes, int newZ)
    {
        var result = new List<Cube>();
        foreach (Cube c in cubes)
        {
            if (c.MinZ >= newZ || c.MaxZ < newZ)
            {
                result.Add(c);
            }
            else
            {
                result.Add(new Cube
                {
                    MinX = c.MinX,
                    MaxX = c.MaxX,
                    MinY = c.MinY,
                    MaxY = c.MaxY,
                    MinZ = newZ - 1,
                    MaxZ = c.MaxZ,
                });

                result.Add(new Cube
                {
                    MinX = c.MinX,
                    MaxX = c.MaxX,
                    MinY = c.MinY,
                    MaxY = c.MaxY,
                    MinZ = c.MinZ,
                    MaxZ = newZ,
                });
            }
        }

        return result;
    }

    public List<Cube> Split(Cube other)
    {
        var result = new List<Cube>() { this };

        result = SplitX(result, other.MinX);
        result = SplitX(result, other.MaxX + 1);
        result = SplitY(result, other.MinZ);
        result = SplitY(result, other.MaxZ + 1);
        result = SplitZ(result, other.MinZ);
        result = SplitZ(result, other.MaxZ + 1);

        return result;
    }

    public bool HasOverlap(Cube other)
    {
        return !((MaxX < other.MinX) || (MinX > other.MaxX)
                || (MaxY < other.MinY) || (MinY > other.MaxY)
                || (MaxZ < other.MinZ) || (MinZ > other.MaxZ));
    }

    public bool Same(Cube other)
    {
        return (MinX == other.MinX && MaxX == other.MaxX
            && MinY == other.MinY && MaxY == other.MaxY
            && MinZ == other.MinZ && MaxZ == other.MaxZ);
    }

    public Cube GetOverlap(Cube other)
    {
        return new Cube
        {
            MinX = Math.Max(MinX, other.MinX),
            MaxX = Math.Min(MaxX, other.MaxX),
            MinY = Math.Max(MinY, other.MinY),
            MaxY = Math.Min(MaxY, other.MaxY),
            MinZ = Math.Max(MinZ, other.MinZ),
            MaxZ = Math.Min(MaxZ, other.MaxZ)
        };
    }

    public long Count => (long)(MaxX + 1 - MinX) * (long)(MaxY + 1 - MinY) * (long)(MaxZ + 1 - MinZ);
}
