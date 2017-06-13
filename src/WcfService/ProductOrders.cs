using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService
{
    public class ProductOrders
    {
        public ProductOrders()
        { }
            public Product p { get; set; }
            public int numOrders { get; set; }        
    }
}