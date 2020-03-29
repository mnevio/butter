using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace butter.backend
{
    public class Backend
    {

        private List<string> VariableTypes = new List<string>();

        public static void AssemblyTransfer(List<string> inputs, 
            string AssemblyPath, 
            string Class, 
            string Method)
        {
            Assembly asm = Assembly.LoadFrom(AssemblyPath);
            Type t = asm.GetType(AssemblyPath.Replace(".dll", "") + "." + Class);
            MethodInfo methodInfo;
            try
            {
                methodInfo = t.GetMethod(Method, new Type[] { typeof(List<string>) });
                if (methodInfo == null)
                {
                    // never throw generic Exception - replace this with some other exception type
                    throw new Exception("Could not find method to transfer.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }



            //unknown.Add("yo");


            var o = Activator.CreateInstance(t, null);

            object[] parameters = new object[1];
            parameters[0] = inputs;

            var r = methodInfo.Invoke(o, parameters);
        }

        internal void IEL(string a)
        {
            VariableTypes.Add(a);
        }
    }

    public class Variable
    {
        public string Name;

        internal static void AddVariableType(string a)
        {
            Backend b = new Backend();
            b.IEL(a);
        }
    }


}
