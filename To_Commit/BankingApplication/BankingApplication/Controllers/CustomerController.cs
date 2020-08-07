using BankingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankingApplication.Controllers
{
    public class CustomerController : Controller
    {
        
        public ActionResult Index()
        {
            CustomerDataAccessLayer customerDataAccess = new CustomerDataAccessLayer();
            Customer customerInformation = customerDataAccess.GetCustomerInformation(1);
            return View(customerInformation);
        }

        public bool CreateCustomerTransaction(int amount, int srcAccountNumber)
        {
            CustomerDataAccessLayer customerDataAccess = new CustomerDataAccessLayer();
            return customerDataAccess.CreateCustomerTransaction(amount, srcAccountNumber);
        }

    }
}