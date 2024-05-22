using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Vers0.Model;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Windows;

namespace Vers0.ViewModel
{
    public class OrdersVeiwModel : INotifyPropertyChanged
    {
        private RelayCommand deleteCommand;
        private RelayCommand addCommand;
        private RelayCommand addProductCommand;
        private RelayCommand saveOrderCommand;
        private RelayCommand printCommand;
        private productDTO selectedProduct;
        public string windowTitle { get; set; }
        public ordersDTO order { get; set; }
        public ObservableCollection<productDTO> products { get; set; } = new ObservableCollection<productDTO>();
        public ObservableCollection<contractorsDTO> contractors { get; set; }
        public productDTO newSelectedProduct { get; set; }
        public ObservableCollection<productDTO> allProducts { get; set; }

        public productDTO SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }
        public double resultCost
        {
            get
            {
                return (double)order.amount;
            }
            set
            {
                order.amount = value;
                OnPropertyChanged();
            }
        }

        public OrdersVeiwModel(ordersDTO order)
        {
            using (inventory_managementEntities db = new inventory_managementEntities())
            {
                products.CollectionChanged += ItemsOnCollectionChanged;
                this.order = order;
                var Listcontractors = db.contractor.Where(x => x.deleted == false).Select(c => new contractorsDTO()
                {
                    inn_c = c.INN,
                    name_c = c.designation_contractor
                }).ToList();
                contractors = new ObservableCollection<contractorsDTO>(Listcontractors);

                if (order.number == 0)
                {
                    windowTitle = "Создание заказа";
                    order.contractor = contractors.FirstOrDefault();
                }

                else
                {
                    windowTitle = "Редактирование заказа";
                    var Listproduct = db.products_in_order
                        .Include(p => p.product)
                        .Where(pio => pio.order_number == order.number)
                        .Select(p => new productDTO()
                        {
                            number = p.number,
                            number_product = p.product_number,
                            name_product = p.product.name_product,
                            article_product = p.product.article,
                            balance_product = p.product.count,
                            count_product = p.quantity,
                            price_product = p.price,
                            cost_product = p.amount
                        })
                        .ToList();
                    foreach (var l in Listproduct)
                    {
                        products.Add(l);
                    }
                }
            }

        }

        private void ItemsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (INotifyPropertyChanged item in e.OldItems)
                    item.PropertyChanged -= UpdateCost;
            }
            if (e.NewItems != null)
            {
                foreach (INotifyPropertyChanged item in e.NewItems)
                    item.PropertyChanged += UpdateCost;
            }
        }

        public void UpdateCost(object sender, PropertyChangedEventArgs e)
        {
            resultCost = products.Sum(x => x.cost_product);

        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand(obj =>
                  {
                      productDTO product = obj as productDTO;
                      if (product != null)
                      {
                          products.Remove(product);
                          UpdateCost(null, null);
                          if (product.number != 0)
                          {
                              using (inventory_managementEntities db = new inventory_managementEntities())
                              {
                                  products_in_order p = db.products_in_order.Find(product.number);
                                  db.products_in_order.Remove(p);
                                  db.SaveChanges();
                              }
                          }
                      }
                  },
                 (obj) => products.Count > 0));
            }
        }

        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand(obj =>
                    {
                        using (inventory_managementEntities db = new inventory_managementEntities())
                        {
                            var ListProduct = db.product.Where(x => x.deleted == false).Select(c => new productDTO()
                            {
                                number_product = c.number,
                                name_product = c.name_product,
                                article_product = c.article,
                                balance_product = c.count,
                                price_product = (double)c.sale_price,
                                count_product = 0,
                                cost_product = 0
                            }).ToList();
                            allProducts = new ObservableCollection<productDTO>(ListProduct);
                        }
                        AddProductWindow addProductWindow = new AddProductWindow();
                        addProductWindow.DataContext = this;
                        addProductWindow.ShowDialog();
                    }));
            }
        }

        public RelayCommand AddProductCommand
        {
            get
            {
                return addProductCommand ??
                    (addProductCommand = new RelayCommand(obj =>
                    {
                        products.Add(newSelectedProduct);
                        selectedProduct = newSelectedProduct;
                    },
                    (obj) => newSelectedProduct != null));
            }
        }

        public RelayCommand SaveOrderCommand
        {
            get
            {
                return saveOrderCommand ??
                    (saveOrderCommand = new RelayCommand(obj =>
                    {
                        foreach (productDTO p in products)
                        {
                            if (order.type == "Заказ_покупателя")
                            {
                                if (p.count_product > p.balance_product)
                                {
                                    MessageBox.Show("Недостаточное количество товара для продажи!", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return;
                                }
                            }
                        }
                        using (inventory_managementEntities db = new inventory_managementEntities())
                        {
                            if (windowTitle == "Создание заказа")
                            {
                                orders o = new orders()
                                {
                                    order_type = order.type,
                                    order_date = order.order_date,
                                    contractor = order.contractor.inn_c,
                                    amount = order.amount,
                                    order_status = order.order_status
                                };
                                db.orders.Add(o);
                                db.SaveChanges();
                                order.number = o.number;
                            }
                            else if (windowTitle == "Редактирование заказа")
                            {
                                orders o = db.orders.Find(order.number);
                                if (o != null)
                                {
                                    o.order_type = order.type;
                                    o.order_date = order.order_date;
                                    o.contractor = order.contractor.inn_c;
                                    o.amount = order.amount;
                                    o.order_status = order.order_status;
                                }
                            }
                            foreach (productDTO p in products)
                            {
                                if (order.type == "Заказ_покупателя")
                                {
                                    if (p.count_product > p.balance_product)
                                    {
                                        MessageBox.Show("Недостаточное количество товара для продажи!", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
                                        return;
                                    }
                                    products_in_order product;
                                    if (p.number != 0)
                                    {
                                        product = db.products_in_order.Find(p.number);
                                        if (product != null)
                                        {
                                            product.quantity = p.count_product;
                                            product.price = p.price_product;
                                            product.amount = p.cost_product;
                                        }
                                    }
                                    else
                                    {
                                        product = new products_in_order()
                                        {
                                            order_number = order.number,
                                            product_number = p.number_product,
                                            quantity = p.count_product,
                                            price = p.price_product,
                                            amount = p.cost_product
                                        };
                                        db.products_in_order.Add(product);
                                    }
                                }
                            }
                            try
                            {
                                db.SaveChanges();
                                OrderWindow w = obj as OrderWindow;
                                w.Close();
                            }
                            catch (DbUpdateException ex)
                            {
                                throw new Exception("Ошибка при сохранении: " + ex.Message);
                            }
                        }
                    },
                    (obj) => products.Count > 0));
            }


        }
        public RelayCommand PrintCommand
        {
            get
            {
                return printCommand ??
                    (printCommand = new RelayCommand(obj =>
                    {
                        products.Add(newSelectedProduct);
                        selectedProduct = newSelectedProduct;
                    },
                    (obj) => newSelectedProduct != null));
            }
        }
    }
}
