using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaSZB
{
    
    class Settings : Enigma 
    {
        /// <summary>
        /// Interfészt kínál az Enigma beállításaihoz. Beállíthatja a Rotorok kezőállapotát (Key), A rotorok sorrendjét (RotorI, RotorII, RotorIII)
        /// Ezeket felülírhatja egyedi Rotorokkal és Reflectorokkal is
        /// </summary>
        public Settings() { }
    }

    class Reflector
    {
        public List<Combination> Combinations { get; set; }

        public int Tranform(int index)
        {
            var left = Combinations.Where(x => x.Left == index);
            var right = Combinations.Where(x => x.Right == index);
            return left.Count() != 0 ? left.First().Right : right.First().Left;
        }
    }

    enum TurnRotor
    {
        RotorI, RotorII, RotorIII, Reflector
    }

    /// <summary>
    /// Az "Enigma" egy olyan titkosító és dekódoló eszköz, amelyet a második világháborúban a német hadsereg és titkosszolgálatok használtak a kommunikációjuk kódolására. 
    /// </summary>
    class Enigma
    {
        /// <summary>
        /// Reflektor egy-egy betű titkosított változata (elemei soha nem hivatkozhatnak önmagukra)
        /// </summary>
        public Reflector Reflector { get; set; }
        public Reflector TempReflector { get; set; }
        /// <summary>
        /// Rotorok sorrendje felcserélhető nem kötelező a megnezésnek megfelelő rotort (tárcsát) belehelyezni
        /// </summary>
        public Rotor RotorI { get; set; }
        /// <summary>
        /// Rotorok sorrendje felcserélhető nem kötelező a megnezésnek megfelelő rotort (tárcsát) belehelyezni
        /// </summary>
        public Rotor RotorII { get; set; }
        /// <summary>
        /// Rotorok sorrendje felcserélhető nem kötelező a megnezésnek megfelelő rotort (tárcsát) belehelyezni
        /// </summary>
        public Rotor RotorIII { get; set; }
        public Rotor TempRotorI { get; set; }
        public Rotor TempRotorII { get; set; }
        public Rotor TempRotorIII { get; set; }
        /// <summary>
        /// 3 db A-Z-ig terjedő karaktert lehet megadni (pl KET). Ez lesz a tárcsák kezdőértéke
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Dekódolási/Tikosítási mechanizmus elnidítása. CSAK angolszász nagybetűs ABC használata megengedett. Speciűlis karakterek, számok titkosítására visszafejtésésre nem alkalmas. Szóközt tartalmazhat a bemeneti szöveg, azt figyelmen kívül hagyja.
        /// </summary>
        /// <param name="text">szöveges adat melyet titkosítani vagy dekódolni szeretne</param>
        /// <returns>Az Enigma beállításainak megfelelően kiértékelt szöveges üzenet</returns>
        public string TransformText(string text)
        {
            TempRotorI = new Rotor { Spin = RotorI.Spin };
            TempRotorII = new Rotor { Spin = RotorII.Spin };
            TempRotorIII = new Rotor { Spin = RotorIII.Spin };
            TempReflector = new Reflector();
            Syncronzie();
            string newText = string.Empty;
            foreach (var letter in text)
            {
                if(letter!=' ')
                {
                    newText += (char)(Transform(letter - 65) + 65);
                }
                else
                {
                    newText += ' ';
                }
            }
            return newText;
        }

        public void TurnTo(int a, int b, TurnRotor turnRotor)
        {
            List<Combination> c = new List<Combination>();
            switch (turnRotor)
            {
                case TurnRotor.Reflector:
                    Reflector.Combinations.ForEach(x => 
                    {
                        c.Add(new Combination
                        {
                            Left = (x.Left + (a + b)) % 26,
                            Right = (x.Right + (a + b) ) % 26
                        });
                    });
                    TempReflector.Combinations = new List<Combination>(c);
                    break;
                case TurnRotor.RotorI:

                    RotorI.Combinations.ForEach(x =>
                    {
                        c.Add(new Combination
                        {
                            Left = x.Left,
                            Right = (x.Right - (a+b) + 26) % 26
                        });
                    });
                    TempRotorI.Combinations = new List<Combination>(c);
                    TempRotorI.CurrentSpin = (a+26) % 26;
                    break;
                case TurnRotor.RotorII:
                    RotorII.Combinations.ForEach(x =>
                    {
                        c.Add(new Combination
                        {
                            Left = x.Left,
                            Right = (x.Right - (a + b) + 26) % 26
                        });
                    });
                    TempRotorII.Combinations = new List<Combination>(c);
                    TempRotorII.CurrentSpin = (a + 26) % 26;
                    break;
                case TurnRotor.RotorIII:
                    RotorIII.Combinations.ForEach(x =>
                    {
                        c.Add(new Combination
                        {
                            Left = x.Left,
                            Right = (x.Right - (a + b) + 26) % 26

                        });
                    });
                    TempRotorIII.Combinations = new List<Combination>(c);
                    TempRotorIII.CurrentSpin = (a + 26) % 26;
                    break;
            }
        }

        public void Turn(TurnRotor turnRotor)
        {
            var key = Key.ToArray();
            switch (turnRotor)
            {
                case TurnRotor.RotorI: key[0] = (char)((((Key[0] - 65) + 1) % 26) + 65); break;
                case TurnRotor.RotorII: key[1] = (char)((((Key[1] - 65) + 1) % 26) + 65); break;
                case TurnRotor.RotorIII: key[2] = (char)((((Key[2] - 65) + 1) % 26) + 65); break;
            }
            Key = string.Join("", key);
            Syncronzie();
        }

        public int Transform(int number)
        {
            if (TempRotorIII.ReadyToSpin())
            {
                Turn(TurnRotor.RotorIII);
                if (TempRotorII.ReadyToSpin())
                {
                    Turn(TurnRotor.RotorII);
                    Turn(TurnRotor.RotorI);
                }
                else
                {
                    Turn(TurnRotor.RotorII);
                }
            }
            else
            {
                // minden gomb leütéskor gördül egyet
                Turn(TurnRotor.RotorIII);
            }
            int index = TempRotorI.TranformRight(TempRotorII.TranformRight(TempRotorIII.TranformRight(number)));
            index = TempReflector.Tranform(index);
            index = TempRotorIII.TranformLeft(TempRotorII.TranformLeft(TempRotorI.TranformLeft(index)));
            return index;
        }

        internal void Syncronzie()
        {
            int index1 = Key[0] - 65;
            int index2 = (Key[1] - 65);
            int index3 = (Key[2] - 65);
            TurnTo(index1, index2 * -1, TurnRotor.RotorI);
            TurnTo(index1,0, TurnRotor.Reflector); // szinkronba kell lennie a rotor 1-el
            TurnTo(index2, index3 * -1, TurnRotor.RotorII);
            TurnTo(index3,0, TurnRotor.RotorIII);
        }
    }

    class Combination
    {
        public int Left { get; set; }
        public int Right { get; set; }
    }

    class Rotor
    {
        public int Spin { get; set; }
        public int CurrentSpin { get; set; }
        public List<Combination> Combinations { get; set; }

        public int TranformRight(int index)
        {
            return Combinations.Where(x => x.Right == index).First().Left;
        }

        public int TranformLeft(int index)
        {
            return Combinations.Where(x => x.Left == index).First().Right;
        }

        public bool ReadyToSpin()
        {
            return CurrentSpin % 26 == Spin;
        }
    }

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

        public static Rotor rotorI = new Rotor
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

        public static Rotor rotorII = new Rotor
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

        public static Rotor rotorIII = new Rotor
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
    }
    partial class Program
    {
        static void Main(string[] args)
        {
            Enigma enigma = new Settings
            {
                Key = "KET",
                Reflector = reflector,
                RotorI = rotorI,
                RotorII = rotorII,
                RotorIII = rotorIII
            };
            string message = enigma.TransformText("HUBNEQ");
            Console.WriteLine(message); 

            Console.ReadKey();
        }
    }
}
