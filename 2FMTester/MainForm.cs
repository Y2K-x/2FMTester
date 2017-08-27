using System;
using System.Timers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace _2FMTester
{
    public partial class MainForm : Form
    {
        OpenFileDialog fileSelect = new OpenFileDialog();

        public MainForm()
        {
            InitializeComponent();
            pcsxCon.Enabled = false;
            readBytesToolStripMenuItem.Enabled = false;

            MainTimer.Start();

        }

        private void OnTimedEvent(object sender, EventArgs e)
        {
            bool pcsx2Seek = DMA_Access.SeekPCSX2();

            if (pcsx2Seek == true)
            {
                pcsxCheck.Text = "Status: PCSX2 Detected!";
                pcsxCon.Enabled = true;
                readBytesToolStripMenuItem.Enabled = true;
            }
            else
            {
                pcsxCheck.Text = "Status: PCSX2 not running...";
                pcsxCon.Enabled = false;
                readBytesToolStripMenuItem.Enabled = false;
            }
        }

        private void pcsxCon_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void readBytesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form fm = new ReadForm();
            Form fc = Application.OpenForms["ReadForm"];

            if (fc != null)
            {

            }  
            else
            {
                fm.Show();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            fileSelect.Filter = "MDLX|*.mdlx|MSET|*.mset";

            if (fileSelect.ShowDialog() == DialogResult.OK)
            {
                label2.Text = "File: " + fileSelect.SafeFileName;
                string ext = Path.GetExtension(fileSelect.FileName);

                if (ext == ".mdlx")
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }

                fileSelect.Dispose();
            }
        }
    }
}
