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

        private void Form1_Load(object sender, EventArgs e)
        {
            Language l = new Language();
            //l.Keywords.Add("int");
            l.Keywords.Add("rgtisadgjfdgfd");
            l.Keywords.Add("RunVoid();");
            //l.Keywords.Add("int");
            l.Operators.Add("=");
            l.Operators.Add("(");
            l.Operators.Add(")");
            //l.Operators.Add("{");
            //l.Operators.Add("}");
           
            l.LanguageName = "ButterLang";
            //l.AddLine("K0", "test.dll", "TheClass", "TheVoid");
            //l.AddLine("K1", "test.dll", "TheClass", "TheVoid");
            //l.AddLine("K0-?-O0-K1", "test.dll", "TheClass", "TheVoid");
            l.AddLine("K0-O1-?-O0-?-O2", "test.dll", "TheClass", "IfStatement");
            LanguageCompiler.ExecuteScript(l, File.ReadAllText("script.txt"));
        }
    }
}
