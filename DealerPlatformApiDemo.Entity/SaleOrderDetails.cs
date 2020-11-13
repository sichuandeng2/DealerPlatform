using System;
using DealerPlatformApiDemo.Core;

namespace DealerPlatformApiDemo.Entity
{
       [Serializable]
    public class SaleOrderDetails:BaseEntity
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public SaleOrderDetails()
        { }

        private System.Int32 _Id;
        private System.String _SaleOrderGuid;
        private System.String _SaleOrderNo;
        private System.String _ProductNo;
        private System.String _ProductName;
        private System.String _ProductPhotoUrl;
        private System.String _CustomerNo;
        private System.DateTime _InputDate;
        private System.Int32 _OrderNum;
        private System.Double _BasePrice;
        private System.Double _DiffPrice;
        private System.Double _SalePrice;

        /// <summary>
        /// Id属性封装
        /// </summary>
        public System.Int32 Id
        {
            set { _Id = value; }
            get { return _Id; }
        }
        /// <summary>
        /// SaleOrderGuid属性封装
        /// </summary>
        public System.String SaleOrderGuid
        {
            set { _SaleOrderGuid = value; }
            get { return _SaleOrderGuid; }
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
        /// ProductNo属性封装
        /// </summary>
        public System.String ProductNo
        {
            set { _ProductNo = value; }
            get { return _ProductNo; }
        }
        /// <summary>
        /// ProductName属性封装
        /// </summary>
        public System.String ProductName
        {
            set { _ProductName = value; }
            get { return _ProductName; }
        }
        /// <summary>
        /// ProductPhotoUrl属性封装
        /// </summary>
        public System.String ProductPhotoUrl
        {
            set { _ProductPhotoUrl = value; }
            get { return _ProductPhotoUrl; }
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
        /// InputDate属性封装
        /// </summary>
        public System.DateTime InputDate
        {
            set { _InputDate = value; }
            get { return _InputDate; }
        }
        /// <summary>
        /// OrderNum属性封装
        /// </summary>
        public System.Int32 OrderNum
        {
            set { _OrderNum = value; }
            get { return _OrderNum; }
        }
        /// <summary>
        /// BasePrice属性封装
        /// </summary>
        public System.Double BasePrice
        {
            set { _BasePrice = value; }
            get { return _BasePrice; }
        }
        /// <summary>
        /// DiffPrice属性封装
        /// </summary>
        public System.Double DiffPrice
        {
            set { _DiffPrice = value; }
            get { return _DiffPrice; }
        }
        /// <summary>
        /// SalePrice属性封装
        /// </summary>
        public System.Double SalePrice
        {
            set { _SalePrice = value; }
            get { return _SalePrice; }
        }
    }
}