using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace Korvauslaskuri
{
    class Program
    {
        static void Main(string[] args)
        {


            List<Tyontekija> henkilot = new List<Tyontekija>();
            Funktiot funktio = new Funktiot();


            // Yritetään lukea tiedosto, johon tallennettu ohjelman käsittelemät tiedot
            // Tiedoston path ongelma? Mikä olisi optimaalinen? Muutenkin tiedostoon tallentamisen varmuus täysi mysteeri. 

            try
            {
                deser();
            }
            catch (Exception)
            {
                Console.WriteLine("");
            }


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
                    ser();                      // Huom. ser-funktio tallentaa (potentiaalisesti) muuttuneet tiedot tiedostoon.
                                                // Jos teet muutoksia ohjelman rakenteeseen pidä huoli, että ser-tulee kutsuttua uusien tietojen tallennusta varten.
                }
                else if (valinta == "2")
                {
                    TyontekijoidenKasittely();
                    ser();
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


            // Funktio - Kirjoitetaan henkilot-listan kaikki sisältö xml-tiedostoon. Täyttä voodoota, toimii lähinnä tuurilla. 

            void ser()
            {

                // Alustaa tiedoston, muutoin voi tulla ongelmia esim. jos yritetään tallentaa tyhjää listaa työntekijätietojen poistamisen jälkeen.
                try
                {
                    FileStream fileStream = File.Open(Environment.CurrentDirectory + "\\mytext.xml", FileMode.Open);
                    fileStream.SetLength(0);
                    fileStream.Close(); 
                }
                catch (Exception)
                {
                    Console.WriteLine(""); 
                }
                

                Stream stream = File.OpenWrite(Environment.CurrentDirectory + "\\mytext.xml");
                DataContractSerializer DataSer = new DataContractSerializer(typeof(List<Tyontekija>));
                DataSer.WriteObject(stream, henkilot);
                stream.Close();
            }


            // Funktio - Puretaan xml-tiedoston sisältö, muunnetaan luettavaan muotoon ja luodaan henkilot-lista sen pohjalta.

            void deser()
            {
                Stream stream = File.OpenRead(Environment.CurrentDirectory + "\\mytext.xml");

                XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(stream, new XmlDictionaryReaderQuotas());
                DataContractSerializer seri = new DataContractSerializer(typeof(List<Tyontekija>));

                henkilot = (List<Tyontekija>)seri.ReadObject(reader, true);

                reader.Close();
                stream.Close();
            }
        }
    }
}
