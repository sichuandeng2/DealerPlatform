namespace DealerPlatformApiDemo.Entity
{
   using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DealerPlatformApiDemo.Core;

    [Serializable]
    public class ShoppingCarts:BaseEntity
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public ShoppingCarts()
        { }

        private System.String _CartGuid;
        private System.String _CustomerNo;
        private System.String _ProductNo;
        private System.Int32 _ProductNum;
        private System.Boolean _CartSelected;

        /// <summary>
        /// CartGuid属性封装
        /// </summary>
        public System.String CartGuid
        {
            set { _CartGuid = value; }
            get { return _CartGuid; }
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
        /// ProductNo属性封装
        /// </summary>
        public System.String ProductNo
        {
            set { _ProductNo = value; }
            get { return _ProductNo; }
        }
        /// <summary>
        /// ProductNum属性封装
        /// </summary>
        public System.Int32 ProductNum
        {
            set { _ProductNum = value; }
            get { return _ProductNum; }
        }
        /// <summary>
        /// CartSelected属性封装
        /// </summary>
        public System.Boolean CartSelected
        {
            set { _CartSelected = value; }
            get { return _CartSelected; }
        }
    }
}
