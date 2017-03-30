using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WavePhaseShifter
{
    public class ResamplerNode : AudioNode
    {
        public AudioParam Rate;

        public ResamplerNode() : base(1, -1)
        {
            Rate = new AudioParam(0, 44100.0, 44100.0);
        }

        public ResamplerNode(double bits) : base(1, -1)
        {
            Rate = new AudioParam(0, 44100.0, bits);
        }

        public ResamplerNode(double bits, int windowSize) : base(1, -1, windowSize)
        {
            Rate = new AudioParam(0, 44100.0, bits);
        }

        public ResamplerNode(double bits, int windowSize, int maxBufferSize) : base(1, -1, windowSize, maxBufferSize)
        {
            Rate = new AudioParam(0, 44100.0, bits);
        }
        
        protected override void Process()
        {
            double s = Rate.Value / 44100.0;
            for (int j = 0; j < m_WindowSize; j++)
            {
                m_OutputBuffer.Add(m_Inputs[0].OutputBuffer[(int)(s*j)]);
            }
            base.Process();
        }

        protected override void OnAllOutputsRead()
        {
            m_OutputBuffer.Remove(m_WindowSize);
            base.OnAllOutputsRead();
        }
    }
}
