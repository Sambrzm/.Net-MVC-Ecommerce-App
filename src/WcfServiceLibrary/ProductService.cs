using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Single)]
    public class ProductService : IProductService
    {        
        private DigitalXDBEntities dxe = new DigitalXDBEntities();               

        public int createOrder(Order request)
        {
            Order o = new Order()
            {
                CustomerID = request.CustomerID,
                AddressID = request.AddressID,
                OrderDate = request.OrderDate,
                Complete = request.Complete
            };
            dxe.Orders.Add(o);            
            dxe.SaveChanges();
            return o.OrderID;
        }

        public int createOrderDetails(OrderDetail request)
        {
            OrderDetail od = new OrderDetail()
            {
                OrderID = request.OrderID,
                ProductID = request.ProductID,
                Quantity = request.Quantity
            };
            dxe.OrderDetails.Add(od);
            dxe.SaveChanges();
            return od.DetailID;
        }

        public int CreateCustomer(Customer request)
        {
            Customer c = new Customer()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName
            };
            dxe.Customer.Add(c);
            dxe.SaveChanges();
            return c.CustomerID;
        }

        public Product find(int id)
        {
            try
            {
                return dxe.Products.Single(p => p.ProductID == id);
            }
            catch (Exception ex)
            {
                SetErrorDetails errorobj = new SetErrorDetails();
                errorobj.ErrorName = "Something went wrong";
                errorobj.ErrorDetails = ex.Message;
                throw new FaultException<SetErrorDetails>(errorobj);
            }
        }

        public List<Product> findAll()
        {
            try
            {
                return dxe.Products.ToList();
            }
            catch (Exception ex)
            {
                SetErrorDetails errorobj = new SetErrorDetails();
                errorobj.ErrorName = "Something went wrong";
                errorobj.ErrorDetails = ex.Message;
                throw new FaultException<SetErrorDetails>(errorobj);
            }
        }

        public Customer findCustomer(string username)
        {
            return dxe.Customer.FirstOrDefault(c => c.UserName == username);            
        }

        public List<Product> topFive()
        {
            try
            {
                var top = (from p in dxe.Products
                           from od in dxe.OrderDetails
                           orderby od.Quantity descending
                           where od.ProductID == p.ProductID
                           //group od by p into pGroups
                           select p
                        //{
                        //    p = pGroups.Key,
                        //    numOrders = pGroups.Count()
                        //}
                        //).OrderByDescending(x => x.numOrders).Distinct().Take(5).Cast<Product>();
                        ).Take(5);

                if (!top.Any())
                {
                    var topPrice = (from p in dxe.Products
                                    orderby p.Price descending
                                    select p).Take(5);
                    return topPrice.ToList();
                }
                return top.ToList();
            }
            catch (Exception ex)
            {
                SetErrorDetails errorobj = new SetErrorDetails();
                errorobj.ErrorName = "Something went wrong";
                errorobj.ErrorDetails = ex.Message;
                throw new FaultException<SetErrorDetails>(errorobj);
            }
        }

        public int createAddress(Address request)
        {
            Address a = new Address
            {
                Street = request.Street,
                Suburb = request.Suburb,
                City = request.City,
                PostalCode = request.PostalCode,
                Country = request.Country
            };
            dxe.Addresses.Add(a);
            dxe.SaveChanges();            
            return a.AddressID;
        }

        public List<Address> findAllAddress(string username)
        {
            var address = (from o in dxe.Orders
                           join a in dxe.Addresses on o.AddressID equals a.AddressID
                           join c in dxe.Customer on o.CustomerID equals c.CustomerID
                           where c.UserName == username
                           select a).ToList();
            return address;
            //return dxe.Addresses.ToList();           
        }

        public List<Order> findOrders(string username)
        {
            var orders = (from o in dxe.Orders
                          from c in dxe.Customer
                          from od in dxe.OrderDetails
                          where o.CustomerID == c.CustomerID
                          where c.UserName == username
                          select o);
            return orders.ToList();
        }

        
    }
}
