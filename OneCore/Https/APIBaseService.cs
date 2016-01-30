using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OneCore.Models;
using OneCore.Untils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace OneCore.Https
{
    /// <summary>
    /// http://mp.weixin.qq.com/s?__biz=MzAwNTMxMzg1MA==&mid=405368669&idx=1&sn=0bad792b9d48893adc99ee39e807692f&scene=0#rd
    /// </summary>
    public class APIBaseService
    {
        public static async Task<string> GetData(ViewType view)
        {
            string uri = string.Empty;
            switch (view)
            {
                case ViewType.Picture:
                    uri = string.Format(ServiceURL.strPictureURL, ServiceURL.strToday);
                    break;
                case ViewType.Article:
                    uri = string.Format(ServiceURL.strArticleURL, ServiceURL.strToday);
                    break;
                case ViewType.Question:
                    uri = string.Format(ServiceURL.strQuestionURL, ServiceURL.strToday);
                    break;
                case ViewType.Thing:
                    uri = string.Format(ServiceURL.strThingURL, ServiceURL.strToday);
                    break;
                default:
                    uri = string.Format(ServiceURL.strPictureURL, ServiceURL.strToday);
                    break;
            }
            return await GetJsons(uri);
        }

        public static async Task<string> GetJsons(string uri)
        {
            try
            {
                using (var handler = new HttpClientHandler())
                {
                    handler.AutomaticDecompression = System.Net.DecompressionMethods.GZip;
                    using (var client = new HttpClient(handler))
                    {
                        //避免网络请求卡顿
                        client.DefaultRequestHeaders.ExpectContinue = false;
                        HttpResponseMessage response = Task.Run(async () => { return await client.GetAsync(uri); }).Result;
                        // 确认响应成功，否则抛出异常
                        response.EnsureSuccessStatusCode();
                        return await response.Content.ReadAsStringAsync(); 
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("GetJsons:" + ex.Message.ToString());
                return string.Empty;
            }
        }
    }
}
