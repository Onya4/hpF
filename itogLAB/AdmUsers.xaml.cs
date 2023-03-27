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
    public partial class AdmUsers : Page
    {
        roleTableAdapter Roll = new roleTableAdapter();
        authorizationTableAdapter Auth = new authorizationTableAdapter();
        userTableAdapter us = new userTableAdapter();
        public AdmUsers()
        {
            InitializeComponent();
            roleGrid.ItemsSource = Roll.GetData();
            autGrid.ItemsSource = Auth.GetData();
            //ввод/вывод информации для таблицы authorization (в комбобокс)
            userBox.ItemsSource = us.GetData();
            userBox.DisplayMemberPath = "role id";
            userBox.SelectedValuePath = "user id";

        }

        private void role_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (roleGrid.SelectedItem != null)
            {
                var roleselect = roleGrid.SelectedItem as DataRowView;
                role.Text = (string)roleselect.Row[1];
            }
        }
        private void aut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (autGrid.SelectedItem != null)
            {
                var autselect = autGrid.SelectedItem as DataRowView;
                log.Text = (string)autselect.Row[1];
                pas.Text = Convert.ToString(autselect.Row[2]);
                userBox.SelectedValue = (int)autselect.Row[3];
            }
        }
        //логика кнопок для таблицы role
        private void insertRole_Click(object sender, RoutedEventArgs e)
        {
            if (role.Text == "")
            {
                errorRole.Text = "Не все поля заполнены";
            }
            else
            {
                Roll.InsertQuery(role.Text);
                roleGrid.ItemsSource = Roll.GetData();
            }
        }
        private void updataRole_Click(object sender, RoutedEventArgs e)
        {
            if (role.Text == "")
            {
                errorRole.Text = "Не все поля заполнены";
            }
            else
            {
                if (roleGrid.SelectedItem != null)
                {
                    var role2 = roleGrid.SelectedItem as DataRowView;
                    Roll.UpdateQuery(role.Text, Convert.ToInt32(role2.Row[0]));
                    roleGrid.ItemsSource = Roll.GetData();
                }
            }
        }
        private void deleteRole_Click(object sender, RoutedEventArgs e)
        {
            int id = (int)(roleGrid.SelectedItem as DataRowView).Row[0];
            Roll.DeleteQuery(id);
            roleGrid.ItemsSource = Roll.GetData();
        }
        //логика для кнопок таблицы authorization
        private void insertAut_Click(object sender, RoutedEventArgs e)
        {
            if (log.Text == "" && pas.Text == "")
            {
                errorAut.Text = "Не все поля заполнены";
            }
            else
            {
                Auth.InsertQuery(log.Text, Convert.ToInt32(pas.Text), Convert.ToInt32(userBox.SelectedValue));
                autGrid.ItemsSource = Auth.GetData();
            }
        }

        private void updataAut_Click_1(object sender, RoutedEventArgs e)
        {
            if (log.Text == "" && pas.Text == "")
            {
                errorAut.Text = "Не все поля заполнены";
            }
            else
            {
                if (autGrid.SelectedItem != null)
                {
                    var auth2 = autGrid.SelectedItem as DataRowView;
                    Auth.UpdateQuery(log.Text, Convert.ToInt32(pas.Text), Convert.ToInt32(userBox.SelectedValue), Convert.ToInt32(auth2.Row[0]));
                    autGrid.ItemsSource = Auth.GetData();
                }
            }
        }

        private void deleteAut_Click_1(object sender, RoutedEventArgs e)
        {
            int id = (int)(autGrid.SelectedItem as DataRowView).Row[0];
            Auth.DeleteQuery(id);
            autGrid.ItemsSource = Auth.GetData();
        }
    }
}
