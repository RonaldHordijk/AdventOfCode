namespace Day15;

internal class Sensor
{
    public int SX { get; set; }
    public int SY { get; set; }
    public int BX { get; set; }
    public int BY { get; set; }

    public int Distance => Math.Abs(SX - BX) + Math.Abs(SY - BY);

    public (int s, int e) GetRow(int y)
    {
        int d = Distance - Math.Abs(SY - y);
        if (d < 0)
            return (1, -1);

        return (SX - d, SX + d);
    }
}
