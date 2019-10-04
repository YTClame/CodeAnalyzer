using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace language_analyzer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String str = textBox1.Text;
            label1.Text = "";
            str = str.TrimStart(' ', '\n', '\r');
            ids.Text = "";
            consts.Text = "";
            Errors er = AnalazerClass.GoAnalyze(str);
            printIDSandCONSTS(er);
        }

        private void printIDSandCONSTS(Errors er)
        {
            if(er.IsGood == false)
            {
                textBox1.Focus();
                textBox1.Select(er.countError, 0);
                label1.ForeColor = Color.Red;
                label1.Text = er.ErrorMessage;
               
            }
            else
            {
                textBox1.Focus();
                label1.ForeColor = Color.LimeGreen;
                label1.Text = "Ошибок нет";
                if (er.loops == -1)
                {
                    label3.Text = "Бесконечность";
                }
                else
                {
                    label3.Text = er.loops.ToString();
                }
            }
            LinkedList<string> IDS = er.IDS;
            LinkedList<string> CONSTS = er.CONSTS;
            if(IDS != null) foreach (string s in IDS) ids.Text = ids.Text + s + '\n';
            if(CONSTS != null) foreach (string s in CONSTS) consts.Text = consts.Text + s + '\n';
        }
    }

}
