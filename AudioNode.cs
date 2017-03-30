using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WavePhaseShifter
{
    public class AudioNode
    {
        protected List<AudioNode> m_Inputs;
        protected List<AudioNode> m_Outputs;

        protected CircularBuffer<double> m_OutputBuffer;
        public CircularBuffer<double> OutputBuffer
        {
            get { return m_OutputBuffer; }
        }

        private int m_InputReady, m_OutputReady;
        private object m_Lock;

        protected int m_WindowSize;

        public event EventHandler OutputRead;

        public AudioNode()
        {
            m_Inputs = new List<AudioNode>();
            m_Outputs = new List<AudioNode>();
            m_OutputBuffer = new CircularBuffer<double>(Constants.MAXBUFFERSIZE);
            m_Lock = new object();
            m_InputReady = 0;
            m_OutputReady = 0;
            m_WindowSize = Constants.DEFAULTWINDOWSIZE;
        }

        public AudioNode(int maxInputs, int maxOutputs)
        {
            if (maxInputs >= 0)
                m_Inputs = new List<AudioNode>(maxInputs);
            else m_Inputs = new List<AudioNode>();
            if (maxOutputs >= 0)
                m_Outputs = new List<AudioNode>(maxOutputs);
            else m_Outputs = new List<AudioNode>();
            m_OutputBuffer = new CircularBuffer<double>(Constants.MAXBUFFERSIZE);
            m_Lock = new object();
            m_InputReady = 0;
            m_OutputReady = 0;
            m_WindowSize = Constants.DEFAULTWINDOWSIZE;
        }

        public AudioNode(int maxInputs, int maxOutputs, int windowSize)
        {
            if (maxInputs >= 0)
                m_Inputs = new List<AudioNode>(maxInputs);
            else m_Inputs = new List<AudioNode>();
            if (maxOutputs >= 0)
                m_Outputs = new List<AudioNode>(maxOutputs);
            else m_Outputs = new List<AudioNode>();
            m_OutputBuffer = new CircularBuffer<double>(Constants.MAXBUFFERSIZE);
            m_Lock = new object();
            m_InputReady = 0;
            m_OutputReady = 0;
            m_WindowSize = windowSize;
        }

        public AudioNode(int maxInputs, int maxOutputs, int windowSize, int maxBufferSize)
        {
            if (maxInputs >= 0)
                m_Inputs = new List<AudioNode>(maxInputs);
            else m_Inputs = new List<AudioNode>();
            if (maxOutputs >= 0)
                m_Outputs = new List<AudioNode>(maxOutputs);
            else m_Outputs = new List<AudioNode>();
            m_OutputBuffer = new CircularBuffer<double>(maxBufferSize);
            m_Lock = new object();
            m_InputReady = 0;
            m_OutputReady = 0;
            m_WindowSize = windowSize;
        }

        private bool m_IsReady = true;

        public void InputUpdated()
        {
            lock (m_Lock)
            {
                m_IsReady = false;
                m_InputReady++;
                if (m_InputReady >= m_Inputs.Count)
                {
                    m_InputReady = 0;
                    Process();
                }
            }
        }

        private void NotifyOutputs()
        {
            if (this.OutputRead != null)
                this.OutputRead(this, EventArgs.Empty);

            foreach (AudioNode a in m_Outputs)
                a.InputUpdated();

        }

        public void OutputUpdated()
        {
            lock (m_Lock)
            {
                m_OutputReady++;
                if (m_OutputReady >= m_Outputs.Count)
                {
                    m_OutputReady = 0;
                    OnAllOutputsRead();
                }
            }
        }

        private void NotifyInputs()
        {
            foreach (AudioNode a in m_Inputs)
                a.OutputUpdated();
            m_IsReady = true;
        }

        /// <summary>
        /// This must be called at the END of each inherited class's implementation of Process() to relay its output to the next nodes.
        /// </summary>
        protected virtual void Process()
        {
            NotifyOutputs();
        }

        protected virtual void OnAllOutputsRead()
        {

            NotifyInputs();
        }

        public bool IsReady() { return m_IsReady; }

        public void AddInput(AudioNode a)
        {
            m_Inputs.Add(a);
        }

        public void RemoveInput(AudioNode a)
        {
            m_Inputs.Remove(a);
        }

        public void RemoveInput(int i)
        {
            m_Inputs.RemoveAt(i);
        }

        public void ClearInputs()
        {
            m_Inputs.Clear();
        }

        public void AddOutput(AudioNode a)
        {
            m_Outputs.Add(a);
        }

        public void RemoveOutput(AudioNode a)
        {
            m_Outputs.Remove(a);
        }

        public void RemoveOutput(int i)
        {
            m_Outputs.RemoveAt(i);
        }

        public void ClearOutputs()
        {
            m_Outputs.Clear();
        }

        public static void TieNodes(AudioNode input, AudioNode output)
        {
            input.AddOutput(output);
            output.AddInput(input);
        }

        public static void UntieNodes(AudioNode input, AudioNode output)
        {
            input.RemoveOutput(output);
            output.RemoveInput(input);
        }
    }
}
