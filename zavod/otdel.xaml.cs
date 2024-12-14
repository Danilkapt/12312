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
    /// Логика взаимодействия для otdel.xaml
    /// </summary>
    public partial class otdel : Page
    {
        public otdel()
        {
            InitializeComponent();
            OtdelDG.ItemsSource = Connect.context.Otdel.ToList();
        }

        private void Nazad_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.GoBack();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var del = OtdelDG.SelectedItems.Cast<Otdel>().ToList();
            foreach (var deleted in del)
                if (Connect.context.Yslyga.Any(x => x.Id_Otdela == deleted.Id_Otdela))
                {
                    MessageBox.Show("Данные используются в таблице выполнения услуг", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            if (MessageBox.Show($"Удалить {del.Count} записей?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Connect.context.Otdel.RemoveRange(del);
            }
            try
            {
                Connect.context.SaveChanges();
                OtdelDG.ItemsSource = Connect.context.Otdel.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new AddOtdel(null));
        }

        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            OtdelDG.ItemsSource = Connect.context.Otdel.OrderBy(x => x.Nazvanie_Otdela).ToList();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new AddOtdel((sender as Button).DataContext as Otdel));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            OtdelDG.ItemsSource = Connect.context.Otdel.ToList();
        }
    }
}
