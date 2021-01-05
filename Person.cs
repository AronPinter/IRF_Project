using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonyvtarCucc
{
    public class Person
    {
        public Person() { vezetekNev = " ";keresztNev = " ";levelezesiCim = " ";tartozas = 0;telefonszam = " "; }
        public string vezetekNev { get; set; }
        public string keresztNev { get; set; }

        public string levelezesiCim { get; set; } //like: 1111 Budapest Irinyi János utca 9-11

        public double tartozas { get; set; }

        public string telefonszam { get; set; }

        public int ID { get; set; }
        public override string ToString()
        {
            return vezetekNev.ToString() + "\t" + keresztNev.ToString() + "\t" + levelezesiCim.ToString()+ "\t" + telefonszam.ToString() + "\t" + tartozas.ToString() + ";" + "\t" + ID.ToString();
        }
    }
}
