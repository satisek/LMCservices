﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Interface;

namespace Orders
{
    public class PrivetOrder : Interface.IprivetOrder
    {
        private Interface.Iworker crateBy;
        private DateTime createdDate;
        private Interface.IprivetCustomer customer;
        private DateTime? dateSendBill;
        private int daysToPaid;
        private string descriptionTask;
        private double hoursUse;
        private int invoiceNo;
        private bool paid;
        private double paidHour;
        private int paidToAcc;
        private DateTime taskDate;

        public PrivetOrder(Interface.Iworker crateBy, DateTime createdDate, Interface.IprivetCustomer customer,
            DateTime dateSendBill, int daysToPaid, string descriptionTask, double hoursUse, int invoiceNo,
            bool paid, double paidHour, int paidToAcc, DateTime taskDate)
        {
            this.crateBy = crateBy;
            this.createdDate = createdDate;
            this.customer = customer;
            this.dateSendBill = dateSendBill;
            this.daysToPaid = daysToPaid;
            this.descriptionTask = descriptionTask;
            this.hoursUse = hoursUse;
            this.invoiceNo = invoiceNo;
            this.paid = paid;
            this.paidHour = paidHour;
            this.paidToAcc = paidToAcc;
            this.taskDate = taskDate;
        }

        public PrivetOrder(PrivetOrder obj)
        {
            this.crateBy = obj.crateBy;
            this.createdDate = obj.createdDate;
            this.customer = obj.customer;
            this.dateSendBill = obj.dateSendBill;
            this.daysToPaid = obj.daysToPaid;
            this.descriptionTask = obj.descriptionTask;
            this.hoursUse = obj.hoursUse;
            this.invoiceNo = obj.invoiceNo;
            this.paid = obj.paid;
            this.paidHour = obj.paidHour;
            this.paidToAcc = obj.paidToAcc;
            this.taskDate = obj.taskDate;
        }

        public PrivetOrder(int invoiceNo)
        {
            this.invoiceNo = invoiceNo;
        }

        public Iworker CreateBy { get { return this.crateBy; } set { this.crateBy = value; } }
        public DateTime CreateDate { get { return this.createdDate; } set { this.createdDate = value; } }
        public IprivetCustomer Customer { get { return this.customer; } set { this.customer = value; } }
        public DateTime? DateSendBill { get { return this.dateSendBill; } set { this.dateSendBill = value; } }
        public int DaysToPaid { get { return this.daysToPaid; } set { this.daysToPaid = value; } }
        public string DescriptionTask { get { return this.descriptionTask; } set { this.descriptionTask = value; } }
        public double HourUse { get { return this.hoursUse; } set { this.hoursUse = value; } }
        public int InvoiceNo { get { return this.invoiceNo; } } // is read only used for the db
        public bool Paid { get { return this.paid; } set { this.paid = value; } }
        public double PaidHour { get { return this.paidHour; } set { this.paidHour = value; } }
        public int PaidToAcc { get { return this.paidToAcc; } set { this.paidToAcc = value; } }
        public DateTime TaskDate { get { return this.taskDate; } set { this.taskDate = value; } }
    }
}
