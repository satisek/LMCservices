﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SqlClient = System.Data.SqlClient;

namespace RDGs
{
    public class RDGtblCompanyCustomers
    {
        public List<Interface.IcompanyCustomer> Get(bool? active)
        {
            var list = new List<Interface.IcompanyCustomer>();

            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
            {
                IQueryable<tblCompanyCustomer> companyCustomers;

                if (active == null)
                {
                    companyCustomers = from tblCompanyCustomer in dbContext.tblCompanyCustomers
                                       select tblCompanyCustomer;
                }
                else
                {
                    companyCustomers = from tblCompanyCustomer in dbContext.tblCompanyCustomers
                                       where tblCompanyCustomer.active == active
                                       select tblCompanyCustomer;
                }

                foreach (var item in companyCustomers)
                {
                    list.Add(new InterfaceAdaptor.CompanyCustomer()
                    {
                        Active = (bool)item.active,
                        Address = item._address,
                        AltPhoneNo = item.altPhoneNo,
                        CompanyCustomersNo = item.companyCustomersNo,
                        ContactPerson = item.companyContactPerson,
                        CvrNo = item.cvrNo,
                        Email = item.email,
                        Name = item.companyName,
                        PhoneNo = item.phoneNo,
                        PostNo = new InterfaceAdaptor.PostNo()
                        {
                            City = item.tblPostNo.city,
                            Id = item.tblPostNo.ID,
                            PostNumber = item.tblPostNo.postNo
                        }
                    });
                }
            }

            return list;
        }

        public Interface.IcompanyCustomer Find(int id)
        {
            Interface.IcompanyCustomer companyCustomer;

            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
            {
                var companyCustomerFound = dbContext.tblCompanyCustomers.SingleOrDefault(
                    x => x.companyCustomersNo == id);

                companyCustomer = new InterfaceAdaptor.CompanyCustomer()
                {
                    Active = (bool)companyCustomerFound.active,
                    Address = companyCustomerFound._address,
                    AltPhoneNo = companyCustomerFound.altPhoneNo,
                    CompanyCustomersNo = companyCustomerFound.companyCustomersNo,
                    ContactPerson = companyCustomerFound.companyContactPerson,
                    CvrNo = companyCustomerFound.cvrNo,
                    Email = companyCustomerFound.email,
                    Name = companyCustomerFound.companyName,
                    PhoneNo = companyCustomerFound.phoneNo,
                    PostNo = new InterfaceAdaptor.PostNo()
                    {
                        City = companyCustomerFound.tblPostNo.city,
                        Id = companyCustomerFound.tblPostNo.ID,
                        PostNumber = companyCustomerFound.tblPostNo.postNo
                    },
                };
            }

            return companyCustomer;
        }

        public void Add(Interface.IcompanyCustomer companyCustomer)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
            {
                var newCompanyCustomer = new tblCompanyCustomer()
                {
                    _address = companyCustomer.Address,
                    active = companyCustomer.Active,
                    altPhoneNo = companyCustomer.AltPhoneNo,
                    companyContactPerson = companyCustomer.ContactPerson,
                    companyCustomersNo = companyCustomer.CompanyCustomersNo,
                    companyName = companyCustomer.Name,
                    cvrNo = companyCustomer.CvrNo,
                    email = companyCustomer.Email,
                    phoneNo = companyCustomer.PhoneNo,
                    postNo = companyCustomer.PostNo.Id,
                };

                dbContext.tblCompanyCustomers.InsertOnSubmit(newCompanyCustomer);
                dbContext.SubmitChanges();
            }
        }

        public void Update(Interface.IcompanyCustomer companyCustomer)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
            {
                var companyCustomerUpdateing = dbContext.tblCompanyCustomers.SingleOrDefault(
                    x => x.companyCustomersNo == companyCustomer.CompanyCustomersNo);

                companyCustomerUpdateing._address = companyCustomer.Address;
                companyCustomerUpdateing.active = companyCustomer.Active;
                companyCustomerUpdateing.altPhoneNo = companyCustomer.AltPhoneNo;
                companyCustomerUpdateing.companyContactPerson = companyCustomer.ContactPerson;
                companyCustomerUpdateing.companyCustomersNo = companyCustomer.CompanyCustomersNo;
                companyCustomerUpdateing.companyName = companyCustomer.Name;
                companyCustomerUpdateing.cvrNo = companyCustomer.CvrNo;
                companyCustomerUpdateing.email = companyCustomer.Email;
                companyCustomerUpdateing.phoneNo = companyCustomer.PhoneNo;
                companyCustomerUpdateing.postNo = companyCustomer.PostNo.Id;

                dbContext.SubmitChanges();
            }
        }

        public void Delete(int id)
        {
            using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
            {
                var deletingItem = dbContext.tblCompanyCustomers.SingleOrDefault(
                    x => x.companyCustomersNo == id);

                var deletingInfo = new StringBuilder();
                deletingInfo.Append("[tblCompanyCustomers] { ");
                deletingInfo.Append("companyCustomersNo = " + deletingItem.companyCustomersNo.ToString() + ", ");
                deletingInfo.Append("companyName = " + deletingItem.companyName + ", ");
                deletingInfo.Append("companyContactPerson = " + deletingItem.companyContactPerson + ", ");
                deletingInfo.Append("cvrNo = " + deletingItem.cvrNo.ToString() + ", ");
                deletingInfo.Append("phoneNo = " + deletingItem.phoneNo + ", ");
                deletingInfo.Append("altPhoneNo = " + deletingItem.altPhoneNo + ", ");
                deletingInfo.Append("_address = " + deletingItem._address + ", ");
                deletingInfo.Append("postNo = " + deletingItem.postNo.ToString() + ", ");
                deletingInfo.Append("email = " + deletingItem.email + ", ");
                deletingInfo.Append("active = ");
                if ((bool)deletingItem.active) { deletingInfo.Append("1"); } else { deletingInfo.Append("0"); }
                deletingInfo.Append(" }");

                dbContext.tblDeleteItems.InsertOnSubmit(new tblDeleteItem()
                {
                    deleteDate = DateTime.Now,
                    itemInfo = deletingInfo.ToString(),
                    restored = false
                });

                dbContext.tblCompanyCustomers.DeleteOnSubmit(deletingItem);

                dbContext.SubmitChanges();
            }
        }

        public int NextId
        {
            get
            {
                string connString = string.Empty;
                using (LMCdatabaseDataContext dbContext = new LMCdatabaseDataContext())
                {
                    connString = dbContext.Connection.ConnectionString;
                }

                var conn = new SqlClient.SqlConnection(connString);
                var cmd = new SqlClient.SqlCommand(@"SELECT IDENT_CURRENT ('[tblCompanyCustomers]')", conn);

                conn.Open();

                decimal result = (decimal)cmd.ExecuteScalar();

                conn.Close();

                return Convert.ToInt32(result) + 1;
            }
        }
    }
}
