using System;
using System.Collections.Generic;
using System.Text;
using DealerPlatformApiDemo.Core;

namespace DealerPlatformApiDemo.Entity
{
    /// <summary>
    /// 用户密码
    /// </summary>
    [Serializable]
    public class CustomerPwds:BaseEntity
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public CustomerPwds()
        { }

        private System.Int32 _id;
        private System.String _CustomerNo;
        private System.String _CustomerPwd;

        /// <summary>
        /// id属性封装
        /// </summary>
        public System.Int32 id
        {
            set { _id = value; }
            get { return _id; }
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
        /// CustomerPwd属性封装
        /// </summary>
        public System.String CustomerPwd
        {
            set { _CustomerPwd = value; }
            get { return _CustomerPwd; }
        }
    }
}
