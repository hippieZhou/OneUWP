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
    public class QuestionViewModel:ViewModelBase
    {
        private OneQuestion _question;
        public OneQuestion Question
        {
            get { return _question ?? (_question = InitOneQuestion().Result); }
            set { Set(ref _question, value); }
        }

        private async Task<OneQuestion> InitOneQuestion()
        {
            string strJson = await APIBaseService.GetData(ViewType.Question);
            JObject obj = JObject.Parse(strJson);
            CacheManager.CheckedCache(ViewType.Question, strJson);
            return Equals(obj["result"].ToString(), "SUCCESS") ?
                JsonConvert.DeserializeObject<OneQuestion>(obj["questionAdEntity"].ToString()) : new OneQuestion();
        }
        public async void UpdateQuestion(string time)
        {
            string strJson = await APIBaseService.GetJsons(string.Format(ServiceURL.strQuestionURL, time));
            JObject obj = JObject.Parse(strJson);
            Question = Equals(obj["result"].ToString(), "SUCCESS") ?
                     JsonConvert.DeserializeObject<OneQuestion>(obj["questionAdEntity"].ToString()) : Question;
        }
    }
}
