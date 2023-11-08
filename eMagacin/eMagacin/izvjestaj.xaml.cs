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

using MySql.Data.MySqlClient;

namespace eMagacin
{
    public partial class izvjestaj : Window
    {
        public izvjestaj(bool? tip)//Promjenljiva tip je bool sa 3 vrijednosti (null-sve komponente, true-Komponente na stanju, false-komponente kojih nema na stanju
        {
            InitializeComponent();
            //Postavljanje zaglavlja u textBoxu u kome se prikazuju podaci
            txtIzvjestaj.Text = "Ladica\tSadržaj\t\t\tKoličina\n";
            //Pribavlju se sve komponente izmagacina
            string sql = "SELECT * FROM magacin";

            if (tip == true)//Ako je u konstruktoru prosijeđeno true upitu se dodaje uslov da je kolicina>0 (komoponente ima na stanju)
                sql += " WHERE kolicina>0";
            else if (tip == false)//Ako je u konstruktoru prosijeđeno false upitu se dodaje uslov da je kolicina=0 (komoponente nema na stanju)
                sql += " WHERE kolicina=0";
            //Podaci u izvještsju se sortiraju u opadajućem redu po količini
            sql += " ORDER BY kolicina DESC";

            //Konekcija se poziva iz klase Konekcija
            MySqlCommand komanda = new MySqlCommand(sql, Konekcija.GetConnection());
            MySqlDataReader reader = komanda.ExecuteReader();

            while (reader.Read())
            {
                //Svi podaci iz reader-a se smjestaju u lokalne promjenljive
                int ladicaBroj = (int)reader["ladicaBroj"];
                int kolicina = (int)reader["kolicina"];
                string sadrzaj = reader["sadrzaj"].ToString();
                //Dobijeni podaci se dodaju u texBox u izvještaju
                txtIzvjestaj.Text += ladicaBroj + "\t" + sadrzaj + "\t\t" + kolicina + "\n";
            }
            //Nakon što se završi sa readerom potrebno ga je zatvoriti
            reader.Close();
        }
    }
}
