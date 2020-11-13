using DealerPlatformApiDemo.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DealerPlatformApiDemo.Entity
{
    [Serializable]
    public class ProductSales : BaseEntity
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public ProductSales()
        { }

        private System.String _SysNo;
        private System.String _ProductNo;
        private System.String _StockNo;
        private System.Double _SalePrice;

        /// <summary>
        /// SysNo属性封装
        /// </summary>
        public System.String SysNo
        {
            set { _SysNo = value; }
            get { return _SysNo; }
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
        /// StockNo属性封装
        /// </summary>
        public System.String StockNo
        {
            set { _StockNo = value; }
            get { return _StockNo; }
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
