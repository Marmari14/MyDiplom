using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Vers0.Converters;

namespace Vers0
{
    public class MyTableColumns
    {
        public static ObservableCollection<DataGridTextColumn> productTable()
        {
            ObservableCollection<DataGridTextColumn> columns = new ObservableCollection<DataGridTextColumn>();
            DataGridTextColumn column = new DataGridTextColumn();
            column.Header = "Наименование";
            column.Binding = new Binding("name_product");
            columns.Add(column);
            column = new DataGridTextColumn();
            column.Header = "Артикул";
            column.Binding = new Binding("article");
            columns.Add(column);
            column = new DataGridTextColumn();
            column.Header = "Единицы измерения";
            column.Binding = new Binding("unit_of_measurement");
            columns.Add(column);
            column = new DataGridTextColumn();
            column.Header = "Цена продажи";
            column.Binding = new Binding("sale_price");
            columns.Add(column);
            return columns;
        }

        public static ObservableCollection<DataGridTextColumn> contractorTable()
        {
            ObservableCollection<DataGridTextColumn> columns = new ObservableCollection<DataGridTextColumn>();
            DataGridTextColumn column = new DataGridTextColumn();
            column.Header = "Наименование";
            column.Binding = new Binding("designation_contractor");
            columns.Add(column);
            column = new DataGridTextColumn();
            column.Header = "Тип контрагента";
            column.Binding = new Binding("type_contractor");
            columns.Add(column);
            column = new DataGridTextColumn();
            column.Header = "Адрес";
            column.Binding = new Binding("address_contractor");
            columns.Add(column);
            return columns;
        }

        public static ObservableCollection<DataGridTextColumn> ordersTable()
        {
            ObservableCollection<DataGridTextColumn> columns = new ObservableCollection<DataGridTextColumn>();
            DataGridTextColumn column = new DataGridTextColumn();
            column.Header = "Номер";
            column.Binding = new Binding("number");
            columns.Add(column);
            column = new DataGridTextColumn();
            column.Header = "Дата";
            column.Binding = new Binding("order_date");
            column.Binding.StringFormat = "dd.MM.yyyy";
            columns.Add(column);
            column = new DataGridTextColumn();
            column.Header = "Контрагент";
            column.Binding = new Binding("contractor.name_c");
            column.Binding.StringFormat = "";
            columns.Add(column);
            column = new DataGridTextColumn();
            column.Header = "Сумма";
            column.Binding = new Binding("amount");
            columns.Add(column);
            column = new DataGridTextColumn();
            column.Header = "Статус";
            column.Binding = new Binding("order_status");
            columns.Add(column);
            return columns;
        }

        public static ObservableCollection<DataGridTextColumn> statisticTable()
        {
            ObservableCollection<DataGridTextColumn> columns = new ObservableCollection<DataGridTextColumn>();
            DataGridTextColumn column = new DataGridTextColumn();
            column.Header = "Товар";
            column.Binding = new Binding("name_product");
            columns.Add(column);
            column = new DataGridTextColumn();
            column.Header = "Артикул";
            column.Binding = new Binding("article");
            columns.Add(column);
            column = new DataGridTextColumn();
            column.Header = "Сумма закупки";
            column.Binding = new Binding("purchase_amount");
            columns.Add(column);
            column = new DataGridTextColumn();
            column.Header = "Сумма продажи";
            column.Binding = new Binding("sale_amount");
            columns.Add(column);
            column = new DataGridTextColumn();
            column.Header = "Прибыль";
            column.Binding = new Binding("profit");
            columns.Add(column);
            return columns;
        }

        public static ObservableCollection<DataGridTextColumn> balancesTable()
        {
            ObservableCollection<DataGridTextColumn> columns = new ObservableCollection<DataGridTextColumn>();
            DataGridTextColumn column = new DataGridTextColumn();
            column.Header = "Товар";
            column.Binding = new Binding("name_product");
            columns.Add(column);
            column = new DataGridTextColumn();
            column.Header = "Артикул";
            column.Binding = new Binding("article");
            columns.Add(column);
            column = new DataGridTextColumn();
            column.Header = "Фактический остаток";
            column.Binding = new Binding("count");
            columns.Add(column);
            column = new DataGridTextColumn();
            column.Header = "Неснижаемый остаток";
            column.Binding = new Binding("minimum_balance");
            columns.Add(column);
            column = new DataGridTextColumn();
            column.Header = "Заказано";
            column.Binding = new Binding("ordered");
            columns.Add(column);
            column = new DataGridTextColumn();
            column.Header = "Заказать";
            column.Binding = new Binding("to_order");
            columns.Add(column);
            column = new DataGridTextColumn();
            column.Header = "Под списание";
            column.Binding = new Binding("for_debiting");
            columns.Add(column);
            column = new DataGridTextColumn();
            column.Header = "Дата списания";
            Binding binding = new Binding("date_of_debiting");
            binding.StringFormat = "dd.MM.yyyy";
            column.Binding = binding;
            columns.Add(column);
            column = new DataGridTextColumn();
            column.Header = "Сезон";
            column.Binding = new Binding("season");
            columns.Add(column);
            return columns;
        }

        public static ObservableCollection<DataGridTextColumn> writeOffTable()
        {
            ObservableCollection<DataGridTextColumn> columns = new ObservableCollection<DataGridTextColumn>();
            DataGridTextColumn column = new DataGridTextColumn();
            column.Header = "Товар";
            column.Binding = new Binding("name_product");
            columns.Add(column);
            column = new DataGridTextColumn();
            column.Header = "Артикул";
            column.Binding = new Binding("article");
            columns.Add(column);
            column = new DataGridTextColumn();
            column.Header = "Дата списания";
            column.Binding = new Binding("date_of_debiting");
            columns.Add(column);
            column = new DataGridTextColumn();
            column.Header = "Количество";
            column.Binding = new Binding("quantity");
            columns.Add(column);
            column = new DataGridTextColumn();
            column.Header = "Цена";
            column.Binding = new Binding("price");
            columns.Add(column);
            column = new DataGridTextColumn();
            column.Header = "Сумма";
            column.Binding = new Binding("amount");
            columns.Add(column);            
            return columns;
        }

        public static ObservableCollection<DataGridColumn> userTable()
        {
            ObservableCollection<DataGridColumn> columns = new ObservableCollection<DataGridColumn>();
            DataGridTextColumn column = new DataGridTextColumn();
            column.Header = "Логин";
            column.Binding = new Binding("username");
            columns.Add(column);
            column = new DataGridTextColumn();
            column.Header = "ФИО";
            column.Binding = new Binding() { Converter = new FullNameConverter() }; ;
            columns.Add(column);
            DataGridCheckBoxColumn column1 = new DataGridCheckBoxColumn();
            column1.Header = "Администратор";
            column1.Binding = new Binding("is_admin");
            columns.Add(column1);
            return columns;
        }
    }
}
