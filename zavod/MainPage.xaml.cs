using System;
using System.Collections.Generic;
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

namespace zavod
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Obor_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new obor());
        }

        private void Sotr_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new sotr());
        }

        private void Yslygi_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new yslygi());
        }

        private void Otdel_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new otdel());
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
