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
using Models;
using Telefonszamok_Alap.Models;

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

        var mindenAdat = (from s in _context.enSzemelyek
                          join h in _context.enHelysegek on s.enHelysegid equals h.id
                          join t in _context.enTelefonszamok on s.id equals t.enSzemelyid into szemelyTelefonszamok
                          select new
                          {
                              Szemely = s,
                              Helyseg = h,
                              Telefonszamok = szemelyTelefonszamok
                          }).ToList().Select(x => new SzemelyesAdatok
                          {
                              Vezeteknev = x.Szemely.Vezeteknev,
                              Utonev = x.Szemely.Utonev,
                              Irsz = x.Helyseg.IRSZ,
                              Lakcim = x.Szemely.Lakcim,
                              Helysegnev = x.Helyseg.Nev,
                              Telefonszamok = string.Join(", ", x.Telefonszamok.Select(t => t.Szam))
                          }).ToList();
        dgAdatracs.ItemsSource = mindenAdat;
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
        dgAdatracs.Visibility = Visibility.Hidden;
        grRacs.Visibility = Visibility.Visible;

        grRacs.DataContext = _context.enHelysegek.ToList();
        cbIrsz.SelectedItem = 0;
    }

    private void cbIrsz_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var enAktualis = ((ComboBox)sender).SelectedItem as enHelyseg;
        cbIrsz.SelectedItem = enAktualis;
        cbHelysegnev.SelectedItem = enAktualis;
        tbIrsz.Text = enAktualis.IRSZ.ToString();
        tbHelysegnev.Text = enAktualis.Nev;
    }

    private void btRogzit_Click(object sender, RoutedEventArgs e)
    {
        var enAktualis = cbIrsz.SelectedItem as enHelyseg;
        if (!btUjHelyseg.IsEnabled)
        {
            enAktualis = new enHelyseg();
            _context.enHelysegek.Add(enAktualis);
        }
        enAktualis.IRSZ = tbIrsz.Text;
        enAktualis.Nev = tbHelysegnev.Text;
        grRacs.Visibility = Visibility.Hidden;
    }

    private void btUjHelyseg_Click(object sender, RoutedEventArgs e)
    {
        Beallit(false);
        tbIrsz.Text = "";
        tbHelysegnev.Text = "";
    }

    private void Beallit(bool b)
    {
        btUjHelyseg.IsEnabled = b;
        cbIrsz.IsEnabled = b;
        cbHelysegnev.IsEnabled = b;
    }

    private void btVissza_Click(object sender, RoutedEventArgs e)
    {
        grRacs.Visibility = Visibility.Hidden;
    }
}