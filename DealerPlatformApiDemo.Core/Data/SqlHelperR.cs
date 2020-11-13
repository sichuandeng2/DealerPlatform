using System;
using System.Collections.Generic;
using System.Text;

namespace DealerPlatformApiDemo.Core.Data
{
    public class SqlHelperR:SqlHelperBase
    {
        public SqlHelperR(SqlHelperOptions options) : base(options)
        {

        }

        public override void OnConfiguring(SqlHelperBuilder sqlHelperBuilder)
        {
            var conStr = SqlConnectionStringHelper.GetSectionValue("ConStrRead");
            sqlHelperBuilder.UseSqlServer(conStr);
        }
    }
}
