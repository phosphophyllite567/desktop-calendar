using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DesktopCalendar.data
{
    class RESTData
    {
        public struct Data
        {
            public int day;
            public string weather;
            public string lunar;
            public void SetValue()
            {
                day = 0;
                weather = " ";
                lunar = " ";
            }
            public Data(int d,string w,string l)
            {
                day = d;
                weather = w;
                lunar = l;
            }
            public Data(int d, string w)
            {
                day = d;
                weather = w;
                lunar = null;
            }
        }

        public List<Data> datas = new List<Data>();
 
        public RESTData()
        {
            datas.Add(new Data(1125, "3-6°C,大雨转阴，北风微风"));
            datas.Add(new Data(1126, "4-9°C,阴，北风3-4级"));
            datas.Add(new Data(1127, "5-10°C,阴，北风3-4级"));
            datas.Add(new Data(1128, "2-8°C,小雨转多云，北风3-4级"));
            datas.Add(new Data(1129, "2-11°C,多云转晴，北风微风"));
        }
    }
}
