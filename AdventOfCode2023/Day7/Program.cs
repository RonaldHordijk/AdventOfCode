using Day7;

var lines = File.ReadAllLines("data.txt");

List<Card> values = [];

foreach (var line in lines)
{
    var s = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
    values.Add(new Card
    {
        Cards = s[0],
        Original = s[0],
        Bid = int.Parse(s[1]),
    });
}

CalcOrder(values);

foreach (var card in values)
{
    if (!card.Cards.Contains("J"))
        continue;

    List<Card> alternatives = [];

    List<char> options = ['2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A'];
    foreach (var c in options)
    {
        alternatives.Add(new()
        {
            Cards = card.Cards.Replace('J', c),
            Original = card.Cards,
        });
    }

    CalcOrder(alternatives);
    SetRank(alternatives);
    //foreach (var x in alternatives)
    //{
    //    Console.WriteLine($"{x.Cards} {x.Rank}");
    //}

    var best = alternatives.OrderBy(a => a.Rank).Last();
    card.Cards = best.Cards;
}



SetRank(values);




// show
foreach (var card in values)
{
    Console.WriteLine($"{card.Cards} {card.Rank}");
}


long sum = 0;
foreach (var card in values)
{
    sum += card.Rank * card.Bid;
}

Console.WriteLine(sum);

void Sort(List<Card> list, int offset)
{
    list.Sort((l, r) => Math.Sign((l.Order - r.Order)));
    for (int i = 0; i < list.Count; i++)
    {
        list[i].Rank = offset + list.Count - 1 - i;
    }
}

void SetRank(List<Card> values)
{
    int offset = 1;

    // five of a kind
    var fives = values.Where(v => v.Cards.Distinct().Count() == 1).ToList();
    Sort(fives, offset);

    offset += fives.Count;

    //Four of a kind
    var fours = values.Where(
        v =>
        {
            var dist = v.Cards.Distinct().ToList();
            if (dist.Count != 2)
                return false;

            var countone = v.Cards.Count(c => c == dist[0]);
            return countone == 1 || countone == 4;
        }).ToList();
    Sort(fours, offset);

    offset += fours.Count;

    // Full house
    var fullhouse = values.Where(
        v =>
        {
            var dist = v.Cards.Distinct().ToList();
            if (dist.Count != 2)
                return false;

            var countone = v.Cards.Count(c => c == dist[0]);
            return countone == 2 || countone == 3;
        }).ToList();
    Sort(fullhouse, offset);

    offset += fullhouse.Count;

    //Three of a kind
    var threeofakind = values.Where(
        v =>
        {
            var dist = v.Cards.Distinct().ToList();
            if (dist.Count != 3)
                return false;

            return v.Cards.Count(c => c == dist[0]) == 3
                || v.Cards.Count(c => c == dist[1]) == 3
                || v.Cards.Count(c => c == dist[2]) == 3;
        }).ToList();
    Sort(threeofakind, offset);

    offset += threeofakind.Count;

    //Two pair
    var twopair = values.Where(
        v =>
        {
            var dist = v.Cards.Distinct().ToList();
            if (dist.Count != 3)
                return false;

            return v.Cards.Count(c => c == dist[0]) < 3
                && v.Cards.Count(c => c == dist[1]) < 3
                && v.Cards.Count(c => c == dist[2]) < 3;
        }).ToList();
    Sort(twopair, offset);

    offset += twopair.Count;


    //One pair
    var onepair = values.Where(
        v => v.Cards.Distinct().ToList().Count == 4).ToList();
    Sort(onepair, offset);

    offset += onepair.Count;

    //High card
    var highcard = values.Where(v => v.Rank == 0).ToList();
    Sort(highcard, offset);

    offset += highcard.Count;

    // reverse rank
    foreach (var card in values)
    {
        card.Rank = values.Count - card.Rank + 1;
    }
}

int CardValue(char v)
{
    if (v == 'A')
        return 14;
    if (v == 'K')
        return 13;
    if (v == 'Q')
        return 12;
    if (v == 'J')
        return 1;
    if (v == 'T')
        return 10;

    return (v - '0');
}

void CalcOrder(List<Card> values)
{
    foreach (var card in values)
    {
        card.Order =
            100000000 * CardValue(card.Original[0])
            + 1000000 * CardValue(card.Original[1])
            + 10000 * CardValue(card.Original[2])
            + 100 * CardValue(card.Original[3])
            + CardValue(card.Original[4]);
    }
}
