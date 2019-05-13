using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMusic.Services
{
   public  interface IAudioVisualizer
    {
        event DataCaptureUpadteEvent OnWaveformUpdate;
        event DataCaptureUpadteEvent OnFftUpdate;
        void WaveformUpadt(IList<byte> args);
        void Init();
        IList<byte> GetWaveformValue();
        IList<byte> GetFftValue();
        void Dispose();
    }
    public delegate void DataCaptureUpadteEvent(IList<byte> args);
}
