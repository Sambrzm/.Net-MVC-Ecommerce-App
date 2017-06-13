using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProductService" in both code and config file together.
    [ServiceContract]
    public interface IProductService
    {        
        [OperationContract]
        List<Product> findAll();

        [OperationContract]
        Product find(int id);

        [OperationContract]
        List<Product> topFive();
    }

    [DataContract]
    public class Products
    {
        [DataMember]
        public int ProductID { get; set; }

        public int RetailerID { get; set; }

        public int SubCategoryID { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public string ProductDescription { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public int UnitsInStock { get; set; }

        [DataMember]
        public byte[] Picture { get; set; }

        public Nullable<bool> IsDiscontinued { get; set; }
    }
}
