using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonyvtarCucc
{
    public class Kolcsonzott
    {
        
         
        public Person ember { get; set; }
        public Book konyv { get; set; }
        public DateTime kikolcsonzesIdeje { get; set; }
        public DateTime lejarat { get; set; } //ezzel megengedem azt hogy hogy a embereknek ne kelljen hetente visszajarni hosszabítani hanem adott idopontig kivehetik

        public double buntetesOsszege = 5000;

        public void tartozasFrissitese()
        {
            if(lejarat > DateTime.Today)
            {
                ember.tartozas += buntetesOsszege;
                lejarat = DateTime.Today.AddDays(7);
            }
        }

        public override string ToString()
        {
            return ember.ID.ToString() + "\t" + konyv.ID.ToString() + "\t" + kikolcsonzesIdeje.ToShortDateString() + "\t" + lejarat.ToShortDateString();
        }
    }
}
