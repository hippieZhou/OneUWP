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
    public class OneArticle
    {
        #region MyRegion

        [DataMember(Name = "strLastUpdateDate")]
        public string LastUpdateDate { get; set; }

        [DataMember(Name = "strContent")]
        public string Content { get; set; }

        [DataMember(Name = "sWebLk")]
        public string WebLk { get; set; }

        [DataMember(Name = "wImgUrl")]
        public string ImgUrl { get; set; }

        [DataMember(Name = "sRdNum")]
        public string RdNum { get; set; }

        [DataMember(Name = "strPraiseNumber")]
        public string PraiseNumber { get; set; }

        [DataMember(Name = "strContDayDiffer")]
        public string ContDayDiffer { get; set; }

        [DataMember(Name = "strContentId")]
        public string ContentId { get; set; }

        [DataMember(Name = "strContTitle")]
        public string ContTitle { get; set; }

        [DataMember(Name = "strContAuthor")]
        public string ContAuthor { get; set; }

        [DataMember(Name = "strContAuthorIntroduce")]
        public string ContAuthorIntroduce { get; set; }

        [DataMember(Name = "strContMarketTime")]
        public string ContMarketTime { get; set; }

        [DataMember(Name = "sGW")]
        public string GW { get; set; }

        [DataMember(Name = "sAuth")]
        public string Auth { get; set; }

        [DataMember(Name = "sWbN")]
        public string WbN { get; set; }

        #endregion
    }
}
