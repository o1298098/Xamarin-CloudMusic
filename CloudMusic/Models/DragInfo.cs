using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CloudMusic.Models
{
   public class DragInfo
    {
        public DragInfo(long id, Point pressPoint)
        {
            Id = id;
            PressPoint = pressPoint;
        }

        public long Id { private set; get; }

        public Point PressPoint { private set; get; }
    }
}
