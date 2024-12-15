class Robot(int posx, int posy, int vx, int vy)
{
    public int PosX { get; set; } = posx;
    public int PosY { get; set; } = posy;
    public int VX { get; set; } = vx;
    public int VY { get; set; } = vy;

    public void DoStep(int maxx, int maxy)
    {
        PosX = (PosX + VX + maxx) % maxx;
        PosY = (PosY + VY + maxy) % maxy;
    }

}
