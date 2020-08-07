using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BankingApplication.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }

        public int AccountNumber { get; set; }

        public string CustomerName { get; set; }

        public string Address { get; set; }

        public decimal AccountBalance { get; set; }

        public IEnumerable<CustomerTransaction> LatestTransactions { get; set; }
    }
}