# Enigma
Az "Enigma" egy olyan titkosító és dekódoló eszköz, amelyet a második világháborúban a német hadsereg és titkosszolgálatok használtak a kommunikációjuk kódolására.
## Enigma működése
https://www.youtube.com/watch?v=ybkkiGtJmkM

## Enigma program használata
```C#
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
```

![Enigma_(crittografia)_-_Museo_scienza_e_tecnologia_Milano](https://github.com/vellt/enigma/assets/61885011/4527b54e-397b-478e-a77a-4efa65b2479b)
