using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SZGYA13C_Vizsga
{
    internal class Vizsgazo
    {
        public string Nev { get; set; }
        public float ITesHalozati { get; set; }
        public float Programozas { get; set; }
        public float HalozatAModul { get; set; }
        public float HalozatBModul { get; set; }
        public float HalozatCModul { get; set; }
        public float HalozatDModul { get; set; }
        public float ITSzobeli { get; set; }
        public float AngolSzobeli { get; set; }
        public IEnumerable<float> ModulEredmenyek => new[]
        {
        ITesHalozati, Programozas, HalozatAModul, HalozatBModul, 
        HalozatCModul, HalozatDModul, ITSzobeli, AngolSzobeli
        };  

        public float Vegeredmeny => ModulEredmenyek.Average();

        public Vizsgazo(string nev, float ith, float programozas, float halozata, float halozatb, float halozatc, float halozatd, float itsz, float angolsz)
        {
            Nev = nev;
            ITesHalozati = ith;
            Programozas = programozas;
            HalozatAModul = halozata;
            HalozatBModul = halozatb;
            HalozatCModul = halozatc;
            HalozatDModul = halozatd;
            ITSzobeli = itsz;
            AngolSzobeli = angolsz;
        }

        public string Erdemjegy()
        {
            if(ModulEredmenyek.Any(e => e < 0.51f))
            {
                return "Elégtelen";
            }
            else
            {
                return Vegeredmeny switch
                {
                    >= 0.81f => "Jeles",
                    >= 0.71f => "Jó",
                    >= 0.61f => "Közepes",
                    >= 0.51f => "Elégséges",
                };
            }
        }

        public static List<Vizsgazo> Fromfile(string p)
        {
            List<Vizsgazo> vizsgazo = new List<Vizsgazo>();

            string[] lines = File.ReadAllLines(p);

            foreach (var l in lines)
            {
                string[] v = l.Split(';');

                string Nev = v[0];
                float ITesHalozati = float.Parse(v[1]);
                float Programozas = float.Parse(v[2]);
                float HalozatAModul = float.Parse(v[3]);
                float HalozatBModul = float.Parse(v[4]);
                float HalozatCModul = float.Parse(v[5]);
                float HalozatDModul = float.Parse(v[6]);
                float ITSzobeli = float.Parse(v[7]);
                float AngolSzobeli = float.Parse(v[8]);

                Vizsgazo vizsgazok = new(Nev, ITesHalozati, Programozas, HalozatAModul, HalozatBModul, HalozatCModul, HalozatDModul, ITSzobeli, AngolSzobeli);
                vizsgazo.Add(vizsgazok);
            }

            return vizsgazo;
        }

        public override string ToString()
        {
            return $"{Nev}, {ITesHalozati}, {Programozas}, {HalozatAModul}, {HalozatBModul}, {HalozatCModul}, {HalozatDModul}, {ITSzobeli}, {AngolSzobeli}";
        }

    }
}
