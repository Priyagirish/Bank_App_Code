using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankingApplication.Models
{
    public class CustomerTransaction
    {
        public int CustomerTransactionID { get; set; }

        public DateTime TransactionDateTime { get; set; }

        public decimal Amount { get; set; }

        public int srcAccountNumber { get; set; }

        public int destAccountNumber { get; set; }
    }
}