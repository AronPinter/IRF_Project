using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KonyvtarCucc
{
    public partial class Kolcsonzes : Form
    {

        List<Person> emberek;
        List<Book> konyvek;
        List<Kolcsonzott> kikolcsonzesek;

        public Kolcsonzes(List<Book> konyvekref,List<Person> emberekref, List<Kolcsonzott> kikolcsonzesekref)
        {
            InitializeComponent();
            button1.Text = "Könyv kölcsönzése";
            emberek = emberekref;
            konyvek = konyvekref;
            kikolcsonzesek = kikolcsonzesekref;
        }

        private void Kolcsonzes_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            
            for (int i = 0; i < emberek.Count; ++i)
                comboBox1.Items.Add(emberek[i].vezetekNev + " " + emberek[i].keresztNev + " " + emberek[i].levelezesiCim);

            comboBox2.Items.Clear();
            for (int i = 0; i < konyvek.Count; ++i)
                comboBox2.Items.Add(konyvek[i].Szerzo + " " + konyvek[i].Name + " " + konyvek[i].KiadasiEv);

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Kolcsonzott newdata = new Kolcsonzott();
            newdata.ember = emberek[comboBox1.SelectedIndex];
            newdata.konyv = konyvek[comboBox2.SelectedIndex];
            newdata.kikolcsonzesIdeje = DateTime.Today;
            newdata.lejarat = monthCalendar1.SelectionStart;
            kikolcsonzesek.Add(newdata);
        }
    }
}
