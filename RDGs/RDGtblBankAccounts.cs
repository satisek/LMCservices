﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SqlClient = System.Data.SqlClient;

namespace RDGs
{
    public class RDGtblBankAccounts
    {
        private string connectionString;

        public RDGtblBankAccounts(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Interface.IbankAccounts> Get()
        {
            var list = new List<Interface.IbankAccounts>();

            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                foreach (var item in dbContext.tblBankAccounts)
                {
                    list.Add(new InterfaceAdaptor.BankAccounts()
                    {
                        AccountName = item.accountName,
                        AccountNo = item.accountNo,
                        Balance = Convert.ToDouble(item.balance),
                        Bank = item.bank,
                        Id = item.Id,
                        RegNo = item.regNo
                    });
                }
            }

            return list;
        }

        public Interface.IbankAccounts Find(int id)
        {
            InterfaceAdaptor.BankAccounts bankAccounts = null;

            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                var bankAccFound = dbContext.tblBankAccounts.SingleOrDefault(
                    x => x.Id == id);

                bankAccounts = new InterfaceAdaptor.BankAccounts()
                {
                    AccountName = bankAccFound.accountName,
                    AccountNo = bankAccFound.accountNo,
                    Balance = Convert.ToDouble(bankAccFound.balance),
                    Bank = bankAccFound.bank,
                    Id = bankAccFound.Id,
                    RegNo = bankAccFound.regNo
                };
            }

            return bankAccounts;
        }

        public void Add(Interface.IbankAccounts bankAccounts)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                var newBankAcc = new tblBankAccount()
                {
                    accountName = bankAccounts.AccountName,
                    accountNo = bankAccounts.AccountNo,
                    balance = Convert.ToDecimal(bankAccounts.Balance),
                    bank = bankAccounts.Bank,
                    regNo = bankAccounts.RegNo,
                };

                dbContext.tblBankAccounts.InsertOnSubmit(newBankAcc);
                dbContext.SubmitChanges();
            }
        }

        public void Update(Interface.IbankAccounts bankAccounts)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                var bankAccUpdateing = dbContext.tblBankAccounts.SingleOrDefault(
                    x => x.Id == bankAccounts.Id);

                bankAccUpdateing.accountName = bankAccounts.AccountName;
                bankAccUpdateing.accountNo = bankAccounts.AccountNo;
                bankAccUpdateing.balance = Convert.ToDecimal(bankAccounts.Balance);
                bankAccUpdateing.bank = bankAccounts.Bank;
                bankAccUpdateing.regNo = bankAccounts.RegNo;

                dbContext.SubmitChanges();
            }
        }

        public void Delete(int id)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
            {
                var bankAccountsDeleteing = dbContext.tblBankAccounts.SingleOrDefault(
                    x => x.Id == id);

                var deleteInfo = new StringBuilder();
                deleteInfo.Append("[tblBankAccounts] { ");
                deleteInfo.Append("indexId = " + bankAccountsDeleteing.Id.ToString() + ", ");
                deleteInfo.Append("bank = " + bankAccountsDeleteing.bank + ", ");
                deleteInfo.Append("accountName = " + bankAccountsDeleteing.accountName + ", ");
                deleteInfo.Append("regNo = " + bankAccountsDeleteing.regNo.ToString() + ", ");
                deleteInfo.Append("accountNo = " + bankAccountsDeleteing.accountNo + ", ");
                deleteInfo.Append("balance = " + bankAccountsDeleteing.balance.ToString() + " }");

                var deleteingItem = new tblDeleteItem() 
                {
                    deleteDate = DateTime.Now,
                    itemInfo = deleteInfo.ToString(),
                    restored = false
                };

                dbContext.tblBankAccounts.DeleteOnSubmit(bankAccountsDeleteing);
                dbContext.tblDeleteItems.InsertOnSubmit(deleteingItem);
                dbContext.SubmitChanges();
            }
        }

        public int NextId
        {
            get
            {
                string connString = string.Empty;
                using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext(this.connectionString))
                {
                    connString = dbContext.Connection.ConnectionString;
                }

                var conn = new SqlClient.SqlConnection(connString);
                var cmd = new SqlClient.SqlCommand(@"SELECT IDENT_CURRENT ('[tblBankAccounts]')", conn);

                conn.Open();

                decimal result = (decimal)cmd.ExecuteScalar();

                conn.Close();

                return Convert.ToInt32(result) + 1;
            }
        }
    }
}
