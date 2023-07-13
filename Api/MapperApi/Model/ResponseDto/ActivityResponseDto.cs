using System;

namespace MapperApi.Model.ResponseDto
{
    public class ActivityResponseDto
    {
        public Guid KeyId { get; set; }
        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActiveName { get; set; }
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
