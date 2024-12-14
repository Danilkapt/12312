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
    /// Логика взаимодействия для Viborka.xaml
    /// </summary>
    public partial class Viborka : Page
    {
        public Viborka()
        {
            InitializeComponent();
        }

        private void Vibor_Click(object sender, RoutedEventArgs e)
        {
            int kl = Convert.ToInt32(text1.Text);
            int tov = Convert.ToInt32(text2.Text);
            var s = Connect.context.Yslyga.Where(x => x.Id_Yslygi == kl && x.Id_sotr == tov).ToList();
            YYS.ItemsSource = s;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.GoBack();
        }
    }
}
