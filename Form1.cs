using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WavePhaseShifter
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Wav Files (*.wav)|*.wav";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                BufferedAudioPort bap = new BufferedAudioPort(ofd.FileName);
                //AdderNode a1 = new AdderNode();
                //AdderNode a2 = new AdderNode();
                //DelayNode d1 = new DelayNode(750);
                //DelayNode d2 = new DelayNode(500);
                //GainNode g1 = new GainNode(0.4);
                //GainNode g2 = new GainNode(0.3);
                ResamplerNode r1 = new ResamplerNode(10000);
                ResamplerNode r2 = new ResamplerNode(10000);
                BitCrusherNode b1 = new BitCrusherNode(6.0);
                BitCrusherNode b2 = new BitCrusherNode(6.0);
                WaveOutPort wop = new WaveOutPort(bap.WavData, "C:\\Users\\Dustin\\Desktop\\" + Path.GetFileNameWithoutExtension(ofd.FileName) + " - Crushed.wav");

                AudioNode.TieNodes(bap.GetChannel(0), r1);
                //AudioNode.TieNodes(d1, a1);
                //AudioNode.TieNodes(a1, g1);
                //AudioNode.TieNodes(g1, d1);
                AudioNode.TieNodes(r1, b1);
                AudioNode.TieNodes(b1, wop.GetChannel(0));

                AudioNode.TieNodes(bap.GetChannel(1), r2);
                //AudioNode.TieNodes(d2, a2);
                //AudioNode.TieNodes(a2, g2);
                //AudioNode.TieNodes(g2, d2);
                AudioNode.TieNodes(r2, b2);
                AudioNode.TieNodes(b2, wop.GetChannel(1));

                bap.Start();

                wop.Save();
            }
        }
    }
}
