using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IO
{
    public class Reader : BaseIO
    {
        /// <summary>
        /// A file reader.
        /// </summary>
        /// <param name="path">Path of the file.</param>
        public Reader(string path)
        {
            br = new BinaryReader(File.OpenRead(path));
            this.byteOrder = ByteOrder.BigEndian;
        }

        /// <summary>
        /// A file reader.
        /// </summary>
        /// <param name="path">Path of the file.</param>
        /// <param name="bo">Byte order of the bytes to be read.</param>
        public Reader(string path, ByteOrder bo)
        {
            br = new BinaryReader(File.OpenRead(path));
            this.byteOrder = bo;
        }

        private BinaryReader br;

        public long Position
        {
            get { return br.BaseStream.Position; }
            set { br.BaseStream.Position = value; }
        }

        public byte ReadByte()
        {
            return br.ReadByte();
        }

        public sbyte ReadSByte()
        {
            return (sbyte)br.ReadByte();
        }

        public short ReadInt16()
        {
            short int16 = br.ReadInt16();
            if (byteOrder == ByteOrder.BigEndian)
            {
                byte[] array16 = BitConverter.GetBytes(int16);
                Array.Reverse(array16);
                int16 = BitConverter.ToInt16(array16, 0);
            }
            return int16;
        }

        public ushort ReadUInt16()
        {
            ushort uint16 = br.ReadUInt16();
            if (byteOrder == ByteOrder.BigEndian)
            {
                byte[] arrayu16 = BitConverter.GetBytes(uint16);
                Array.Reverse(arrayu16);
                uint16 = BitConverter.ToUInt16(arrayu16, 0);
            }

            return uint16;
        }

        public int ReadInt32()
        {
            int int32 = br.ReadInt32();
            if (byteOrder == ByteOrder.BigEndian)
            {
                byte[] array32 = BitConverter.GetBytes(int32);
                Array.Reverse(array32);
                int32 = BitConverter.ToInt32(array32, 0);
            }
            return int32;
        }

        public uint ReadUInt32()
        {
            uint uint32 = br.ReadUInt32();
            if (byteOrder == ByteOrder.BigEndian)
            {
                byte[] arrayu32 = BitConverter.GetBytes(uint32);
                Array.Reverse(arrayu32);
                uint32 = BitConverter.ToUInt32(arrayu32, 0);
            }
            return uint32;
        }

        public byte[] ReadBytes(int length)
        {
            byte[] buffer = br.ReadBytes(length);
            if (byteOrder == ByteOrder.LittleEndian)
            {
                Array.Reverse(buffer);
            }
            return buffer;
        }

        public string ReadString(int length)
        {
            return new string(br.ReadChars(length));
        }

        public char[] ReadChars(int length)
        {
            char[] buffer = br.ReadChars(length);
            if (byteOrder == ByteOrder.LittleEndian)
            {
                Array.Reverse(buffer);
            }
            return buffer;
        }

        public void Close()
        {
            br.Close();
        }
    }

    public class Writer : BaseIO
    {
        BinaryWriter bw;

        /// <summary>
        /// A file writer.
        /// </summary>
        /// <param name="path">Path of the file.</param>
        public Writer(string path)
        {
            bw = new BinaryWriter(File.OpenWrite(path));
            byteOrder = ByteOrder.BigEndian;
        }

        /// <summary>
        /// A file writer.
        /// </summary>
        /// <param name="path">Path of the file.</param>
        /// <param name="bo">Byte order of the bytes to be read.</param>
        public Writer(string path, ByteOrder bo)
        {
            bw = new BinaryWriter(File.OpenWrite(path));
            byteOrder = bo;
        }

        public long Position
        {
            get { return bw.BaseStream.Position; }
            set { bw.BaseStream.Position = value; }
        }

        public void WriteByte(byte byteToWrite)
        {
            bw.Write(byteToWrite);
        }

        public void WriteBytes(byte[] bytesToWrite)
        {
            if (byteOrder == ByteOrder.BigEndian)
                Array.Reverse(bytesToWrite);
            bw.Write(bytesToWrite);
        }

        public void WriteInt16(short int16toWrite)
        {
            byte[] buffer = BitConverter.GetBytes(int16toWrite);
            if (byteOrder == ByteOrder.BigEndian)
                Array.Reverse(buffer);
            bw.Write(buffer);
        }

        public void WriteUInt16(ushort uint16toWrite)
        {
            byte[] buffer = BitConverter.GetBytes(uint16toWrite);
            if (byteOrder == ByteOrder.BigEndian)
                Array.Reverse(buffer);
            bw.Write(buffer);
        }

        public void WriteInt32(int int32toWrite)
        {
            byte[] buffer = BitConverter.GetBytes(int32toWrite);
            if (byteOrder == ByteOrder.BigEndian)
                Array.Reverse(buffer);
            bw.Write(buffer);
        }

        public void WriteUInt32(uint uint32toWrite)
        {
            byte[] buffer = BitConverter.GetBytes(uint32toWrite);
            if (byteOrder == ByteOrder.BigEndian)
                Array.Reverse(buffer);
            bw.Write(buffer);
        }

        public void WriteString(string stringToWrite)
        {
            bw.Write(stringToWrite.ToCharArray());
        }

        public void Close()
        {
            bw.Close();
        }
    }

    public abstract class BaseIO
    {
        public enum ByteOrder
        {
            BigEndian,
            LittleEndian
        }
        protected ByteOrder byteOrder;

        public void ChangeByteOrder(ByteOrder bo)
        {
            this.byteOrder = bo;
        }
    }
}
