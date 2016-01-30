using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
using Windows.Data.Json;

namespace OneCore.ViewModels
{
    public class PictureViewModel:ViewModelBase
    {
        private OnePicture _picture;
        public OnePicture Picture
        {
            get { return _picture ?? (_picture = InitOnePicture().Result); }
            set { Set(ref _picture, value); }
        }

        private string _oneTime;
        public string OneTime
        {
            get { return _oneTime ?? (_oneTime = ServiceURL.strToday); }
            set { Set(ref _oneTime, value); }
        }



        private async Task<OnePicture> InitOnePicture()
        {

            string strJson = await APIBaseService.GetData(ViewType.Picture);
            JObject obj = JObject.Parse(strJson);
            CacheManager.CheckedCache(ViewType.Picture, strJson);
            return Equals(obj["result"].ToString(), "SUCCESS") ?
                      JsonConvert.DeserializeObject<OnePicture>(obj["hpEntity"].ToString()) : new OnePicture();
        }

        public async void UpdatePicture(string time)
        {
            string strJson = await APIBaseService.GetJsons(string.Format(ServiceURL.strPictureURL, time));
            JObject obj = JObject.Parse(strJson);
            Picture = Equals(obj["result"].ToString(), "SUCCESS") ?
                     JsonConvert.DeserializeObject<OnePicture>(obj["hpEntity"].ToString()) : Picture;
        }
    }
}
