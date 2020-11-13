using DealerPlatformApiDemo.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DealerPlatformApiDemo.Entity
{
    [Serializable]
    public class ProductPhotos : BaseEntity
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public ProductPhotos()
        { }

        private System.String _SysNo;
        private System.String _ProductNo;
        private System.String _ProductPhotoUrl;

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
        /// ProductPhotoUrl属性封装
        /// </summary>
        public System.String ProductPhotoUrl
        {
            set { _ProductPhotoUrl = value; }
            get { return _ProductPhotoUrl; }
        }
    }
}
