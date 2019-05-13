using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMusic.Services
{
   public  interface IAudioVisualizer
    {
        event WaveformUpadteEvent OnWaveformUpadte;
        void WaveformUpadt(IList<byte> args);
        void Init();
        IList<byte> GetWaveformValue();
        void Dispose();
    }
    public delegate void WaveformUpadteEvent(IList<byte> args);
}
