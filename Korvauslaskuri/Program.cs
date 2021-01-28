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
                        TyontekijoidenKasittely();
                    }
                }
                else if (valinta == "3")
                {
                    KorvaustenTarkastelu();
                }
                else if (valinta == "4")
                {
                    break;
                }
            }



            // Funktio - Lisätään matkoja työntekijälle ja tulostetaan työntekijäkohtaista tietoa

            void TyontekijoidenKasittely()
            {
                Tyontekija valittu_tyontekija = TyontekijanValinta();   // Tässä vaiheessa valitaan funktion avulla työntekijä, johon toiminnot kohdistuvat
                while (true)
                {
                    Console.WriteLine("\nValittu työntekijä: " + valittu_tyontekija.getName());
                    Console.WriteLine("\nMitä haluat tehdä?");
                    Console.WriteLine("1 - Kirjaa uusi matka");
                    Console.WriteLine("2 - Kuittaa korvauksia maksetuiksi");
                    Console.WriteLine("3 - Katso maksamattomat ja maksetut korvaukset");
                    Console.WriteLine("4 - Työntekijän työmatkat");
                    Console.WriteLine("5 - Palaa alkuvalikkoon");
                    Console.WriteLine("6 - Valitse uusi työntekijä");

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
                        return;
                    }
                    else if (vastaus == "6")
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
                    MuutaKorvaukset(kk, puoliPR, kokoPR, out kk, out puoliPR, out kokoPR);     // Siirretty omaksi funktiokseen
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



            // Funktio korvausten muokkaamista varten, kutsutaan matkojen kirjaamisen yhteydessä

            void MuutaKorvaukset(double kk_v, double puoliPR_v, double kokoPR_v, out double kk, out double puoliPR, out double kokoPR)
            {
                Console.WriteLine("\nAnna uusi kilometrikorvauksen arvo (nykyinen: {0}): ", kk_v);
                kk = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Anna uusi puolipäivärahan arvo (nykyinen {0}): ", puoliPR_v);
                puoliPR = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Anna uusi päivärahan arvo (nykyinen: {0}): ", kokoPR_v);
                kokoPR = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Korvauksien arvot muutettu \n");
            }



            // Funktio jonka kautta tarkastellaan matkakulujen kokonaiskertymiä

            void KorvaustenTarkastelu()
            {
                List<int> vuodet = new List<int>();     // Kerätään listaan kaikki vuodet, joilta maksettavia matkakorvauksia on kertynyt
                foreach (var x in henkilot)
                {
                    foreach (var y in x.getMatkat())
                    {
                        int vuosi = y.Tag;
                        if (vuodet.Contains(vuosi))
                        {
                            continue;
                        }
                        else
                        {
                            vuodet.Add(vuosi);
                        }
                    }
                }
                if (vuodet.Count > 0) { vuodet.Sort(); }

                while (true)
                {
                    Console.WriteLine("\nValitse toiminto: ");
                    Console.WriteLine("1 - Tämänhetkiset maksamattomat korvaukset yhteensä");
                    Console.WriteLine("2 - Vuosittain kertyneet korvaukset");
                    Console.WriteLine("3 - Yksittäisten työntekijöiden vuotuiset työmatkat");
                    Console.WriteLine("4 - Palaa alkuvalikkoon");

                    string valinta = Console.ReadLine();
                    switch (valinta)
                    {

                        // Loopataan kaikkien tallennettujen henkilöiden Maksamattomat-arvot läpi
                        case "1":
                            double summa = 0;
                            foreach (var x in henkilot)
                            {
                                summa += x.Maksamattomat;
                            }
                            Console.WriteLine("Tämänhetkiset maksamattomat korvaukset: {0} euroa", summa);
                            continue;

                        // Loopataan aiemmin kerättyjen vuosilukujen avulla kaikkien työntekijöiden kaikki tallennetut matkat
                        // Jos matkan tagi vastaa vuosilukua, matkan kerryttämät korvaukset lisätään vuosiluvun korvaussummaan
                        case "2":
                            foreach (var x in vuodet)
                            {
                                double vuosi_summa = 0;
                                foreach (var y in henkilot)
                                {
                                    foreach (var z in y.getMatkat())
                                    {
                                        if (x == Convert.ToInt32(z.Tag))
                                        {
                                            vuosi_summa += z.Korvaukset();
                                        }
                                    }
                                }
                                Console.WriteLine("Vuonna {0} kertyneet korvaukset: {1} euroa", x, vuosi_summa);
                            }
                            continue;

                        // Loopataan jokaisen henkilön vuosittain kertyneet matkakulut
                        case "3":
                            foreach (var x in henkilot)
                            {
                                if (x.getMatkat().Count > 0) { Console.WriteLine("Henkilön {0} työmatkat: ", x.getName()); }
                                double kokonaissumma = 0;
                                foreach (var y in vuodet)
                                {
                                    double osasumma = 0;
                                    int counter = 0;
                                    foreach (var z in x.getMatkat())
                                    {
                                        if (y == Convert.ToInt32(z.Tag))
                                        {
                                            Console.WriteLine(z.Tiedot());
                                            osasumma += z.Korvaukset();
                                            counter++;
                                        }
                                    }
                                    if (counter > 0)
                                    {
                                        Console.WriteLine("Vuoden {0} korvaukset yhteensä: {1}e \n", y, osasumma);
                                        kokonaissumma += osasumma;
                                    }

                                }
                                Console.WriteLine("Henkilölle {0} kertyneet matkakorvaukset yhteensä: {1}e \n", x.getName(), kokonaissumma);
                            }
                            continue;

                        case "4":
                            return;

                        default:
                            continue;
                    }
                }
            }
        }
    }
}
