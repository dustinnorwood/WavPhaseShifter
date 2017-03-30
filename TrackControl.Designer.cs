namespace WavePhaseShifter
{
    partial class TrackControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.knobControl1 = new WavePhaseShifter.KnobControl();
            this.knobControl2 = new WavePhaseShifter.KnobControl();
            this.knobControl3 = new WavePhaseShifter.KnobControl();
            this.knobControl4 = new WavePhaseShifter.KnobControl();
            this.faderControl1 = new WavePhaseShifter.FaderControl();
            this.SuspendLayout();
            // 
            // knobControl1
            // 
            this.knobControl1.BorderColor = System.Drawing.Color.Black;
            this.knobControl1.KnobRadius = 12D;
            this.knobControl1.Location = new System.Drawing.Point(0, 0);
            this.knobControl1.MaximumValue = 1D;
            this.knobControl1.MinimumValue = 0D;
            this.knobControl1.Name = "knobControl1";
            this.knobControl1.NeedleColor = System.Drawing.Color.White;
            this.knobControl1.Size = new System.Drawing.Size(71, 60);
            this.knobControl1.TabIndex = 0;
            // 
            // knobControl2
            // 
            this.knobControl2.BorderColor = System.Drawing.Color.Black;
            this.knobControl2.KnobRadius = 20D;
            this.knobControl2.Location = new System.Drawing.Point(0, 66);
            this.knobControl2.MaximumValue = 1D;
            this.knobControl2.MinimumValue = 0D;
            this.knobControl2.Name = "knobControl2";
            this.knobControl2.NeedleColor = System.Drawing.Color.White;
            this.knobControl2.Size = new System.Drawing.Size(71, 66);
            this.knobControl2.TabIndex = 1;
            // 
            // knobControl3
            // 
            this.knobControl3.BorderColor = System.Drawing.Color.Black;
            this.knobControl3.KnobRadius = 20D;
            this.knobControl3.Location = new System.Drawing.Point(0, 138);
            this.knobControl3.MaximumValue = 1D;
            this.knobControl3.MinimumValue = 0D;
            this.knobControl3.Name = "knobControl3";
            this.knobControl3.NeedleColor = System.Drawing.Color.White;
            this.knobControl3.Size = new System.Drawing.Size(71, 66);
            this.knobControl3.TabIndex = 2;
            // 
            // knobControl4
            // 
            this.knobControl4.BorderColor = System.Drawing.Color.Black;
            this.knobControl4.KnobRadius = 20D;
            this.knobControl4.Location = new System.Drawing.Point(0, 210);
            this.knobControl4.MaximumValue = 1D;
            this.knobControl4.MinimumValue = 0D;
            this.knobControl4.Name = "knobControl4";
            this.knobControl4.NeedleColor = System.Drawing.Color.White;
            this.knobControl4.Size = new System.Drawing.Size(71, 66);
            this.knobControl4.TabIndex = 3;
            // 
            // faderControl1
            // 
            this.faderControl1.BorderColor = System.Drawing.Color.Black;
            this.faderControl1.FaderHeight = 20;
            this.faderControl1.FaderWidth = 50;
            this.faderControl1.KnobRadius = 30D;
            this.faderControl1.Location = new System.Drawing.Point(0, 294);
            this.faderControl1.MaximumValue = 1D;
            this.faderControl1.MinimumValue = 0D;
            this.faderControl1.Name = "faderControl1";
            this.faderControl1.Size = new System.Drawing.Size(71, 227);
            this.faderControl1.TabIndex = 4;
            this.faderControl1.TrackColor = System.Drawing.Color.Black;
            // 
            // TrackControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.faderControl1);
            this.Controls.Add(this.knobControl4);
            this.Controls.Add(this.knobControl3);
            this.Controls.Add(this.knobControl2);
            this.Controls.Add(this.knobControl1);
            this.Name = "TrackControl";
            this.Size = new System.Drawing.Size(71, 536);
            this.ResumeLayout(false);

        }

        #endregion

        private KnobControl knobControl1;
        private KnobControl knobControl2;
        private KnobControl knobControl3;
        private KnobControl knobControl4;
        private FaderControl faderControl1;
    }
}
