using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WavePhaseShifter
{
    public class WaveOutPort
    {
        private WavData m_WavData;

        private List<byte> m_Data = new List<byte>();

        private int m_WindowSize = Constants.DEFAULTWINDOWSIZE;

        public event EventHandler Processed;

        private string m_Path;
        
        public int Position
        {
            get
            {
                return m_Data.Count;
            }
        }

        protected int m_BytesPerFrame
        {
            get
            {
                return (((m_WavData.bitDepth + 7) >> 3) * m_Channels.Count);
            }
        }

        private int m_InputReady = 0;
        private object m_Lock = new object();

        protected List<ThroughNode> m_Channels;
        public ThroughNode GetChannel(int channel)
        {
            if (channel >= 0 && channel < m_Channels.Count)
                return m_Channels[channel];
            else return null;
        }

        public WaveOutPort(WavData wav, string path, int windowSize = Constants.DEFAULTWINDOWSIZE, int maxBufferSize = Constants.MAXBUFFERSIZE)
        {
            m_WavData = wav.Copy();
            m_Path = path;
            m_Channels = new List<ThroughNode>(wav.channels);
            for (int k = 0; k < wav.channels; k++)
            {
                ThroughNode an = new ThroughNode();
                an.OutputRead += An_OutputRead;
                m_Channels.Add(an);
            }
        }

        private void An_OutputRead(object sender, EventArgs e)
        {
            lock (m_Lock)
            {
                m_InputReady++;
                if (m_InputReady >= m_Channels.Count)
                {
                    m_InputReady = 0;
                    Enframe();
                }
            }
        }

        public void Save()
        {
            m_WavData.data = m_Data.ToArray();
            m_WavData.Save(m_Path);
        }

        public int Enframe()
        {
            int framesQueued = m_WindowSize;
            byte b1, b2;
            double d;
            for (int k = 0; k < framesQueued; k++)
            {
                for (int j = 0; j < m_Channels.Count; j++)
                {
                    d = GetSample(j, k);
                    MakeBytes(d, out b1, out b2);
                    m_Data.Add(b1);
                    m_Data.Add(b2);
                }
            }
            foreach (AudioNode a in m_Channels)
                a.OutputUpdated();

            return framesQueued;
        }

        protected double GetSample(int channel, int index)
        {
            double sample = 0.0;
            if (channel >= 0 && channel < m_Channels.Count)
            {
                sample = m_Channels[channel].OutputBuffer[index];
            }

            return sample;
        }

        protected void MakeBytes(double d, out byte b1, out byte b2)
        {
            short s = (short)d;
            b1 = (byte)(s & 0xff);
            b2 = (byte)((s & 0xff00) >> 8);
        }
    }
}
