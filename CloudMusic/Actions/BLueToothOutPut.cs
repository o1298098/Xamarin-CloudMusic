using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace CloudMusic.Actions
{
    public class BLueToothOutPut
    {
        static public string OutPut(string num)
        {
            string result = num;
            if (Convert.ToBoolean(Preferences.Get("SubEnable", "false")))
            {
                int LNumber = int.Parse(Preferences.Get("LSubNum", "0"));
                int RNumber = int.Parse(Preferences.Get("RSubNum", "0"));
                result = result.Substring(LNumber);
                result = result.Substring(0, result.Length - RNumber);
            }
            return result.Replace("\r", "");
        }
           
    }
}
