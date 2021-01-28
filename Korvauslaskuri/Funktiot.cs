using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korvauslaskuri
{
    // Luokka mainissa käytettäville funktioille, siirretty omaan luokkaansa siisteyden takia
    class Funktiot
    {
        // Työntekijä-objektien lisääminen/poistaminen 
        public List<Tyontekija> LuoPoistaTyontekija(List<Tyontekija> henkilot)
        {
            List<Tyontekija> newlist = new List<Tyontekija>();
            foreach (var x in henkilot)
            {
                newlist.Add(x);
            }
            while (true)
            {
                Console.WriteLine("Valitse toiminto: ");
                Console.WriteLine("1 - Luo työntekijä ");
                Console.WriteLine("2 - Poista työntekijä");
                Console.WriteLine("3 - Tallenna muutokset ja palaa alkuun");
                Console.WriteLine("4 - Hylkää muutokset ja palaa alkuun");

                string valinta = Console.ReadLine();
                Console.WriteLine("\n");
                switch (valinta)
                {
                    case "1":

                        Console.WriteLine("Anna työntekijän nimi");
                        string nimi = Console.ReadLine();
                        if (!String.IsNullOrEmpty(nimi))
                        {
                            Tyontekija uusi = new Tyontekija(nimi);
                            newlist.Add(uusi);
                            Console.WriteLine("Työntekijä luotu\n");
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("Et antanut nimeä.");
                            continue;
                        }
                    case "2":
                        if (newlist.Count() == 0)
                        {
                            Console.WriteLine("Ei poistettavia työntekijöitä\n");
                            continue; 
                        }
                        else
                        {
                            Console.WriteLine("Valitse poistettavaa henkilöä vastaava numero: \n");
                            for (int i = 0; i < newlist.Count(); i++)
                            {
                                Console.WriteLine("{0} - {1}", i+1, newlist[i].getName());
                            }
                            string poista = Console.ReadLine();
                            if (Convert.ToInt32(poista) - 1 >= 0 && Convert.ToInt32(poista) - 1 < newlist.Count())
                            {
                                newlist.RemoveAt(Convert.ToInt32(poista) - 1);
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("Väärä syöte\n");
                                continue;
                            }
                        }

                    case "3":
                        return newlist;

                    case "4":
                        return henkilot; 

                    default:
                        continue;
                }
            }
        }


        // Funktio korvausten arvojen muokkaamista varten, kutsutaan matkojen kirjaamisen yhteydessä
        public void MuutaKorvaukset(double kk_v, double puoliPR_v, double kokoPR_v, out double kk, out double puoliPR, out double kokoPR)
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

        public  void KorvaustenTarkastelu(List<Tyontekija> henkilot)
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
