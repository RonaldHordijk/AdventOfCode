




class Fence(double x1, double y1, double x2, double y2, int dx, int dy)
{
    public int X1 { get; set; } = (int)(2 * x1);
    public int X2 { get; set; } = (int)(2 * x2);
    public int Y1 { get; set; } = (int)(2 * y1);
    public int Y2 { get; set; } = (int)(2 * y2);

    public int DX { get; set; } = dx;
    public int DY { get; set; } = dy;

    public bool CanJoin(Fence other)
    {
        if (DX != other.DX || DY != other.DY)
            return false;

        if (X2 == other.X1 && Y2 == other.Y1)
            return true;

        return (X1 == other.X2 && Y1 == other.Y2);
    }

    public void Join(Fence other)
    {
        if (X2 == other.X1 && Y2 == other.Y1)
        {
            X2 = other.X2;
            Y2 = other.Y2;
        }
        else
        {
            X1 = other.X1;
            Y1 = other.Y1;
        }
    }
}
