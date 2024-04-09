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
        static void Main(string[] args)
        {
            // ENIGMA beállításai
            // Key (kulcs - tárcsák állása): K-E-T
            // Rotorok (tárcsák) sorrendje: 1-2-3
            // Plugboard (kapcsolótábla): nincs beállítva
            Enigma setup1 = new Settings
            {
                Key = "KET",
                Reflector = reflector,
                Rotor1 = rotor1,
                Rotor2 = rotor2,
                Rotor3 = rotor3,
            };
            Console.WriteLine(setup1.TransformText("HUBNEQ")); // kapott üzenet ENIGMA 

            // ENIGMA beállításai
            // Key (kulcs - tárcsák állása): K-E-T
            // Rotorok (tárcsák) sorrendje: 1-2-3
            // Plugboard (kapcsolótábla): H-B, C-K, O-P, T-Q, E-N, J-R
            Enigma setup2 = new Settings
            {
                Key = "KET",
                Reflector = reflector,
                Rotor1 = rotor1,
                Rotor2 = rotor2,
                Rotor3 = rotor3,
                Plugboard = new Plugboard
                {
                    Cable1 = H.Is(B),
                    Cable2 = C.Is(K),
                    Cable3 = O.Is(P),
                    Cable4 = T.Is(Q),
                    Cable5 = E.Is(N),
                    Cable6 = J.Is(R)
                }
            };
            Console.WriteLine(setup2.TransformText("XCHENT"));  // ENIGMA

            // ENIGMA beállításai
            // Key (kulcs - tárcsák állása): K-E-T
            // Rotorok (tárcsák) sorrendje: 3-2-1
            // Plugboard (kapcsolótábla): H-B, C-K, O-P, T-Q, E-N, J-R
            Enigma setup3 = new Settings
            {
                Key = "KET",
                Reflector = reflector,
                Rotor1 = rotor3,
                Rotor2 = rotor2,
                Rotor3 = rotor1,
                Plugboard = new Plugboard
                {
                    Cable1 = H.Is(B),
                    Cable2 = C.Is(K),
                    Cable3 = O.Is(P),
                    Cable4 = T.Is(Q),
                    Cable5 = E.Is(N),
                    Cable6 = J.Is(R)
                }
            };
            Console.WriteLine(setup3.TransformText("OQBSJT"));  // ENIGMA
            Console.ReadKey();
        }
    }
}
