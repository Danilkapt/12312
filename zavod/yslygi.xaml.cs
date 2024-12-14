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
using Excel = Microsoft.Office.Interop.Excel;

namespace zavod
{
    /// <summary>
    /// Логика взаимодействия для yslygi.xaml
    /// </summary>
    public partial class yslygi : Page
    {
        public yslygi()
        {
            InitializeComponent();
            YslygDG.ItemsSource = Connect.context.Yslyga.ToList();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            YslygDG.ItemsSource = Connect.context.Yslyga.ToList();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new AddYslygi((sender as Button).DataContext as Yslyga));
        }

        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            var s = Connect.context.Yslyga.ToList();
            YslygDG.ItemsSource = s.OrderBy(x => x.Date_Yslygi).ToList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new AddYslygi(null));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var del = YslygDG.SelectedItems.Cast<Yslyga>().ToList();
            if (MessageBox.Show($"Удалить {del.Count} записей?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Connect.context.Yslyga.RemoveRange(del);
            }
            try
            {
                Connect.context.SaveChanges();
                YslygDG.ItemsSource = Connect.context.Yslyga.ToList();
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

        private void Viborka_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new Viborka());
        }

        private void Otchet_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application app = new Excel.Application()
            {
                Visible = true,
                SheetsInNewWorkbook = 1
            };
            Excel.Workbook workbook = app.Workbooks.Add(Type.Missing);
            app.DisplayAlerts = false;
            Excel.Worksheet sheet = (Excel.Worksheet)app.Worksheets.get_Item(1);
            sheet.Name = "Оказание услуг";
            sheet.Cells[1, 1] = "ID Услуги";
            sheet.Cells[1, 2] = "Оборудование";
            sheet.Cells[1, 3] = "Сотрудник";
            sheet.Cells[1, 4] = "ID Отдела";
            sheet.Cells[1, 5] = "Название услуги";
            sheet.Cells[1, 6] = "Дата";
            var currentRow = 2;
            var s = Connect.context.Yslyga.Select(x =>
            new
            {
                Yslyga = x,
                Yslygas = x.Id_Yslygi,

            }).ToList();
            foreach (var item in s)
            {

                sheet.Cells[currentRow, 1] = item.Yslyga.Id_Yslygi;
                sheet.Cells[currentRow, 2] = item.Yslyga.Obor.Naimen_Obor;
                sheet.Cells[currentRow, 3] = item.Yslyga.Sotr.Surname;
                sheet.Cells[currentRow, 4] = item.Yslyga.Id_Otdela;
                sheet.Cells[currentRow, 5] = item.Yslyga.Name_Yslygi;
                sheet.Cells[currentRow, 6] = item.Yslyga.Date_Yslygi;

                currentRow++;
            }
            sheet.SaveAs("D:\\Okazanie_yslyg.xlsx");
        }
    }
}
