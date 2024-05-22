using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using Vers0.Model;

namespace Vers0
{
    public static class MyTableColumns
    {
        //inventory_managementEntities db = new inventory_managementEntities();

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
    }
}
