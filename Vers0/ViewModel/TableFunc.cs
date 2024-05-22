using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vers0.Model;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Vers0.ViewModel
{
    internal class TableFunc
    {
        ObservableCollection<product> products = new ObservableCollection<product>();
        ObservableCollection<contractor> contractors = new ObservableCollection<contractor>();
        ObservableCollection<ordersDTO> orders = new ObservableCollection<ordersDTO>();
        ObservableCollection<balances_Result> balances = new ObservableCollection<balances_Result>();
        ObservableCollection<users> users = new ObservableCollection<users>();
        ObservableCollection<GetProfitDataForPeriod_Result> statistic = new ObservableCollection<GetProfitDataForPeriod_Result>();
        public ObservableCollection<product> getProducts()
        {
            using (inventory_managementEntities db = new inventory_managementEntities())
            {
                var productsList = db.product.Where(x => x.deleted == false).ToList();
                products = new ObservableCollection<product>(productsList);
                return products;
            }
        }

        public ObservableCollection<contractor> getContractor()
        {
            using (inventory_managementEntities db = new inventory_managementEntities())
            {
                var contractorList = db.contractor.Where(x => x.deleted == false).ToList();
                contractors = new ObservableCollection<contractor>(contractorList);
                return contractors;
            }
        }

        public ObservableCollection<users> getUsers()
        {
            using (inventory_managementEntities db = new inventory_managementEntities())
            {
                var usersList = db.users.Where(x => x.deleted == false).ToList();
                users = new ObservableCollection<users>(usersList);
                return users;
            }
        }

        public ObservableCollection<ordersDTO> getOrdersToContractors()
        {
            using (inventory_managementEntities db = new inventory_managementEntities())
            {
                var ordersList = db.orders.Include(c => c.contractor).Where(c => c.order_type == "Заказ_поставщику").Where(c => c.deleted == false).Select(c => new ordersDTO()
                {
                    number = c.number,
                    type = c.order_type,
                    order_date = c.order_date,
                    contractor = new contractorsDTO()
                    {
                        inn_c = c.contractor1.INN,
                        name_c = c.contractor1.designation_contractor
                    },
                    amount = c.amount,
                    order_status = c.order_status
                }).ToList();
                orders = new ObservableCollection<ordersDTO>(ordersList);
                return orders;
            }
        }

        public ObservableCollection<ordersDTO> getCustomerOrders()
        {
            using (inventory_managementEntities db = new inventory_managementEntities())
            {
                var ordersList = db.orders.Include(c => c.contractor).Where(c => c.order_type == "Заказ_покупателя").Where(c => c.deleted == false).Select(c => new ordersDTO()
                {
                    number = c.number,
                    type = c.order_type,
                    order_date = c.order_date,
                    contractor = new contractorsDTO()
                    {
                        inn_c = c.contractor1.INN,
                        name_c = c.contractor1.designation_contractor
                    },
                    amount = c.amount,
                    order_status = c.order_status
                }).ToList();
                orders = new ObservableCollection<ordersDTO>(ordersList);
                return orders;
            }
        }

        public ObservableCollection<ordersDTO> getSupply()
        {
            using (inventory_managementEntities db = new inventory_managementEntities())
            {
                var ordersList = db.orders.Include(c => c.contractor).Where(c => c.order_type == "Поступление").Where(c => c.deleted == false).Select(c => new ordersDTO()
                {
                    number = c.number,
                    type = c.order_type,
                    order_date = c.order_date,
                    contractor = new contractorsDTO()
                    {
                        inn_c = c.contractor1.INN,
                        name_c = c.contractor1.designation_contractor
                    },
                    amount = c.amount,
                    order_status = c.order_status
                }).ToList();
                orders = new ObservableCollection<ordersDTO>(ordersList);
                return orders;
            }
        }

        public ObservableCollection<balances_Result> getBalances()
        {
            using (inventory_managementEntities db = new inventory_managementEntities())
            {
                var balancesList = db.balances().OrderByDescending(b => b.to_order).ThenBy(b => b.date_of_debiting).ToList();
                balances = new ObservableCollection<balances_Result>(balancesList);
                return balances;
            }
        }

        public ObservableCollection<GetProfitDataForPeriod_Result> getStatistic(DateTime? dateStart = default, DateTime? dateFinish = default)
        {
            using (inventory_managementEntities db = new inventory_managementEntities())
            {
                if (dateStart == default)
                {
                    dateStart = DateTime.Now.AddMonths(-1);
                }

                if (dateFinish == default)
                {
                    dateFinish = DateTime.Now;
                }

                var statisticList = db.GetProfitDataForPeriod(dateStart, dateFinish).ToList();
                statistic = new ObservableCollection<GetProfitDataForPeriod_Result>(statisticList);
                return statistic;
            }
        }

        public bool delete(object obj)
        {
            using (inventory_managementEntities db = new inventory_managementEntities())
            {
                if (obj != null)
                {
                    if (obj is users user)
                    {
                        try
                        {
                            db.users.Remove(user);
                            users.Remove(user);
                        }
                        catch { return false; }
                    }
                    else if (obj is ordersDTO ordersD)
                    {
                        try
                        {
                            orders o = db.orders.Find(ordersD.number);
                            db.orders.Remove(o);
                            orders.Remove(ordersD);
                        }
                        catch { return false; }
                    }
                    else if (obj is product product)
                    {
                        try
                        {
                            db.product.Remove(product);
                            products.Remove(product);
                        }
                        catch { return false; }
                    }
                    else if (obj is contractor c)
                    {
                        try
                        {
                            db.contractor.Remove(c);
                            contractors.Remove(c);
                        }
                        catch { return false; }
                    }
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public bool create(object obj)
        {
            using (inventory_managementEntities db = new inventory_managementEntities())
            {
                if (obj != null)
                {
                    if (obj is product product)
                    {
                        try
                        {
                            products.Add(product);
                            db.product.Add(product);
                        }
                        catch { throw new Exception("Не удалось добавить товар!"); }
                    }
                    else if (obj is contractor contractor)
                    {
                        try
                        {
                            contractors.Add(contractor);
                            db.contractor.Add(contractor);
                        }
                        catch { throw new Exception("Не удалось добавить контрагента!"); }
                    }
                    else if (obj is users user)
                    {
                        try
                        {
                            users.Add(user);
                            db.users.Add(user);
                        }
                        catch { throw new Exception("Не удалось добавить пользователя!"); }
                    }

                    db.SaveChanges();
                    return true;
                }
                return false;
            }

        }

        public bool edit(object obj)
        {

            if (obj != null)
            {
                using (inventory_managementEntities db = new inventory_managementEntities())
                {
                    if (obj is product product)
                    {
                        product p = db.product.Find(product.number);
                        if (p != null)
                        {
                            p.name_product = product.name_product;
                            p.article = product.article;
                            p.unit_of_measurement = product.unit_of_measurement;
                            p.minimum_balance = product.minimum_balance;
                            p.description_product = product.description_product;
                            p.sale_price = product.sale_price;
                            p.season = product.season;
                            p.Shelf_life = product.Shelf_life;
                        }
                    }
                    else if (obj is contractor contractor)
                    {
                        contractor c = db.contractor.Find(contractor.INN);
                        if (c != null)
                        {
                            c.INN = contractor.INN;
                            c.type_contractor = contractor.type_contractor;
                            c.address_contractor = contractor.address_contractor;
                            c.RCBIC = contractor.RCBIC;
                            c.bank = contractor.bank;
                            c.corr_acc = contractor.corr_acc;
                            c.acc = contractor.acc;
                            c.designation_contractor = contractor.designation_contractor;
                            c.KPP = contractor.KPP;
                            c.OGRN = contractor.OGRN;
                            c.surname = contractor.surname;
                            c.name_contractor = contractor.name_contractor;
                            c.middle_name = contractor.middle_name;
                            c.date_birth = contractor.date_birth;
                            c.OGRNIP = contractor.OGRNIP;
                            c.deleted = false;
                        }
                    }
                    else if (obj is users user)
                    {
                        users u = db.users.Find(user.username);
                        if (u != null)
                        {
                            u.username = user.username;
                            u.surname = user.surname;
                            u.name_user = user.name_user;
                            u.middle_name = user.middle_name;
                            u.is_admin = user.is_admin;
                        }
                    }
                    try
                    {
                        db.SaveChanges();
                        return true;
                    }
                    catch (DbUpdateException ex)
                    {
                        throw new Exception("Ошибка при сохранении изменений: " + ex.Message);
                    }
                }
            }
            return false;
        }
    }
}
