using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMusic.Services
{
   public interface IAudioVisualizer
    {
        void Init();
        byte[] GetWaveformValue();
    }
}
