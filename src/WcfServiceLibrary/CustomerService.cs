using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class CustomerService : ICustomerService
    {
        private DigitalXDBEntities dxe = new DigitalXDBEntities();

        public int CreateCustomer(Customer request)
        {
            ClassLibrary.Customer c = new ClassLibrary.Customer()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName
            };
            dxe.Customers.Add(c);
            dxe.SaveChanges();
            return c.CustomerID;
        }

        public Customer find(int id)
        {
            try
            {
                return dxe.Customers.Single(p => p.CustomerID == id);
            }
            catch (Exception ex)
            {
                SetErrorDetails errorobj = new SetErrorDetails();
                errorobj.ErrorName = "Something went wrong";
                errorobj.ErrorDetails = ex.Message;
                throw new FaultException<SetErrorDetails>(errorobj);
            }
        }

        public List<Customer> findAll()
        {
            try
            {
                return dxe.Customers.ToList();
            }
            catch (Exception ex)
            {
                SetErrorDetails errorobj = new SetErrorDetails();
                errorobj.ErrorName = "Something went wrong";
                errorobj.ErrorDetails = ex.Message;
                throw new FaultException<SetErrorDetails>(errorobj);
            }
        }
    }


}
