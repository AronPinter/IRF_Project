using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonyvtarCucc
{
    public class Book
    {
        public Book() { Name = " ";Szerzo = " "; KiadasiEv = DateTime.Now.Year.ToString(); leltariSzam = "-1"; ID = 0; }
        public string Name { get; set; }
        public string Szerzo { get; set; }
        public string KiadasiEv { get; set; }
        public string leltariSzam { get; set; }

        public int ID { get; set; }

        public override string ToString()
        {
            return Name.ToString() + "\t" + Szerzo.ToString()+ "\t" + KiadasiEv.ToString() + "\t" + leltariSzam.ToString() + ";" +"\t" + ID.ToString();
        }


    }
}
