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
    /// Логика взаимодействия для obor.xaml
    /// </summary>
    public partial class obor : Page
    {
        public obor()
        {
            InitializeComponent();
            OborDG.ItemsSource = Connect.context.Obor.ToList();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            OborDG.ItemsSource = Connect.context.Obor.ToList();
        }

        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            OborDG.ItemsSource = Connect.context.Obor.OrderBy(x => x.Naimen_Obor).ToList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new AddObor(null));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var del = OborDG.SelectedItems.Cast<Obor>().ToList();
            foreach (var deleted in del)
                if (Connect.context.Yslyga.Any(x => x.Id_Obor == deleted.Id_Obor))
                {
                    MessageBox.Show("Данные используются в таблице выполнения услуг", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (MessageBox.Show($"Удалить {del.Count} записей?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Connect.context.Obor.RemoveRange(del);
            }
            try
            {
                Connect.context.SaveChanges();
                OborDG.ItemsSource = Connect.context.Obor.ToList();
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

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new AddObor((sender as Button).DataContext as Obor));
        }
    }
}
