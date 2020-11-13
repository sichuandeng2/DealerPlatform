using System;
using DealerPlatformApiDemo.Core;

namespace DealerPlatformApiDemo.Entity
{
  [Serializable]
    public class SaleOrderMasters:BaseEntity
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public SaleOrderMasters()
        { }

        private System.Int32 _Id;
        private System.String _SaleOrderNo;
        private System.String _CustomerNo;
        private System.String _InvoiceNo;
        private System.DateTime _InputDate;
        private System.String _StockNo;
        private System.String _EditUserNo;
        private System.DateTime _DeliveryDate;
        private System.String _Remark;

        /// <summary>
        /// Id属性封装
        /// </summary>
        public System.Int32 Id
        {
            set { _Id = value; }
            get { return _Id; }
        }
        /// <summary>
        /// SaleOrderNo属性封装
        /// </summary>
        public System.String SaleOrderNo
        {
            set { _SaleOrderNo = value; }
            get { return _SaleOrderNo; }
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
        /// InputDate属性封装
        /// </summary>
        public System.DateTime InputDate
        {
            set { _InputDate = value; }
            get { return _InputDate; }
        }
        /// <summary>
        /// StockNo属性封装
        /// </summary>
        public System.String StockNo
        {
            set { _StockNo = value; }
            get { return _StockNo; }
        }
        /// <summary>
        /// EditUserNo属性封装
        /// </summary>
        public System.String EditUserNo
        {
            set { _EditUserNo = value; }
            get { return _EditUserNo; }
        }
        /// <summary>
        /// DeliveryDate属性封装
        /// </summary>
        public System.DateTime DeliveryDate
        {
            set { _DeliveryDate = value; }
            get { return _DeliveryDate; }
        }
        /// <summary>
        /// Remark属性封装
        /// </summary>
        public System.String Remark
        {
            set { _Remark = value; }
            get { return _Remark; }
        }
    }
}