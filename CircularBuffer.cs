using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WavePhaseShifter
{
    public class CircularBuffer<T>
    {
        protected T[] List;
        protected int N;
        protected int m_Start = 0;
        protected int m_End = 0;

        public T this[int k]
        {
            get
            {
                return List[(m_Start + k) % N];
            }
        }

        public CircularBuffer(int n)
        {
            N = n;
            List = new T[N];
        }

        public bool IsEmpty { get { return m_Start == m_End; } }
        public int Count
        {
            get
            {
                lock (List)
                {
                    return (N + m_End - m_Start) % N;
                }
            }
        }

        public int Capacity { get { return N; } }

        public void Add(T t)
        {
            lock (List)
            {
                List[m_End] = t;
                m_End = (m_End + 1) % N;
                if (m_End == m_Start)
                {
                    m_Start = (m_Start + 1) % N;
                }
            }
        }

        public void AddRange(T[] t)
        {
            lock (List)
            {
                int count = 0;
                for (; count < t.Length; count++)
                {
                    List[m_End] = t[count];
                    m_End = (m_End + 1) % N;
                    if (m_End == m_Start)
                    {
                        m_Start = (m_Start + 1) % N;
                    }
                }
            }
        }

        public void Remove(int num)
        {
            lock (List)
            {
                int count = 0;
                for (; count < num; count++)
                {
                    List[m_Start] = default(T);
                    m_Start = (m_Start + 1) % N;
                    if (m_End == m_Start)
                    {
                        break;
                    }
                }
            }
        }

        public void Clear()
        {
            lock (List)
            {
                m_Start = 0;
                m_End = 0;
            }
        }
    }
}
