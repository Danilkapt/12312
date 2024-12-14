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
    /// Логика взаимодействия для sotr.xaml
    /// </summary>
    public partial class sotr : Page
    {
        public sotr()
        {
            InitializeComponent();
            SotrDG.ItemsSource = Connect.context.Sotr.ToList();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SotrDG.ItemsSource = Connect.context.Sotr.ToList();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new AddSotr((sender as Button).DataContext as Sotr));
        }

        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            SotrDG.ItemsSource = Connect.context.Sotr.OrderBy(x => x.Surname).ToList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new AddSotr(null));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var del = SotrDG.SelectedItems.Cast<Sotr>().ToList();
            foreach (var deleted in del)
                if (Connect.context.Yslyga.Any(x => x.Id_sotr == deleted.Id_sotr))
                {
                    MessageBox.Show("Данные используются в таблице выполнения услуг", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (MessageBox.Show($"Удалить {del.Count} записей?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Connect.context.Sotr.RemoveRange(del);
            }
            try
            {
                Connect.context.SaveChanges();
                SotrDG.ItemsSource = Connect.context.Sotr.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.GoBack();
        }
    }
}
