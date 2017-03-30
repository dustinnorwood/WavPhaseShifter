using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WavePhaseShifter
{
    public class WavData
    {
        public int chunkID;
        public int fileSize;
        public int riffType;
        public int fmtID;
        public int fmtSize;
        public short fmtCode;
        public short channels;
        public int sampleRate;
        public int fmtAvgBPS;
        public short fmtBlockAlign;
        public short bitDepth;
        public short fmtExtraSize;
        public int dataID;
        public int dataSize;
        public byte[] data;

        public WavData() { }

        public WavData Copy()
        {
            WavData w = new WavData();
            w.chunkID = this.chunkID;
            w.fileSize = this.fileSize;
            w.riffType = this.riffType;
            w.fmtID = this.fmtID;
            w.fmtSize = this.fmtSize;
            w.fmtCode = this.fmtCode;
            w.channels = this.channels;
            w.sampleRate = this.sampleRate;
            w.fmtAvgBPS = this.fmtAvgBPS;
            w.fmtBlockAlign = this.fmtBlockAlign;
            w.bitDepth = this.bitDepth;
            w.fmtExtraSize = this.fmtExtraSize;
            w.dataID = this.dataID;
            w.dataSize = this.dataSize;
            w.data = new byte[this.data.Length];
            this.data.CopyTo(w.data, 0);
            return w;
        }

        public static WavData Load(string path)
        {
            WavData w = new WavData();
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fs))
                {


                    w.chunkID = reader.ReadInt32();
                    w.fileSize = reader.ReadInt32();
                    w.riffType = reader.ReadInt32();
                    w.fmtID = reader.ReadInt32();
                    w.fmtSize = reader.ReadInt32();
                    w.fmtCode = reader.ReadInt16();
                    w.channels = reader.ReadInt16();
                    w.sampleRate = reader.ReadInt32();
                    w.fmtAvgBPS = reader.ReadInt32();
                    w.fmtBlockAlign = reader.ReadInt16();
                    w.bitDepth = reader.ReadInt16();

                    if (w.fmtSize == 18)
                    {
                        // Read any extra values
                        w.fmtExtraSize = reader.ReadInt16();
                        reader.ReadBytes(w.fmtExtraSize);
                    }

                    w.dataID = reader.ReadInt32();
                    w.dataSize = reader.ReadInt32();


                    // Store the audio data of the wave file to a byte array. 

                    w.data = reader.ReadBytes(w.dataSize);
                }
            }

            return w;
        }

        public void Save(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (BinaryWriter writer = new BinaryWriter(fs))
                {

                    //Read the wave file header from the buffer. 
                    writer.Write(this.chunkID);
                    writer.Write(36 + this.data.Length);
                    writer.Write(this.riffType);
                    writer.Write(this.fmtID);
                    writer.Write(this.fmtSize);
                    writer.Write(this.fmtCode);
                    writer.Write(this.channels);
                    writer.Write(this.sampleRate);
                    writer.Write(this.fmtAvgBPS);
                    writer.Write(this.fmtBlockAlign);
                    writer.Write(this.bitDepth);

                    writer.Write(this.dataID);
                    writer.Write(this.data.Length);
                    writer.Write(this.data);

                }
            }
        }
    }
}
