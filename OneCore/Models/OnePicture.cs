using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OneCore.Untils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OneCore.Models
{
    [DataContract]
    public class OnePicture
    {
        #region PhotoModelProperty
        [DataMember(Name = "strLastUpdateDate")]
        public string LastUpdateDate { get; set; }

        [DataMember(Name = "strDayDiffer")]
        public string DayDiffer { get; set; }

        [DataMember(Name = "strHpId")]
        public string HpId { get; set; }

        [DataMember(Name = "strHpTitle")]
        public string HpTitle { get; set; }

        [DataMember(Name = "strThumbnailUrl")]
        public string ThumbnailUrl { get; set; }

        [DataMember(Name = "strOriginalImgUrl")]
        public string OriginalImgUrl { get; set; }

        [DataMember(Name = "strAuthor")]
        public string Author { get; set; }

        [DataMember(Name = "strContent")]
        public string Content { get; set; }

        [DataMember(Name = "strMarketTime")]
        public string MarketTime { get; set; }

        [DataMember(Name = "sWebLk")]
        public string WebLk { get; set; }

        [DataMember(Name = "strPn")]
        public string Pn { get; set; }

        [DataMember(Name = "wImgUrl")]
        public string ImgUrl { get; set; }

        #endregion
    }
}
