using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WavePhaseShifter
{
    public class DelayNode : AudioNode
    {
        public AudioParam Delay;

        public DelayNode():base(1,-1)
        {
            Delay = new AudioParam(0.0, double.MaxValue, 1000.0);
        }

        public DelayNode(double milli):base(1,-1)
        {
            Delay = new AudioParam(0.0, double.MaxValue, milli);
        }

        public DelayNode(double milli, int windowSize):base(1,-1,windowSize)
        {
            Delay = new AudioParam(0.0, double.MaxValue, milli);
        }

        public DelayNode(double milli, int windowSize, int maxBufferSize):base(1,-1,windowSize,maxBufferSize)
        {
            Delay = new AudioParam(0.0, double.MaxValue, milli);
        }

        protected override void Process()
        {
            int count = m_OutputBuffer.Count;
            int numSamplesDelayed = (int)(Constants.SAMPLERATE * Delay.Value / 1000);
            if (count < numSamplesDelayed)
            {
                m_OutputBuffer.AddRange(new double[numSamplesDelayed - count]);
            }
            for(int j=0;j<m_WindowSize;j++)
            {
                m_OutputBuffer.Add(m_Inputs[0].OutputBuffer[j]);
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
