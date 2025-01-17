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

            
        }

        private void sikeresBTN_Click(object sender, RoutedEventArgs e)
        {
            sikeresLB.Content = string.Empty;
            var eredmeny = vizsgazok.Count(e => e.Erdemjegy() != "Elégtelen");
            sikeresLB.Content = $"{eredmeny} fő";
        }

        private void allomanybaIrasBTN_Click(object sender, RoutedEventArgs e)
        {
            var sikeres = vizsgazok.Select(s => $"{s.Nev}\t{Math.Round(s.Vegeredmeny, 2)}\t{s.Erdemjegy()}").ToList();

            File.WriteAllLines(@"..\..\..\src\vizsgaredmenyek.txt", sikeres);

            if (sikeres != null)
            {
                MessageBox.Show("Sikeres!", "Siker", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Hiba!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}