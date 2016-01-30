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
    public class OneThing
    {
        #region MyRegion

        [DataMember(Name = "strLastUpdateDate")]
        public string LastUpdateDate { get; set; }

        [DataMember(Name = "strPn")]
        public string Pn { get; set; }

        [DataMember(Name = "strBu")]
        public string Bu { get; set; }
        [DataMember(Name = "strTm")]
        public string Tm { get; set; }

        [DataMember(Name = "strWu")]
        public string Wu { get; set; }

        [DataMember(Name = "strId")]
        public string Id { get; set; }

        [DataMember(Name = "strTt")]
        public string Tt { get; set; }

        [DataMember(Name = "strTc")]
        public string Tc { get; set; }
        #endregion
    }

}
