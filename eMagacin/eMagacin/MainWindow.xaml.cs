using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MySql.Data.MySqlClient;

namespace eMagacin
{
    public partial class MainWindow : Window
    {
        //Definisan static wrapPanel jer se koristi u static funkciji ucitajPodatke
        public static WrapPanel wp = null;
        public MainWindow()
        {
            InitializeComponent();
            wp = wpKomponente;
            ucitajPodatke();
        }

        //Zatvaranje aplikacije
        private void lblZatvori_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void label1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Omogucava da se prozor pomjera po ekranu
            this.DragMove();
        }

        //Funkcija za prikaz komponenti iz magacina
        //Funkcija je public da bi se mogla pozvati iz druge klase istatic jer se neće kreirati objekat ove klase (samo e učitavaju podaci)
        public static void ucitajPodatke()
        {
            //Prvo se ocisti WrapPanel
            wp.Children.Clear();
            //Upit kojim se pribavljaju svi podaci iz tabele magacin
            string sql = "SELECT * FROM magacin";
            //Konekcija se poziva iz klase Konekcija
            MySqlCommand komanda = new MySqlCommand(sql, Konekcija.GetConnection());
            MySqlDataReader reader = komanda.ExecuteReader();

            while (reader.Read())
            {
                //Svi podaci iz reader-a se smjestaju u lokalne promjenljive
                int id = (int)reader["id"];
                int ladicaBroj = (int)reader["ladicaBroj"];
                int kolicina = (int)reader["kolicina"];
                string sadrzaj = reader["sadrzaj"].ToString();
                string slika = reader["slika"].ToString();

                //Za svaki red iz baze se kreira po jedna nova kntrola i smješta se u wrapPanel
                ucKomponenta komponenta = new ucKomponenta(id, ladicaBroj, kolicina, sadrzaj, slika);
                wp.Children.Add(komponenta);
            }
            //Nakon što se završi sa readerom potrebno ga je zatvoriti
            reader.Close();
        }
    }
}
