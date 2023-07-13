using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTest.Model
{
    /// <summary>
    /// 临时测试mongo用户实体
    /// </summary>
    public class user_mongo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { set; get; }
        public string user_id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string user_name { get; set; }
        /// <summary>
        /// 账户密码
        /// </summary>
        public string pass_word { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string add_time { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public string update_time { get; set; }
    }
}
