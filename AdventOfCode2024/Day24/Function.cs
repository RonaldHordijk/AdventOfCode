
class Function(string val1, string val2, string op, string result)
{
    public string Val1 { get; } = val1;
    public string Val2 { get; } = val2;
    public string Op { get; } = op;
    public string Result { get; set; } = result;

    public bool Fault { get; set; }
}
