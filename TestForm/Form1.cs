﻿using System;
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
            l.Keywords.Add("ButterLang");

            l.VariableBacker = "";

            //l.Keywords.Add("int");
            l.Operators.Add("=");
            l.Operators.Add("(");
            l.Operators.Add(")");
            l.Operators.Add("#");
            //l.Operators.Add("{");
            //l.Operators.Add("}");

            l.LanguageName = "ButterLang";
            //l.AddLine("K0", "test.dll", "TheClass", "TheVoid");
            //l.AddLine("K1", "test.dll", "TheClass", "TheVoid");
            //l.AddLine("K0-?-O0-K1", "test.dll", "TheClass", "TheVoid");
            //List<string> s1 = new List<string>();
            //s1.Add("test.dll");
            //List<string> s2 = new List<string>();
            //s2.Add("TheClass");
            //List<string> s3 = new List<string>();
           // s3.Add("IfStatement");

            l.AddLine("K0-O1-?2-O0-?4-O2", 
                "test.dll",
                "TheClass",
                "IfStatement");

            //l.AddLine("O3-K2-?", "test.dll", "TheClass", "TheVoid");
            LanguageCompiler.ExecuteScript(l, File.ReadAllText("script.txt"));
        }
    }
}
