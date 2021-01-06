using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace KonyvtarCucc
{
    public partial class Form1 : Form
    {
        public FileStream PersonDB;
        public FileStream BookDB;
        public FileStream BorrowDB;

        public List<Person> emberek;
        public List<Book> konyvek;
        public List<Kolcsonzott> kikolcsonzesek;

        public Form konyvhozzaadas;
        public Form emberFelvetele;
        public Form KolcsonzesForm;

        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 60 * 1000; //1 sec
            button1.Text = "Új könyv felvétele";
            button2.Text = "Új ember felvétele";
            button3.Text = "Tartozások listázása";
            button4.Text = "Könyv kikölcsönzése";
            emberek = new List<Person>();
            konyvek = new List<Book>();
            kikolcsonzesek = new List<Kolcsonzott>();

            konyvhozzaadas = new AddKonyv(konyvek);
            emberFelvetele = new Addember(emberek);
            KolcsonzesForm = new Kolcsonzes(konyvek, emberek, kikolcsonzesek);

            
            loadDB();
        }

        public void loadDB()
        {
            string pathToBookDB = "..\\..\\bookdb.txt";
            string pathToPersonDB = "..\\..\\persondb.txt";
            string pathToKolcsonzesekDB = "..\\..\\kolcsonzesekdb.txt";

            PersonDB = new FileStream(pathToPersonDB, FileMode.OpenOrCreate);
            BookDB = new FileStream(pathToBookDB, FileMode.OpenOrCreate);
            BorrowDB = new FileStream(pathToKolcsonzesekDB, FileMode.OpenOrCreate);

            //load persons
            PersonDB.Position = 0;
            StreamReader read = new StreamReader(PersonDB);
            string sor;
            while( (sor = read.ReadLine()) != null )
            {
                Person input = new Person();
                string[] data = sor.Split('\t');
                input.keresztNev = data[0];
                input.vezetekNev = data[1];
                input.levelezesiCim = data[2];
                input.telefonszam = data[3];
                input.tartozas = Convert.ToDouble(data[4].Remove(data[4].Length-1) );
                input.ID = Convert.ToInt32(data[5]);
                emberek.Add(input);

            }

            BookDB.Position = 0;
            read = new StreamReader(BookDB);
            
            while ((sor = read.ReadLine()) != null)
            {
                Book input = new Book();
                string[] data = sor.Split('\t');
                input.Name = data[0];
                input.Szerzo = data[1];
                input.KiadasiEv = data[2];
                input.leltariSzam = data[3].Remove(data[3].Length - 1);
                input.ID = Convert.ToInt32(data[4]);
                konyvek.Add(input);
            }

            BorrowDB.Position = 0;
            read = new StreamReader(BorrowDB);

            while ((sor = read.ReadLine()) != null)
            {
                Kolcsonzott input = new Kolcsonzott();
                string[] data = sor.Split('\t');
                int emberid = Convert.ToInt32(data[0]);
                int konyvid = Convert.ToInt32(data[1]);
                for (int i = 0; i < emberek.Count; ++i)
                {
                    if (emberek[i].ID == emberid)
                    {
                        input.ember = emberek[i];
                        break;
                    }
                }
                for(int i = 0; i < konyvek.Count;++i)
                {
                    if(konyvek[i].ID == konyvid)
                    {
                        input.konyv = konyvek[i];
                        break;
                    }
                }
                input.kikolcsonzesIdeje = DateTime.Parse(data[2]);
                input.lejarat = DateTime.Parse(data[3]);
            }

            }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for(int i=0;i<kikolcsonzesek.Count; ++i)
            {
                kikolcsonzesek[i].tartozasFrissitese();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            konyvhozzaadas.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            emberFelvetele.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            KolcsonzesForm.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            string pathToBookDB = "..\\..\\bookdb.txt";
            string pathToPersonDB = "..\\..\\persondb.txt";
            string pathToKolcsonzesekDB = "..\\..\\kolcsonzesekdb.txt";

            PersonDB.Close();
            BookDB.Close();
            BorrowDB.Close();

            PersonDB = new FileStream(pathToPersonDB, FileMode.Create);
            BookDB = new FileStream(pathToBookDB, FileMode.Create);
            BorrowDB = new FileStream(pathToKolcsonzesekDB, FileMode.Create);


            //emberek kiirasa db be
            for (int i=0;i<emberek.Count;++i)
            {
                string person = emberek[i].ToString()+"\n";
                byte[] info = new UTF8Encoding(true).GetBytes(person);
                PersonDB.Write(info, 0, info.Length);
            }

            for (int i = 0; i < konyvek.Count; ++i)
            {
                string book = konyvek[i].ToString()+"\n";
                byte[] info = new UTF8Encoding(true).GetBytes(book);
                BookDB.Write(info, 0, info.Length);

            }
            for(int i = 0; i<kikolcsonzesek.Count;++i)
            {
                string output = kikolcsonzesek[i].ToString() + "\n";
                byte[] info = new UTF8Encoding(true).GetBytes(output);
                BorrowDB.Write(info, 0, info.Length);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Add("Vn", "Vezetéknév");
            dataGridView1.Columns.Add("Kn", "Keresztnév");
            dataGridView1.Columns.Add("Lc", "Levelezési cím");                        
            dataGridView1.Columns.Add("Ta", "Tartozás");
            dataGridView1.Columns.Add("Te", "Telefonszám");
            dataGridView1.Columns.Add("Id", "ID");
            for (int i =0; i<emberek.Count;++i)
            {
                if(emberek[i].tartozas > 0 )
                {
                    dataGridView1.Rows.Add(emberek[i].vezetekNev.ToString(), emberek[i].keresztNev.ToString(), emberek[i].levelezesiCim.ToString(), emberek[i].tartozas, emberek[i].telefonszam.ToString(), emberek[i].ID);
                }
            }
        }
    }
}
