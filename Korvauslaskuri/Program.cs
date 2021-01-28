using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korvauslaskuri
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Tyontekija> henkilot = new List<Tyontekija>();

            // Päävalikko

            while (true)
            {
                Console.WriteLine("\nValitse toiminto:");
                Console.WriteLine("1 - Luo työntekijä");
                Console.WriteLine("2 - Kirjaa työmatkoja ja tarkastele työntekijöiden matkatietoja");
                Console.WriteLine("3 - Kertyneiden korvauksien tarkastelu");
                Console.WriteLine("4 - Poistu ohjelmasta");
                string valinta = Console.ReadLine();
                Console.WriteLine("\n");

                if (valinta == "1")
                {
                    Console.WriteLine("Anna työntekijän nimi");
                    string nimi = Console.ReadLine();
                    if (!String.IsNullOrEmpty(nimi))
                    {
                        Tyontekija uusi = new Tyontekija(nimi);
                        henkilot.Add(uusi);
                        Console.WriteLine("Työntekijä tallennettu\n");
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Et antanut nimeä.");
                        continue;
                    }
                }
                else if (valinta == "2")
                {
                    if (henkilot.Count == 0)
                    {
                        Console.WriteLine("Ei tallennettuja työntekijöitä");
                        continue;
                    }
                    else
                    {
                    //    TyontekijoidenKasittely();
                    }
                }
                else if (valinta == "3")
                {
                //    KorvaustenTarkastelu();
                }
                else if (valinta == "4")
                {
                    break;
                }
            }
        }
    }
}
