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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using itogLAB.farmDataSetTableAdapters;

namespace itogLAB
{
    public partial class AdmDataFarm : Page
    {
        //подключаем таблицы, с которыми будем взаимодействовать в нашей программе
        data_farmTableAdapter dFarm = new data_farmTableAdapter();
        branchTableAdapter branch = new branchTableAdapter();
        contactsTableAdapter contact = new contactsTableAdapter();
        roleTableAdapter role = new roleTableAdapter();
        data_chekTableAdapter datCheck = new data_chekTableAdapter();
        productTableAdapter product = new productTableAdapter();    
        public AdmDataFarm()
        {
            InitializeComponent();
            //выводим информацию с трёх основных таблиц, относящихся к группе данной странице
            Date.ItemsSource = dFarm.GetData();
            Branch.ItemsSource = branch.GetData();
            Contacts.ItemsSource = contact.GetData();
            //ввод/вывод информации для таблицы data_farm
            branID.ItemsSource = branch.GetData();
            branID.DisplayMemberPath = "city";
            branID.SelectedValuePath = "branch id";

            roleID.ItemsSource = role.GetData();
            roleID.DisplayMemberPath = "name_role";
            roleID.SelectedValuePath = "role id";

            data_checkID.ItemsSource = datCheck.GetData();
            data_checkID.DisplayMemberPath = "data chek id";
            data_checkID.SelectedValuePath = "data chek id";

            productID.ItemsSource = product.GetData();
            productID.DisplayMemberPath = "name_product";
            productID.SelectedValuePath = "product id";

            //ввод/вывод информации для таблицы contacts
            branchID.ItemsSource = branch.GetData();
            branchID.DisplayMemberPath = "city";
            branchID.SelectedValuePath = "branch id";
        }

        private void Date_SelectionChanged(object sender, SelectionChangedEventArgs e)        
        {
            if (Date.SelectedItem != null)
            {
                var dataselect = Date.SelectedItem as DataRowView;
                branID.SelectedValue = (int)dataselect.Row[1];
                roleID.SelectedValue = (int)dataselect.Row[2];
                data_checkID.SelectedValue = (int)dataselect.Row[3];
                productID.SelectedValue = (int)dataselect.Row[4];
            }
        }
        private void Branch_SelectionChanged(object sender, SelectionChangedEventArgs e)        
        {
            if (Branch.SelectedItem != null)
            {
                var branchselect = Branch.SelectedItem as DataRowView;
                city.Text = (string)branchselect.Row[1];
                street.Text = (string)branchselect.Row[2];
                home.Text = Convert.ToString(branchselect.Row[3]);
            }
        }
        private void Contacts_SelectionChanged(object sender, SelectionChangedEventArgs e)        
        {
            if (Contacts.SelectedItem != null)
            {
                var contactselect = Contacts.SelectedItem as DataRowView;
                email.Text = (string)contactselect.Row[1];
                branchID.SelectedValue = (int)contactselect.Row[2];
                number.Text = Convert.ToString(contactselect.Row[3]);
            }
        }
        //логика кнопок таблицы data_farm
        private void insert1Click(object sender, RoutedEventArgs e)//добавление данных в таблицу data_farm
        {
            dFarm.InsertQuery(Convert.ToInt32(branID.SelectedValue), Convert.ToInt32(roleID.SelectedValue),
                    Convert.ToInt32(data_checkID.SelectedValue), Convert.ToInt32(productID.SelectedValue));
            Date.ItemsSource = dFarm.GetData();
        }
        private void update1Click(object sender, RoutedEventArgs e)//изменение данных в data_farm
        {
            if (Date.SelectedItem != null)
            {
                var data = Date.SelectedItem as DataRowView;
                dFarm.UpdateQuery(Convert.ToInt32(branID.SelectedValue), Convert.ToInt32(roleID.SelectedValue),
                    Convert.ToInt32(data_checkID.SelectedValue), Convert.ToInt32(productID.SelectedValue), Convert.ToInt32(data.Row[0]));
                Date.ItemsSource = dFarm.GetData();
            }
        }
        private void delete1Click(object sender, RoutedEventArgs e)//удаление данных в data_farm
        {
            int id = (int)(Date.SelectedItem as DataRowView).Row[0];
            dFarm.DeleteQuery(id);
            Date.ItemsSource = dFarm.GetData();
        }
        //логика кнопок таблицы branch
        private void insert2Click(object sender, RoutedEventArgs e)
        {   
            if (city.Text == "" && street.Text == "" && home.Text == "")
            {
                errorBranch.Text = "Не все поля заполнены";
            }
            else
            {
                branch.InsertQuery(city.Text, street.Text, Convert.ToInt32(home.Text));
                Branch.ItemsSource = branch.GetData();

                branID.ItemsSource = branch.GetData();
                branchID.ItemsSource = branch.GetData();
            }
        }
        private void update2Click(object sender, RoutedEventArgs e)
        {
            if (city.Text == "" && street.Text == "" && home.Text == "")
            {
                errorBranch.Text = "Не все поля заполнены";
            }
            else 
            {
                if (Branch.SelectedItem != null)
                {
                    var adress = Branch.SelectedItem as DataRowView;
                    branch.UpdateQuery(city.Text, street.Text, Convert.ToInt32(home.Text), Convert.ToInt32(adress.Row[0]));
                    Branch.ItemsSource = branch.GetData();
                    branID.ItemsSource = branch.GetData();
                    branchID.ItemsSource = branch.GetData();
                }         
            }
        }
        private void delete2Click(object sender, RoutedEventArgs e)
        {
            int id = (int)(Branch.SelectedItem as DataRowView).Row[0];
            branch.DeleteQuery(id);
            Branch.ItemsSource = branch.GetData();
            branID.ItemsSource = branch.GetData();
            branchID.ItemsSource = branch.GetData();
        }
        //логика кнопок таблицы contacts
        private void insert3Click(object sender, RoutedEventArgs e)
        {
            string regex = "[0-9]{3}[0-9]{3}[0-9]{4}";
            if (email.Text == "" && number.Text == "")
            {
                errorContacts.Text = "Не все поля заполнены";
            }
            else if (Regex.IsMatch(number.Text, regex, RegexOptions.IgnoreCase) == false)
            {
                errorContacts.Text = "В поле номера телефона введён не номер";
            }
            else
            {
                contact.InsertQuery(email.Text, (int)Convert.ToInt64(number.Text), (int)Convert.ToInt32(branchID.SelectedValue));
                Contacts.ItemsSource = contact.GetData();
            }
        }
        private void update3Click(object sender, RoutedEventArgs e)
        {
            string regex = "[0-9]{3}[0-9]{3}[0-9]{4}";
            if (email.Text == "" && number.Text == "")
            {
                errorContacts.Text = "Не все поля заполнены";
            }
            else if (Regex.IsMatch(number.Text, regex, RegexOptions.IgnoreCase) == false)
            {
                errorContacts.Text = "В поле номера телефона введён не номер";
            }
            else
            {
                if (Branch.SelectedItem != null)
                {
                    var cont = Contacts.SelectedItem as DataRowView;
                    contact.UpdateQuery(email.Text, (int)Convert.ToInt64(number.Text), (int)branchID.SelectedValue, (int)cont.Row[0]);
                    Contacts.ItemsSource = contact.GetData();
                }
            }
        }
        private void delete3Click(object sender, RoutedEventArgs e)
        {
            int id = (int)(Contacts.SelectedItem as DataRowView).Row[0];
            contact.DeleteQuery(id);
            Contacts.ItemsSource = contact.GetData();
        }
    }
}
