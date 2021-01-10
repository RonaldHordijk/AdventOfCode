using System;
using System.Collections.Generic;
using System.Linq;

namespace Day07
{
    public class Machine
    {
        long pos = 0;
        bool ended = false;
        long[] values;
        List<long> input = new List<long>();

        public Machine(long[] values)
        {
            this.values = new long[values.Length];
            values.CopyTo(this.values, 0);
        }

        public long Process()
        {
            long output = 0;
            while (values[pos] != 99)
            {
                var mode = values[pos] % 100;
                bool p1Immediate = (values[pos] / 100) % 2 == 1;
                bool p2Immediate = (values[pos] / 1000) % 2 == 1;
                bool p3Immediate = (values[pos] / 10000) % 2 == 1;

                switch (mode)
                {
                    case 1:
                        {
                            var p1 = p1Immediate ? values[pos + 1] : values[values[pos + 1]];
                            var p2 = p2Immediate ? values[pos + 2] : values[values[pos + 2]];
                            var p3 = values[pos + 3];
                            values[p3] = p1 + p2;
                            pos += 4;
                            break;
                        }
                    case 2:
                        {
                            var p1 = p1Immediate ? values[pos + 1] : values[values[pos + 1]];
                            var p2 = p2Immediate ? values[pos + 2] : values[values[pos + 2]];
                            var p3 = values[pos + 3];
                            values[p3] = p1 * p2;
                            pos += 4;
                            break;
                        }
                    case 3:
                        {
                            var p1 = values[pos + 1];
                            values[p1] = input[0];
                            input.RemoveAt(0);
                            pos += 2;
                            break;
                        }
                    case 4:
                        {
                            var p1 = p1Immediate ? values[pos + 1] : values[values[pos + 1]];
                            output = p1;
                            pos += 2;
                            return output;
                            break;
                        }
                    case 5:
                        {
                            var p1 = p1Immediate ? values[pos + 1] : values[values[pos + 1]];
                            var p2 = p2Immediate ? values[pos + 2] : values[values[pos + 2]];
                            pos = (p1 != 0) ? p2 : pos + 3;
                            break;
                        }
                    case 6:
                        {
                            var p1 = p1Immediate ? values[pos + 1] : values[values[pos + 1]];
                            var p2 = p2Immediate ? values[pos + 2] : values[values[pos + 2]];
                            pos = (p1 == 0) ? p2 : pos + 3;
                            break;
                        }
                    case 7:
                        {
                            var p1 = p1Immediate ? values[pos + 1] : values[values[pos + 1]];
                            var p2 = p2Immediate ? values[pos + 2] : values[values[pos + 2]];
                            var p3 = values[pos + 3];
                            values[p3] = (p1 < p2) ? 1 : 0;
                            pos += 4;
                            break;
                        }
                    case 8:
                        {
                            var p1 = p1Immediate ? values[pos + 1] : values[values[pos + 1]];
                            var p2 = p2Immediate ? values[pos + 2] : values[values[pos + 2]];
                            var p3 = values[pos + 3];
                            values[p3] = (p1 == p2) ? 1 : 0;
                            pos += 4;
                            break;
                        }
                }
            }

            ended = true;
            return output;
        }

        class Program
        {
            static void Main(string[] args)
            {
                var values =
                        "3,8,1001,8,10,8,105,1,0,0,21,38,55,80,97,118,199,280,361,442,99999,3,9,101,2,9,9,1002,9,5,9,1001,9,4,9,4,9,99,3,9,101,5,9,9,102,2,9,9,1001,9,5,9,4,9,99,3,9,1001,9,4,9,102,5,9,9,101,4,9,9,102,4,9,9,1001,9,4,9,4,9,99,3,9,1001,9,3,9,1002,9,2,9,101,3,9,9,4,9,99,3,9,101,5,9,9,1002,9,2,9,101,3,9,9,1002,9,5,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1002,9,2,9,4,9,99,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,99,3,9,102,2,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,99,3,9,1002,9,2,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,102,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,1001,9,1,9,4,9,3,9,101,2,9,9,4,9,3,9,102,2,9,9,4,9,3,9,101,2,9,9,4,9,3,9,1001,9,1,9,4,9,99,3,9,102,2,9,9,4,9,3,9,101,1,9,9,4,9,3,9,1002,9,2,9,4,9,3,9,101,1,9,9,4,9,3,9,1001,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1002,9,2,9,4,9,3,9,1001,9,2,9,4,9,3,9,1001,9,1,9,4,9,3,9,102,2,9,9,4,9,99"
                        //"3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0"
                        //"3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0"
                        //"3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5"
                        //"3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10"
                        .Split(',')
                        .Select(s => Int64.Parse(s))
                        .ToArray();

                var permutations = DoPermute(new List<long> { 5, 6, 7, 8, 9 }, 0, 4, new List<List<long>>());

                //var r1 = ProcessWholeMachineLoop(values, new List<long> { 9, 8, 7, 6, 5 });
                //Console.WriteLine($"Result is {r1}");

                long max = 0;
                string maxsig = "";
                foreach (var per in permutations)
                {
                    var r = ProcessWholeMachineLoop(values, per);
                    if (r > max)
                    {
                        max = r;
                        maxsig = new string(per.Select(v => (char)(v + '0')).ToArray());
                    }

                }
                Console.WriteLine($"Maxsig is {max} for {maxsig}");

            }

            static List<List<long>> DoPermute(List<long> nums, int start, int end, List<List<long>> list)
            {
                if (start == end)
                {
                    // We have one of our possible n! solutions,
                    // add it to the list.
                    list.Add(new List<long>(nums));
                }
                else
                {
                    for (var i = start; i <= end; i++)
                    {
                        long temp = nums[start];
                        nums[start] = nums[i];
                        nums[i] = temp;

                        DoPermute(nums, start + 1, end, list);

                        // reset for next pass
                        temp = nums[start];
                        nums[start] = nums[i];
                        nums[i] = temp;

                    }
                }

                return list;
            }

            static public long ProcessWholeMachine(long[] values, List<long> order)
            {
                long result = 0;
                for (int i = 0; i < order.Count; i++)
                {
                    var input = new List<long> { order[i], result };
                    //result = Process(values, input);
                }

                return result;
            }

            static public long ProcessWholeMachineLoop(long[] values, List<long> order)
            {
                var machines = new List<Machine>
                {
                    new Machine(values),
                    new Machine(values),
                    new Machine(values),
                    new Machine(values),
                    new Machine(values),
                };

                for (int i = 0; i < order.Count; i++)
                {
                    machines[i].input.Add(order[i]);
                }

                long result = 0;
                while (!machines.Any(m => m.ended))
                {
                    for (int i = 0; i < order.Count; i++)
                    {
                        machines[i].input.Add(result);
                        long r = machines[i].Process();
                        if (r == 0)
                            break;
                        result = r;
                    }
                }

                return result;
            }
        }
    }
}

