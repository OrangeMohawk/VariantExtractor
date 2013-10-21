using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using IO;

namespace MVAR_MPVR_Extraction
{
    class RawVariants
    {
        /// <summary>
        /// A method to get the raw MPVR data from the start of the MPVR chunk.
        /// Returns the path of the raw MPVR file.
        /// </summary>
        /// <param name="path">The path to the file that is being read.</param>
        /// <param name="_offset">The offset of the MPVR chunk</param>
        public static string RawMPVRCreate(string path, int _offset)
        {
            Reader r = new Reader(path);
            r.Position = _offset + 0x24;
            int size = r.ReadInt32();

            r.Position = _offset + 0x28;
            byte[] bytes = r.ReadBytes(size);

            for (int i = 1; i <= 4; i++) BitShift.ShiftLeft(bytes);

            r.Close();

            string mpvrcreate = Path.ChangeExtension(path, "mglo");

            Writer w = new Writer(mpvrcreate, BaseIO.ByteOrder.LittleEndian);
            w.Position = 0;
            w.WriteBytes(bytes);
            w.Close();

            return mpvrcreate;
        }

        /// <summary>
        /// A method to get the header from a raw MPVR file.
        /// </summary>
        /// <param name="rawmpvr">The raw MPVR file the header is being gotten from.</param>
        public static void RawMPVRHeaderCreate(string rawmpvr)
        {
            Reader r = new Reader(rawmpvr);
            var endoffsetless57 = new int();
            r.Position = 0x2;
            byte check1 = r.ReadByte();
            if (check1 == 0)
            {
                for (int i = 0; ; i += 2)
                {
                    r.Position = 0x57 + i;
                    short check = r.ReadInt16();
                    if (check == 0)
                    {
                        r.Position = 0x59 + i;
                        byte check2 = r.ReadByte();
                        if (check2 != 0)
                        {
                            endoffsetless57 = i;
                            break;
                        }
                        r.Position = 0x57 + i;
                        int check3 = r.ReadInt32();
                        if (check3 == 0)
                        {
                            endoffsetless57 = i + 3;
                            break;
                        }
                    }
                }
                int endoffset = endoffsetless57 + 0x5A;
                int readamount = endoffset - 8;
                r.Position = 8;
                byte[] headerbytes = r.ReadBytes(readamount);
                r.Close();

                string mpvrheadercreate = Path.ChangeExtension(rawmpvr, "mgloheader");

                Writer w = new Writer(mpvrheadercreate, BaseIO.ByteOrder.LittleEndian);
                w.Position = 0;
                w.WriteBytes(headerbytes);

                int lastbyteindex = headerbytes.Length - 1;

                byte lastbyte = headerbytes[lastbyteindex];
                int newlastbyte = lastbyte - 2;

                w.Position = lastbyteindex;
                w.WriteByte((byte)newlastbyte);
                w.Close();
            }
            var endoffsetless63 = new int();
            var check343 = new int();
            if (check1 == 1)
            {
                for (int i = 0; ; i += 2)
                {
                    r.Position = 0x63 + i;
                    short check = r.ReadInt16();
                    if (check == 0)
                    {
                        check343++;
                        if (check343 == 2)
                        {
                            endoffsetless63 = i;
                            break;
                        }
                    }
                }
                int endoffset = endoffsetless63 + 0x66;
                int readamount = endoffset - 8;
                r.Position = 8;
                byte[] headerbytes = r.ReadBytes(readamount);
                r.Close();

                string mpvrheadercreate = Path.ChangeExtension(rawmpvr, "mgloheader");

                Writer w = new Writer(mpvrheadercreate, BaseIO.ByteOrder.LittleEndian);
                w.Position = 0;
                w.WriteBytes(headerbytes);

                int lastbyteindex = headerbytes.Length - 1;

                byte lastbyte = headerbytes[lastbyteindex];
                int newlastbyte = lastbyte - 2;

                w.Position = lastbyteindex;
                w.WriteByte((byte)newlastbyte);
                w.Close();
            }   
        }

        /// <summary>
        /// A method to get the raw MPVR data and its header from the start of the MPVR chunk.
        /// </summary>
        /// <param name="path">The path to the file that is being read.</param>
        /// <param name="_offset">The offset of the MPVR chunk</param>
        public static void CreateMPVRandHeader(string path, int _offset)
        {
            Reader r = new Reader(path);
            r.Position = _offset + 0x24;
            int size = r.ReadInt32();

            r.Position = _offset + 0x28;
            byte[] bytes = r.ReadBytes(size);

            for (int i = 1; i <= 4; i++) BitShift.ShiftLeft(bytes);

            r.Close();

            string mpvrcreate = Path.ChangeExtension(path, "mglo");

            Writer w = new Writer(mpvrcreate, BaseIO.ByteOrder.LittleEndian);
            w.Position = 0;
            w.WriteBytes(bytes);
            w.Close();

            Reader nr = new Reader(mpvrcreate);
            var endingoffsethelper = new int();
            nr.Position = 0x2;
            byte check1 = nr.ReadByte();
            if (check1 == 0)
            {
                for (int i = 0; ; i += 2)
                {
                    nr.Position = 0x57 + i;
                    short headerendcheck1 = nr.ReadInt16();
                    if (headerendcheck1 == 0)
                    {
                        nr.Position = 0x59 + i;
                        byte headerendcheck2 = nr.ReadByte();
                        if (headerendcheck2 != 0)
                        {
                            endingoffsethelper = i;
                            break;
                        }
                        nr.Position = 0x57 + i;
                        int headerendcheck3 = nr.ReadInt32();
                        if (headerendcheck3 == 0)
                        {
                            endingoffsethelper = i + 3;
                            break;
                        }
                    }
                }
                int endoffset = endingoffsethelper + 0x5A;
                int readamount = endoffset - 8;
                nr.Position = 8;
                byte[] headerbytes = nr.ReadBytes(readamount);
                nr.Close();

                string mpvrheadercreate = Path.ChangeExtension(path, "mgloheader");

                Writer nw = new Writer(mpvrheadercreate, BaseIO.ByteOrder.LittleEndian);
                nw.Position = 0;
                nw.WriteBytes(headerbytes);

                int lastbyteindex = headerbytes.Length - 1;

                byte lastbyte = headerbytes[lastbyteindex];
                int newlastbyte = lastbyte - 2;

                nw.Position = lastbyteindex;
                nw.WriteByte((byte)newlastbyte);
                nw.Close();
            }
            var halo4check = new int();
            if (check1 == 1)
            {
                for (int i = 0; ; i += 2)
                {
                    nr.Position = 0x63 + i;
                    short headerendcheck = nr.ReadInt16();
                    if (headerendcheck == 0)
                    {
                        halo4check++;
                        if (halo4check == 2)
                        {
                            endingoffsethelper = i;
                            break;
                        }
                    }
                }
                int endoffset = endingoffsethelper + 0x66;
                int readamount = endoffset - 8;
                nr.Position = 8;
                byte[] headerbytes = nr.ReadBytes(readamount);
                nr.Close();

                string mpvrheadercreate = Path.ChangeExtension(path, "mgloheader");

                Writer nw = new Writer(mpvrheadercreate, BaseIO.ByteOrder.LittleEndian);
                nw.Position = 0;
                nw.WriteBytes(headerbytes);

                int lastbyteindex = headerbytes.Length - 1;

                byte lastbyte = headerbytes[lastbyteindex];
                int newlastbyte = lastbyte - 2;

                nw.Position = lastbyteindex;
                nw.WriteByte((byte)newlastbyte);
                nw.Close();
            }   
        }

        /// <summary>
        /// A method to get the raw MVAR data from the start of the MVAR chunk.
        /// Returns the path of the raw MVAR file.
        /// </summary>
        /// <param name="path">The path to the file that is being read.</param>
        /// <param name="_offset">The offset of the MVAR chunk</param>
        public static string RawMVARCreate(string path, int _offset)
        {
            Reader r = new Reader(path);
            r.Position = _offset + 0x20;
            int size = r.ReadInt32();

            r.Position = _offset + 0x24;
            byte[] bytes = r.ReadBytes(size);

            r.Close();

            string mvarcreate = Path.ChangeExtension(path, "rawmvar");

            Writer w = new Writer(mvarcreate, BaseIO.ByteOrder.LittleEndian);
            w.Position = 0;
            w.WriteBytes(bytes);
            w.Close();

            return mvarcreate;
        }

        /// <summary>
        /// A method to get the header from a raw MVAR file.
        /// </summary>
        /// <param name="rawmpvr">The raw MVAR file the header is being gotten from.</param>
        public static void RawMVARHeaderCreate(string rawmvar)
        {
            Reader r = new Reader(rawmvar);
            var endoffsetless51 = new int();
            r.Position = 0x34;
            short check1 = r.ReadInt16();
            if (check1 != 0x7D)
            {
                for (int i = 0; ; i += 2)
                {
                    r.Position = 0x51 + i;
                    short check = r.ReadInt16();
                    if (check == 0)
                    {
                        r.Position = 0x53 + i;
                        byte check2 = r.ReadByte();
                        if (check2 == 0x3E || check2 == 0x3F)
                        {
                            endoffsetless51 = i;
                            break;
                        }
                        r.Position = 0x51 + i;
                        int check3 = r.ReadInt32();
                        if (check3 == 0)
                        {
                            endoffsetless51 = i + 3;
                            break;
                        }
                    }
                }
                int endoffset = endoffsetless51 + 0x53;
                r.Position = 0;
                byte[] headerbytes = r.ReadBytes(endoffset);
                r.Close();

                string mvarheadercreate = Path.ChangeExtension(rawmvar, "rawmvarheader");

                Writer nw = new Writer(mvarheadercreate, BaseIO.ByteOrder.LittleEndian);
                nw.Position = 0;
                nw.WriteBytes(headerbytes);
                nw.Close();
            }
            var check3432 = new int();
            if (check1 == 0x7D)
            {
                for (int i = 0; ; i += 2)
                {
                    r.Position = 0x6B + i;
                    short check = r.ReadInt16();
                    if (check == 0)
                    {
                        check3432++;
                        if (check3432 == 2)
                        {
                            endoffsetless51 = i;
                            break;
                        }
                    }
                }
                int endoffset = endoffsetless51 + 0x6D;
                r.Position = 0;
                byte[] headerbytes = r.ReadBytes(endoffset);
                r.Close();

                string mvarheadercreate = Path.ChangeExtension(rawmvar, "rawmvarheader");

                Writer nw = new Writer(mvarheadercreate, BaseIO.ByteOrder.LittleEndian);
                nw.Position = 0;
                nw.WriteBytes(headerbytes);
                nw.Close();
            }
        }

        /// <summary>
        /// A method to get the raw MVAR data and its header from the start of the MVAR chunk.
        /// </summary>
        /// <param name="path">The path to the file that is being read.</param>
        /// <param name="_offset">The offset of the MVAR chunk</param>
        public static void CreateMVARandHeader(string path, int _offset)
        {
            Reader r = new Reader(path);
            r.Position = _offset + 0x20;
            int size = r.ReadInt32();

            r.Position = _offset + 0x24;
            byte[] bytes = r.ReadBytes(size);

            r.Close();

            string mvarcreate = Path.ChangeExtension(path, "rawmvar");

            Writer w = new Writer(mvarcreate, BaseIO.ByteOrder.LittleEndian);
            w.Position = 0;
            w.WriteBytes(bytes);
            w.Close();

            Reader nr = new Reader(mvarcreate);
            var endingoffsethelper = new int();
            nr.Position = 0x34;
            short halo4check = nr.ReadInt16();
            if (halo4check != 0x7D)
            {
                for (int i = 0; ; i += 2)
                {
                    nr.Position = 0x51 + i;
                    short headerendcheck1 = nr.ReadInt16();
                    if (headerendcheck1 == 0)
                    {
                        nr.Position = 0x53 + i;
                        byte headerendcheck2 = nr.ReadByte();
                        if (headerendcheck2 == 0x3E || headerendcheck2 == 0x3F)
                        {
                            endingoffsethelper = i;
                            break;
                        }
                        nr.Position = 0x51 + i;
                        int headerendcheck3 = nr.ReadInt32();
                        if (headerendcheck3 == 0)
                        {
                            endingoffsethelper = i + 3;
                            break;
                        }
                    }
                }
                int endoffset = endingoffsethelper + 0x53;
                nr.Position = 0;
                byte[] headerbytes = nr.ReadBytes(endoffset);
                nr.Close();

                string mvarheadercreate = Path.ChangeExtension(path, "rawmvarheader");

                Writer nw = new Writer(mvarheadercreate, BaseIO.ByteOrder.LittleEndian);
                nw.Position = 0;
                nw.WriteBytes(headerbytes);
                nw.Close();
            }
            var halo4check2 = new int();
            if (halo4check == 0x7D)
            {
                for (int i = 0; ; i += 2)
                {
                    nr.Position = 0x6B + i;
                    short headerendcheck1 = nr.ReadInt16();
                    if (headerendcheck1 == 0)
                    {
                        halo4check2++;
                        if (halo4check2 == 2)
                        {
                            endingoffsethelper = i;
                            break;
                        }
                    }
                }
                int endoffset = endingoffsethelper + 0x6D;
                nr.Position = 0;
                byte[] headerbytes = nr.ReadBytes(endoffset);
                nr.Close();

                string mvarheadercreate = Path.ChangeExtension(path, "rawmvarheader");

                Writer nw = new Writer(mvarheadercreate, BaseIO.ByteOrder.LittleEndian);
                nw.Position = 0;
                nw.WriteBytes(headerbytes);
                nw.Close();
            }
        }
    }
}
