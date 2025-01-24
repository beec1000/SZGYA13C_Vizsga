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
using System.IO;
using SZGYA13C_Vizsga;

namespace SZGYA13C_Vizsga
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Vizsgazo> vizsgazok = new List<Vizsgazo>();
        public MainWindow()
        {
            InitializeComponent();
            sikeresLB.Content = string.Empty;

            vizsgazok = Vizsgazo.Fromfile(@"..\..\..\src\adatok.txt");

            var nevek = vizsgazok.Select(n => n.Nev);
            foreach (var n in nevek)
            {       
                vizsgazokList.Items.Add(n);
            }

            vizsgazokLB.Content = $"{vizsgazok.Count} vizsgázó adatait beolvastuk";

            legjobbEredmeny.Content = string.Empty;
            leggyengebbEredmeny.Content = string.Empty;
            vizsgaEredmenye.Content = string.Empty;

        }

        private void sikeresBTN_Click(object sender, RoutedEventArgs e)
        {
            sikeresLB.Content = string.Empty;
            var eredmeny = vizsgazok.Count(e => e.Erdemjegy() != "Elégtelen");
            sikeresLB.Content = $"{eredmeny} fő";
        }

        private void allomanybaIrasBTN_Click(object sender, RoutedEventArgs e)
        {

            var sikeres = vizsgazok.Where(e => e.Erdemjegy() != "Elégtelen");
            var sikeresAdatok = sikeres.Select(s => $"{s.Nev}\t{Math.Round(s.Vegeredmeny, 2)}\t{s.Erdemjegy()}").ToList();

            File.WriteAllLines(@"..\..\..\src\vizsgaredmenyek.txt", sikeresAdatok);

            if (sikeresAdatok != null)
            {
                MessageBox.Show("Sikeres!", "Siker", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Hiba!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void vizsgazokList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listabolValasztott = vizsgazokList.SelectedItem.ToString().ToLower();
            var tanuloNeve = listabolValasztott;
            keresettTanulo.Text = tanuloNeve;
            keresettTanulo.Focus();
        }

        private void keresettTanulo_KeyChanged(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var tanuloNeve = keresettTanulo.Text.ToLower();
            var tanuloAdatai = vizsgazok.Where(t => t.Nev.ToLower().Contains(tanuloNeve)).FirstOrDefault();

            if (e.Key == Key.Enter)
            {
                if (tanuloAdatai != null)
                {
                    legjobbEredmeny.Content = $"Legjobb eredménye: {tanuloAdatai.ModulEredmenyek.Max() * 100}%";
                    leggyengebbEredmeny.Content = $"Leggyengébb eredménye: {tanuloAdatai.ModulEredmenyek.Min() * 100}%";
                    if (tanuloAdatai.Erdemjegy() == "Elégtelen")
                    {
                        vizsgaEredmenye.Content = $"Sikertelen vizsgát tett";
                    }
                    else
                    {
                        vizsgaEredmenye.Content = $"Sikeres vizsgát tett";
                    }

                }
                else
                {
                    MessageBox.Show("Ez a tanuló nincs az állományban!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
                    keresettTanulo.Text = string.Empty;
                    keresettTanulo.Focus();
                    legjobbEredmeny.Content = string.Empty;
                    leggyengebbEredmeny.Content = string.Empty;
                    vizsgaEredmenye.Content = string.Empty;
                }
            }
        }
    }
}