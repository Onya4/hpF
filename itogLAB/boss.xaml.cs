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
using System.Windows.Shapes;
using itogLAB.farmDataSetTableAdapters;

namespace itogLAB
{
    public partial class boss : Window
    {
        public boss()
        {InitializeComponent(); }
        private void dataFarmClick(object sender, RoutedEventArgs e)
        {pageNew.Content = new AdmDataFarm();}

        private void productClick(object sender, RoutedEventArgs e)
        {pageNew.Content = new AdmPriduct(); }

        private void usersClick(object sender, RoutedEventArgs e)
        { pageNew.Content = new AdmUsers();}

        private void CheckClick(object sender, RoutedEventArgs e)
        { pageNew.Content = new AdmCheck();}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
             cass cassa = new cass();
             cassa.Show();
        }

        private void authorReturn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }
    }
}
