using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day09
{
    public class Machine
    {
        long pos = 0;
        long RelativeBase = 0;
        public bool ended = false;
        long[] values;
        public List<long> input = new List<long>();
        Dictionary<long, long> ExtMemory = new Dictionary<long, long>();

        public Machine(long[] values)
        {
            this.values = new long[values.Length];
            values.CopyTo(this.values, 0);
        }

        private long GetMemValue(long pos)
        {
            if (pos < values.Length)
                return values[pos];

            if (!ExtMemory.ContainsKey(pos))
                ExtMemory.Add(pos, 0);

            return ExtMemory[pos];
        }

        private void SetMemValue(long pos, long value)
        {
            if (pos < values.Length)
            {
                values[pos] = value;
                return;
            }

            if (!ExtMemory.ContainsKey(pos))
                ExtMemory.Add(pos, 0);

            ExtMemory[pos] = value;
        }

        private long GetValue(bool immediate, bool relative, long pos)
        {
            if (relative)
                return GetMemValue(RelativeBase + GetMemValue(pos));

            if (immediate)
                return GetMemValue(pos);

            return GetMemValue(GetMemValue(pos));
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
                bool p1Relative = (values[pos] / 100) % 10 == 2;
                bool p2Relative = (values[pos] / 1000) % 10 == 2;
                bool p3Relative = (values[pos] / 10000) % 10 == 2;

                switch (mode)
                {
                    case 1:
                        {
                            var p1 = GetValue(p1Immediate, p1Relative, pos + 1);
                            var p2 = GetValue(p2Immediate, p2Relative, pos + 2);
                            var p3 = GetMemValue(pos + 3);
                            SetMemValue(p3, p1 + p2);
                            pos += 4;
                            break;
                        }
                    case 2:
                        {
                            var p1 = GetValue(p1Immediate, p1Relative, pos + 1);
                            var p2 = GetValue(p2Immediate, p2Relative, pos + 2);
                            var p3 = GetMemValue(pos + 3);
                            SetMemValue(p3, p1 * p2);
                            pos += 4;
                            break;
                        }
                    case 3:
                        {
                            var p1 = GetMemValue(pos + 1);
                            SetMemValue(p1, input[0]);
                            input.RemoveAt(0);
                            pos += 2;
                            break;
                        }
                    case 4:
                        {
                            var p1 = GetValue(p1Immediate, p1Relative, pos + 1);
                            output = p1;
                            pos += 2;
                            return output;
                            break;
                        }
                    case 5:
                        {
                            var p1 = GetValue(p1Immediate, p1Relative, pos + 1);
                            var p2 = GetValue(p2Immediate, p2Relative, pos + 2);
                            pos = (p1 != 0) ? p2 : pos + 3;
                            break;
                        }
                    case 6:
                        {
                            var p1 = GetValue(p1Immediate, p1Relative, pos + 1);
                            var p2 = GetValue(p2Immediate, p2Relative, pos + 2);
                            pos = (p1 == 0) ? p2 : pos + 3;
                            break;
                        }
                    case 7:
                        {
                            var p1 = GetValue(p1Immediate, p1Relative, pos + 1);
                            var p2 = GetValue(p2Immediate, p2Relative, pos + 2);
                            var p3 = GetMemValue(pos + 3);
                            SetMemValue(p3, (p1 < p2) ? 1 : 0);
                            pos += 4;
                            break;
                        }
                    case 8:
                        {
                            var p1 = GetValue(p1Immediate, p1Relative, pos + 1);
                            var p2 = GetValue(p2Immediate, p2Relative, pos + 2);
                            var p3 = GetMemValue(pos + 3);
                            SetMemValue(p3, (p1 == p2) ? 1 : 0);
                            pos += 4;
                            break;
                        }
                    case 9:
                        {
                            var p1 = GetValue(p1Immediate, p1Relative, pos + 1);
                            RelativeBase += p1;
                            pos += 2;
                            break;
                        }
                }
            }

            ended = true;
            return output;
        }
    }
}
