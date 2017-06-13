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

        public Product find(int id)
        {
            return dxe.Products.Single(p => p.ProductID == id);
        }

        public List<Product> findAll()
        {
            return dxe.Products.ToList();
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
