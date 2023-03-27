using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;
using itogLAB.farmDataSetTableAdapters;

namespace itogLAB
{
    public partial class users : Window
    {
        productTableAdapter product = new productTableAdapter();
        type_productTableAdapter type = new type_productTableAdapter();
        contactsTableAdapter conta = new contactsTableAdapter();
        branchTableAdapter branch = new branchTableAdapter();
        public users()
        {
            InitializeComponent();
            //указываем из какой таблицы и какие данные надо вывводить в комбобоксы
            branID.ItemsSource = branch.GetData();
            branID.DisplayMemberPath = "city";
            branID.SelectedValuePath = "branch id";
            twoID.ItemsSource = type.GetData();
            twoID.DisplayMemberPath = "name_type";
            twoID.SelectedValuePath = "type_product id";   
        }
        private void Button_Click(object sender, RoutedEventArgs e)//кнопка возвращения к окну авторизации с закрытием окна пользователя
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }
        private void Button2_Click(object sender, RoutedEventArgs e)//вызов при нажатии таблицы с продуктами
        {prod.ItemsSource = product.GetData(); }

        private void Button3_Click(object sender, RoutedEventArgs e)// вызов при нажатии таблицы с контактами фермера/фермеров
        {GridCont.ItemsSource= conta.GetData(); }
        //показываем куда и какие данные надо выводить при нажатии на строки таблиц
        private void prod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (prod.SelectedItem != null)
            {
                var prodselect = prod.SelectedItem as DataRowView;
                name.Text = (string)prodselect.Row[1];
                twoID.SelectedValue = (int)prodselect.Row[2];
                price.Text = Convert.ToString(prodselect.Row[3]);
            }
        }
        private void GridCont_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GridCont.SelectedItem != null)
            {
                var contactselect = GridCont.SelectedItem as DataRowView;
                email.Text = (string)contactselect.Row[1];
                branID.SelectedValue = (int)contactselect.Row[2];
                number.Text = Convert.ToString(contactselect.Row[3]);
            }
        }
    }
}
