using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WavePhaseShifter
{
    public class GainNode : AudioNode
    {
        public AudioParam Gain;

        public GainNode() : base(1, -1)
        {
            Gain = new AudioParam(0, double.MaxValue, 1.0);
        }

        public GainNode(double gain) : base(1, -1)
        {
            Gain = new AudioParam(0, double.MaxValue, gain);
        }

        public GainNode(double gain, int windowSize) : base(1, -1, windowSize)
        {
            Gain = new AudioParam(0, double.MaxValue, gain);
        }

        public GainNode(double gain, int windowSize, int maxBufferSize) : base(1, -1, windowSize, maxBufferSize)
        {
            Gain = new AudioParam(0, double.MaxValue, gain);
        }

        protected override void Process()
        {
            for (int j = 0; j < m_WindowSize; j++)
            {
                m_OutputBuffer.Add(m_Inputs[0].OutputBuffer[j] * Gain.Value);
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
