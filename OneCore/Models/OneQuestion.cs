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
    public class OneQuestion
    {
        #region MyRegion
        //public class enQNCmt
        //{
        //    public string strId { get; set; }
        //    public string strCnt { get; set; }
        //    public string strD { get; set; }
        //    public string pNum { get; set; }
        //    public string upFg { get; set; }

        //}
        [DataMember(Name = "strLastUpdateDate")]
        public string LastUpdateDate { get; set; }

        [DataMember(Name = "strDayDiffer")]
        public string DayDiffer { get; set; }

        [DataMember(Name = "sWebLk")]
        public string WebLk { get; set; }

        [DataMember(Name = "strPraiseNumber")]
        public string PraiseNumber { get; set; }

        [DataMember(Name = "strQuestionId")]
        public string QuestionId { get; set; }

        [DataMember(Name = "strQuestionTitle")]
        public string QuestionTitle { get; set; }

        [DataMember(Name = "strQuestionContent")]
        public string QuestionContent { get; set; }

        [DataMember(Name = "strAnswerTitle")]
        public string AnswerTitle { get; set; }

        [DataMember(Name = "strAnswerContent")]
        public string AnswerContent { get; set; }

        [DataMember(Name = "strQuestionMarketTime")]
        public string QuestionMarketTime { get; set; }

        [DataMember(Name = "sEditor")]
        public string Editor { get; set; }

        #endregion
    }

}
