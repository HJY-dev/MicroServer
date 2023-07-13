using System;

namespace MapperApi.Model.RequestDto
{
    public class CreateActivityRequestDto
    {
        public string KeyId { get; set; }
        /// <summary>
        /// 活动名称
        /// </summary>
        public string? ActiveName { get; set; }
        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
    }
}
