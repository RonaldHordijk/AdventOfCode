using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace Day21
{
    class Food
    {
        public List<string> Ingredients { get; set; } = new List<string>();
        public List<string> Allergens{ get; set; } = new List<string>();
    }

    class Program
    {

        static private List<string> AllAllergens(List<Food> foods)
        {
            var result = new List<string>();

            foreach(var food in foods)
            {
                foreach (var al in food.Allergens)
                {
                    if (!result.Contains(al))
                        result.Add(al);
                }
            }

            return result;
        }

        static private List<string> AllIngedients(List<Food> foods)
        {
            var result = new List<string>();

            foreach (var food in foods)
            {
                foreach (var ing in food.Ingredients)
                {
                    if (!result.Contains(ing))
                        result.Add(ing);
                }
            }

            return result;
        }


        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("data.txt");

            var foods = new List<Food>();
            var cdi = new Dictionary<string, string>();

            foreach(var line in lines)
            {
                var p = line.Split("(contains ");

                foods.Add(new Food
                {
                    Ingredients = p[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList(),
                    Allergens = p[1].Split(" ").Select(s=>s[..^1]).ToList()
                });
            }

            while (AllAllergens(foods).Count > 0)
            {
                foreach (var al in AllAllergens(foods))
                {
                    bool first = true;
                    var filterIngredients = new List<string>();
                    foreach (var food in foods.Where(f => f.Allergens.Contains(al)))
                    {
                        if (first)
                        {
                            filterIngredients.AddRange(food.Ingredients);
                            first = false;
                            continue;
                        }

                        filterIngredients = filterIngredients.Where(i => food.Ingredients.Contains(i)).ToList();
                    }
                    if (filterIngredients.Count == 1)
                    {
                        cdi.Add(al, filterIngredients[0]);
                        Remove(foods, filterIngredients[0], al);
                    }
                }
            }

            Console.WriteLine($"Remaining ingredients are {foods.Sum(f=>f.Ingredients.Count)}");
            var sortedKeys = cdi.Keys.ToList();
            sortedKeys.Sort();
            Console.WriteLine($"The Cdi is {string.Join(',', sortedKeys.Select(k => cdi[k]))}");
        }

        private static void Remove(List<Food> foods, string ing, string al)
        {
            foreach (var food in foods)
            {
                food.Ingredients.Remove(ing);
                food.Allergens.Remove(al);
            }
        }
    }
}
