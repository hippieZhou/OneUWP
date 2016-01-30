using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCore.Https
{
    public static class ServiceURL
    {
        #region Port
        //POST /OneForWeb/one/getHp_N HTTP/1.1
        //Content-Length: 27
        //Content-Type: application/x-www-form-urlencoded
        //Host: rest.wufazhuce.com
        //Connection: Keep-Alive
        //User-Agent: android-async-http/2.0 (http://loopj.com/android-async-http)
        //Accept-Encoding: gzip
        #endregion

        //http://rest.wufazhuce.com/OneForWeb/one/getHp_N?strDate=2016-01-20&strRow=1


        public static string strToday = DateTime.Now.ToString("yyyy-MM-dd");

        public static string strPictureURL = "http://rest.wufazhuce.com/OneForWeb/one/getHp_N?strDate={0}&strRow=1";

        public static string strArticleURL = "http://rest.wufazhuce.com/OneForWeb/one/getC_N?strMS=1&strDate={0}&strRow=1";

        public static string strQuestionURL = "http://rest.wufazhuce.com/OneForWeb/one/getQ_N?strDate={0}&strRow=1";

        public static string strThingURL = "http://rest.wufazhuce.com/OneForWeb/one/o_f?strDate={0}&strRow=1";
    }
}
