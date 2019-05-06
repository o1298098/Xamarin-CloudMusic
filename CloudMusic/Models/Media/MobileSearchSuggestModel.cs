using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CloudMusic.Models.Media
{
   public  class MobileSearchSuggestModel
    {
            public Result result { get; set; }
            public int code { get; set; }
      

        public class Result
        {
            public ObservableCollection<Allmatch> allMatch { get; set; }
        } 

        public class Allmatch
        {
            public string keyword { get; set; }
            public int type { get; set; }
            public string alg { get; set; }
            public string lastKeyword { get; set; }
        }

    }
}
