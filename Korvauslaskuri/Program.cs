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
            Funktiot funktio = new Funktiot();
            
            // Päävalikko

            while (true)
            {
                Console.WriteLine("\nValitse toiminto:");
                Console.WriteLine("1 - Luo tai poista työntekijä");
                Console.WriteLine("2 - Kirjaa työmatkoja ja tarkastele työntekijöiden matkatietoja");
                Console.WriteLine("3 - Kertyneiden korvausten tarkastelu");
                Console.WriteLine("4 - Poistu ohjelmasta");
                string valinta = Console.ReadLine();
                Console.WriteLine("\n");

                if (valinta == "1")
                {
                    henkilot = funktio.LuoPoistaTyontekija(henkilot);
                }
                else if (valinta == "2")
                {
                    TyontekijoidenKasittely();
                }
                else if (valinta == "3")
                {
                    funktio.KorvaustenTarkastelu(henkilot);
                }
                else if (valinta == "4")
                {
                    break;
                }
            }



            // Funktio - Lisätään matkoja työntekijälle ja tulostetaan työntekijäkohtaista tietoa

            void TyontekijoidenKasittely()
            {
                if (henkilot.Count == 0) { return; }

                Tyontekija valittu_tyontekija = funktio.TyontekijanValinta(henkilot);   // Tässä vaiheessa valitaan funktion avulla työntekijä, johon toiminnot kohdistuvat
                while (true)
                {
                    Console.WriteLine("\nValittu työntekijä: " + valittu_tyontekija.getName());
                    Console.WriteLine("\nMitä haluat tehdä?");
                    Console.WriteLine("1 - Kirjaa uusi matka");
                    Console.WriteLine("2 - Kuittaa korvauksia maksetuiksi");
                    Console.WriteLine("3 - Katso maksamattomat ja maksetut korvaukset");
                    Console.WriteLine("4 - Työntekijän työmatkat");
                    Console.WriteLine("5 - Poista tallennettu työmatka");
                    Console.WriteLine("6 - Palaa alkuvalikkoon");
                    Console.WriteLine("7 - Valitse uusi työntekijä");

                    string vastaus = Console.ReadLine();
                    Console.WriteLine("\n");


                    if (vastaus == "1")
                    {
                        funktio.MatkanTallennus(valittu_tyontekija);
                    }


                    else if (vastaus == "2")
                    {
                        funktio.KuittaaMaksetuiksi(valittu_tyontekija);  
                    }


                    else if (vastaus == "3")
                    {
                        Console.WriteLine("\nHenkilölle " + valittu_tyontekija.getName() + " maksamattomat korvaukset: " + valittu_tyontekija.Maksamattomat);
                        Console.WriteLine("Henkilölle " + valittu_tyontekija.getName() + " maksetut korvaukset: " + valittu_tyontekija.Maksetut);
                    }


                    else if (vastaus == "4")
                    {
                        if (valittu_tyontekija.getMatkat().Count > 0)
                        {
                            var matkalista = valittu_tyontekija.getMatkat();    // Haetaan valitun työntekijän Matkat-listaan tallennetut Matka-objektit. 
                            Console.WriteLine("Työmatkojen tiedot: ");
                            foreach (Matka x in matkalista)
                            {
                                Console.WriteLine(x.Tiedot());                  // Tulostetaan tallennettujen matkojen tiedot Matka-luokasta löytyvän Tiedot-metodin avulla.
                            }
                        }
                        else
                        {
                            Console.WriteLine("Henkilöllä ei ole tallennettuja matkoja");
                        }
                    }
                    else if (vastaus == "5")
                    {
                        funktio.PoistaMatka(valittu_tyontekija);
                    }
                    else if (vastaus == "6")
                    {
                        return;
                    }
                    else if (vastaus == "7")
                    {
                        valittu_tyontekija = funktio.TyontekijanValinta(henkilot);
                        continue;
                    }
                }
            }
        }
    }
}
