using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data;
using System.Reflection;
using System.Data.Objects;

namespace Magic.Extensions
{
    
    public static class EntityExtensions
    {
        public static IQueryable<T> Include<T>
            (this IQueryable<T> source, string path)
        {
            var objectQuery = source as ObjectQuery<T>;
            if (objectQuery != null)
            {
                return objectQuery.Include(path);
            }
            return source;
        }

        //partial credit to http://www.singingeels.com/Blogs/Nullable/2008/03/26/Dynamic_LINQ_OrderBy_using_String_Names.aspx
        public static List<T> Sort<T>(this List<T> entity, string columnName)
        {  
            string[] columns = columnName.Split('.');            
            List<ParameterExpression> parameters = new List<ParameterExpression>();
            Expression exp = null;
            for (int i=0; i<columns.Length; i++)
            {
                if (i==0)
                {
                    parameters.Add(Expression.Parameter(typeof(T),typeof(T).Name));
                    exp = Expression.Property(parameters[i], columns[i]);
                }
                else
                {
                    parameters.Add(Expression.Parameter(parameters[i-1].Type.GetProperty(columns[i-1]).PropertyType,parameters[i-1].Type.GetProperty(columns[i-1]).Name));
                    exp = Expression.Property(exp, parameters[i-1].Type.GetProperty(columns[i-1]).PropertyType, columns[i]);
                }
            }
            var sortExpression = Expression.Lambda<Func<T, object>>(Expression.Convert(exp, typeof(object)), parameters[0]);
            return entity.AsQueryable().OrderBy(sortExpression).ToList();
        }

       
        public static List<T> SortDESC<T>(this List<T> entity, string columnName)
        {
            string[] columns = columnName.Split('.');
            List<ParameterExpression> parameters = new List<ParameterExpression>();
            Expression exp = null;
            for (int i = 0; i < columns.Length; i++)
            {
                if (i == 0)
                {
                    parameters.Add(Expression.Parameter(typeof(T), typeof(T).Name));
                    exp = Expression.Property(parameters[i], columns[i]);
                }
                else
                {
                    parameters.Add(Expression.Parameter(parameters[i - 1].Type.GetProperty(columns[i - 1]).PropertyType, parameters[i - 1].Type.GetProperty(columns[i - 1]).Name));
                    exp = Expression.Property(exp, parameters[i - 1].Type.GetProperty(columns[i - 1]).PropertyType, columns[i]);
                }
            }

            var sortExpression = Expression.Lambda<Func<T, object>>(Expression.Convert(exp, typeof(object)), parameters[0]);

            return entity.AsQueryable().OrderByDescending(sortExpression).ToList();
        }

        /// <summary>
        /// Filters an entity by the specified columnname and value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<T> Filter<T>(this List<T> entity, string columnName, object value) 
        {
            return entity.Filter<T>(columnName, value, OperatorType.Equals);
        }

        public enum OperatorType : int
        {
             Equals
            ,GreaterThan
            ,EqualToOrGreaterThan
            ,LessThan
            ,EqualToOrLessThan
        }

        public static List<T> Filter<T>(this List<T> entity, string columnName, object value, OperatorType opType)
        {
            var param = Expression.Parameter(typeof(T), typeof(T).Name);

            switch (opType)
            {
                
                case OperatorType.EqualToOrGreaterThan:
                    var filterEqualsOrGreater = Expression.Lambda<Func<T, bool>>(Expression.GreaterThanOrEqual(Expression.Property(param, columnName), Expression.Constant(value)), param);
                    return entity.AsQueryable().Where(filterEqualsOrGreater).ToList();
                case OperatorType.GreaterThan:
                    var filterGreater = Expression.Lambda<Func<T, bool>>(Expression.GreaterThanOrEqual(Expression.Property(param, columnName), Expression.Constant(value)), param);
                    return entity.AsQueryable().Where(filterGreater).ToList();
                case OperatorType.EqualToOrLessThan:
                    var filterEqualToOrLessThan = Expression.Lambda<Func<T, bool>>(Expression.GreaterThanOrEqual(Expression.Property(param, columnName), Expression.Constant(value)), param);
                    return entity.AsQueryable().Where(filterEqualToOrLessThan).ToList();
                case OperatorType.LessThan:
                    var filterLessThan = Expression.Lambda<Func<T, bool>>(Expression.GreaterThanOrEqual(Expression.Property(param, columnName), Expression.Constant(value)), param);
                    return entity.AsQueryable().Where(filterLessThan).ToList();
                case OperatorType.Equals:
                default:
                    var filterEquals = Expression.Lambda<Func<T, bool>>(Expression.Equal(Expression.Property(param, columnName), Expression.Constant(value)), param);
                    return entity.AsQueryable().Where(filterEquals).ToList();
            }        
        }

        public static object GetValueFromEntity(this object entity, string path)
        {
            try
            {

                if (path.Contains('.'))
                {
                    return entity.GetType().GetProperty(path.Substring(0, path.IndexOf('.'))).GetValue(entity, null).GetValueFromEntity(path.Substring(path.IndexOf('.') + 1));
                }
                else
                {
                    return entity.GetType().GetProperty(path).GetValue(entity, null);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static void SetPropertyValue(this object entity, string path, object value)
        {
            try
            {
                if (path.Contains('.'))
                {
                    entity.GetType().GetProperty(path.Substring(0, path.IndexOf('.'))).GetValue(entity, null).SetPropertyValue(path.Substring(path.IndexOf('.') + 1),value);
                }
                else
                {
                    entity.GetType().GetProperty(path).SetValue(entity, value, null);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static System.Linq.IQueryable<T> Filter<T>(this System.Linq.IQueryable<T> entity, string columnName, object value, OperatorType opType)
        {
            var param = Expression.Parameter(typeof(T), typeof(T).Name);

            switch (opType)
            {
                case OperatorType.EqualToOrGreaterThan:
                    var filterEqualsOrGreater = Expression.Lambda<Func<T, bool>>(Expression.GreaterThanOrEqual(Expression.Property(param, columnName), Expression.Constant(value)), param);
                    return entity.AsQueryable().Where(filterEqualsOrGreater);
                case OperatorType.GreaterThan:
                    var filterGreater = Expression.Lambda<Func<T, bool>>(Expression.GreaterThanOrEqual(Expression.Property(param, columnName), Expression.Constant(value)), param);
                    return entity.AsQueryable().Where(filterGreater);
                case OperatorType.EqualToOrLessThan:
                    var filterEqualToOrLessThan = Expression.Lambda<Func<T, bool>>(Expression.GreaterThanOrEqual(Expression.Property(param, columnName), Expression.Constant(value)), param);
                    return entity.AsQueryable().Where(filterEqualToOrLessThan);
                case OperatorType.LessThan:
                    var filterLessThan = Expression.Lambda<Func<T, bool>>(Expression.GreaterThanOrEqual(Expression.Property(param, columnName), Expression.Constant(value)), param);
                    return entity.AsQueryable().Where(filterLessThan);
                case OperatorType.Equals:
                default:
                    var filterEquals = Expression.Lambda<Func<T, bool>>(Expression.Equal(Expression.Property(param, columnName), Expression.Constant(value)), param);
                    return entity.AsQueryable().Where(filterEquals);
            }        
        }


        public static DataTable ToDataTable<T>(this T entity)
        {
            return Magically.BuildTableFromEntity(entity);
        }
    }
}
