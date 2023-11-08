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
using System.Windows.Shapes;

namespace eMagacin
{
    public partial class podizanjeKomponenti : Window
    {
        public int kolicinaPodici { get; set; }
        public podizanjeKomponenti(int kolicina)
        {
            InitializeComponent();

            lblInfo.Content = "Maksimalno možete podići " + kolicina + " komponenti!";
            slKolicinaZaPodici.Maximum = kolicina;
        }

        private void slKolicinaZaPodici_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblKolicinaZaPodici.Content = slKolicinaZaPodici.Value;
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void btnPodigni_Click(object sender, RoutedEventArgs e)
        {
            kolicinaPodici = (int)slKolicinaZaPodici.Value;
            this.DialogResult = true;
        }
    }
}
