using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using itogLAB.farmDataSetTableAdapters;

namespace itogLAB
{
    public partial class AdmPriduct : Page
    {
        productTableAdapter product = new productTableAdapter();
        type_productTableAdapter type = new type_productTableAdapter();

        public AdmPriduct()
        {
            InitializeComponent();
            prodGrid.ItemsSource = product.GetData();
            typeGrid.ItemsSource = type.GetData();
            //ввод/вывод информации для таблицы data_farm (в комбобокс)
            typebox.ItemsSource = type.GetData();
            typebox.DisplayMemberPath = "name_type";
            typebox.SelectedValuePath = "type_product id";
        }
        private void prod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (prodGrid.SelectedItem != null)
            {
                var prodselect = prodGrid.SelectedItem as DataRowView;
                name.Text = (string)prodselect.Row[1];
                typebox.SelectedValue = (int)prodselect.Row[2];
                price.Text = Convert.ToString(prodselect.Row[3]);
            }
        }
        private void type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (typeGrid.SelectedItem != null)
            {
                var typeselect = typeGrid.SelectedItem as DataRowView;
                name_type.Text = (string)typeselect.Row[1];
            }
        }
        //логика кнопок для таблицы product
        private void insertProd_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text == "" && price.Text == "") 
            {
                errorProduct.Text = "Не все поля заполнены";
            }
            else
            {
                product.InsertQuery(name.Text, Convert.ToInt32(typebox.SelectedValue), Convert.ToDecimal(price.Text));
                prodGrid.ItemsSource = product.GetData();
            }
        }
        private void updataProd_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text == "" && price.Text == "")
            {
                errorProduct.Text = "Не все поля заполнены";
            }
            else
            {
                if (prodGrid.SelectedItem != null)
                {
                    var prod2 = prodGrid.SelectedItem as DataRowView;
                    product.UpdateQuery(name.Text, Convert.ToInt32(typebox.SelectedValue), Convert.ToDecimal(price.Text), Convert.ToInt32(prod2.Row[0]));
                    prodGrid.ItemsSource = product.GetData();
                }
            }
        }
        private void deleteProd_Click(object sender, RoutedEventArgs e)
        {
            int id = (int)(prodGrid.SelectedItem as DataRowView).Row[0];
            product.DeleteQuery(id);
            prodGrid.ItemsSource = product.GetData();
        }
        //логика кнопок для таблицы type_product
        private void insertType_Click(object sender, RoutedEventArgs e)
        {
            if (name_type.Text == "")
            {
                errorType.Text = "Не все поля заполнены";
            }
            else
            {
                type.InsertQuery(name_type.Text);
                typeGrid.ItemsSource = type.GetData();
                typebox.ItemsSource = type.GetData();
            }
        }
        private void updataType_Click(object sender, RoutedEventArgs e)
        {
            if (name_type.Text == "")
            {
                errorType.Text = "Не все поля заполнены";
            }
            else
            {
                if (prodGrid.SelectedItem != null)
                {
                    var typeID = typeGrid.SelectedItem as DataRowView;
                    type.UpdateQuery(name_type.Text, Convert.ToInt32(typeID.Row[0]));
                    typeGrid.ItemsSource = type.GetData();
                    typebox.ItemsSource = type.GetData();
                }
            }
        }
        private void deleteType_Click(object sender, RoutedEventArgs e)
        {
            int id = (int)(typeGrid.SelectedItem as DataRowView).Row[0];
            type.DeleteQuery(id);
            typeGrid.ItemsSource = type.GetData();
            typebox.ItemsSource = type.GetData();
        }
    }
}
