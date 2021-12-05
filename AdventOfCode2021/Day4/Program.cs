var lines = File.ReadAllLines("data.txt");

var numbers = lines[0].Split(',');

var offset = 2;
while (offset < lines.Length)
{
    var board = LoadBoard(offset);
    var (nrdraws, value) = PlayBingo(board);
    if (nrdraws > 80)
        Console.WriteLine($"{nrdraws} - {numbers[nrdraws]} - {value} = {int.Parse(numbers[nrdraws]) * value}");
    offset += 6;
}

(int nrDraws, int count) PlayBingo(string[,] board)
{
    for (int i = 0; i < numbers.Length; i++)
    {
        Mark(numbers[i], ref board);
        if (HasBingo(board))
            return (i, Count(board));
    }

    return (0, 0);
}

int Count(string[,] board)
{
    int tot = 0;

    for (int i = 0; i < 5; i++)
    {
        for (int j = 0; j < 5; j++)
        {
            if (!string.IsNullOrEmpty(board[i, j]))
                tot += int.Parse(board[i, j]);
        }
    }

    return tot;
}

bool HasBingo(string[,] board)
{
    for (int i = 0; i < 5; i++)
    {
        // row
        bool allzero = true;
        for (int j = 0; j < 5; j++)
        {
            if (!string.IsNullOrEmpty(board[i, j]))
                allzero = false;
        }

        if (allzero)
            return true;

        // col
        allzero = true;
        for (int j = 0; j < 5; j++)
        {
            if (!string.IsNullOrEmpty(board[j, i]))
                allzero = false;
        }

        if (allzero)
            return true;
    }

    return false;
}

void Mark(string draw, ref string[,] board)
{
    for (int i = 0; i < 5; i++)
    {
        for (int j = 0; j < 5; j++)
        {
            if (board[i, j] == draw)
                board[i, j] = String.Empty;
        }
    }
}

string[,] LoadBoard(int offset)
{
    var board = new string[5, 5];

    for (int i = 0; i < 5; i++)
    {
        var w = lines[offset + i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        for (int j = 0; j < 5; j++)
        {
            board[i, j] = w[j];
        }
    }

    return board;
}
