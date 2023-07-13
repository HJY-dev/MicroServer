using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTest.Model
{
    /// <summary>
    /// k线数据
    /// </summary>
    public class Kline
    {
        /// <summary>
        /// 开盘价
        /// </summary>
        public double open { get; set; }
        /// <summary>
        /// 昨收价
        /// </summary>
        public double close { get; set; }
        /// <summary>
        /// 最高价
        /// </summary>
        public double high { get; set; }
        /// <summary>
        /// 最低价
        /// </summary>
        public double low { get; set; }
        /// <summary>
        /// 成交量
        /// </summary>
        public double vol { get; set; }
        /// <summary>
        /// 成交额
        /// </summary>
        public double val { get; set; }
        /// <summary>
        /// utc时间戳
        /// </summary>
        public long times { get; set; }
    }
}
