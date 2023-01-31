using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Collections;
using System.ComponentModel;

namespace NevgeneratorWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<string> csaladnevek = new ObservableCollection<string>();
        private ObservableCollection<string> utonevek = new ObservableCollection<string>();
        private ObservableCollection<string> generaltnevek = new ObservableCollection<string>();

        Random vel;
        public MainWindow()
        {
            InitializeComponent();
            vel = new Random();

        }

        private void btnbetoltutonev_Click(object sender, RoutedEventArgs e)
        {
            {
                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() == true)

                    foreach (var nev in File.ReadAllLines(ofd.FileName).ToList())

                    {
                        utonevek.Add(nev);
                        lbutonevek.ItemsSource = utonevek;
                    }


            }

        }

        private void btnbetoltcsaladnev_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)

                foreach (var nev in File.ReadAllLines(ofd.FileName).ToList())

                {
                    csaladnevek.Add(nev);
                    lbcsaladnevek.ItemsSource = csaladnevek;
                }
            lbl_slihatarertek.Content = lbl_CsaladNevekSzama.Content;


        }


        private void rdo_utonevekketto_Click(object sender, RoutedEventArgs e)
        {
            if (rdo_utonevekketto.IsChecked == true) rdo_utonevekegy.IsChecked = false;
        }

        private void rdo_utonevekegy_Click(object sender, RoutedEventArgs e)
        {
            if (rdo_utonevekegy.IsChecked == true) rdo_utonevekketto.IsChecked = false;

        }



        private void btn_NeveketGeneral_Click(object sender, RoutedEventArgs e)
        {


            if (lbcsaladnevek.Items.Count == 0 || lbutonevek.Items.Count == 0)

            {
                MessageBox.Show("Nincs név a listában");
                return;

            }


            for (int Index = 0; Index < sli_GeneraltNevekSzama.Value; Index++)
            {

                int kivalasztottVezeteknevIndexe = vel.Next(lbcsaladnevek.Items.Count);
                int kivalasztottUtonevIndexe = vel.Next(lbutonevek.Items.Count);


                if (rdo_utonevekegy.IsChecked == true || !utonevek[kivalasztottVezeteknevIndexe].Contains(" ") || rdo_utonevekketto.IsChecked == false)



                {

                    generaltnevek.Add($"{csaladnevek[kivalasztottVezeteknevIndexe]} {utonevek[kivalasztottUtonevIndexe]}");
                    csaladnevek.RemoveAt(kivalasztottVezeteknevIndexe);
                    utonevek.RemoveAt(kivalasztottUtonevIndexe);
                    lbGeneraltnevek.ItemsSource = generaltnevek;

                }


                ///Ezen még dologzni kell!!!!!
                if (rdo_utonevekegy.IsChecked == false || rdo_utonevekketto.IsChecked == true || utonevek[kivalasztottVezeteknevIndexe].Contains(" "))
                {

                    generaltnevek.Add($"{csaladnevek[kivalasztottVezeteknevIndexe]} {utonevek[kivalasztottUtonevIndexe]}");
                    csaladnevek.RemoveAt(kivalasztottVezeteknevIndexe);
                    utonevek.RemoveAt(kivalasztottUtonevIndexe);
                    lbGeneraltnevek.ItemsSource = generaltnevek;
                }


            }






        }









        private void btn_GeneraltNevektorles_Click(object sender, RoutedEventArgs e)
        {
            lbGeneraltnevek.ItemsSource = null;

        }

        private void btn_NevekMentése_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.AddExtension = true;
            sfd.DefaultExt = "txt";
            sfd.Filter = "Szöveges fájl (*.txt)|*.txt|CSV fájl (*.csv)|*.csv|Összes fájl(*.*)| *.*";
            sfd.Title = "Adja meg a névsor nevét!";
            if (sfd.ShowDialog() == true)
                try
                {


                    using (var sw = new StreamWriter(sfd.FileName, false, encoding: Encoding.UTF8))
                        foreach (var item in lbcsaladnevek.Items)
                            sw.Write(item.ToString() + Environment.NewLine);







                }
                catch (Exception)
                {
                    MessageBox.Show("Hiba a mentéskor. Kérem próbálja újra!");

                }

        }

        private void txt_LetrehozandoNevekSzama_Loaded(object sender, RoutedEventArgs e)
        {
            txt_LetrehozandoNevekSzama.Text = "0";
        }

        private void btn_Nevekrendezése_Click(object sender, RoutedEventArgs e)
        {
            lbGeneraltnevek.Items.SortDescriptions.Add(new SortDescription("", ListSortDirection.Ascending));
            lbl_statusbarkiiras.Content = "Nevek rendezve";
        }

        private void lbGeneraltnevek_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            string kesznev = "";
            string keresztnev = " ";
            kesznev = lbGeneraltnevek.SelectedValue.ToString();
            
            string veznev = "";
           
            ///szétválasztáson még dolgozni kell
            for (int i=0; i < kesznev.Length; i++)
            {
                if (kesznev[i]!=' ') veznev += kesznev[i];

                else if (kesznev[i] == ' ') keresztnev +=kesznev[i];



            }


       
            csaladnevek.Add(veznev);
            utonevek.Add(keresztnev);
            lbcsaladnevek.ItemsSource = csaladnevek;
            lbutonevek.ItemsSource = utonevek;


            generaltnevek.RemoveAt(lbGeneraltnevek.SelectedIndex);

        }
        
            
        }
}







