using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ClassLibrary;

namespace WcfServiceLibrary
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Single)]
    public class WCFService : IWCFService
    {
        private DigitalXDBEntities dxe = new DigitalXDBEntities();

        public List<Customer> allCustomers()
        {
            return dxe.Customers.ToList();
        }

        public int CreateCustomer(Customer request)
        {
            Customer c = new ClassLibrary.Customer()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName
            };
            dxe.Customers.Add(c);
            dxe.SaveChanges();
            return c.CustomerID;
        }

        public Product find(int id)
        {
           
                return dxe.Products.Single(p => p.ProductID == id);
            
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

        public Customer findCust(string username)
        {
            //return dxe.Customers.Single(c => c.UserName = username);
            throw new Exception();
        }

        public List<Product> topFive()
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
        }
    }

