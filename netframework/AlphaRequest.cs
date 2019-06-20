using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace ClassLib
{
    public static class AlphaRequest
    {
        static string Key = "JDO2JPAY1HDYESW5";
        public static string Request(string SeriesType, string Symbol, String Interval = "5min")
        {
            String Uri;
            if (SeriesType == "TIME_SERIES_INTRADAY")
            {
                Uri = $"https://www.alphavantage.co/query?function={SeriesType.ToUpper()}&symbol={Symbol.ToUpper()}&interval={Interval.ToLower()}&apikey={Key}";
            }
            else
            {
                Uri = $"https://www.alphavantage.co/query?function={SeriesType.ToUpper()}&symbol={Symbol.ToUpper()}&apikey={Key}";
            }

            WebRequest requestObject = WebRequest.Create(Uri);
            requestObject.Method = "GET";
            HttpWebResponse responseObject = (HttpWebResponse)requestObject.GetResponse();
            string response;
            using (var stream = responseObject.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                response = sr.ReadToEnd();
                sr.Close();
            }
            return response;
        }
    }
}
