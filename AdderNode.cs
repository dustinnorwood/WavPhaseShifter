using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WavePhaseShifter
{
    public class AdderNode : AudioNode
    {
        public AdderNode():base(-1,-1)
        {

        }

        public AdderNode(int windowSize):base(-1,-1,windowSize)
        {

        }

        public AdderNode(int windowSize, int maxBufferSize):base(-1,-1,windowSize,maxBufferSize)
        {

        }

        protected override void Process()
        {
            for(int k=0;k<m_WindowSize;k++)
            {
                double v = 0.0;
                foreach (AudioNode a in m_Inputs)
                    v += a.OutputBuffer[k];
                m_OutputBuffer.Add(v);
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
