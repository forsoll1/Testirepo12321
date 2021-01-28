using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korvauslaskuri
{
    [Serializable()]
    class Matka
    {
        private int tag;            // Irrotetaan vuosiluku tagiksi jonka perusteella yksittäisi matkoja voidaan lajitella, oli asiakkaan toiveissa
        private string pvm;
        private string matka_aika;
        private double kilometrikorvaus;
        private double paivaraha;


        // Constructor tekee suurimman osan oleellisista töistä eli laskee matkalta kertyneet kustannukset
        // ja tallentaa annettuja tietoja objektiin
        public Matka(string paivamaara, string lahto, string paluu, int kilometrit, double kk, double puoliPR, double kokoPR)
        {
            string[] pvm_jako = paivamaara.Split('.');
            tag = Convert.ToInt32(pvm_jako[2]);


            string[] lahto_jako = lahto.Split(':');
            string[] paluu_jako = paluu.Split(':');

            pvm = paivamaara;
            matka_aika = lahto_jako[0] + ":" + lahto_jako[1] + " - " + paluu_jako[0] + ":" + paluu_jako[1];


            DateTime date1 = new DateTime(2010, 1, 1, Convert.ToInt32(lahto_jako[0]), Convert.ToInt32(lahto_jako[1]), 0);
            DateTime date2 = new DateTime(2010, 1, 1, Convert.ToInt32(paluu_jako[0]), Convert.ToInt32(paluu_jako[1]), 0);

            TimeSpan interval = date2 - date1;
            if (interval.TotalHours > 10.0)
            {
                paivaraha = kokoPR;
            }
            else if (interval.TotalHours > 6.0)
            {
                paivaraha = puoliPR;
            }
            else
            {
                paivaraha = 0;
            }

            kilometrikorvaus = kilometrit * kk;
        }

        // Palauttaa stringinä yksittäisen matkan tiedot
        public string Tiedot()
        {
            return "Päivämäärä: " + pvm + "  Matka-aika: " + matka_aika + "  Kilometrikorvaus: " + kilometrikorvaus + "  Päiväraha: " + paivaraha;
        }

        // Tulostaa matkasta aiheutuvat korvaukset
        public double Korvaukset()
        {
            return (Convert.ToDouble(paivaraha) + kilometrikorvaus);
        }

        // Vuosiluvun tarkastelu matkojen lajittelua varten
        public int Tag
        {
            get { return tag; }
        }
    }
}
