using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Test
{
    public class DapperHelper : IDisposable
    {
        public IDbConnection Connection { get; }
        public enum Providers
        {
            SqlClient,
            // ReSharper disable once InconsistentNaming
            SQLite,
            MySql
        }

        public DapperHelper(string connectionString, Providers providers)
        {
            switch (providers)
            {
                case Providers.SqlClient:
                    Connection = new SqlConnection(connectionString);
                    break;
                case Providers.MySql:
                    Connection = new MySqlConnection(connectionString);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(providers), providers, null);
            }
        }
        public void Dispose() => Connection.Dispose();

        #region 通用方法
        public IDbTransaction BeginTransaction() => Connection.BeginTransaction();
        public IDbTransaction BeginTransaction(IsolationLevel il) => Connection.BeginTransaction(il);
        public void ChangeDatabase(string databaseName) => Connection.ChangeDatabase(databaseName);
        public void Close() => Connection.Close();
        public IDbCommand CreateCommand() => Connection.CreateCommand();

        public int Execute(string sql, object param = null) => Connection.Execute(sql, param);
        public IDataReader ExecuteReader(string sql, object param = null) => Connection.ExecuteReader(sql, param);
        public object ExecuteScalar(string sql, object param = null) => Connection.ExecuteScalar(sql, param);
        public SqlMapper.GridReader QueryMultiple(string sql, object param = null) => Connection.QueryMultiple(sql, param);

        public IEnumerable<dynamic> Query(string sql, object param = null) => Connection.Query(sql, param);
        public dynamic QueryFirst(string sql, object param = null) => Connection.QueryFirst(sql, param);
        public dynamic QueryFirstOrDefault(string sql, object param = null) => Connection.QueryFirstOrDefault(sql, param);
        public dynamic QuerySingle(string sql, object param = null) => Connection.QuerySingle(sql, param);
        public dynamic QuerySingleOrDefaul(string sql, object param = null) => Connection.QuerySingleOrDefault(sql, param);

        public IEnumerable<T> Query<T>(string sql, object param = null) => Connection.Query<T>(sql, param);
        public T QueryFirst<T>(string sql, object param = null) => Connection.QueryFirst<T>(sql, param);
        public T QueryFirstOrDefault<T>(string sql, object param = null) => Connection.QueryFirstOrDefault<T>(sql, param);
        public T QuerySingle<T>(string sql, object param = null) => Connection.QuerySingle<T>(sql, param);
        public T QuerySingleOrDefault<T>(string sql, object param = null) => Connection.QuerySingleOrDefault<T>(sql, param);

        public Task<int> ExecuteAsync(string sql, object param = null) => Connection.ExecuteAsync(sql, param);
        public Task<IDataReader> ExecuteReaderAsync(string sql, object param = null) => Connection.ExecuteReaderAsync(sql, param);
        public Task<object> ExecuteScalarAsync(string sql, object param = null) => Connection.ExecuteScalarAsync(sql, param);
        public Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null) => Connection.QueryMultipleAsync(sql, param);

        public Task<IEnumerable<dynamic>> QueryAsync(string sql, object param = null) => Connection.QueryAsync(sql, param);
        public Task<dynamic> QueryFirstAsync(string sql, object param = null) => Connection.QueryFirstAsync(sql, param);
        public Task<dynamic> QueryFirstOrDefaultAsync(string sql, object param = null) => Connection.QueryFirstOrDefaultAsync(sql, param);
        public Task<dynamic> QuerySingleAsync(string sql, object param = null) => Connection.QuerySingleAsync(sql, param);
        public Task<dynamic> QuerySingleOrDefaultAsync(string sql, object param = null) => Connection.QuerySingleOrDefaultAsync(sql, param);

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null) => Connection.QueryAsync<T>(sql, param);
        public Task<T> QueryFirstAsync<T>(string sql, object param = null) => Connection.QueryFirstAsync<T>(sql, param);
        public Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null) => Connection.QueryFirstOrDefaultAsync<T>(sql, param);
        public Task<T> QuerySingleAsync<T>(string sql, object param = null) => Connection.QuerySingleAsync<T>(sql, param);
        public Task<T> QuerySingleOrDefaultAsync<T>(string sql, object param = null) => Connection.QuerySingleOrDefaultAsync<T>(sql, param);

        #endregion

        #region 参数拼接
        /// <summary>
        /// 获取类名字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string ClassName<T>() => typeof(T).ToString().Split('.').Last();

        /// <summary>
        /// 属性名称拼接并附加连接符
        /// </summary>
        /// <param name="separator">分隔符</param>
        /// <param name="param">要拼接的动态类型</param>
        /// <param name="isParam">是否为参数,即是否增加前缀'@'</param>
        /// <returns></returns>
        public static string Joint(string separator, object param, bool isParam = false)
        {
            var prefix = isParam ? "@" : string.Empty;
            var propertys = param.GetType().GetProperties().Select(t => $"{prefix}{t.Name}").ToArray();
            var joint = new StringBuilder();
            for (var i = 0; i < propertys.Length; i++)
            {
                joint.Append(i != 0 ? $"{separator}{propertys[i]}" : propertys[i]);
            }
            return joint.ToString();
        }

        /// <summary>
        /// 以"param=@param"格式拼接属性名称并附加连接符
        /// </summary>
        /// <param name="separator">分隔符</param>
        /// <param name="param">要拼接的动态类型</param>
        /// <returns></returns>
        public static string ParamJoint(string separator, object param)
        {
            var propertys = param.GetType().GetProperties().Where(t => t.GetValue(param) != null).Select(t => t.Name).Select(t => $"{t}=@{t}").ToArray();
            var joint = new StringBuilder();
            for (var i = 0; i < propertys.Length; i++)
            {
                joint.Append(i != 0 ? $"{separator}{propertys[i]}" : propertys[i]);
            }
            return joint.ToString();
        }

        /// <summary>
        /// 将参数名和参数值拼接并附加连接符,用于where语句拼接
        /// </summary>
        /// <param name="separator"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string ValueJoint(string separator, object param)
        {
            var joint = new StringBuilder();
            var count = 0;
            foreach (var item in param.GetType().GetProperties())
            {
                var value = item.GetValue(param, null);
                if (value == null) continue;
                var slice = $"{item.Name}=\'{value}\'";
                joint.Append(count != 0 ? $"{separator}{slice}" : slice);
                count++;
            }
            return joint.ToString();
        }

        #endregion

        #region 语句拼接
        public static string CompileInsert<T>(object param)
        {
            return $"insert into {ClassName<T>()}({Joint(",", param)}) values ({Joint(",", param, true)})";
        }

        public static string CompileDelete<T>(object param)
        {
            return $"delete from {ClassName<T>()} where {ParamJoint(" and ", param)}";
        }

        public static string CompileUpdate<T>(object setParam, object whereParam)
        {
            return $"update {ClassName<T>()} set {ValueJoint(",", setParam)} where {ValueJoint(" and ", whereParam)}";
        }

        public static string CompileSelect<T>(object param)
        {
            return $"select {Joint(",", param)} from {ClassName<T>()}";
        }

        #endregion

        #region 便捷查询
        public static T GetQuery<T>(DapperHelper conn, dynamic param)
        {
            return conn.QueryFirstOrDefault<T>($"select * from {ClassName<T>()} where {ParamJoint(" and ", param)}", param);
        }

        #endregion

    }
}
