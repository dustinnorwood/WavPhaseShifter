using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WavePhaseShifter
{
    public class BufferedAudioPort
    {
        private WavData m_WavData;
        public WavData WavData { get { return m_WavData; } }
        private int m_WindowSize = Constants.DEFAULTWINDOWSIZE;

        public event EventHandler Processed;

        protected int m_Position;
        public int Position { get { return m_Position; } set { m_Position = value; } }

        protected int m_BytesPerFrame { get { return (((m_WavData.bitDepth + 7) >> 3) * m_Channels.Count); } }

        protected int m_FramesLeft { get { return (m_WavData.data.Length - m_Position) / m_BytesPerFrame; } }

        protected List<ThroughNode> m_Channels;
        public ThroughNode GetChannel(int channel)
        {
            if (channel >= 0 && channel < m_Channels.Count)
                return m_Channels[channel];
            else return null;
        }

        public BufferedAudioPort(WavData wav, int windowSize = Constants.DEFAULTWINDOWSIZE, int maxBufferSize = Constants.MAXBUFFERSIZE)
        {
            m_WavData = wav;
            m_Channels = new List<ThroughNode>(wav.channels);
            for (int k = 0; k < wav.channels; k++)
            {
                m_Channels.Add(new ThroughNode());
            }
        }

        public BufferedAudioPort(string path, int windowSize = Constants.DEFAULTWINDOWSIZE, int maxBufferSize = Constants.MAXBUFFERSIZE)
        {
            m_WavData = WavData.Load(path);
            m_Channels = new List<ThroughNode>(m_WavData.channels);
            for (int k = 0; k < m_WavData.channels; k++)
            {
                m_Channels.Add(new ThroughNode());
            }
        }

        public void Start()
        {
            while (Deframe() > 0);
        }

        public int Deframe()
        {
            int framesLeft = m_FramesLeft;
            int framesQueued = framesLeft >= m_WindowSize ? m_WindowSize : framesLeft;
            byte b1, b2;
            for(int k = 0; k < framesQueued; k++)
            {
                for(int j = 0; j < m_Channels.Count; j++)
                {
                    b1 = m_WavData.data[m_Position + k * m_BytesPerFrame + 2 * j];
                    b2 = m_WavData.data[m_Position + k * m_BytesPerFrame + 2 * j + 1];
                    AddSample(j, MakeDouble(b1, b2));
                }
            }
            foreach (ThroughNode a in m_Channels)
                a.InputUpdated();

            m_Position += framesQueued * m_BytesPerFrame;

            return framesQueued;
        }

        protected void AddSample(int channel, double sample)
        {
            if(channel >= 0 && channel < m_Channels.Count)
            m_Channels[channel].OutputBuffer.Add(sample);
        }

        protected double MakeDouble(byte b1, byte b2)
        {
            return (double)((short)((b2 << 8) | b1));
        }
    }
}
