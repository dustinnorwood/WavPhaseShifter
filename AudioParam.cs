using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WavePhaseShifter
{
    public class AudioParam
    {
        private double m_Value;
        public double Value
        {
            get { return m_Value; }
            set
            {
                if (value > m_MaximumValue)
                    m_Value = m_MaximumValue;
                else if (value < m_MinimumValue)
                    m_Value = m_MinimumValue;
                else m_Value = value;
            }
        }

        private double m_MinimumValue;
        public double MinimumValue
        {
            get { return m_MinimumValue; }
            set { m_MinimumValue = value; }
        }

        private double m_MaximumValue;
        public double MaximumValue
        {
            get { return m_MaximumValue; }
            set { m_MaximumValue = value; }
        }

        public AudioParam()
        {
            m_MinimumValue = 0;
            m_MaximumValue = 1.0;
            m_Value = 0.5;
        }
   
        public AudioParam(double min, double max)
        {
            m_MinimumValue = min;
            m_MaximumValue = max;
            Value = (min + max) / 2;
        }

        public AudioParam(double min, double max, double val)
        {
            m_MinimumValue = min;
            m_MaximumValue = max;
            Value = val;
        }
    }
}
