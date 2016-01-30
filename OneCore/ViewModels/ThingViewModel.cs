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
    public class ThingViewModel:ViewModelBase
    {
        private OneThing _thing;

        public OneThing Thing
        {
            get { return _thing??(_thing = InitOneThing().Result); }
            set { Set(ref _thing, value); }
        }

        private async Task<OneThing> InitOneThing()
        {
            string strJson = await APIBaseService.GetData(ViewType.Thing);
            JObject obj = JObject.Parse(strJson);
            CacheManager.CheckedCache(ViewType.Thing, strJson);
            return Equals(obj["rs"].ToString(), "SUCCESS") ?
                JsonConvert.DeserializeObject<OneThing>(obj["entTg"].ToString()) : new OneThing();
        }

        public async void UpdateThing(string time)
        {
            string strJson = await APIBaseService.GetJsons(string.Format(ServiceURL.strThingURL, time));
            JObject obj = JObject.Parse(strJson);
            Thing = Equals(obj["rs"].ToString(), "SUCCESS") ?
                     JsonConvert.DeserializeObject<OneThing>(obj["entTg"].ToString()) : Thing;
        }
    }
}
