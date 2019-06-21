
namespace KeMengUtils.LocationHelper.Models
{
    /// <summary>
    /// 位置信息
    /// </summary>
    public class LocationInfo
    {
        /// <summary>
        /// 近似地址
        /// </summary>
        public string approximate_addr { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string detailed_addr { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public decimal? lat { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public decimal? lng { get; set; }
    }
}
