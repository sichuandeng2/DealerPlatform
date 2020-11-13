using System;
using System.Collections.Generic;
using System.Text;
using DealerPlatformApiDemo.Core;

namespace DealerPlatformApiDemo.Entity
{
    /// <summary>
    /// 开户行信息
    /// </summary>
    [Serializable]
    public class CustomerInvoices: BaseEntity
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public CustomerInvoices()
        { }

        private System.Int32 _Id;
        private System.String _CustomerNo;
        private System.String _InvoiceNo;
        private System.String _InvoiceEin;
        private System.String _InvoiceBank;
        private System.String _InvoiceAccount;
        private System.String _InvoiceAddress;
        private System.String _InvoicePhone;

        /// <summary>
        /// Id属性封装
        /// </summary>
        public System.Int32 Id
        {
            set { _Id = value; }
            get { return _Id; }
        }
        /// <summary>
        /// CustomerNo属性封装
        /// </summary>
        public System.String CustomerNo
        {
            set { _CustomerNo = value; }
            get { return _CustomerNo; }
        }
        /// <summary>
        /// InvoiceNo属性封装
        /// </summary>
        public System.String InvoiceNo
        {
            set { _InvoiceNo = value; }
            get { return _InvoiceNo; }
        }
        /// <summary>
        /// InvoiceEin属性封装
        /// </summary>
        public System.String InvoiceEin
        {
            set { _InvoiceEin = value; }
            get { return _InvoiceEin; }
        }
        /// <summary>
        /// InvoiceBank属性封装
        /// </summary>
        public System.String InvoiceBank
        {
            set { _InvoiceBank = value; }
            get { return _InvoiceBank; }
        }
        /// <summary>
        /// InvoiceAccount属性封装
        /// </summary>
        public System.String InvoiceAccount
        {
            set { _InvoiceAccount = value; }
            get { return _InvoiceAccount; }
        }
        /// <summary>
        /// InvoiceAddress属性封装
        /// </summary>
        public System.String InvoiceAddress
        {
            set { _InvoiceAddress = value; }
            get { return _InvoiceAddress; }
        }
        /// <summary>
        /// InvoicePhone属性封装
        /// </summary>
        public System.String InvoicePhone
        {
            set { _InvoicePhone = value; }
            get { return _InvoicePhone; }
        }
    }
}
