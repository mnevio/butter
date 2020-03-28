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
            l.VariableBacker = "";

            l.LanguageName = "UniLang";

            #region Keywords
            l.Keywords.Add("var");
            l.Keywords.Add("as");
            l.Keywords.Add("string");
            l.Keywords.Add("integer");
            l.Keywords.Add("boolean");
            l.Keywords.Add("floatingpoint");
            l.Keywords.Add("if");
            l.Keywords.Add("on");
            l.Keywords.Add("other");
            l.Keywords.Add("debug");
            l.Keywords.Add("msgraw");
            l.Keywords.Add("msgdrawn");
            #endregion

            #region Operators
            l.Operators.Add("=");
            l.Operators.Add(".");
            l.Operators.Add(",");
            l.Operators.Add("(");
            l.Operators.Add(")");
            l.Operators.Add("{");
            l.Operators.Add("}");
            l.Operators.Add(";");
            l.Operators.Add("+");
            l.Operators.Add("-");
            l.Operators.Add("?");
            #endregion

            #region Lines
            l.AddLine("K9-O1-K10-O3-?-O4-O7",
                "unilang.dll",
                "Methods",
                "msgraw");

            #endregion

            LanguageCompiler.ExecuteScript(l,
                File.ReadAllText("script.txt"));
        }
    }
}
