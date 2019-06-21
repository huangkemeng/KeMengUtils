
namespace KeMengUtils.LocationHelper.Models
{
    /// <summary>
    /// QQ地图查询参数
    /// </summary>
    public class QQMapParams
    {
        /// <summary>
        /// qt
        /// </summary>
        public string qt { get; set; }
        /// <summary>
        /// wd
        /// </summary>
        public string wd { get; set; }
        /// <summary>
        /// c
        /// </summary>
        public string c { get; set; }
        /// <summary>
        /// pn
        /// </summary>
        public string pn { get; set; }
        /// <summary>
        /// key必填
        /// </summary>
        public string key { get; set; }
        /// <summary>
        /// rn
        /// </summary>
        public string rn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rich_source { get; set; }
        /// <summary>
        /// rich
        /// </summary>
        public string rich { get; set; }
        /// <summary>
        /// nj
        /// </summary>
        public string nj { get; set; }
        /// <summary>
        /// 输出的数据类型 json jsonp等，默认json
        /// </summary>
        public string output { get; set; }
        /// <summary>
        /// pf
        /// </summary>
        public string pf { get; set; }
        /// <summary>
        /// ref
        /// </summary>
        public string @ref { get; set; }
    }
}
