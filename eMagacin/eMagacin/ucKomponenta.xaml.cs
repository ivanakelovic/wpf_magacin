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
    public partial class ucKomponenta : UserControl
    {
        private int trenutnaKolicina, idKomponente;

        public ucKomponenta(int id, int ladicaBroj, int kolicina, string  sadrzaj, string slika)
        {
            InitializeComponent();

            this.trenutnaKolicina = kolicina;
            this.idKomponente = id;

            lblKolicina.Content = kolicina;
            lblLadicaBroj.Content = ladicaBroj;
            lblSadrzaj.Content = sadrzaj;

            imgSlika.Source = new ImageSourceConverter().ConvertFromString(@"Resources\" + slika) as ImageSource;

            if (kolicina > 0)
                this.Background = Brushes.DarkGreen;
            else
                this.Background = Brushes.DarkRed;
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (trenutnaKolicina > 0)
            {
                podizanjeKomponenti podigni = new podizanjeKomponenti(trenutnaKolicina);
                podigni.ShowDialog();
                if (podigni.DialogResult == true)
                {
                    trenutnaKolicina = trenutnaKolicina - podigni.kolicinaPodici;
                    string sql = "UPDATE magacin SET Kolicina = '" + trenutnaKolicina + "' WHERE ID = " + idKomponente;
                    MySqlCommand komanda = new MySqlCommand(sql, Konekcija.GetConnection());
                    komanda.ExecuteNonQuery();

                    MainWindow.ucitajPodatke();
                }
            }
            else
                MessageBox.Show("Ove komponente nema na stanju!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void mniIzvjestajSve_Click(object sender, RoutedEventArgs e)
        {
            //Za sve komponente iz magacina konstruktoru se prosleđuje null
            izvjestaj izvjestjSve = new izvjestaj(null);
            izvjestjSve.ShowDialog();
        }

        private void mniIzvjestajDostupne_Click(object sender, RoutedEventArgs e)
        {
            //Za dostupne komponente iz magacina konstruktoru se prosleđuje true
            izvjestaj izvjestjDostupne = new izvjestaj(true);
            izvjestjDostupne.ShowDialog();
        }

        private void mniIzvjestajNedostupne_Click(object sender, RoutedEventArgs e)
        {
            //Za nedostupne komponente iz magacina konstruktoru se prosleđuje false
            izvjestaj izvjestjNeDostupne = new izvjestaj(false);
            izvjestjNeDostupne.ShowDialog();

        }
    }
}
