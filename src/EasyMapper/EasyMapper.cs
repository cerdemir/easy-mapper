using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EasyMapper
{   
    public class EasyMapper
    {
        public void SetItemFromRow<T>(T item, DataRow row) where T : new()
        {
            foreach (DataColumn c in row.Table.Columns)
            {
                PropertyInfo p = item.GetType().GetProperty(c.ColumnName);
                if (p != null && row[c] != DBNull.Value)
                {
                    p.SetValue(item, row[c], null);
                }
            }
        }

        public void SetItemToRow<T>(T item, DataRow row) where T : new()
        {
            foreach (DataColumn c in row.Table.Columns)
            {
                PropertyInfo p = item.GetType().GetProperty(c.ColumnName);
                if (p != null)
                {
                    var o = p.GetValue(item);
                    if (o is DateTime)
                        if ((DateTime)o == DateTime.MinValue)
                            o = DBNull.Value;
                    row[c.ColumnName] = o;
                }
            }
        }

        public T CreateItemFromRow<T>(DataRow row) where T : new()
        {
            T item = new T();
            if (row != null)
                SetItemFromRow(item, row);
            return item;
        }

        public List<T> CreateListFromTable<T>(DataTable dt) where T : new()
        {
            var ret = new List<T>();
            if (dt == null) return ret;
            foreach (DataRow row in dt.Rows)
            {
                var item = CreateItemFromRow<T>(row);
                ret.Add(item);
            }
            return ret;
        }

        public DataTable CreateDataTable<T>(List<T> list) where T : new()
        {
            var ret = new DataTable();
            var firstitem = list[0];
            var props = GetSortedProperties(firstitem.GetType());
            foreach (var prop in props)
            {
                var isExcluded = Attribute.IsDefined(prop, typeof(ExcludeMappingAttribute));
                if (!isExcluded)
                {
                    ret.Columns.Add(prop.Name, prop.PropertyType);
                }
            }
            foreach (var item in list)
            {
                var newrow = ret.NewRow();
                foreach (DataColumn c in ret.Columns)
                {
                    PropertyInfo p = item.GetType().GetProperty(c.ColumnName);
                    var propertyValue = p.GetValue(item);
                    newrow[c.ColumnName] = propertyValue;
                }
                ret.Rows.Add(newrow);
            }
            return ret;
        }

        private IOrderedEnumerable<PropertyInfo> GetSortedProperties(Type type)
        {
            var properties = type.GetProperties();
            var orderedProperties = properties.OrderBy(p =>
                (
                    (MappingOrderAttribute)p.GetCustomAttributes(typeof(MappingOrderAttribute), false).FirstOrDefault()
                )?.Order
            );
            return orderedProperties;
        }
    }
}
