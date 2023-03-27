using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
using System.Xml.Linq;
using itogLAB.farmDataSetTableAdapters;

namespace itogLAB
{
    public partial class AdmCheck : Page
    {
        chekTableAdapter check = new chekTableAdapter();
        data_chekTableAdapter data_check = new data_chekTableAdapter();   
        userTableAdapter us = new userTableAdapter();
        productTableAdapter product = new productTableAdapter();
        public AdmCheck()
        {
            InitializeComponent();
            checkGrid.ItemsSource = check.GetData();
            data_chechGrid.ItemsSource = data_check.GetData();
            //ввод/вывод информации для таблицы сheck
            userId.ItemsSource = us.GetData();
            userId.DisplayMemberPath = "role id";
            userId.SelectedValuePath = "user id";

            data_checkId.ItemsSource = data_check.GetData();
            data_checkId.DisplayMemberPath = "data chek id";
            data_checkId.SelectedValuePath = "data chek id";

            productId.ItemsSource = product.GetData();
            productId.DisplayMemberPath = "name_product";
            productId.SelectedValuePath = "product id";
        }

        private void checkGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (checkGrid.SelectedItem != null)
            {
                var checkselect = checkGrid.SelectedItem as DataRowView;
                data_checkId.SelectedValue = (int)checkselect.Row[1];
                productId.SelectedValue = (int)checkselect.Row[2];
            }
        }

        private void data_chechGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (data_chechGrid.SelectedItem != null)
            {
                var data_checkselect = data_chechGrid.SelectedItem as DataRowView;
                time.Text = Convert.ToDateTime(data_checkselect.Row[1]).ToShortDateString();
                userId.SelectedValue = (int)data_checkselect.Row[2];
                itogPrice.Text = Convert.ToString(data_checkselect.Row[3]);
            }
        }

    }
}
