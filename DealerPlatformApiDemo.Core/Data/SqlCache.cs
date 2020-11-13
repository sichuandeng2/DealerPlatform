using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DealerPlatformApiDemo.Core.Data
{
    public partial class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        static Repository()
        {
            _type = typeof(TEntity);
            _props = _type.GetProperties();
            string strCols = GetColumnsStr();
            var cols = GetColumns().Where(m => !m.Equals("Id"));
            string strColsWithoutId = string.Join(",", cols);
            //listAll
            _listAllText = $"SELECT {strCols} FROM {_type.GetTableName()}";
            //listById
            _listByIdText = $@"SELECT {strCols} FROM {_type.GetTableName()} WHERE Id = @Id";
            //insert
            _insertText =
                $"INSERT INTO {_type.GetTableName()} ({string.Join(",", strColsWithoutId)}) VALUES ({string.Join(",", cols.Select(m => "@" + m))})";
            //insertBackRecord
            _insertBackText =
                $"INSERT INTO {_type.GetTableName()} ({string.Join(",", strColsWithoutId)}) output inserted.Id VALUES ({string.Join(",", cols.Select(m => "@" + m))})";
            //update
            List<string> list = new List<string>();
            foreach (var prop in _props)
            {
                if (prop.GetColumnName() != "Id")
                {
                    list.Add($"{prop.GetColumnName()} = @{prop.GetColumnName()}");
                }
            }

            string updateItem = string.Join(",", list);
            _updateText = $"UPDATE {_type.GetTableName()} SET {updateItem} WHERE Id=@Id";

            //delete
            _deleteText = $"DELETE FROM {_type.GetTableName()}";
        }

        private static Type _type;
        private static PropertyInfo[] _props;
        private static string _listAllText;
        private static string _listByIdText;
        private static string _insertText;
        private static string _insertBackText;
        private static string _updateText;
        private static string _deleteText;
    }
}
