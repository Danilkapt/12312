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
    /// Логика взаимодействия для AddOtdel.xaml
    /// </summary>
    public partial class AddOtdel : Page
    {
        Otdel ot;
        public AddOtdel(Otdel c)
        {
            InitializeComponent();
            if (c == null)
                c = new Otdel();
            DataContext = ot = c;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (ot.Id_Otdela == 0)
            {
                Connect.context.Otdel.Add(ot);
            }
            try
            {
                Connect.context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Nav.MainFrame.GoBack();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.GoBack();
        }
    }
}
