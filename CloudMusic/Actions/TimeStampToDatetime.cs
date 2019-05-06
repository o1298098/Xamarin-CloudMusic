using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMusic.Actions
{
   public class TimeStampToDatetime
    {
      public  static DateTime GetTime(long timeStamp)
        {
            DateTime dtStart = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            long lTime = long.Parse(timeStamp + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime dtResult = dtStart.Add(toNow);
            return dtResult;
        }
    }
}
