using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMusic.Services
{
    /// <summary>
    /// 自定义状态栏通知接口，待完成
    /// </summary>
    public interface INotification
    {
      void ProgressUpdate(int id,int ProgressPercentage);
      void NotificationCancel(int id);
    }
}
