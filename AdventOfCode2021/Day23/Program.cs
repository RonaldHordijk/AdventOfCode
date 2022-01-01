
var start = new Step
{
    //#############
    //#...........#
    //###B#C#B#D###
    //  #A#D#C#A#
    //  #########
    //    Alley = new int[4, 2] { { 2, 1 }, { 3, 4 }, { 2, 3 }, { 4, 1 } },

    //#############
    //#..2.4.6.8..#
    //###D#B#A#C###
    //  #C#A#D#B#
    //  #########
    //Alley = new int[4, 2] { { 4, 3 }, { 2, 1 }, { 1, 4 }, { 3, 2 } },

    //#############
    //#...........#
    //###B#C#B#D###
    //  #D#C#B#A#
    //  #D#B#A#C#
    //  #A#D#C#A#
    //  #########
    //Alley = new int[4, 4] { { 2, 4, 4, 1 }, { 3, 3, 2, 4 }, { 2, 2, 1, 3 }, { 4, 1, 3, 1 } },

    //#############
    //#...........#
    //###D#B#A#C###
    //  #D#C#B#A#
    //  #D#B#A#C#
    //  #C#A#D#B#
    //  #########
    Alley = new int[4, 4] { { 4, 4, 4, 3 }, { 2, 3, 2, 1 }, { 1, 2, 1, 4 }, { 3, 1, 3, 2 } },
};



int MinEnergy = 50000;

List<Step> steps = new List<Step>();
steps.Add(start);
long cnt = 0;
while (true)
{

    //steps.First().Show();

    var work = steps.First();
    var newsteps = work.GetNextSteps();
    if (newsteps.Count == 0 && work.TopRow.Sum() == 0)
    {
        if (work.Energy < MinEnergy)
            MinEnergy = work.Energy;
    }


    steps.AddRange(newsteps);
    steps.RemoveAt(0);

    //Console.WriteLine(steps.Count.ToString());
    //foreach (var step in newsteps)
    //{
    //    step.Show();
    //}

    cnt++;
    if (cnt % 100000 == 0)
    {
        steps.First().Show();
        Console.WriteLine($"{cnt} - {steps.First().Score} - {steps.First().Energy} - {steps.Count()} = {MinEnergy}");

        steps = steps.Where(s => s.Energy < MinEnergy).OrderBy(s => -s.Score).ToList();
    }
}


//foreach (var step in steps)
//{
//    step.Show();
//}

class Step
{
    public int[] TopRow = new int[11];
    public int[,] Alley = new int[4, 4]; // 2 4 6 8

    public int Energy = 0;
    public long Score = 0;


    public List<Step> GetNextSteps()
    {
        var result = new List<Step>();

        result.AddRange(MoveOutAlley(0));
        result.AddRange(MoveOutAlley(1));
        result.AddRange(MoveOutAlley(2));
        result.AddRange(MoveOutAlley(3));

        result.AddRange(MoveBackToAlley());

        return result;
    }

    private List<Step> MoveBackToAlley()
    {
        var result = new List<Step>();

        for (int i = 0; i < 11; i++)
        {
            if (TopRow[i] == 0)
                continue;

            int animal = TopRow[i];
            bool canAdd = Alley[animal - 1, 0] == 0
                && (Alley[animal - 1, 1] == 0 || Alley[animal - 1, 1] == animal)
                && (Alley[animal - 1, 2] == 0 || Alley[animal - 1, 2] == animal)
                && (Alley[animal - 1, 3] == 0 || Alley[animal - 1, 3] == animal);

            if (canAdd)
            {
                // is path blocked
                bool blocked = false;
                int destRowPos = animal * 2;
                if (destRowPos < i)
                {
                    for (int j = destRowPos; j < i; j++)
                    {
                        if (TopRow[j] != 0)
                            blocked = true;
                    }
                }
                else
                {
                    for (int j = i + 1; j < destRowPos; j++)
                    {
                        if (TopRow[j] != 0)
                            blocked = true;
                    }
                }

                if (!blocked)
                {
                    var newStep = Clone();
                    newStep.TopRow[i] = 0;

                    int destTop = 0;
                    if (Alley[animal - 1, 0] == 0)
                        destTop = 1;
                    if (Alley[animal - 1, 1] == 0)
                        destTop = 2;
                    if (Alley[animal - 1, 2] == 0)
                        destTop = 3;

                    newStep.Alley[animal - 1, destTop] = animal;

                    newStep.Energy += Cost(animal, 1 + Math.Abs(i - destRowPos) + destTop);
                    newStep.CalcScore();

                    result.Add(newStep);
                }
            }
        }
        return result;
    }

    private List<Step> MoveOutAlley(int nr)
    {
        var result = new List<Step>();

        // out of A
        int top = 0;
        if (Alley[nr, 0] == 0)
            top = 1;
        if (Alley[nr, 1] == 0)
            top = 2;
        if (Alley[nr, 2] == 0)
            top = 3;

        // empty
        if (Alley[nr, top] == 0)
            return result;

        // the one belonging to it
        if ((top == 0 && Alley[nr, 0] == nr + 1 && Alley[nr, 1] == nr + 1)
            || (top == 1 && Alley[nr, top] == nr + 1))
            return result;

        int rowpos = nr * 2 + 2;

        // can move to correct place
        // if so return
        int animal = Alley[nr, top];
        bool canAdd = Alley[animal - 1, 0] == 0
            && (Alley[animal - 1, 1] == 0 || Alley[animal - 1, 1] == animal)
            && (Alley[animal - 1, 2] == 0 || Alley[animal - 1, 2] == animal)
            && (Alley[animal - 1, 3] == 0 || Alley[animal - 1, 3] == animal);

        if (canAdd)
        {
            // is path blocked
            bool blocked = false;
            int destRowPos = animal * 2;
            if (destRowPos < rowpos)
            {
                for (int i = destRowPos; i < rowpos; i++)
                {
                    if (TopRow[i] != 0)
                        blocked = true;
                }
            }
            else
            {
                for (int i = rowpos; i < destRowPos; i++)
                {
                    if (TopRow[i] != 0)
                        blocked = true;
                }
            }

            if (!blocked)
            {
                var newStep = Clone();
                newStep.Alley[nr, top] = 0;

                int destTop = 0;
                if (Alley[animal - 1, 0] == 0)
                    destTop = 1;
                if (Alley[animal - 1, 1] == 0)
                    destTop = 2;
                if (Alley[animal - 1, 2] == 0)
                    destTop = 3;
                newStep.Alley[animal - 1, destTop] = animal;

                newStep.Energy += Cost(animal, 2 + Math.Abs(rowpos - destRowPos) + top + destTop);
                newStep.CalcScore();

                result.Add(newStep);
                return result;
            }
        }

        // move left
        for (int i = rowpos - 1; i >= 0; i--)
        {
            if (TopRow[i] != 0)
                break;

            if ((i == 2) || (i == 4) || (i == 6) || (i == 8))
                continue;

            var newStep = Clone();
            newStep.Alley[nr, top] = 0;
            newStep.TopRow[i] = Alley[nr, top];
            newStep.Energy += Cost(Alley[nr, top], 1 + (rowpos - i) + top);
            newStep.CalcScore();

            result.Add(newStep);
        }
        // move right
        for (int i = rowpos + 1; i <= 10; i++)
        {
            if (TopRow[i] != 0)
                break;

            if ((i == 2) || (i == 4) || (i == 6) || (i == 8))
                continue;

            var newStep = Clone();
            newStep.Alley[nr, top] = 0;
            newStep.TopRow[i] = Alley[nr, top];
            newStep.Energy += Cost(Alley[nr, top], 1 + (i - rowpos) + top);
            newStep.CalcScore();

            result.Add(newStep);
        }

        return result;
    }

    private void CalcScore()
    {
        long score = 0;
        if (Alley[0, 3] == 1)
        {
            score += 1;
            if (Alley[0, 2] == 1)
            {
                score += 1;
                if (Alley[0, 1] == 1)
                {
                    score += 1;
                    if (Alley[0, 0] == 1)
                    {
                        score += 1;
                    }
                }
            }
        }

        if (Alley[1, 3] == 2)
        {
            score += 10;
            if (Alley[1, 2] == 2)
            {
                score += 10;
                if (Alley[1, 1] == 2)
                {
                    score += 10;
                    if (Alley[1, 0] == 2)
                    {
                        score += 10;
                    }
                }
            }
        }

        if (Alley[2, 3] == 3)
        {
            score += 100;
            if (Alley[2, 2] == 3)
            {
                score += 100;
                if (Alley[2, 1] == 3)
                {
                    score += 100;
                    if (Alley[2, 0] == 3)
                    {
                        score += 100;
                    }
                }
            }
        }

        if (Alley[3, 3] == 4)
        {
            score += 1000;
            if (Alley[3, 2] == 4)
            {
                score += 1000;
                if (Alley[3, 1] == 4)
                {
                    score += 1000;
                    if (Alley[3, 0] == 4)
                    {
                        score += 1000;
                    }
                }
            }
        }

        //long score = 0;
        //if (Alley[0, 0] == 1)
        //    score += 9;
        //if (Alley[0, 0] == 0)
        //{
        //    score += 1;
        //    if (Alley[0, 1] == 1)
        //        score += 7;

        //    if (Alley[0, 1] == 0)
        //    {
        //        score += 1;
        //        if (Alley[0, 2] == 1)
        //            score += 5;

        //        if (Alley[0, 2] == 0)
        //        {
        //            score += 1;
        //            if (Alley[0, 3] == 1)
        //                score += 3;

        //            if (Alley[0, 3] == 0)
        //                score += 1;
        //        }
        //    }
        //}

        //if (Alley[1, 0] == 2)
        //    score += 90;
        //if (Alley[1, 0] == 0)
        //{
        //    score += 10;
        //    if (Alley[1, 1] == 2)
        //        score += 70;

        //    if (Alley[1, 1] == 0)
        //    {
        //        score += 10;
        //        if (Alley[1, 2] == 2)
        //            score += 50;

        //        if (Alley[1, 2] == 0)
        //        {
        //            score += 10;
        //            if (Alley[1, 3] == 2)
        //                score += 30;

        //            if (Alley[1, 3] == 0)
        //                score += 10;
        //        }
        //    }
        //}

        //if (Alley[2, 0] == 3)
        //    score += 900;
        //if (Alley[2, 0] == 0)
        //{
        //    score += 100;
        //    if (Alley[2, 1] == 3)
        //        score += 700;

        //    if (Alley[2, 1] == 0)
        //    {
        //        score += 100;
        //        if (Alley[2, 2] == 3)
        //            score += 500;

        //        if (Alley[2, 2] == 0)
        //        {
        //            score += 100;
        //            if (Alley[2, 3] == 3)
        //                score += 300;

        //            if (Alley[2, 3] == 0)
        //                score += 100;
        //        }
        //    }
        //}

        //if (Alley[3, 0] == 3)
        //    score += 9000;
        //if (Alley[3, 0] == 0)
        //{
        //    score += 1000;
        //    if (Alley[3, 1] == 3)
        //        score += 7000;

        //    if (Alley[3, 1] == 0)
        //    {
        //        score += 1000;
        //        if (Alley[3, 2] == 3)
        //            score += 5000;

        //        if (Alley[3, 2] == 0)
        //        {
        //            score += 1000;
        //            if (Alley[3, 3] == 3)
        //                score += 3000;

        //            if (Alley[3, 3] == 0)
        //                score += 1000;
        //        }
        //    }
        //}

        Score = ((score + 1) * 100000) - Energy;
    }

    private int Cost(int item, int path)
    {
        if (item == 1)
            return path;
        if (item == 2)
            return 10 * path;
        if (item == 3)
            return 100 * path;
        if (item == 4)
            return 1000 * path;
        return 0;
    }

    public void Show()
    {
        Console.WriteLine();
        Console.WriteLine(new String(TopRow.Select(x => x == 0 ? '.' : (char)('A' + x - 1)).ToArray()));
        Console.WriteLine($"  {(char)(Alley[0, 0] == 0 ? '.' : Alley[0, 0] + 'A' - 1)} {(char)(Alley[1, 0] == 0 ? '.' : Alley[1, 0] + 'A' - 1)} {(char)(Alley[2, 0] == 0 ? '.' : Alley[2, 0] + 'A' - 1)} {(char)(Alley[3, 0] == 0 ? '.' : Alley[3, 0] + 'A' - 1)}");
        Console.WriteLine($"  {(char)(Alley[0, 1] == 0 ? '.' : Alley[0, 1] + 'A' - 1)} {(char)(Alley[1, 1] == 0 ? '.' : Alley[1, 1] + 'A' - 1)} {(char)(Alley[2, 1] == 0 ? '.' : Alley[2, 1] + 'A' - 1)} {(char)(Alley[3, 1] == 0 ? '.' : Alley[3, 1] + 'A' - 1)}");
        Console.WriteLine($"  {(char)(Alley[0, 2] == 0 ? '.' : Alley[0, 2] + 'A' - 1)} {(char)(Alley[1, 2] == 0 ? '.' : Alley[1, 2] + 'A' - 1)} {(char)(Alley[2, 2] == 0 ? '.' : Alley[2, 2] + 'A' - 1)} {(char)(Alley[3, 2] == 0 ? '.' : Alley[3, 2] + 'A' - 1)}");
        Console.WriteLine($"  {(char)(Alley[0, 3] == 0 ? '.' : Alley[0, 3] + 'A' - 1)} {(char)(Alley[1, 3] == 0 ? '.' : Alley[1, 3] + 'A' - 1)} {(char)(Alley[2, 3] == 0 ? '.' : Alley[2, 3] + 'A' - 1)} {(char)(Alley[3, 3] == 0 ? '.' : Alley[3, 3] + 'A' - 1)}");
        //        Console.WriteLine(Energy);
    }

    private Step Clone()
    {
        return new Step
        {
            TopRow = TopRow.ToArray(),
            Alley = new int[4, 4] {
                { Alley[0, 0], Alley[0, 1], Alley[0, 2], Alley[0, 3] },
                { Alley[1, 0], Alley[1, 1], Alley[1, 2], Alley[1, 3] },
                { Alley[2, 0], Alley[2, 1], Alley[2, 2], Alley[2, 3] },
                { Alley[3, 0], Alley[3, 1], Alley[3, 2], Alley[3, 3] }
            },
            Energy = Energy,
        };
    }
}
