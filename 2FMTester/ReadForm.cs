using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2FMTester
{

    public partial class ReadForm : Form
    {
        int offset;
        bool setActive;

        delegate void StringArgReturningVoidDelegate(string text);

        public ReadForm()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task t = Task.Run(() =>
            {
                //try
                //{
                    int textLength = textBox1.Text.Length;
                    string userOffset = textBox1.Text;

                    if (textLength == 8)
                    {
                        setActive = true;

                        while (setActive == true)
                        {
                            offset = Convert.ToInt32(userOffset, 16);
                            var val = DMA_Access.ReadInteger(offset);
                            this.SetText("Value: 0x" + val.ToString("X8"));
                            Thread.Sleep(500);
                        }
                    }
                    else
                    {
                        //throw new Exception();
                    }

                //}
                //catch
                //{
                   // MessageBox.Show("The value you entered is not a valid offset.", "Invalid offset", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            });
        }

        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            try
            {
                if (this.valueLabel.InvokeRequired)
                {
                    StringArgReturningVoidDelegate d = new StringArgReturningVoidDelegate(SetText);
                    this.Invoke(d, new object[] { text });
                }
                else
                {
                    this.valueLabel.Text = text;
                }
            }
            catch
            {

            }
        }


        private void newValue_Click(object sender, EventArgs e)
        {
            setActive = false;
            textBox1.Text = "";
            valueLabel.Text = "Value: 0x????????";
        }
    }
}
