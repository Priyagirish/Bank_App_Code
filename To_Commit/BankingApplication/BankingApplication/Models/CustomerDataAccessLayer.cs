using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Globalization;

namespace BankingApplication.Models
{
    public class CustomerDataAccessLayer
    {
        string connectionString = "Data Source=LAPTOP-BFG19C7K\\SQL_EXPRESS;Initial Catalog=BankApp;integrated security=True;Application Name=Bank_Application;";
        public Customer GetCustomerInformation(int customerId)
        {
            Customer customerInformation = null;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT * FROM Customer WHERE CustomerId = {customerId}", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        customerInformation = new Customer();
                        customerInformation.CustomerID = Convert.ToInt32(rdr["CustomerId"]);
                        customerInformation.AccountNumber = Convert.ToInt32(rdr["AccountNumber"]);
                        customerInformation.CustomerName = rdr["CustomerName"].ToString();
                        customerInformation.Address = rdr["Address"].ToString();
                        customerInformation.AccountBalance = Convert.ToInt32(rdr["AccountBalance"]);
                    }
                    con.Close();
                }

                customerInformation.LatestTransactions = GetCustomerTransactions(customerInformation.AccountNumber);
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return customerInformation;
        }

        public IEnumerable<CustomerTransaction> GetCustomerTransactions(int customerAccountNumber)
        {
            List<CustomerTransaction> customerTransactions = new List<CustomerTransaction>();
            CustomerTransaction customerTransaction = null;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand($"SELECT TOP 5 * FROM CustomerTransaction " +
                                        $"WHERE srcAccountNumber = {customerAccountNumber} OR destAccountNumber = {customerAccountNumber} " +
                                        $"ORDER BY TransactionDateTime DESC", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    CultureInfo culture = new CultureInfo("en-US");
                    while (rdr.Read())
                    {
                        customerTransaction = new CustomerTransaction();
                        customerTransaction.CustomerTransactionID = Convert.ToInt32(rdr["CustomerTransactionID"]);
                        customerTransaction.Amount = Convert.ToInt32(rdr["Amount"]);
                        customerTransaction.TransactionDateTime = Convert.ToDateTime(rdr["TransactionDateTime"],culture);
                        customerTransaction.srcAccountNumber = Convert.ToInt32(rdr["srcAccountNumber"].ToString());
                        customerTransaction.destAccountNumber = Convert.ToInt32(rdr["destAccountNumber"].ToString());
                        customerTransactions.Add(customerTransaction);
                    }
                    con.Close();
                }
            }
            catch(Exception ex)
            {
                throw;
            }

            return customerTransactions;
        }

        public bool CreateCustomerTransaction(int amount, int srcAccountNumber)
        {
            int destAccountNumber = 234567;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = null;

                    cmd = new SqlCommand($"UPDATE Customer " +
                        $"SET AccountBalance = AccountBalance - {amount} WHERE AccountNumber = {srcAccountNumber}"
                         , con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    con.Close();
                    CultureInfo culture = new CultureInfo("en-US");
                    cmd = new SqlCommand($"INSERT INTO CustomerTransaction (Amount, TransactionDateTime, srcAccountNumber, destAccountNumber) VALUES" +
                                                    $"('{amount}', '{DateTime.Now.ToString(culture)}', '{srcAccountNumber}', '{destAccountNumber}')", con);
                    cmd.CommandType = CommandType.Text;
                    con.Open();
                    rdr = cmd.ExecuteReader();
                    con.Close();
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}