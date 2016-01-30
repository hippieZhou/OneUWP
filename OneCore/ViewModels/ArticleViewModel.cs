using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OneCore.Https;
using OneCore.Models;
using OneCore.Untils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCore.ViewModels
{
    public class ArticleViewModel:ViewModelBase
    {
        private OneArticle _article;
        public OneArticle Article
        {
            get { return _article ?? (_article = InitOneArticle().Result); }
            set { Set(ref _article, value); }
        }

        private async Task<OneArticle> InitOneArticle()
        {
            string strJson = await APIBaseService.GetData(ViewType.Article);
            JObject obj = JObject.Parse(strJson);
            CacheManager.CheckedCache(ViewType.Article, strJson);
            return Equals(obj["result"].ToString(), "SUCCESS") ?
                    JsonConvert.DeserializeObject<OneArticle>(obj["contentEntity"].ToString()) : new OneArticle();
        }
        public async void UpdateArticle(string time)
        {
            string strJson = await APIBaseService.GetJsons(string.Format(ServiceURL.strArticleURL, time));
            JObject obj = JObject.Parse(strJson);
            Article = Equals(obj["result"].ToString(), "SUCCESS") ?
                     JsonConvert.DeserializeObject<OneArticle>(obj["contentEntity"].ToString()) : Article;
        }
    }
}
