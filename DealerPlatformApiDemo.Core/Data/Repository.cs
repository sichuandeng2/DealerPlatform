using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using static DealerPlatformApiDemo.Core.Data.RepositoryCompare;

namespace DealerPlatformApiDemo.Core.Data
{
    public partial class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity//,new()
    {
        private readonly SqlHelperW _sqlHelperW;
        private readonly SqlHelperR _sqlHelperR;

        public Repository(SqlHelperW sqlHelperW, SqlHelperR sqlHelperR)
        {
            _sqlHelperW = sqlHelperW;
            _sqlHelperR = sqlHelperR;
        }


        public IEnumerable<TEntity> ListAll()
        {
            //string strCols = GetColumnsStr();
            DataTable dt = _sqlHelperR.ExecuteTable(_listAllText);
            ToModelList(dt, out List<TEntity> list);
            return list;
        }
        public IEnumerable<TEntity> ListById(int id)
        {
            //string strCols = GetColumnsStr();
            DataTable dt = _sqlHelperR.ExecuteTable(_listByIdText,
                new SqlParameter("@Id", id));
            ToModelList(dt, out List<TEntity> list);
            return list;
        }

        public IEnumerable<TEntity> ListByCustom(
            string fieldName, 
            string fieldValue, 
            CompareType compareType = CompareType.Equal)
        {
            string eq = GetCompare(compareType);
            string strCols = GetColumnsStr();
            DataTable dt =
                _sqlHelperR.ExecuteTable(
                    $"{_listAllText} WHERE {fieldName} {eq} @fieldName",
                    new SqlParameter("@fieldName", fieldValue));
            ToModelList(dt, out List<TEntity> list);
            return list;
        }
        public IEnumerable<TEntity> ListByCustoms(Dictionary<string, string> keyValuePairs)
        {
            //放置搜索语句
            List<string> whereValue = new List<string>();
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            foreach (var keyValuePair in keyValuePairs)
            {
                whereValue.Add($"{keyValuePair.Key} = @{keyValuePair.Key}");
                sqlParameters.Add(new SqlParameter($"@{keyValuePair.Key}", keyValuePair.Value));
            }
            string strCols = GetColumnsStr();
            DataTable dt = _sqlHelperR.ExecuteTable(
                $"{_listAllText} WHERE {string.Join(" AND ", whereValue)}",
                sqlParameters.ToArray());
            ToModelList(dt, out List<TEntity> list);
            return list;
        }
        public IEnumerable<TEntity> ListByCustoms(Dictionary<string, ListByCustomsEntity> keyValuePairs)
        {
            //放置搜索语句
            List<string> whereValue = new List<string>();
            //放置参数
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            string whereText = "";
            int i = 0;
            foreach (var keyValuePair in keyValuePairs)
            {
                var value = keyValuePair.Value;
                string eq = value.IsEq ? "=" : "<>"; //判断是等于或不等于
                //whereValue.Add($"{keyValuePair.Key} {eq} @{keyValuePair.Key}");
                string andText = value.IsAnd ? "AND" : "OR"; // 是否为AND或OR连接
                whereText += $"{(i++ == 0 ? "" : " " + andText)} {keyValuePair.Key} {eq} @{keyValuePair.Key}"; // 拼接where语句
                sqlParameters.Add(new SqlParameter($"@{keyValuePair.Key}", value.Value)); //添加筛选参数
            }
            //whereText = whereText.Substring(whereText.IndexOf(" "), whereText.Length - 1);
            string strCols = GetColumnsStr();
            DataTable dt = _sqlHelperR.ExecuteTable(
                $"{_listAllText} WHERE {whereText}",
                sqlParameters.ToArray());
            ToModelList(dt, out List<TEntity> list);
            return list;
        }

        public IEnumerable<TEntity> ListByCustomWhereIn(string fieldName, params string[] fieldValues)
        {
            //SELECT * FROM xxTable WHERE IN(@field1,@field2,@field3)
            //var ints =[1,2,3,4,5,6]
            //string.join("|",ints)=>"1|2|3|4|5|6"
            if (fieldValues == null || fieldValues.Length == 0) return ListAll();
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            List<string> sqlParams = new List<string>();
            for (int i = 0; i < fieldValues.Length; i++)
            {
                sqlParams.Add("@field" + i);
                sqlParameters.Add(new SqlParameter("@field" + i, fieldValues[i]));
            }
            string strCols = GetColumnsStr();
            DataTable dt = _sqlHelperR.ExecuteTable(
                $"{_listAllText} WHERE {fieldName} IN({string.Join(",", sqlParams)})",
                sqlParameters.ToArray());
            ToModelList(dt, out List<TEntity> list);
            return list;
        }

        public int Insert(TEntity entity)
        {
            //INSERT INTO Customers {col1,col2,clo3} VALUES(@Col1,@Col2,@Col3);
            IEnumerable<PropertyInfo> props = _type.GetProperties().Where(m => !m.Name.Equals("Id"));
            //IEnumerable<string> strCols = GetColumns().Where(m => !m.Equals("Id"));

            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            foreach (var prop in props)
            {
                sqlParameters.Add(new SqlParameter($"@{prop.Name}", SqlHelperBase.ToDbValue(prop.GetValue(entity))));
            }

            return _sqlHelperW.ExecuteNoQuery(_insertText, sqlParameters.ToArray());
        }
        public object InsertBackRecord(TEntity entity)
        {
            //INSERT INTO Customers {col1,col2,clo3} VALUES(@Col1,@Col2,@Col3);

            var props = _type.GetProperties().Where(m => !m.Name.Equals("Id"));
            //var strCols = GetColumns().Where(m => !m.Equals("Id"));
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            foreach (var prop in props)
            {
                sqlParameters.Add(new SqlParameter($"@{prop.Name}", SqlHelperBase.ToDbValue(prop.GetValue(entity))));
            }

            return _sqlHelperW.ExecuteScalar(_insertBackText, sqlParameters.ToArray());
        }

        public int Update(TEntity entity)
        {
            //UPDATE Customers SET Col1=@Col1,Col2=@Col2,Col3=@Col3;
            var props = _type.GetProperties();
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            foreach (var prop in props)
            {
                sqlParameters.Add(new SqlParameter($"@{prop.Name}", SqlHelperBase.ToDbValue(prop.GetValue(entity))));
            }

            return _sqlHelperW.ExecuteNoQuery(_updateText, sqlParameters.ToArray());
        }

        public int Delete(TEntity entity)
        {
            string sqlText = $"{_deleteText} WHERE Id = @Id";
            return _sqlHelperW.ExecuteNoQuery(sqlText, new SqlParameter("@Id", _type.GetProperty("Id").GetValue(entity)));
        }
        public int Delete(int id)
        {
            string sqlText = $"{_deleteText} WHERE Id = @Id";
            return _sqlHelperW.ExecuteNoQuery(sqlText, new SqlParameter("@Id", id));
        }

        public int DeleteByCustomer(string fieldName, string fieldValue)
        {
            if (string.IsNullOrEmpty(fieldValue)) return 0;
            string sqlText = $"{_deleteText} WHERE {fieldName} = @{fieldName}";
            return _sqlHelperW.ExecuteNoQuery(sqlText, new SqlParameter($"@{fieldName}", fieldValue));
        }

        private static void ToModelList(DataTable dt, out List<TEntity> list)
        {
            list = new List<TEntity>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(ToModel(dr));
            }
        }

        private static TEntity ToModel(DataRow dr)
        {
            //如果给泛型加了一个new()的约束，那么次泛型可以直接通过new方法实例化
            //TEntity entity = new TEntity();
            //如果没有给泛型加new()的约束，则需要通过反射生成实例
            TEntity entity = (TEntity)Activator.CreateInstance(_type);
            var props = _type.GetProperties();
            foreach (var prop in props)
            {
                if (dr.Table.Columns.Contains(prop.Name))
                {
                    prop.SetValue(entity, SqlHelperBase.FromDbValue(dr[prop.Name]));
                }
            }
            return entity;
        }

        private static string GetColumnsStr()
        {
            var cols = GetColumns();
            string strCols = string.Join(",", cols);
            return strCols;
        }
        private static IEnumerable<string> GetColumns()
        {
            var cols = _props.Select(m => m.GetColumnName());
            return cols;
        }

        private string GetCompare(CompareType compareType)
        {
            switch (compareType)
            {
                default:
                case CompareType.Equal:
                    return "=";
                case CompareType.Unequal:
                    return "<>";
                case CompareType.greaterThan:
                    return ">";
                case CompareType.lessThan:
                    return "<";
            }
        }
    }



    // public class Users
    // {
    //     public int Id { get; set; }
    //     public string UserName { get; set; }
    // }
}
