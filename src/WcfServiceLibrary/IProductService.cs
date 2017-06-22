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
    public interface IProductService
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
        int CreateCustomer(Customer request);

        [OperationContract]
        Customer findCustomer(int id);
    }

    [DataContract]
    public class SetErrorDetails
    {
        public string errorName;
        public string errorDetails;

        [DataMember]
        public string ErrorName
        {
            get
            {
                return errorName;
            }

            set
            {
                errorName = value;
            }
        }

        [DataMember]
        public string ErrorDetails
        {
            get
            {
                return errorDetails;
            }

            set
            {
                errorDetails = value;
            }
        }

    }
}
