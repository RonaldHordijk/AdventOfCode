namespace Day16;
public class Valve
{
    public string Name { get; set; }
    public int Rate { get; set; }
    public List<string> Neighbours { get; set; } = new();

    public bool Open { get; set; } = false;
}
