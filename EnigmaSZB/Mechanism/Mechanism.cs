using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaSZB.Mechanism
{
    class Plugboard
    {
        /// <summary>
        /// Összecsatolt betűt listája. 1-6 kábelkapcsolatot (betű párt) lehet kialakítani.
        /// </summary>
        public Plugboard() { }
        public PlugBoardCable Cable1 { get; set; }
        public PlugBoardCable Cable2 { get; set; }
        public PlugBoardCable Cable3 { get; set; }
        public PlugBoardCable Cable4 { get; set; }
        public PlugBoardCable Cable5 { get; set; }
        public PlugBoardCable Cable6 { get; set; }

        internal char CheckConnection(char letter)
        {
            char newLetter = letter;
            if (Cable1 != null)
            {
                newLetter = Cable1.A.Value == newLetter ? Cable1.B.Value : Cable1.B.Value == newLetter ? Cable1.A.Value : newLetter;
            }
            if (Cable2 != null)
            {
                newLetter = Cable2.A.Value == newLetter ? Cable2.B.Value : Cable2.B.Value == newLetter ? Cable2.A.Value : newLetter;
            }
            if (Cable3 != null)
            {
                newLetter = Cable3.A.Value == newLetter ? Cable3.B.Value : Cable3.B.Value == newLetter ? Cable3.A.Value : newLetter;
            }
            if (Cable4 != null)
            {
                newLetter = Cable4.A.Value == newLetter ? Cable4.B.Value : Cable4.B.Value == newLetter ? Cable4.A.Value : newLetter;
            }
            if (Cable5 != null)
            {
                newLetter = Cable5.A.Value == newLetter ? Cable5.B.Value : Cable5.B.Value == newLetter ? Cable5.A.Value : newLetter;
            }
            if (Cable6 != null)
            {
                newLetter = Cable6.A.Value == newLetter ? Cable6.B.Value : Cable6.B.Value == newLetter ? Cable6.A.Value : newLetter;
            }
            ;
            return newLetter;
        }
    }

    class PlugBoardCable
    {
        public Letter A { get; set; }
        public Letter B { get; set; }


    }
    class Letter
    {
        public char Value { get; set; }
        public PlugBoardCable Is(Letter letter)
        {
            return new PlugBoardCable { A = this, B = letter };
        }
    }

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
        Rotor1, Rotor2, Rotor3, Reflector
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
        private Reflector TempReflector;
        /// <summary>
        /// Rotorok sorrendje felcserélhető nem kötelező a megnezésnek megfelelő rotort (tárcsát) belehelyezni
        /// </summary>
        public Rotor Rotor1 { get; set; }
        /// <summary>
        /// Rotorok sorrendje felcserélhető nem kötelező a megnezésnek megfelelő rotort (tárcsát) belehelyezni
        /// </summary>
        public Rotor Rotor2 { get; set; }
        /// <summary>
        /// Rotorok sorrendje felcserélhető nem kötelező a megnezésnek megfelelő rotort (tárcsát) belehelyezni
        /// </summary>
        public Rotor Rotor3 { get; set; }
        private Rotor TempRotor1;
        private Rotor TempRotor2;
        private Rotor TempRotor3;
        /// <summary>
        /// 3 db A-Z-ig terjedő karaktert lehet megadni (pl KET). Ez lesz a tárcsák kezdőértéke
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Kapcsolótábla ahol kétvezetéskes csatlakozókábelekkel kötünk össze két betűt. Önmagára nem lehet két betűt összekapcsolni, továbbá amire már van hivatkozás arra vagy annak NEM lehet új hivatkozás beállítani.
        /// </summary>
        public Plugboard Plugboard = new Plugboard();

        /// <summary>
        /// Dekódolási/Tikosítási mechanizmus elnidítása. CSAK angolszász nagybetűs ABC- kerülnek titkosításra vagy visszafejtésre. A többi karakter (szám, kisbetű, speciális karakterek [pont, szóköz, stb..]) változatléanul marad.
        /// </summary>
        /// <param name="text">szöveges adat melyet titkosítani vagy dekódolni szeretne</param>
        /// <returns>Az Enigma beállításainak megfelelően kiértékelt szöveges üzenet</returns>
        public string TransformText(string text)
        {
            TempRotor1 = new Rotor { Spin = Rotor1.Spin };
            TempRotor2 = new Rotor { Spin = Rotor2.Spin };
            TempRotor3 = new Rotor { Spin = Rotor3.Spin };
            TempReflector = new Reflector();
            Syncronzie();
            string newText = string.Empty;
            foreach (var letter in text)
            {
                if (letter >= 'A' && letter <= 'Z')
                {
                    char checkedLetter = Plugboard.CheckConnection(letter);
                    char newCheckedLetter = Plugboard.CheckConnection((char)(Transform(checkedLetter - 65) + 65));
                    ;
                    newText += newCheckedLetter;
                }
                else
                {
                    newText += ' ';
                }
            }
            return newText;
        }

        private void TurnTo(int a, int b, TurnRotor turnRotor)
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
                            Right = (x.Right + (a + b)) % 26
                        });
                    });
                    TempReflector.Combinations = new List<Combination>(c);
                    break;
                case TurnRotor.Rotor1:

                    Rotor1.Combinations.ForEach(x =>
                    {
                        c.Add(new Combination
                        {
                            Left = x.Left,
                            Right = (x.Right - (a + b) + 26) % 26
                        });
                    });
                    TempRotor1.Combinations = new List<Combination>(c);
                    TempRotor1.CurrentSpin = (a + 26) % 26;
                    break;
                case TurnRotor.Rotor2:
                    Rotor2.Combinations.ForEach(x =>
                    {
                        c.Add(new Combination
                        {
                            Left = x.Left,
                            Right = (x.Right - (a + b) + 26) % 26
                        });
                    });
                    TempRotor2.Combinations = new List<Combination>(c);
                    TempRotor2.CurrentSpin = (a + 26) % 26;
                    break;
                case TurnRotor.Rotor3:
                    Rotor3.Combinations.ForEach(x =>
                    {
                        c.Add(new Combination
                        {
                            Left = x.Left,
                            Right = (x.Right - (a + b) + 26) % 26

                        });
                    });
                    TempRotor3.Combinations = new List<Combination>(c);
                    TempRotor3.CurrentSpin = (a + 26) % 26;
                    break;
            }
        }

        private void Turn(TurnRotor turnRotor)
        {
            var key = Key.ToArray();
            switch (turnRotor)
            {
                case TurnRotor.Rotor1: key[0] = (char)((((Key[0] - 65) + 1) % 26) + 65); break;
                case TurnRotor.Rotor2: key[1] = (char)((((Key[1] - 65) + 1) % 26) + 65); break;
                case TurnRotor.Rotor3: key[2] = (char)((((Key[2] - 65) + 1) % 26) + 65); break;
            }
            Key = string.Join("", key);
            Syncronzie();
        }

        private int Transform(int number)
        {
            if (TempRotor3.ReadyToSpin())
            {
                Turn(TurnRotor.Rotor3);
                if (TempRotor2.ReadyToSpin())
                {
                    Turn(TurnRotor.Rotor2);
                    Turn(TurnRotor.Rotor1);
                }
                else
                {
                    Turn(TurnRotor.Rotor2);
                }
            }
            else
            {
                // minden gomb leütéskor gördül egyet
                Turn(TurnRotor.Rotor3);
            }
            int index = TempRotor1.TranformRight(TempRotor2.TranformRight(TempRotor3.TranformRight(number)));
            index = TempReflector.Tranform(index);
            index = TempRotor3.TranformLeft(TempRotor2.TranformLeft(TempRotor1.TranformLeft(index)));
            return index;
        }

        private void Syncronzie()
        {
            int index1 = Key[0] - 65;
            int index2 = (Key[1] - 65);
            int index3 = (Key[2] - 65);
            TurnTo(index1, index2 * -1, TurnRotor.Rotor1);
            TurnTo(index1, 0, TurnRotor.Reflector); // szinkronba kell lennie a rotor 1-el
            TurnTo(index2, index3 * -1, TurnRotor.Rotor2);
            TurnTo(index3, 0, TurnRotor.Rotor3);
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
}
