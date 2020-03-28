using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Text.RegularExpressions;

namespace butter
{
    public static class Versioning
    {
        public static string ReturnVersion()
        {
            return "Butter Scripting Engine v:1.0";
        }
    }

    public class Language
    {
        public string LanguageName = "ButterLang";
        public List<string> Keywords = new List<string>();
        public List<string> Operators = new List<string>();

        public string VariableBacker = "_";

        private List<Line> Lines = new List<Line>();

        public void AddLine(string line = "K0-O1", string dllpath = "mydll.dll",
            string classToEnter = "MyClass", string methodname = "myMethod")
        {
            Line n = new Line();
            n.line = line;
            n.DLLToRun = dllpath;
            n.ClassToEnter = classToEnter;
            n.MethodToRun = methodname;
            Lines.Add(n);
        }

        public List<Line> GetLines()
        {
            return Lines;
        }

    }

    public class Line
    {
        public string line;

        public string DLLToRun;
        public string ClassToEnter;
        public string MethodToRun;
    }


    public static class LanguageCompiler
    {
        public static void ExecuteScript(Language l, string script)
        {

            string[] lines = script.Split(
           new[] { "\r\n", "\r", "\n", Environment.NewLine },
           StringSplitOptions.None
           );

            //Iterate through every line of the presented script
            foreach (string item in lines)
            {

                //Iterate through every line registered into the Language
                //string theline = item.Replace(" ", "");
                foreach (Line i in l.GetLines())
                {

                    List<string> unknown = new List<string>();
                    //int unknowncurrentnumber = 0;
                    string ll = "";


                    string[] indcom = i.line.Split('-');
                    foreach (string t in indcom)
                    {
                        t.Replace("-", "");
                    }

                    //Iterate through every section of the registered line
                    string cache = item;
                    //bool LastWasUnknown = false;
                    int dd = -1;
                    foreach (string m in indcom)
                    {
                        dd++;
                        if (m.Contains("K"))
                        {
                            string xl = m.Replace("K", "");
                            

                                ll = ll + l.Keywords[Convert.ToInt32(xl)];
                            
                            
                        }
                        if (m.Contains("O"))
                            {
                                string xl = m.Replace("O", "");
                               
                                    ll = ll + l.Operators[Convert.ToInt32(xl)];
                                
                               
                            }
                        if (m.Contains("?"))
                            {
                            //LastWasUnknown = true;
                            //unknown.Add(m);
                            //unknowncurrentnumber = unknown.Count;
                            if (l.VariableBacker != "")
                            {
                                string xl = m.Replace("?", "");
                                List<string> s = new List<string>();

                                string[] llll = cache.Split(Convert.ToChar(l.VariableBacker));
                                unknown.Add(llll[Convert.ToInt32(xl)]);
                                ll = ll + llll[Convert.ToInt32(xl)];
                            }
                            else
                            {
                                string BeforeOneSyntax = indcom[dd - 1];
                                string NextOneSyntax = indcom[dd+1];

                                string BeforeOne = "";
                                string NextOne = "";

                                if (BeforeOneSyntax.Contains("K"))
                                {

                                    string xm = BeforeOneSyntax.Replace("K", "");
                                    BeforeOne = l.Keywords[Convert.ToInt32(xm)];

                                }
                                else if (BeforeOneSyntax.Contains("O"))
                                {
                                    string xm = BeforeOneSyntax.Replace("O", "");
                                    BeforeOne = l.Operators[Convert.ToInt32(xm)];
                                }
                                else if (BeforeOneSyntax.Contains("?"))
                                {
                                    throw new Exception("Error: cannot have two user inputs next to each other without a variable backer!");
                                }

                                if (NextOneSyntax.Contains("K"))
                                {

                                    string xm = NextOneSyntax.Replace("K", "");
                                    NextOne = l.Keywords[Convert.ToInt32(xm)];

                                }
                                else if (NextOneSyntax.Contains("O"))
                                {
                                    string xm = NextOneSyntax.Replace("O", "");
                                    NextOne = l.Operators[Convert.ToInt32(xm)];
                                }
                                else if (NextOneSyntax.Contains("?"))
                                {
                                    throw new Exception("Error: cannot have two user inputs next to each other without a variable backer!");
                                }

                                string ccache = cache.Replace(ll, "");

                                string result = ccache.Substring(0, ccache.IndexOf(NextOne) + 1);
                                foreach (string a in l.Keywords)
                                {
                                    result = result.Replace(a, "");
                                }
                                foreach (string a in l.Operators)
                                {
                                    result = result.Replace(a, "");
                                }

                                unknown.Add(result);
                                ll = ll + result;

                                //int start = ll.LastIndexOf(BeforeOne);
                                //string finder = ll + NextOne;
                                //int pTo = finder.LastIndexOf(NextOne);

                                // string result = finder.Substring(start, pTo - start);


                            }
                            
                        }
                    }





                    //Run the code behind the line of script code
                    if (l.VariableBacker == "")
                    {
                        if (ll == item)
                        {
                            Assembly asm = Assembly.LoadFrom(i.DLLToRun);
                            Type t = asm.GetType(i.DLLToRun.Replace(".dll", "") + "." + i.ClassToEnter);
                            var methodInfo = t.GetMethod(i.MethodToRun, new Type[] { typeof(List<string>) });
                            if (methodInfo == null)
                            {
                                // never throw generic Exception - replace this with some other exception type
                                throw new Exception("Could not find " + l.LanguageName + " method.");
                            }


                            //unknown.Add("yo");


                            var o = Activator.CreateInstance(t, null);

                            object[] parameters = new object[1];
                            parameters[0] = unknown;

                            var r = methodInfo.Invoke(o, parameters);
                        }
                    }
                    else
                    {
                        if (ll == item.Replace(" ", "").Replace(l.VariableBacker, ""))
                        {
                            Assembly asm = Assembly.LoadFrom(i.DLLToRun);
                            Type t = asm.GetType(i.DLLToRun.Replace(".dll", "") + "." + i.ClassToEnter);
                            var methodInfo = t.GetMethod(i.MethodToRun, new Type[] { typeof(List<string>) });
                            if (methodInfo == null)
                            {
                                // never throw generic Exception - replace this with some other exception type
                                throw new Exception("Could not find " + l.LanguageName + " method.");
                            }


                            //unknown.Add("yo");


                            var o = Activator.CreateInstance(t, null);

                            object[] parameters = new object[1];
                            parameters[0] = unknown;

                            var r = methodInfo.Invoke(o, parameters);
                        }
                    }
                        




                    }

                }


            }
        }



    }

