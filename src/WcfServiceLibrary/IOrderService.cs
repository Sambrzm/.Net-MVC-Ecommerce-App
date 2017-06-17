using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IOrderService" in both code and config file together.
    [ServiceContract]
    public interface IOrderService
    {
        [OperationContract]
        [FaultContract(typeof(SetErrorDetails))]
        Order findOrder(int id);
    }

    [DataContract]
    public class Orders
    {
        [DataMember]
        public int OrderID { get; set; }
        [DataMember]
        public int CustomerID { get; set; }
        [DataMember]
        public int AddressID { get; set; }
        [DataMember]
        public System.DateTime OrderDate { get; set; }
        [DataMember]
        public bool Complete { get; set; }
        [DataMember]
        public ICollection<OrderDetail> OrderDetails { get; set; }
        [DataMember]
        public int ProductID { get; set; }
        [DataMember]
        public int Quantity { get; set; }
    }
}
