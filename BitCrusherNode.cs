using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WavePhaseShifter
{
    public class BitCrusherNode : AudioNode
    {
        public AudioParam Bits;

        public BitCrusherNode() : base(1, -1)
        {
            Bits = new AudioParam(0, 64.0, 16.0);
        }

        public BitCrusherNode(double bits) : base(1, -1)
        {
            Bits = new AudioParam(0, 64.0, bits);
        }

        public BitCrusherNode(double bits, int windowSize) : base(1, -1, windowSize)
        {
            Bits = new AudioParam(0, 64.0, bits);
        }

        public BitCrusherNode(double bits, int windowSize, int maxBufferSize) : base(1, -1, windowSize, maxBufferSize)
        {
            Bits = new AudioParam(0, 64.0, bits);
        }

        protected override void Process()
        {
            double div = 0x8000 / Math.Pow(2, Bits.Value - 1);
            for (int j = 0; j < m_WindowSize; j++)
            {
                m_OutputBuffer.Add(Math.Floor(m_Inputs[0].OutputBuffer[j] / div)*div);
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
