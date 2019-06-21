using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;
using KeMengUtils.DataHelper;
using KeMengUtils.LocationHelper.Models;

namespace KeMengUtils.LocationHelper
{
    /// <summary>
    /// 位置服务帮助类
    /// </summary>
    public static class LocationService
    {
        /// <summary>
        /// 根据坐标获取详细的位置信息
        /// </summary>
        /// <param name="location">位置信息，填入经纬度即可</param>
        /// <param name="map_key">腾讯地图key</param>
        /// <param name="qqmap_api_url">腾讯地图经纬度获取位置信息接口地址</param>
        /// <returns></returns>
        public static LocationInfo GetDetailedLocation(this LocationInfo location, string map_key, string qqmap_api_url = @"https://apis.map.qq.com/jsapi")
        {
            WebClient client = new WebClient();
            client.Credentials = CredentialCache.DefaultCredentials;
            QQMapParams @params = new QQMapParams
            {
                qt = "poi",
                wd = location.approximate_addr,
                c = location.approximate_addr,
                pn = "0",
                key = map_key,
                rn = "1",
                rich_source = "qipao",
                rich = "web",
                nj = "0",
                output = "json",
                pf = "jsapi",
                @ref = "jsapi"
            };
            string url = qqmap_api_url;
            byte[] by = client.DownloadData(
                url.AddParam(() => @params.qt)
                   .AddParam(() => @params.wd)
                   .AddParam(() => @params.c)
                   .AddParam(() => @params.pn)
                   .AddParam(() => @params.key)
                   .AddParam(() => @params.rn)
                   .AddParam(() => @params.rich_source)
                   .AddParam(() => @params.rich)
                   .AddParam(() => @params.nj)
                   .AddParam(() => @params.output)
                   .AddParam(() => @params.pf)
                   .AddParam(() => @params.@ref)
                );
            string sres = Encoding.Default.GetString(by);
            JObject json_object = JObject.Parse(sres);
            return location;
        }
    }
}
