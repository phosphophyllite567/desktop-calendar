using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopCalendar
{
    class CalendarData
    {
        
        public struct Data
        {
            public string Date;
            public string Weather;
            public int WeekDay; 
            public void SetValues()
            {
                Date = " ";
                Weather = " ";
                WeekDay = 0;
            }
        }

    

        public List<Data> datas =new List<Data>();

        public CalendarData()
        {
            InitializeDate();
        }

        private void SortData()
        {
            
        }

        private void InitializeDate()
        {
            string[,] A = new string[5,7];
            A[0,0] = "9    廿四";
            A[0,1] = "10    廿五";
            A[0,2] = "11    廿六";
            A[0,3] = "12    廿七";
            A[0,4] = "13    廿八";
            A[0,5] = "14    廿九";
            A[0,6] = "15    十月";
            A[1,0] = "16    初二";
            A[1,1] = "17    初三";
            A[1,2] = "18    初四";
            A[1,3] = "19    初五";
            A[1,4] = "20    初六";
            A[1,5] = "21    初七";
            A[1,6] = "22    小雪";
            A[2,0] = "23    初九";
            A[2,1] = "24    初十";
            A[2,2] = "25    十一";
            A[2,3] = "26    十二";
            A[2,4] = "27    十三";
            A[2,5] = "28    十四";
            A[2,6] = "29    十五";
            A[3,0] = "30    十六";
            A[3,1] = "12月    十七";
            A[3,2] = "1    十八";
            A[3,3] = "2    十九";
            A[3,4] = "3    二十";
            A[3,5] = "4    廿一";
            A[4,6] = "5    廿二";
            A[4,0] = "6    廿三";
            A[4,1] = "7    大雪";
            A[4,2] = "8    廿五";
            A[4,3] = "9    廿六";
            A[4,4] = "10    廿七";
            A[4,5] = "11    廿八";
            A[4,6] = "12    廿九";
            
            for (int i = 0; i < 35; i++)
            {
                Data data = new Data();
                data.SetValues();
                data.Date = A [i / 7,i % 7];
                datas.Add(data);
            }

            /*A[0,0] = "28    十二";
            A[0,1] = "29    十三";
            A[0,2] = "30    十四";
            A[0,3] = "10月    十五";
            A[0,4] = "2    十六";
            A[0,5] = "3    十七";
            A[0,6] = "4    十八";
            A[1,0] = "5    十九";
            A[1,1] = "6    二十";
            A[1,2] = "7    廿一";
            A[1,3] = "8    廿二";
            A[1,4] = "9    廿三";
            A[1,5] = "10    廿四";
            A[1,6] = "11    廿五";
            A[2,0] = "12    廿六";
            A[2,1] = "13    廿七";
            A[2,2] = "14    廿八";
            A[2,3] = "15    廿九";
            A[2,4] = "16    三十";
            A[2,5] = "17    九月";
            A[2,6] = "18    初二";
            A[3,0] = "19    初三";
            A[3,1] = "20    初四";
            A[3,2] = "21    初五";
            A[3,3] = "22    初六";
            A[3,4] = "23    初七";
            A[3,5] = "24    初八";
            A[4,6] = "25    初九";
            A[4,0] = "26    初十";
            A[4,1] = "27    十一";
            A[4,2] = "28    十二";
            A[4,3] = "29    十三";
            A[4,4] = "30    十四";
            A[4,5] = "31    十五";
            A[4,6] = "11月    十六";*/
        }
    }
}
