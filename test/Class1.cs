using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
    public class TheClass
    {
        public void TheVoid(List<string> hi)
        {
            MessageBox.Show("Hi");          
        }
        public void IfStatement(List<string> input)
        {
            if (input[0] == input[1])
            {
                MessageBox.Show("Hi");
            }
        }
    }
}
