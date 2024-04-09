using EnigmaSZB.Mechanism;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaSZB.Boilerplate
{
    partial class Program
    {
        public static Reflector reflector = new Reflector
        {
            Combinations = new List<Combination>
            {
                new Combination { Left=0, Right=24 },
                new Combination { Left=1, Right=17 },
                new Combination { Left=2, Right=20 },
                new Combination { Left=3, Right=7 },
                new Combination { Left=4, Right=16 },
                new Combination { Left=5, Right=18 },
                new Combination { Left=6, Right=11 },
                new Combination { Left=8, Right=15 },
                new Combination { Left=9, Right=23 },
                new Combination { Left=10, Right=13 },
                new Combination { Left=12, Right=14 },
                new Combination { Left=19, Right=25 },
                new Combination { Left=21, Right=22 },
            }
        };

        public static Rotor rotor1 = new Rotor
        {
            Spin = 16,
            Combinations = new List<Combination>
            {
                new Combination { Left=0, Right=20 },
                new Combination { Left=1, Right=22 },
                new Combination { Left=2, Right=24 },
                new Combination { Left=3, Right=6 },
                new Combination { Left=4, Right=0},
                new Combination { Left=5, Right=3},
                new Combination { Left=6, Right=5},
                new Combination { Left=7, Right=15},
                new Combination { Left=8, Right=21},
                new Combination { Left=9, Right=25},
                new Combination { Left=10, Right=1},
                new Combination { Left=11, Right=4},
                new Combination { Left=12, Right=2},
                new Combination { Left=13, Right=10},
                new Combination { Left=14, Right=12},
                new Combination { Left=15, Right=19},
                new Combination { Left=16, Right=7},
                new Combination { Left=17, Right=23},
                new Combination { Left=18, Right=18},
                new Combination { Left=19, Right=11},
                new Combination { Left=20, Right=17},
                new Combination { Left=21, Right=8},
                new Combination { Left=22, Right=13},
                new Combination { Left=23, Right=16},
                new Combination { Left=24, Right=14},
                new Combination { Left=25, Right=9},
            }
        };

        public static Rotor rotor2 = new Rotor
        {
            Spin = 4,
            Combinations = new List<Combination>
            {
                new Combination { Left=0, Right=0},
                new Combination { Left=1, Right=9},
                new Combination { Left=2, Right=15},
                new Combination { Left=3, Right=2},
                new Combination { Left=4, Right=25},
                new Combination { Left=5, Right=22},
                new Combination { Left=6, Right=17},
                new Combination { Left=7, Right=11},
                new Combination { Left=8, Right=5},
                new Combination { Left=9, Right=1},
                new Combination { Left=10, Right=3},
                new Combination { Left=11, Right=10},
                new Combination { Left=12, Right=14},
                new Combination { Left=13, Right=19},
                new Combination { Left=14, Right=24},
                new Combination { Left=15, Right=20},
                new Combination { Left=16, Right=16},
                new Combination { Left=17, Right=6},
                new Combination { Left=18, Right=4},
                new Combination { Left=19, Right=13},
                new Combination { Left=20, Right=7},
                new Combination { Left=21, Right=23},
                new Combination { Left=22, Right=12},
                new Combination { Left=23, Right=8},
                new Combination { Left=24, Right=21},
                new Combination { Left=25, Right=18},
            }
        };

        public static Rotor rotor3 = new Rotor
        {
            Spin = 21,
            Combinations = new List<Combination>
            {
                new Combination { Left=0, Right=19 },
                new Combination { Left=1, Right=0 },
                new Combination { Left=2, Right=6 },
                new Combination { Left=3, Right=1 },
                new Combination { Left=4, Right=15 },
                new Combination { Left=5, Right=2 },
                new Combination { Left=6, Right=18 },
                new Combination { Left=7, Right=3 },
                new Combination { Left=8, Right=16 },
                new Combination { Left=9, Right=4 },
                new Combination { Left=10, Right=20 },
                new Combination { Left=11, Right=5 },
                new Combination { Left=12, Right=21 },
                new Combination { Left=13, Right=13 },
                new Combination { Left=14, Right=25 },
                new Combination { Left=15, Right=7 },
                new Combination { Left=16, Right=24 },
                new Combination { Left=17, Right=8 },
                new Combination { Left=18, Right=23 },
                new Combination { Left=19, Right=9 },
                new Combination { Left=20, Right=22 },
                new Combination { Left=21, Right=11 },
                new Combination { Left=22, Right=17 },
                new Combination { Left=23, Right=10 },
                new Combination { Left=24, Right=14 },
                new Combination { Left=25, Right=12 },
            }
        };

        public static Letter A = new Letter { Value = 'A' };
        public static Letter B = new Letter { Value = 'B' };
        public static Letter C = new Letter { Value = 'C' };
        public static Letter D = new Letter { Value = 'D' };
        public static Letter E = new Letter { Value = 'E' };
        public static Letter F = new Letter { Value = 'F' };
        public static Letter G = new Letter { Value = 'G' };
        public static Letter H = new Letter { Value = 'H' };
        public static Letter I = new Letter { Value = 'I' };
        public static Letter J = new Letter { Value = 'J' };
        public static Letter K = new Letter { Value = 'K' };
        public static Letter L = new Letter { Value = 'L' };
        public static Letter M = new Letter { Value = 'M' };
        public static Letter N = new Letter { Value = 'N' };
        public static Letter O = new Letter { Value = 'O' };
        public static Letter P = new Letter { Value = 'P' };
        public static Letter Q = new Letter { Value = 'Q' };
        public static Letter R = new Letter { Value = 'R' };
        public static Letter S = new Letter { Value = 'S' };
        public static Letter T = new Letter { Value = 'T' };
        public static Letter U = new Letter { Value = 'U' };
        public static Letter V = new Letter { Value = 'V' };
        public static Letter W = new Letter { Value = 'W' };
        public static Letter X = new Letter { Value = 'X' };
        public static Letter Y = new Letter { Value = 'Y' };
        public static Letter Z = new Letter { Value = 'Z' };
    }
}
