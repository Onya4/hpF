using System;
using System.Collections.Generic;
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
    public partial class MainWindow : Window
    {
        //подключаем таблицу с логинами и паролями
        authorizationTableAdapter aut = new authorizationTableAdapter();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
            //прописываем логику для кнопки
        {
            var allaut = aut.GetData().Rows;//сохраняем данные из таблицы в DataGrid
            for (int i = 0; i < allaut.Count; i++)//запускаем цикл проверки введённых данных с данными из таблицы
            {
                //осущесвляем саму проверку данных
                if (allaut[i][1].ToString() == log.Text && allaut[i][2].ToString() == pas.Password)
                {
                    //проверяем к какой роли относиться пользователь и открываем соответствующее окно
                    int roleID = (int)allaut[i][3];
                    switch (roleID)
                    {
                        case 2:
                            boss admin = new boss();
                            admin.Show();
                            break;
                        case 4:
                            users user = new users();
                            user.Show();
                            break;
                    }
                    Close();
                }
                else
                {
                //в случае ошибки выводим данный текст
                    error.Text = "Введено неверное значение";
                }
            }
        }
    }
}
