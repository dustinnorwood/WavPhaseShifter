using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WavePhaseShifter
{
    public class ThroughNode : AudioNode
    {
        public ThroughNode():base(1,1)
        {

        }

        public ThroughNode(int windowSize):base(1,1,windowSize)
        {

        }

        public ThroughNode(int windowSize, int maxBufferSize):base(1,1,windowSize,maxBufferSize)
        {

        }

        protected override void Process()
        {
            if (m_Inputs.Count > 0)
            {
                for (int k = 0; k < m_WindowSize; k++)
                {
                    double v = m_Inputs[0].OutputBuffer[k];
                    m_OutputBuffer.Add(v);
                }
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
