using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Vers0.Model;
using Vers0.ViewModel;
using System.Data.Entity;
using System.Collections.ObjectModel;
using static Vers0.AppProperties;
using System.Globalization;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace Vers0
{
    public partial class MainWindow : Window
    {
        string season;
        TableFunc table = new TableFunc();
        AppProperties appProperties = new AppProperties();
        bool isEndSeason = false;

        private bool isStartNewSeason(string season)
        {
            if (season == "Зима")
            {
                if (DateTime.Now.Month == 11 && DateTime.Now.Day >= 17)
                    return true;
            }
            if (season == "Весна")
            {
                if (DateTime.Now.Month == 2 && DateTime.Now.Day >= 17)
                    return true;
            }
            if (season == "Лето")
            {
                if (DateTime.Now.Month == 5 && DateTime.Now.Day >= 17)
                    return true;
            }
            if (season == "Осень")
            {
                if (DateTime.Now.Month == 8 && DateTime.Now.Day >= 17)
                    return true;
            }
            return false;
        }

        public MainWindow()
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            authorizationWindow.ShowDialog();
            DataContext = authorizationWindow.DataContext;
            if ((DataContext as AuthorizationViewModel).authorizationAccess == false) Close();
            InitializeComponent();
            seasonProduct.ItemsSource = appProperties.seasons;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;
            switch (month)
            {
                case 2: 
                    if (day>=17)
                        isEndSeason = true;
                    season = "Зима";
                    break;
                case 12:
                case 1:
                    season = "Зима";
                    break;
                case 5:
                    if (day >= 17)
                        isEndSeason = true;
                    season = "Весна";
                    break;
                case 3:
                case 4:
                    season = "Весна";
                    break;
                case 8:
                    if (day >= 17)
                        isEndSeason = true;
                    season = "Лето";
                    break;
                case 6:
                case 7:
                    season = "Лето";
                    break;
                case 11:
                    if (day >= 17)
                        isEndSeason = true;
                    season = "Осень";
                    break;
                case 9:
                case 10:
                    season = "Осень";
                    break;
            }
        }
        private void productsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainTable.Columns.Clear();
            tableTitle.Content = "Товары";
            deleteBtn.IsEnabled = true;
            createBtn.IsEnabled = true;
            period.Visibility = Visibility.Hidden;
            foreach (DataGridColumn item in MyTableColumns.productTable())
            {
                MainTable.Columns.Add(item);
            }
            MainTable.ItemsSource = table.getProducts();
        }

        private void contractorMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainTable.Columns.Clear();
            tableTitle.Content = "Контрагенты";
            deleteBtn.IsEnabled = true;
            createBtn.IsEnabled = true;
            period.Visibility = Visibility.Hidden;
            foreach (DataGridColumn item in MyTableColumns.contractorTable())
            {
                MainTable.Columns.Add(item);
            }
            MainTable.ItemsSource = table.getContractor();
        }

        private void usersMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainTable.Columns.Clear();
            tableTitle.Content = "Пользователи";
            deleteBtn.IsEnabled = true;
            createBtn.IsEnabled = true;
            period.Visibility = Visibility.Hidden;
            foreach (DataGridColumn item in MyTableColumns.userTable())
            {
                MainTable.Columns.Add(item);
            }
            MainTable.ItemsSource = table.getUsers();
        }

        private void ordersToContractorsMenuItem_Click(object sender, EventArgs e)
        {
            MainTable.Columns.Clear();
            tableTitle.Content = "Заказы поставщикам";
            deleteBtn.IsEnabled = true;
            createBtn.IsEnabled = true;
            period.Visibility = Visibility.Hidden;
            foreach (DataGridColumn item in MyTableColumns.ordersTable())
            {
                MainTable.Columns.Add(item);
            }

            MainTable.ItemsSource = table.getOrdersToContractors();
        }

        private void customerOrdersMenuItem_Click(object sender, EventArgs e)
        {
            MainTable.Columns.Clear();
            tableTitle.Content = "Заказы покупателей";
            deleteBtn.IsEnabled = true;
            createBtn.IsEnabled = true;
            period.Visibility = Visibility.Hidden;
            foreach (DataGridColumn item in MyTableColumns.ordersTable())
            {
                MainTable.Columns.Add(item);
            }

            MainTable.ItemsSource = table.getCustomerOrders();
        }

        private void supplyMenuItem_Click(object sender, EventArgs e)
        {
            MainTable.Columns.Clear();
            tableTitle.Content = "Поступления";
            deleteBtn.IsEnabled = true;
            createBtn.IsEnabled = true;
            period.Visibility = Visibility.Hidden;
            foreach (DataGridColumn item in MyTableColumns.ordersTable())
            {
                MainTable.Columns.Add(item);
            }

            MainTable.ItemsSource = table.getSupply();
        }

        private void balancesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainTable.Columns.Clear();
            tableTitle.Content = "Остатки";
            deleteBtn.IsEnabled = false;
            createBtn.IsEnabled = false;
            period.Visibility = Visibility.Hidden;
            foreach (DataGridColumn item in MyTableColumns.balancesTable())
            {
                MainTable.Columns.Add(item);
            }

            MainTable.ItemsSource = table.getBalances();
        }

        private void statisticsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainTable.Columns.Clear();
            tableTitle.Content = "Статистика";
            deleteBtn.IsEnabled = false;
            createBtn.IsEnabled = false;
            period.Visibility = Visibility.Visible;
            startDate.SelectedDate = DateTime.Now.AddMonths(-1);
            endDate.SelectedDate = DateTime.Now;
            foreach (DataGridColumn item in MyTableColumns.statisticTable())
            {
                MainTable.Columns.Add(item);
            }           
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            table.delete(MainTable.SelectedItem);
        }

        private void createBtn_Click(object sender, RoutedEventArgs e)
        {
            if (tableTitle.Content.ToString() == "Товары")
            {
                namePanel.Content = "Создание товара";
                createProductPanel.DataContext = null;
                createProductPanel.Visibility = Visibility.Visible;
                createConstractorPanel.Visibility = Visibility.Collapsed;
                createUserPanel.Visibility = Visibility.Collapsed;
            }
            else if (tableTitle.Content.ToString() == "Контрагенты")
            {
                namePanel1.Content = "Создание контрагента";
                createConstractorPanel.DataContext = null;
                createConstractorPanel.Visibility = Visibility.Visible;
                createProductPanel.Visibility = Visibility.Collapsed;
                createUserPanel.Visibility = Visibility.Collapsed;
                type_c.IsEnabled = true;
            }
            else if (tableTitle.Content.ToString() == "Заказы поставщикам" || tableTitle.Content.ToString() == "Заказы покупателей" || tableTitle.Content.ToString() == "Поступления")
            {
                ordersDTO o = new ordersDTO();
                OrderWindow createOrder = new OrderWindow(o);
                createOrder.Closed += EditOrder_Closed;
                createOrder.ShowDialog();
            }
            else if (tableTitle.Content.ToString() == "Пользователи")
            {
                namePanel2.Content = "Создание пользователя";
                createUserPanel.DataContext = null;
                createConstractorPanel.Visibility = Visibility.Collapsed;
                createProductPanel.Visibility = Visibility.Collapsed;
                createUserPanel.Visibility = Visibility.Visible;
                passwordUser.IsEnabled = true;
            }
        }

        private void closeProductPanel_Click(object sender, RoutedEventArgs e)
        {
            createProductPanel.Visibility = Visibility.Collapsed;
            createProductPanel.DataContext = null;
            articleProduct.Text = string.Empty;
            nameProduct.Text = string.Empty;
            priceProduct.Text = string.Empty;
            minBalanceProduct.Text = string.Empty;
            descriptionProduct.Text = string.Empty;
            shelfLifeProduct.Text = string.Empty;
            unitProduct.SelectedIndex = -1;
            seasonProduct.SelectedIndex = -1;
            createConstractorPanel.Visibility= Visibility.Collapsed;
            createUserPanel.Visibility= Visibility.Collapsed;
        }

        private void closeContractorPanel_Click(object sender, RoutedEventArgs e)
        {           
            createConstractorPanel.Visibility = Visibility.Collapsed;
            fullName_c.Text = string.Empty;
            INN_c.Text= string.Empty;
            address_c.Text = string.Empty;
            bic_c.Text = string.Empty;
            bank_c.Text = string.Empty;
            corrAcc_c.Text = string.Empty;
            acc_c.Text = string.Empty;
            type_c.SelectedIndex=-1;
            KPP_c.Text = string.Empty;
            OGRNIP_c.Text = string.Empty;
            surname_c.Text = string.Empty;
            name_c.Text = string.Empty;
            m_name_c.Text = string.Empty;
            OGRN_c.Text = string.Empty;
        }

        private void closeUserPanel_Click(object sender, RoutedEventArgs e)
        {
            createUserPanel.Visibility = Visibility.Collapsed;
            usernameUser.Text = string.Empty;
            passwordUser.Password = string.Empty;
            surnameUser.Text = string.Empty;
            nameUser.Text = string.Empty;
            m_nameUser.Text = string.Empty;
            isAdminUser.IsChecked = false;
        }

        private void BtnCreateProduct_Click(object sender, RoutedEventArgs e)
        {
            product p = new product();
            if (namePanel.Content.ToString() == "Редактирование товара")
                p = (product)createProductPanel.DataContext;
            try
            {
                p.article = articleProduct.Text;
                p.name_product = nameProduct.Text;
                p.unit_of_measurement = unitProduct.Text;
                p.sale_price = decimal.Parse(priceProduct.Text, CultureInfo.InvariantCulture);
                p.minimum_balance = int.Parse(minBalanceProduct.Text);
                p.count = 0;
                p.deleted = false;
                if (!string.IsNullOrEmpty(descriptionProduct.Text))
                    p.description_product = descriptionProduct.Text;
                else
                    p.description_product = null;
                if (!string.IsNullOrEmpty(shelfLifeProduct.Text))
                    p.Shelf_life = int.Parse(shelfLifeProduct.Text);
                else
                    p.Shelf_life = null;
                if (!string.IsNullOrEmpty(seasonProduct.Text))
                    p.season = seasonProduct.Text;
                else
                    p.season = null;
            }
            catch
            {
                MessageBox.Show("Не все обязательные поля заполнены!", namePanel.Content.ToString());
                return;
            }


            try 
            {
                if (namePanel.Content.ToString() == "Создание товара")
                {
                    if (table.create(p))
                    {
                        MessageBox.Show("Товар добавлен!", namePanel.Content.ToString());
                        closeProductPanel_Click(sender, e);
                    }
                }
                else if (namePanel.Content.ToString()=="Редактирование товара")
                {
                    if (table.edit(p))
                    {
                        MessageBox.Show("Товар изменен!", namePanel.Content.ToString());
                        closeProductPanel_Click(sender, e);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, namePanel.Content.ToString());
            }      
        }

        private void BtnCreateContractor_Click(object sender, RoutedEventArgs e)
        {
            contractor c = new contractor();
            if (namePanel1.Content.ToString() == "Редактирование контрагента") 
            {               
                c = (contractor)createConstractorPanel.DataContext;
            }
            try
            {
                c.INN = INN_c.Text;
                c.type_contractor = type_c.SelectedValue.ToString();
                c.address_contractor = address_c.Text;
                c.RCBIC = bic_c.Text;
                c.bank = bank_c.Text;
                c.corr_acc = corrAcc_c.Text;
                c.acc = acc_c.Text;
                c.designation_contractor = fullName_c.Text;
                if (!string.IsNullOrEmpty(KPP_c.Text))
                    c.KPP = KPP_c.Text;
                else
                    c.KPP = null;
                if (!string.IsNullOrEmpty(OGRN_c.Text))
                    c.OGRN = OGRN_c.Text;
                else
                    c.OGRN = null;
                if (!string.IsNullOrEmpty(surname_c.Text))
                    c.surname = surname_c.Text;
                else c.surname = null;
                if (!string.IsNullOrEmpty(name_c.Text))
                    c.name_contractor = name_c.Text;
                else c.name_contractor = null;
                if (!string.IsNullOrEmpty(m_name_c.Text))
                    c.middle_name = m_name_c.Text;
                else c.middle_name = null;
                if (dateBirth_c.SelectedDate.HasValue)
                    c.date_birth = dateBirth_c.SelectedDate;
                else
                    c.date_birth = null;
                if (!string.IsNullOrEmpty(OGRNIP_c.Text))
                    c.OGRNIP = OGRNIP_c.Text;
                else c.OGRNIP = null;
                c.deleted = false;
            }
            catch
            {
                MessageBox.Show("Не все обязательные поля заполнены!", namePanel.Content.ToString());
                return;
            }

            try
            {
                if (namePanel1.Content.ToString() == "Создание контрагента")
                {
                    if (table.create(c))
                    {
                        MessageBox.Show("Контрагент добавлен!", namePanel.Content.ToString());
                    }
                }
                else if (namePanel1.Content.ToString() == "Редактирование контрагента")
                {
                    if (table.edit(c))
                    {
                        MessageBox.Show("Контрагент изменен!", namePanel.Content.ToString());
                    }
                }
                closeContractorPanel_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, namePanel1.Content.ToString());
            }
        }

        private void BtnCreateUser_Click(object sender, RoutedEventArgs e)
        {
            users u = new users();
            if (namePanel2.Content.ToString() == "Редактирование пользователя")
            {
                u = (users)createUserPanel.DataContext;
            }

            try
            {
                u.username = usernameUser.Text;
                if (namePanel.Content.ToString() != "Редактирование пользователя")
                    u.password = passwordUser.Password;
                if (!string.IsNullOrEmpty(surnameUser.Text))
                    u.surname = surnameUser.Text;
                else
                    u.surname = null;
                if (!string.IsNullOrEmpty(nameUser.Text))
                    u.name_user = nameUser.Text;
                else
                    u.name_user = null;
                if (!string.IsNullOrEmpty(m_nameUser.Text))
                    u.middle_name = m_nameUser.Text;
                else
                    u.middle_name = null;
                u.is_admin = (bool)isAdminUser.IsChecked;
                u.deleted = false;
            }
            catch
            {
                MessageBox.Show("Не все обязательные поля заполнены!", namePanel2.Content.ToString());
                return;
            }


            try
            {
                if (namePanel2.Content.ToString() == "Создание пользователя")
                {
                    if (table.create(u))
                    {
                        MessageBox.Show("Пользователь добавлен!", namePanel2.Content.ToString());
                        closeUserPanel_Click(sender, e);
                    }
                }
                else if (namePanel2.Content.ToString() == "Редактирование пользователя")
                {
                    if (table.edit(u))
                    {
                        MessageBox.Show("Пользователь изменен!", namePanel2.Content.ToString());
                        closeUserPanel_Click(sender, e);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, namePanel2.Content.ToString());
            }
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MainTable.SelectedItem != null)
            {
                if (MainTable.SelectedItem is product p)
                {
                    createBtn_Click(sender, e);
                    createProductPanel.DataContext = p;
                    namePanel.Content = "Редактирование товара";
                }
                else if (MainTable.SelectedItem is contractor c)
                {
                    createBtn_Click(sender, e);
                    createConstractorPanel.DataContext = c;
                    namePanel1.Content = "Редактирование контрагента";
                    type_c.IsEnabled = false;
                }
                else if(MainTable.SelectedItem is ordersDTO order)
                {
                    OrderWindow editOrder = new OrderWindow(order);
                    editOrder.Closed += EditOrder_Closed;
                    editOrder.ShowDialog();
                }
                else if(MainTable.SelectedItem is users u)
                {
                    createBtn_Click(sender, e);
                    createUserPanel.DataContext = u;
                    namePanel2.Content = "Редактирование пользователя";
                    passwordUser.IsEnabled = false;
                }

            }
        }

        private void EditOrder_Closed(object sender, EventArgs e)
        {
            if (tableTitle.Content.ToString() == "Заказы поставщикам")
                ordersToContractorsMenuItem_Click(sender, e);
            else if (tableTitle.Content.ToString() == "Заказы покупателей")
                customerOrdersMenuItem_Click(sender, e);
            else if (tableTitle.Content.ToString() == "Поступления")
                supplyMenuItem_Click(sender, e);
        }

        private void startDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            MainTable.ItemsSource = table.getStatistic(startDate.SelectedDate, endDate.SelectedDate).ToList();
        }

        private void MainTable_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (e.Row.Item is balances_Result b)
            {
                if (b.to_order >0)
                {
                    Style redStyle = new Style(typeof(DataGridRow));
                    redStyle.Setters.Add(new Setter(BackgroundProperty, Brushes.Salmon));
                    e.Row.Style = redStyle;
                }
                if (b.season != season && b.season != null && !isStartNewSeason(b.season))
                {
                    Style greyStyle = new Style(typeof(DataGridRow));
                    greyStyle.Setters.Add(new Setter(BackgroundProperty, Brushes.LightGray));
                    e.Row.Style = greyStyle;
                }
                if (b.season == season && isEndSeason)
                {
                    Style blueStyle = new Style(typeof(DataGridRow));
                    blueStyle.Setters.Add(new Setter(BackgroundProperty, Brushes.LightBlue));
                    e.Row.Style = blueStyle;
                }
                if(b.date_of_debiting != null)
                {
                    if(b.date_of_debiting.Value.Month-DateTime.Now.Month<=appProperties.expiration_date && b.date_of_debiting.Value.Year == DateTime.Now.Year)
                    {
                        Style yellowStyle = new Style(typeof(DataGridRow));
                        yellowStyle.Setters.Add(new Setter(BackgroundProperty, Brushes.LightYellow));
                        e.Row.Style = yellowStyle;
                    }
                }
            }
        }

        private void type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (type_c.SelectedValue is null)
            {
                legal_entity.Visibility = Visibility.Collapsed;
                FIO.Visibility = Visibility.Collapsed;
                natural_person.Visibility = Visibility.Collapsed;
                individual_entrepreneur.Visibility = Visibility.Collapsed;
            }
            else if (type_c.SelectedValue.ToString()== "Юридическое лицо")
            {
                legal_entity.Visibility = Visibility.Visible;
                FIO.Visibility = Visibility.Collapsed;
                natural_person.Visibility = Visibility.Collapsed;
                individual_entrepreneur.Visibility = Visibility.Collapsed;
            }
            else if (type_c.SelectedValue.ToString() == "Физическое лицо")
            {
                legal_entity.Visibility = Visibility.Collapsed;
                FIO.Visibility = Visibility.Visible;
                natural_person.Visibility = Visibility.Visible;
                individual_entrepreneur.Visibility = Visibility.Collapsed;
            }
            else if (type_c.SelectedValue.ToString() == "Индивидуальный предприниматель")
            {
                legal_entity.Visibility = Visibility.Collapsed;
                FIO.Visibility = Visibility.Visible;
                natural_person.Visibility = Visibility.Collapsed;
                individual_entrepreneur.Visibility = Visibility.Visible;
            }
           
        }
    } 
}
