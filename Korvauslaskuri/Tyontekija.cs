using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace Korvauslaskuri
{
    // Luokkaan tallennetaan yksittäiselle työntekijälle tulevat matkat ja matkoista aiheutuvat korvaukset.
    // Matkat tallennettuna listaan, korvauksista (maksetuista ja maksamattomista) tallennettuna yksinkertainen luku
    [DataContract()]
    class Tyontekija
    {
        [DataMember]
        private string nimi;

        [DataMember]
        private double maksamattomat = 0;

        [DataMember]
        private double maksetut = 0;

        [DataMember]
        private List<Matka> matkat = new List<Matka>();

        public Tyontekija(string nimi)
        {
            this.nimi = nimi;
        }
        public string getName()
        {
            return nimi;
        }
        public double Maksamattomat
        {
            get { return maksamattomat; }
            set { maksamattomat += value; }
        }
        public double Maksetut
        {
            get { return maksetut; }
        }
        public void MaksaKorvauksia(double maara)
        {
            if (maksamattomat == 0)
            {
                Console.WriteLine("Henkilöllä ei ole maksamattomia korvauksia");
                return;
            }
            else if (maara > maksamattomat)
            {
                Console.WriteLine("Yrität maksaa liikaa, tarkista summa");
                return;
            }
            else
            {
                maksetut += maara;
                maksamattomat -= maara;
            }
        }


        // Metodi jota käytetään aina Matka-luokan luonnin yhteydessä. 
        // Kun matka luodaan se liitetään työntekijään tällä metodilla
        public void AddMatka(Matka uusi)
        {
            matkat.Add(uusi);
        }

        // Metodi, jolla työntekijälle tallennetut Matkat saadaan ulos tarkastelua varten
        public List<Matka> getMatkat()
        {
            return matkat;
        }
    }
}
