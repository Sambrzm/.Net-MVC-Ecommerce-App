using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary
{
    [ServiceContract]
    public interface IWCFService
    {
        [OperationContract]
        [FaultContract(typeof(SetErrorDetails))]
        List<Product> findAll();

        [OperationContract]
        [FaultContract(typeof(SetErrorDetails))]
        Product find(int id);

        [OperationContract]
        [FaultContract(typeof(SetErrorDetails))]
        List<Product> topFive();

        [OperationContract]
        [FaultContract(typeof(SetErrorDetails))]
        List<Customer> allCustomers();

        [OperationContract]
        [FaultContract(typeof(SetErrorDetails))]
        Customer findCust(string username);

        [OperationContract]
        int CreateCustomer(Customer request);
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

    [DataContract]
    public class Customers
    {
        [DataMember]
        public int CustomerID { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
    }

    [DataContract]
    public class SetErrorDetails
    {
        public string errorName;
        public string errorDetails;

        [DataMember]
        public string ErrorName
        {
            get { return errorName; }
            set{ errorName = value; }
        }
        [DataMember]
        public string ErrorDetails
        {
            get{ return errorDetails; }
            set{ errorDetails = value; }
        }
    }
}
