using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IO;

namespace MVAR_MPVR_Extraction
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            fileText.Text = "";
            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
            string lastFile = files[files.Length - 1];
            {
                string path = lastFile;

                var readoffset = new int();
                readoffset = 0;

                //Check to see if the first chunk is MPVR, MVAR, or EOF.
                //If it is one of those types, extract if not EOF.
                int ii = NonVitalCheck(path, 0);
                if (ii == 1)
                {
                    VitalPass(path, 0);
                }
                //If not one of those types, check if it doesn't have a BLF header.
                int blf = BLFCheck(path, 0);
                if (blf == 0 && ii == 0)
                {
                    MessageBox.Show("Invalid File");
                }
                //If it does, loop through each section.
                if (blf == 1 && ii == 0)
                {
                    int block2offset = ReadBlock(path, 0);
                    readoffset = readoffset + block2offset;
                    //Checks each section in a BLF file until it finds MPVR, MVAR, or EOF.
                    for (int i = 0; ; i++)
                    {
                        int i2 = NonVitalCheck(path, readoffset);
                        if (i2 == 1)
                        {
                            //If it is, Determine which type it is, and break the loop.
                            VitalPass(path, readoffset);
                            break;
                        }
                        else
                        {
                            int readsize = ReadBlock(path, readoffset);
                            readoffset = readoffset + readsize;
                        }
                    }
                }
            }
        }

        private void MainForm_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void VitalPass(string path, int _offset)
        {
            int d = VitalDetermine(path, _offset);
            if (d == 1)
            {
                RawVariants.CreateMPVRandHeader(path, _offset);
            }
            if (d == 2)
            {
                RawVariants.CreateMVARandHeader(path, _offset);
            }
            if (d == 3)
            {
                MessageBox.Show("No MPVR or MVAR data could be found.");
            }
            if (d == 1 || d == 2)
            {
                fileText.Text = Path.GetFileNameWithoutExtension(path) + " successfully extracted!";
                fileText.Location = new System.Drawing.Point((dropPanel.Size.Width / 2) - (fileText.Width / 2), (dropPanel.Size.Height / 2) + (fileText.Height / 2));
            }
        }

        public string BlockMagicCheck(string _path, int start)
        {
            try
            {
                Reader r = new Reader(_path);
                r.Position = start;
                string blockmagic = r.ReadString(4);
                r.Close();
                return blockmagic;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return "";
            }
        }

        public int ReadBlock(string _path, int start)
        {
            try
            {
                Reader r = new Reader(_path);

                r.Position = start + 4;
                int blocksize = r.ReadInt32();
                r.Close();
                return blocksize;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return 0;
            }
        }

        public int NonVitalCheck(string _path, int start)
        {
            string mpvr = "mpvr";
            string mvar = "mvar";
            string _eof = "_eof";

            string blockmagic = BlockMagicCheck(_path, start);
            if (blockmagic != mpvr && blockmagic != mvar && blockmagic != _eof)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public int BLFCheck(string _path, int start)
        {
            string _blf = "_blf";

            string blockmagic = BlockMagicCheck(_path, start);
            if (blockmagic == _blf)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int VitalDetermine(string _path, int start)
        {
            string mpvr = "mpvr";
            string mvar = "mvar";
            string _eof = "_eof";

            string blockmagic = BlockMagicCheck(_path, start);
            if (blockmagic == mpvr)
            {
                return 1;
            }
            if (blockmagic == mvar)
            {
                return 2;
            }
            if (blockmagic == _eof)
            {
                return 3;
            }
            else
            {
                return 0;
            }
        }
    }
}
