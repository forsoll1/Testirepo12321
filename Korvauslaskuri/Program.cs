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
                Console.WriteLine("3 - Kertyneiden korvauksien tarkastelu");
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

                Tyontekija valittu_tyontekija = TyontekijanValinta();   // Tässä vaiheessa valitaan funktion avulla työntekijä, johon toiminnot kohdistuvat
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
                        MatkanTallennus(valittu_tyontekija);
                    }


                    else if (vastaus == "2")
                    {
                        if (valittu_tyontekija.Maksamattomat == 0)
                        {
                            Console.WriteLine("Ei maksamattomia korvauksia");
                            continue;
                        }
                        Console.WriteLine("Maksamattomia korvauksia: " + valittu_tyontekija.Maksamattomat);
                        Console.WriteLine("Kuinka suurella summalla haluat kuitata korvauksia maksetuiksi?");
                        double maksa_korvauksia = Convert.ToDouble(Console.ReadLine());
                        valittu_tyontekija.MaksaKorvauksia(maksa_korvauksia);
                        Console.WriteLine("Henkilölle " + valittu_tyontekija.getName() + " maksettu " + maksa_korvauksia);
                        Console.WriteLine("Maksettuja: " + valittu_tyontekija.Maksetut + "  Maksamattomia: " + valittu_tyontekija.Maksamattomat);
                    }


                    else if (vastaus == "3")
                    {
                        Console.WriteLine("Henkilölle " + valittu_tyontekija.getName() + " maksamattomat korvaukset: " + valittu_tyontekija.Maksamattomat);
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
                        while (true)
                        {
                            if (valittu_tyontekija.getMatkat().Count > 0)
                            {
                                var matkalista = valittu_tyontekija.getMatkat();     
                                Console.WriteLine("Työmatkojen tiedot: ");
                                for (int i = 0; i < matkalista.Count(); i++)
                                {
                                    Console.WriteLine("{0} - {1}", i + 1, matkalista[i].Tiedot());
                                }
                                string poista = Console.ReadLine();
                                if (Convert.ToInt32(poista) - 1 >= 0 && Convert.ToInt32(poista) - 1 < matkalista.Count())
                                {
                                    matkalista.RemoveAt(Convert.ToInt32(poista) - 1);
                                    continue;
                                }
                                else
                                {
                                    Console.WriteLine("Väärä syöte\n");
                                    continue;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Henkilöllä ei ole tallennettuja matkoja");
                                break;
                            }
                        }
                    }
                    else if (vastaus == "6")
                    {
                        return;
                    }
                    else if (vastaus == "7")
                    {
                        valittu_tyontekija = TyontekijanValinta();
                        continue;
                    }
                }
            }



            // Funktio - syötetään matkakohtaiset tiedot tallennusta varten

            void MatkanTallennus(Tyontekija valittu_tyontekija)
            {
                double kk = 0.44;       // Korvausten arvot, oletuksena vuoden 2021 arvot
                double puoliPR = 20;
                double kokoPR = 44;

                Console.WriteLine("Oletusarvona vuoden 2021 korvaukset: Kilometrikorvaus {0} e/kk, Puolipäiväraha {1}e, Päiväraha {2}e", kk, puoliPR, kokoPR);
                Console.WriteLine("Haluatko muuttaa käytössä olevat korvaukset? K/E");
                string vastaus = Console.ReadLine();
                if (vastaus == "K")
                {
                    funktio.MuutaKorvaukset(kk, puoliPR, kokoPR, out kk, out puoliPR, out kokoPR);     // Siirretty omaksi funktiokseen
                }

                Console.WriteLine("Anna matkan päivämäärä muodossa p.kk.v");
                string pvm = Console.ReadLine();
                Console.WriteLine("Anna lähtöaika (muoto 12:34)");
                string lahto = Console.ReadLine();
                Console.WriteLine("Anna paluuaika (muoto 15:22)");
                string paluu = Console.ReadLine();
                Console.WriteLine("Anna matkatut kilometrit");
                int kilometrit = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("\n");

                Matka uusi = new Matka(pvm, lahto, paluu, kilometrit, kk, puoliPR, kokoPR);  // Uusi Matka-objekti, jolle syötetään tarvittavat tiedot korvauslaskuja varten
                Console.WriteLine(uusi.Tiedot());
                valittu_tyontekija.Maksamattomat = uusi.Korvaukset();                       // Maksamattomat-metodin "set" komennolla lisätään valitulle työntekijälle matkan korvaukset
                valittu_tyontekija.AddMatka(uusi);                                          // Tallennetaan Matka työntekijälle
                Console.WriteLine("Työntekijälle kertynyt maksamattomia korvauksia: " + valittu_tyontekija.Maksamattomat);
            }



            // Funktio - Valitaan Mainissa olevasta työntekijälistasta työntekijä-objekti käsittelyä varten

            Tyontekija TyontekijanValinta()
            {
                Tyontekija valittu_henkilo;

                Console.WriteLine("Valitse työntekijää vastaava numero");
                int counter = 0;
                while (true)
                {
                    foreach (var x in henkilot)
                    {
                        counter += 1;
                        Console.WriteLine(counter + " : " + x.getName());
                    }
                    string t_valinta = Console.ReadLine();
                    int t_valinta_int = Convert.ToInt32(t_valinta);
                    if (t_valinta_int - 1 <= henkilot.Count && t_valinta_int > 0)
                    {
                        valittu_henkilo = henkilot[(t_valinta_int - 1)];
                        return valittu_henkilo;
                    }
                    else
                    {
                        Console.WriteLine("Väärä valinta, yritä uudelleen");
                    }
                }
            }            
        }
    }
}
