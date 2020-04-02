using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using butter;

namespace TestForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public List<object> func(List<string> s)
        {
            MessageBox.Show("yo");
            return new List<object>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Language l = new Language();
            l.Keywords.Add("a");
            l.Operators.Add("-");

            Func<List<string>, List<object>> SentFunc = func;
            l.AddLineWithFunc(SentFunc, "K0-O0");

            LanguageCompiler.ExecuteScriptWithFunc(l, "a-");

        }
    }
}
