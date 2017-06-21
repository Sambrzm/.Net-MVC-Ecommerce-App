using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICustomerService" in both code and config file together.
    [ServiceContract]
    public interface ICustomerService
    {
        [OperationContract]
        [FaultContract(typeof(SetErrorDetails))]
        List<Customer> findAll();

        [OperationContract]
        [FaultContract(typeof(SetErrorDetails))]
        Customer find(int id);

        [OperationContract]
        int CreateCustomer(Customer request);

    }

    [DataContract]
    public class Customers
    {
        [DataMember]
        public int CustomerID { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Username { get; set; }
    }
}
