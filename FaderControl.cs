using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WavePhaseShifter
{
    public partial class FaderControl : UserControl
    {
        private Color m_BorderColor = Color.Black;
        public Color BorderColor { get { return m_BorderColor; } set { m_BorderColor = value; } }

        private Color m_TrackColor = Color.Black;
        public Color TrackColor { get { return m_TrackColor; } set { m_TrackColor = value; } }

        private double m_KnobRadius = 30.0;
        public double KnobRadius { get { return m_KnobRadius; } set { m_KnobRadius = value; } }

        private double m_MinimumValue = 0.0;
        public double MinimumValue { get { return m_MinimumValue; } set { m_MinimumValue = value; if (ControlParameter != null) ControlParameter.MinimumValue = value; } }

        private double m_MaximumValue = 1.0;
        public double MaximumValue { get { return m_MaximumValue; } set { m_MaximumValue = value; if (ControlParameter != null) ControlParameter.MaximumValue = value; } }

        private int m_FaderWidth = 50;
        public int FaderWidth {  get { return m_FaderWidth; } set { m_FaderWidth = value; } }

        private int m_FaderHeight = 20;
        public int FaderHeight { get { return m_FaderHeight; } set { m_FaderHeight = value; } }

        public AudioParam ControlParameter = new AudioParam(0.0, 1.0, 0.5);

        private double m_Percentage
        {
            get
            {
                return (ControlParameter.Value - m_MinimumValue) / (m_MaximumValue - m_MinimumValue);
            }
        }

        private int m_FaderLowerBound //The value of the bottom side of the current fader position
        {
            get
            {
               return (int)((1 - m_Percentage) * (this.Height - m_FaderHeight) + m_FaderHeight);
            }
        }

        private int m_FaderUpperBound //The current Y coordinate of the top of the fader
        {
            get
            {
                return (int)((1 - m_Percentage) * (this.Height - m_FaderHeight));
            }
        }

        private double m_DownValue = 0.0;
        private int m_DownY = 0;

        private bool m_MouseIsDown = false;

        public FaderControl()
        {
            InitializeComponent();
        }

        private double GetValueByVerticalDifferential(int dy)
        {
            return -dy / (double)(this.Height - FaderHeight);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {

            //          base.OnPaintBackground(e);

            Graphics g = e.Graphics;
            Rectangle faderRect = new Rectangle((this.Width - m_FaderWidth) / 2, m_FaderUpperBound, m_FaderWidth, m_FaderHeight);
            using (SolidBrush back = new SolidBrush(this.BackColor), fore = new SolidBrush(this.ForeColor))
            {
                using (Pen border = new Pen(this.BorderColor), track = new Pen(this.TrackColor))
                {
                    g.FillRectangle(back, this.ClientRectangle);
                    g.DrawLine(track, this.Width / 2, m_FaderHeight / 2, this.Width / 2, this.Height - m_FaderHeight / 2);
                    g.FillRectangle(fore, faderRect);
                    g.DrawRectangle(border, faderRect);
                }
            }

        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if(e.Y >= m_FaderUpperBound && e.Y <= m_FaderLowerBound)
            m_MouseIsDown = true;
            m_DownValue = ControlParameter.Value;
            m_DownY = e.Y;
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (m_MouseIsDown)
            {
                ControlParameter.Value = m_DownValue + GetValueByVerticalDifferential(e.Y - m_DownY);
            }

            base.OnMouseMove(e);

            this.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            m_MouseIsDown = false;
            base.OnMouseUp(e);
        }
    }
}
