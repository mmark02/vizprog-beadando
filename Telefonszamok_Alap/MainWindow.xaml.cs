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
using cnTelefonkonyv;

namespace Telefonszamok_Alap;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    TelefonkonyvContext _context;
    public MainWindow()
    {
        InitializeComponent();
        _context = new TelefonkonyvContext();
        //Teszt();
        
    }

    //private void Teszt()
    //{
    //    MessageBox.Show(_context.enSzemelyek.First().Vezeteknev);
    //}

    private void miMentes_Click(object sender, RoutedEventArgs e)
    {
        _context.SaveChanges();
    }

    private void miKilepes_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
    
    private void miMindenAdat_Click(object sender, RoutedEventArgs e)
    {
        dgAdatracs.Visibility = Visibility.Visible;
        grRacs.Visibility = Visibility.Hidden;

        
    }

    private void miHelysegek_Click(object sender, RoutedEventArgs e)
    {
        dgAdatracs.Visibility = Visibility.Visible;
        grRacs.Visibility = Visibility.Hidden;

        var linq = (from h in _context.enHelysegek
                    select new { h.IRSZ, h.Nev }).ToList();
        dgAdatracs.ItemsSource = linq;
    }

    private void miHelysegekModositas_Click(object sender, RoutedEventArgs e)
    {

    }
}