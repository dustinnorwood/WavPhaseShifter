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
    public partial class KnobControl : UserControl
    {
        private Color m_BorderColor = Color.Black;
        public Color BorderColor { get { return m_BorderColor; } set { m_BorderColor = value; } }

        private Color m_NeedleColor = Color.White;
        public Color NeedleColor { get { return m_NeedleColor; } set { m_NeedleColor = value; } }

        private int m_BorderThickness = 2;
        public int BorderThickness { get { return m_BorderThickness; } set { m_BorderThickness = value; } }

        private int m_NeedleThickness = 2;
        public int NeedleThickness { get { return m_NeedleThickness; } set { m_NeedleThickness = value; } }

        private double m_KnobRadius = 30.0;
        public double KnobRadius { get { return m_KnobRadius; } set { m_KnobRadius = value; } }

        private double m_MinimumValue = 0.0;
        public double MinimumValue {  get { return m_MinimumValue; } set { m_MinimumValue = value; if (ControlParameter != null) ControlParameter.MinimumValue = value; } }

        private double m_MaximumValue = 1.0;
        public double MaximumValue {  get { return m_MaximumValue; } set { m_MaximumValue = value; if(ControlParameter != null) ControlParameter.MaximumValue = value; } }

        public AudioParam ControlParameter = new AudioParam(0.0, 1.0, 0.5);

        private double m_LeftAngle = 5 * Math.PI / 4;
     //   public double LeftAngle { get { return m_LeftAngle; } set { m_LeftAngle = value; } }

        private double m_RightAngle = -Math.PI / 4;
    //    public double RightAngle { get { return m_RightAngle; } set { m_RightAngle = value; } }

        private Point m_Center { get { return new Point(this.Width / 2, this.Height / 2); } }

        private Point m_NeedlePosition
        {
            get
            {
                double rho = m_KnobRadius;
                double theta = m_CurrentAngle;
                double x = rho*Math.Cos(theta);
                double y = rho*Math.Sin(theta);
                Point c = m_Center;
                return new Point((int)(c.X + x), (int)(c.Y - y));
            }
        }

        private double m_Percentage
        {
            get
            {
                return (ControlParameter.Value - m_MinimumValue) / (m_MaximumValue - m_MinimumValue); 
            }
        }

        private double m_CurrentAngle
        {
            get
            {
                return m_LeftAngle - m_Percentage * (m_LeftAngle - m_RightAngle);
            }
        }

        private double m_DownAngle = 0.0;
        private double m_DownValue = 0.0;

        private bool m_MouseIsDown = false;

        public KnobControl()
        {
            InitializeComponent();
        }

        private double GetAngleAtPointFromCenter(int x, int y)
        {
            Point c = m_Center;

            double d = Math.Atan2(c.Y - y, x - c.X);

            if (d < -Math.PI / 2)
                d += 2 * Math.PI;

            return d;
        }

        private double GetValueByAngleDifferential(double angle)
        {
            return angle * (m_MaximumValue - m_MinimumValue) / (m_LeftAngle - m_RightAngle);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {

  //          base.OnPaintBackground(e);

            Graphics g = e.Graphics;
            Point c = m_Center;
            Rectangle knobRect = new Rectangle((int)(c.X - m_KnobRadius), (int)(c.Y - m_KnobRadius), (int)(2 * m_KnobRadius), (int)(2 * m_KnobRadius));
            using (SolidBrush back = new SolidBrush(this.BackColor), fore = new SolidBrush(this.ForeColor))
            {
                using (Pen border = new Pen(this.BorderColor, this.BorderThickness), needle = new Pen(this.NeedleColor, this.NeedleThickness))
                {
                    g.FillRectangle(back, this.ClientRectangle);
                    g.FillEllipse(fore, knobRect);
                    g.DrawEllipse(border, knobRect);
                    g.DrawLine(needle, c, m_NeedlePosition);
                }
            }

        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            m_MouseIsDown = true;
            m_DownAngle = GetAngleAtPointFromCenter(e.X, e.Y);
            m_DownValue = ControlParameter.Value;
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (m_MouseIsDown)
            {
                double angle = GetAngleAtPointFromCenter(e.X, e.Y);
                ControlParameter.Value = m_DownValue + GetValueByAngleDifferential(m_DownAngle - angle);


                base.OnMouseMove(e);

                this.Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            m_MouseIsDown = false;
            base.OnMouseUp(e);
        }
    }
}
