using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DealerPlatformApiDemo.Core.Data
{
    public class SqlHelperW: SqlHelperBase
    {
        public SqlHelperW(SqlHelperOptions options) : base(options)
        {

        }

        public override void OnConfiguring(SqlHelperBuilder sqlHelperBuilder)
        {
            if (!sqlHelperBuilder.IsConfiged)
            {
                var conStr = SqlConnectionStringHelper.GetSectionValue("ConStrWrite");
                sqlHelperBuilder.UseSqlServer(conStr);
            }
           
        }
    }
}
