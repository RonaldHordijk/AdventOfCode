var lines = File.ReadAllLines("data.txt");
string data = lines[0];
//string data = "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw";

//mjqjpqmgbljsphdztnvjfqwrcgsmlb 7
//bvwbjplbgvbhsrlpgdmjqwftvncz: first marker after character 5
//nppdvjthqldpwncqszvftbrmjlhg: first marker after character 6
//nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg: first marker after character 10
//zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw: first marker after character 11

int first = GetFirstMarker(data);

Console.WriteLine($"First Marker {GetFirstMarker(data)}");
Console.WriteLine($"First Message {GetFirstMessage(data)}");

int GetFirstMarker(string data)
{
    for (int i = 0; i < data.Length - 4; i++)
    {
        if (data.ToCharArray().Skip(i).Take(4).GroupBy(c => c).Count() == 4)
            return i + 4;
    }

    return 0;
}

int GetFirstMessage(string data)
{
    for (int i = 0; i < data.Length - 4; i++)
    {
        if (data.ToCharArray().Skip(i).Take(14).GroupBy(c => c).Count() == 14)
            return i + 14;
    }

    return 0;
}
