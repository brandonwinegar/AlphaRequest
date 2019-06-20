using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLib
{
    public class DataService
    {
        public string Symbol { get; }
        public Dictionary<string, string> MetaData { get; set; }
        public Dictionary<string, string> CurrentData { get; set; }
        public Dictionary<string, Dictionary<string, string>> ValueSeries { get; set; }

        public DataService(string Symbol)
        {
            this.Symbol = Symbol.ToUpper();
        }
        public void BuildStandardSets(string SeriesType, String Interval = "5min")
        {
            var ValuePlaceHolder = new Dictionary<string, Dictionary<string, string>>();
            var Json = AlphaRequest.Request(SeriesType, Symbol, Interval);
            var JsonDeserialized = JsonConvert.DeserializeObject<Dictionary<string, object>>(Json); //<s,o>

            Dictionary<string, string> MetaData = JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonDeserialized["Meta Data"].ToString());
            JsonDeserialized.Remove("Meta Data");
            this.MetaData = MetaData;
            JsonDeserialized = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonDeserialized.ElementAt(0).Value.ToString());    //<s,o>
            foreach (KeyValuePair<string, object> item in JsonDeserialized)
            {
                var valueDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(item.Value.ToString());
                ValuePlaceHolder.Add(item.Key, valueDict);
            }
            ValueSeries = ValuePlaceHolder;
        }
        public void BuildCurrentSet()
        {
            //var ValuePlaceHolder = new Dictionary<string, Dictionary<string, string>>();
            var Json = AlphaRequest.Request("GLOBAL_QUOTE", Symbol);
            var JsonDeserialized = JsonConvert.DeserializeObject<Dictionary<string, object>>(Json); //<s,o>
            var ValuePlaceHolder = JsonConvert.DeserializeObject<Dictionary<string, string>>(JsonDeserialized.ElementAt(0).Value.ToString());
            CurrentData = ValuePlaceHolder;
        }
        public double HundredDayHigh()
        {
            if (ValueSeries == null)
            {
                BuildStandardSets("TIME_SERIES_DAILY");
            }
            double High = 0;
            double value;
            foreach (KeyValuePair<string, Dictionary<string, string>> item in ValueSeries)
            {
                value = Convert.ToDouble(item.Value["2. high"]);
                if (value > High)
                {
                    High = value;
                }
            }
            return High;
        }

        public List<double> GetDataSet(int Points)
        {
            List<double> ReturnSet = new List<double>();
            if(ValueSeries == null)
            {
                BuildStandardSets("TIME_SERIES_DAILY");
            }
            for(int i = 0; i < Points; i++)
            {
                var item = ValueSeries.ElementAt(i);
                var value = Convert.ToDouble(item.Value["2. high"]);
                //ReturnSet.Add(value);
                ReturnSet.Insert(0, value);
            }
            return ReturnSet;
        }
        public void CacheDataSet()
        {
            string BasePath = Environment.CurrentDirectory;
        }
    }
}
