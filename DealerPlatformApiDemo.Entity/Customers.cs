using System;
using System.Collections.Generic;
using System.Text;
using DealerPlatformApiDemo.Core;

namespace DealerPlatformApiDemo.Entity
{
    /// <summary>
    /// 用户主实体
    /// </summary>
    [Table("Customers")]
    [Serializable]
    public class Customers: BaseEntity
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public Customers()
        { }

        private System.String _customerNo;
        private System.String _CustomerName;
        private System.String _FirstAreaNo;
        private System.String _FirstAreaName;
        private System.String _AreaNo;
        private System.String _AreaName;
        private System.String _Province;
        private System.String _City;
        private System.String _Tel;
        private System.String _Phone;
        private System.String _Fax;
        private System.String _Address;
        private System.String _BankAccount;
        private System.String _BankNo;
        private System.String _BankName;
        private System.String _Ein;
        private System.String _CustomerNote;
        private System.String _OwnerWorkerNo;
        private System.String _OwnerWorkerName;
        private System.String _OwnerWorkerTel;
        private System.String _OpenId;
        private System.String _HeadImgUrl;
        /// <summary>
        /// CustomerNo属性封装
        /// </summary>
        [Column("CustomerNo")]
        public System.String CustomerNo
        {
            set { _customerNo = value; }
            get { return _customerNo; }
        }
        /// <summary>
        /// CustomerName属性封装
        /// </summary>
        public System.String CustomerName
        {
            set { _CustomerName = value; }
            get { return _CustomerName; }
        }
        /// <summary>
        /// FirstAreaNo属性封装
        /// </summary>
        public System.String FirstAreaNo
        {
            set { _FirstAreaNo = value; }
            get { return _FirstAreaNo; }
        }
        /// <summary>
        /// FirstAreaName属性封装
        /// </summary>
        public System.String FirstAreaName
        {
            set { _FirstAreaName = value; }
            get { return _FirstAreaName; }
        }
        /// <summary>
        /// AreaNo属性封装
        /// </summary>
        public System.String AreaNo
        {
            set { _AreaNo = value; }
            get { return _AreaNo; }
        }
        /// <summary>
        /// AreaName属性封装
        /// </summary>
        public System.String AreaName
        {
            set { _AreaName = value; }
            get { return _AreaName; }
        }
        /// <summary>
        /// Province属性封装
        /// </summary>
        public System.String Province
        {
            set { _Province = value; }
            get { return _Province; }
        }
        /// <summary>
        /// City属性封装
        /// </summary>
        public System.String City
        {
            set { _City = value; }
            get { return _City; }
        }
        /// <summary>
        /// Tel属性封装
        /// </summary>
        public System.String Tel
        {
            set { _Tel = value; }
            get { return _Tel; }
        }
        /// <summary>
        /// Phone属性封装
        /// </summary>
        public System.String Phone
        {
            set { _Phone = value; }
            get { return _Phone; }
        }
        /// <summary>
        /// Fax属性封装
        /// </summary>
        public System.String Fax
        {
            set { _Fax = value; }
            get { return _Fax; }
        }
        /// <summary>
        /// Address属性封装
        /// </summary>
        public System.String Address
        {
            set { _Address = value; }
            get { return _Address; }
        }
        /// <summary>
        /// BankAccount属性封装
        /// </summary>
        public System.String BankAccount
        {
            set { _BankAccount = value; }
            get { return _BankAccount; }
        }
        /// <summary>
        /// BankNo属性封装
        /// </summary>
        public System.String BankNo
        {
            set { _BankNo = value; }
            get { return _BankNo; }
        }
        /// <summary>
        /// BankName属性封装
        /// </summary>
        public System.String BankName
        {
            set { _BankName = value; }
            get { return _BankName; }
        }
        /// <summary>
        /// Ein属性封装
        /// </summary>
        public System.String Ein
        {
            set { _Ein = value; }
            get { return _Ein; }
        }
        /// <summary>
        /// CustomerNote属性封装
        /// </summary>
        public System.String CustomerNote
        {
            set { _CustomerNote = value; }
            get { return _CustomerNote; }
        }
        /// <summary>
        /// OwnerWorkerNo属性封装
        /// </summary>
        public System.String OwnerWorkerNo
        {
            set { _OwnerWorkerNo = value; }
            get { return _OwnerWorkerNo; }
        }
        /// <summary>
        /// OwnerWorkerName属性封装
        /// </summary>
        public System.String OwnerWorkerName
        {
            set { _OwnerWorkerName = value; }
            get { return _OwnerWorkerName; }
        }
        /// <summary>
        /// OwnerWorkerTel属性封装
        /// </summary>
        public System.String OwnerWorkerTel
        {
            set { _OwnerWorkerTel = value; }
            get { return _OwnerWorkerTel; }
        }
        /// <summary>
        /// OpenId属性封装
        /// </summary>
        public System.String OpenId
        {
            set { _OpenId = value; }
            get { return _OpenId; }
        }
        /// <summary>
        /// HeadImgUrl属性封装
        /// </summary>
        public System.String HeadImgUrl
        {
            set { _HeadImgUrl = value; }
            get { return _HeadImgUrl; }
        }
    }
}
