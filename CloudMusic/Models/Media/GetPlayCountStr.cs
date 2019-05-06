using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMusic.Models.Media
{
    public static class GetPlayCountStr
    {
        public static string PlayCountstr(int i)
        {
                if (i > 10000)
                    return (i / 10000) + "万";
                else
                    return i.ToString();
        }
    }
}
