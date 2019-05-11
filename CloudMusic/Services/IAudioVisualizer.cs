using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMusic.Services
{
   public  interface IAudioVisualizer
    {
        event DataCaptureUpadteEvent OnWaveformUpdate;
        event DataCaptureUpadteEvent OnFftUpadate;
        void WaveformUpadt(IList<byte> args);
        void Init();
        IList<byte> GetWaveformValue();
        void Dispose();
    }
    public delegate void DataCaptureUpadteEvent(IList<byte> args);
}
