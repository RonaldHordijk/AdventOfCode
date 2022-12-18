var lines = File.ReadAllLines("data.txt");

int size = 99;
var heights = new int[size, size];
var visible = new bool[size, size];

for (int x = 0; x < size; x++)
    for (int y = 0; y < size; y++)
        heights[x, y] = int.Parse(lines[x][y].ToString());

for (int x = 0; x < size; x++)
{
    // down
    int max = heights[x, 0];
    visible[x, 0] = true;

    for (int y = 1; y < size; y++)
    {
        if (heights[x, y] > max)
        {
            max = heights[x, y];
            visible[x, y] = true;
        }
    }

    // up
    max = heights[x, size - 1];
    visible[x, size - 1] = true;

    for (int y = 1; y < size; y++)
    {
        if (heights[x, size - 1 - y] > max)
        {
            max = heights[x, size - 1 - y];
            visible[x, size - 1 - y] = true;
        }
    }

    // right
    max = heights[0, x];
    visible[0, x] = true;

    for (int y = 1; y < size; y++)
    {
        if (heights[y, x] > max)
        {
            max = heights[y, x];
            visible[y, x] = true;
        }
    }

    // left
    max = heights[size - 1, x];
    visible[size - 1, x] = true;

    for (int y = 1; y < size; y++)
    {
        if (heights[size - 1 - y, x] > max)
        {
            max = heights[size - 1 - y, x];
            visible[size - 1 - y, x] = true;
        }
    }

}



int sum = 0;
for (int x = 0; x < size; x++)
    for (int y = 0; y < size; y++)
        if (visible[x, y])
            sum++;

//for (int x = 0; x < size; x++)
//{
//    for (int y = 0; y < size; y++)
//        Console.Write(heights[x, y].ToString() + (visible[x, y] ? "." : " "));
//    Console.WriteLine();
//}

Console.WriteLine($"Total visible {sum}");

int maxScore = 0;
int maxloc = 0;

for (int x = 0; x < size; x++)
    for (int y = 0; y < size; y++)
    {
        int score = GetScore(x, y);
        if (score > maxScore)
        {
            maxScore = score;
            maxloc = 100 * x + y;
        }
    }

int GetScore(int x, int y)
{
    int totalScore = 1;

    // up
    int score = 0;
    for (int i = x - 1; i >= 0; i--)
    {
        score++;

        if (heights[i, y] >= heights[x, y])
        {
            break;
        }
    }

    totalScore *= score;


    // down
    score = 0;
    for (int i = x + 1; i < size; i++)
    {
        score++;

        if (heights[i, y] >= heights[x, y])
        {
            break;
        }
    }
    totalScore *= score;

    // left
    score = 0;
    for (int i = y - 1; i >= 0; i--)
    {
        score++;

        if (heights[x, i] >= heights[x, y])
        {
            break;
        }
    }
    totalScore *= score;

    // right
    score = 0;
    for (int i = y + 1; i < size; i++)
    {
        score++;

        if (heights[x, i] >= heights[x, y])
        {
            break;
        }
    }
    totalScore *= score;

    return totalScore;
}

Console.WriteLine($"Best Score is {maxScore} at {maxloc}");
