using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

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

            foreach (string item in lines)
            {
                string theline = item.Replace(" ", "");
                foreach (Line i in l.GetLines())
                {
                    List<string> unknown = new List<string>();
                    int unknowncurrentnumber = 0;
                    string ll = "";

                    string[] indcom = i.line.Split('-');
                    foreach (string t in indcom)
                    {
                        t.Replace("-", "");
                    }


                    string cache = theline;
                    bool LastWasUnknown = false;
                    foreach (string m in indcom)
                    {

                        if (m.Contains("K"))
                        {
                            if (LastWasUnknown == false)
                            {
                                string x = m.Replace("K", "");
                                ll = ll + l.Keywords[Convert.ToInt32(x)];
                            }
                            else
                            {
                                string x = m.Replace("K", "");

                                string o = cache.Substring(0, cache.IndexOf(l.Keywords[Convert.ToInt32(x)]) + 1).Replace(l.Keywords[Convert.ToInt32(x)], "");
                                string result = o;
                                foreach (string om in l.Keywords)
                                {
                                    if (o.Contains(om))
                                    {
                                        result = result.Replace(om, "");
                                    }
                                }
                                foreach (string om in l.Operators)
                                {
                                    if (o.Contains(om))
                                    {
                                        result = result.Replace(om, "");
                                    }
                                }

                                List<string> TheInputs = new List<string>();
                                string[] que = result.Split(Convert.ToChar(l.VariableBacker));
                                foreach (string e in que)
                                {
                                    if (e == l.VariableBacker || e == "")
                                    {

                                    }
                                    else
                                    {

                                        TheInputs.Add(e.Replace(l.VariableBacker, ""));
                                    }
                                }


                                ll = ll + TheInputs[unknown.Count];
                                ll = ll + l.Keywords[Convert.ToInt32(x)];
                                unknown.Add(TheInputs[unknown.Count]);
                                LastWasUnknown = false;
                            }
                        }

                        if (m.Contains("O"))
                        {
                            if (LastWasUnknown == false)
                            {
                                string x = m.Replace("O", "");
                                ll = ll + l.Operators[Convert.ToInt32(x)];
                            }
                            else
                            {
                                string x = m.Replace("O", "");


                                //int pFrom = cache.IndexOf(cache) + cache.Length;
                                //int pTo = cache.LastIndexOf(l.Operators[Convert.ToInt32(x)]);
                                //String result = cache.Substring(pFrom, pTo - pFrom);

                                string o = cache.Substring(0, cache.IndexOf(l.Operators[Convert.ToInt32(x)]) + 1).Replace(l.Operators[Convert.ToInt32(x)], "");
                                string result = o;
                                foreach (string om in l.Keywords)
                                {
                                    if (o.Contains(om))
                                    {
                                        result = result.Replace(om, "");
                                    }
                                }
                                foreach (string om in l.Operators)
                                {
                                    if (o.Contains(om))
                                    {
                                        result = result.Replace(om, "");
                                    }
                                }

                                List<string> TheInputs = new List<string>();
                                string[] que = result.Split(Convert.ToChar(l.VariableBacker));
                                foreach (string e in que)
                                {
                                    if (e == l.VariableBacker || e == "")
                                    {

                                    }
                                    else
                                    {

                                        TheInputs.Add(e.Replace(l.VariableBacker, ""));
                                    }
                                }


                                ll = ll + TheInputs[unknown.Count];
                                 ll = ll + l.Operators[Convert.ToInt32(x)];
                                unknown.Add(TheInputs[unknown.Count]);
                                LastWasUnknown = false;
                            }
                        }

                        if (m.Contains("?"))
                        {
                            //ll = ll + m;
                            LastWasUnknown = true;
                            //unknown.Add(m);
                            unknowncurrentnumber = unknown.Count;
                        }

                    }


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
