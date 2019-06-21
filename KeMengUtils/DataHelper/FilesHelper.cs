using Qiniu.Http;
using Qiniu.IO;
using Qiniu.IO.Model;
using Qiniu.Util;
using System;


namespace KeMengUtils.DataHelper
{
    /// <summary>
    /// 文件帮助类
    /// </summary>
    public static class FilesHelper
    {

        /// <summary>
        /// 上传文件到七牛
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <param name="file_name">文件名(用于判断是否是支持的后缀名)</param>
        /// <param name="AK">AK</param>
        /// <param name="SK">SK</param>
        /// <param name="bucket">bucket</param>
        /// <param name="Domain">Domain</param>
        /// <param name="save_name">以什么文件名保存在七牛服务器上</param>
        /// <param name="max_size_limit">文件大小限制</param>
        /// <param name="file_type_limit">文件格式限制，默认值为{"bmp", "jpg", "jepg", "png"}</param>
        /// <returns></returns>
        public static HttpResult UploadFileToQiNiu(this System.IO.Stream fileStream, string file_name, string AK, string SK, string bucket, string Domain, string save_name, double max_size_limit = 3145728, string[] file_type_limit = null)
        {
            try
            {
                string filename = file_name;
                string fileType = filename.Substring(filename.LastIndexOf(".") + 1);
                if (file_type_limit == null)
                {

                    file_type_limit = new[] { "bmp", "jpg", "jepg", "png" };
                }
                if (Array.IndexOf(file_type_limit, fileType) == -1 || fileStream.Length > max_size_limit)
                {
                    return new HttpResult() { Code = 403, Text = "文件太大或不符合格式！" };
                }
                Mac mac = new Mac(AK, SK);
                string saveKey = save_name;
                PutPolicy putPolicy = new PutPolicy();
                putPolicy.Scope = bucket;
                putPolicy.SetExpires(3600);
                string jstr = putPolicy.ToJsonString();
                string token = Auth.CreateUploadToken(mac, jstr);
                FormUploader fu = new FormUploader();
                return fu.UploadStream(fileStream, saveKey, token);
            }
            catch
            {
                return new HttpResult() { Code = 500, Text = "上传出错！" };
            }
        }

    }
}