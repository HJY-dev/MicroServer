using InfluxData.Net.Common.Enums;
using InfluxData.Net.Common.Helpers;
using InfluxData.Net.Common.Infrastructure;
using InfluxData.Net.InfluxDb;
using InfluxData.Net.InfluxDb.Models;
using MicroServer.Common.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServer.Common.InfluxDb
{
    public class InfluxDbRepository
    {
        public static readonly InfluxDbRepository DefaultInstance = new InfluxDbRepository();
        private InfluxDbClient influxDbClient;

        public string DbUrl => ConfigurationManager.Appsettings.GetSection("influxdb:url").Value ?? "http://127.0.0.1:8086";
        public string DbUserName => ConfigurationManager.Appsettings.GetSection("influxdb:username").Value ?? "root";
        public string DbPassword => ConfigurationManager.Appsettings.GetSection("influxdb:password").Value ?? "root";
        public string DbName => ConfigurationManager.Appsettings.GetSection("influxdb:dbname").Value ?? "Market";

        private InfluxDbRepository(string url, string userName, string password)
        {
            this.influxDbClient = new InfluxDbClient(url, userName, password, InfluxDbVersion.Latest);
        }
        private InfluxDbRepository()
        {
            this.influxDbClient = new InfluxDbClient(DbUrl, DbUserName, DbPassword, InfluxDbVersion.Latest);
        }

        /// <summary>
        /// 创建数据库
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public async Task<IInfluxDataApiResponse> CreateDb(string dbName = null)
        {
            if (string.IsNullOrEmpty(dbName))
                dbName = DbName;
            return await influxDbClient.Database.CreateDatabaseAsync(dbName);
        }

        /// <summary>
        /// 写入单个对象
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="baseLog">基本信息</param>
        /// <returns></returns>
        public async Task InsertAsync<T>(T model, string dbName, string tbName) where T : new()
        {
            try
            {
                var tags = new Dictionary<string, object>();
                var fields = new Dictionary<string, object>();
                var propertyInfos = typeof(T).GetProperties();
                foreach (var item in propertyInfos)
                {
                    fields.Add(item.Name, item.GetValue(model, null));
                }
                var pointModel = new Point()
                {
                    Name = tbName,//表名
                    Tags = tags,
                    Fields = fields,
                    Timestamp = DateTime.UtcNow
                };

                await influxDbClient.Client.WriteAsync(pointModel, dbName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 从InfluxDB中读取数据 返回string，和返回List的性能几乎一样
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public async Task<string> GetDataAsync(string sql, string dbName)
        {
            string rtn = string.Empty;
            try
            {
                //传入查询命令，支持多条
                var queries = new[] { sql };
                Stopwatch sw = new Stopwatch();
                sw.Start();
                //从指定库中查询数据
                var response = await influxDbClient.Client.QueryAsync(queries, dbName);
                sw.Stop();
                long dur = sw.ElapsedMilliseconds;
                //得到Serie集合对象（返回执行多个查询的结果）
                var series = response.ToList();

                if (series.Any())
                {
                    var dt = new DataTable();
                    //取出第一条命令的查询结果，是一个集合
                    var column = series[0].Columns;
                    foreach (var item in column)
                    {
                        dt.Columns.Add(item, typeof(string));
                    }
                    var list = series[0].Values;
                    foreach (var row in list)
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < row.Count; i++)
                        {
                            dr[i] = row[i];
                        }
                        dt.Rows.Add(dr);
                    }
                    rtn = JsonConvert.SerializeObject(dt);
                }
                else
                {
                    rtn = "";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rtn;
        }

        /// <summary>
        /// 查询 返回List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="dbName"></param>
        /// <returns></returns>
        public async Task<List<T>> GetDataListAsync<T>(string sql, string dbName) where T : new()
        {
            List<T> listrtn = new List<T>();
            try
            {
                //传入查询命令，支持多条
                var queries = new[] { sql };
                //从指定库中查询数据
                var response = await influxDbClient.Client.QueryAsync(queries, dbName);
                //得到Serie集合对象（返回执行多个查询的结果）
                var series = response.ToList();
                if (series.Any())
                {
                    //取出第一条命令的查询结果，是一个集合
                    var column = series[0].Columns.ToList();
                    var list = series[0].Values;
                    for (int i = 0; i < list.Count(); i++)
                    {
                        var temp = new T();
                        var propertyInfos = typeof(T).GetProperties();
                        foreach (var item in propertyInfos)
                        {
                            if (item.Name != "time")
                            {
                                int index = column.FindIndex(x => x.Equals(item.Name));
                                if (index != -1)
                                {
                                    item.SetValue(temp, list[i][index], null);//给对象赋值
                                }
                            }
                        }
                        listrtn.Add(temp);
                    }
                }
                else
                {
                    listrtn = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listrtn;
        }

    }
}
