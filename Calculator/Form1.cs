using Accessibility;
using System.Data;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Calculator
{

    public partial class Form1 : Form
    {
        StringBuilder x;
        public Form1()
        {

            InitializeComponent();

        }
        private void calculate_(ref string input, string pattern, int increase)
        {
            // string pattern = "cos(";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(input);

            if (match.Success)
            {
                int index = match.Index;
                string ex = "";
                int i = index + increase;
                while (input[i] != ')')
                {
                    ex += input[i];
                    i++;
                }
                double result = 0;
                switch (pattern)
                {
                    case "cos":
                        result = Math.Cos(int.Parse(ex) * Math.PI / 180);
                        break;
                    case "sin":
                        result = Math.Sin(int.Parse(ex) * Math.PI / 180);
                        break;
                    case "tan":
                        result = Math.Tan(int.Parse(ex) * Math.PI / 180);
                        break;
                    case "sinh":
                        result = Math.Sinh(int.Parse(ex) * Math.PI / 180);
                        break;
                    case "cosh":
                        result = Math.Cosh(int.Parse(ex) * Math.PI / 180);
                        break;
                    case "tanh":
                        result = Math.Tanh(int.Parse(ex) * Math.PI / 180);
                        break;
                    case "sqrt":
                        result = Math.Sqrt(int.Parse(ex));
                        break;
                    case "log":
                        result = Math.Log(int.Parse(ex));
                        break;
                    case "pow":
                        string[] exp = ex.Split(',');
                        result = Math.Pow(int.Parse(exp[0]), int.Parse(exp[1]));
                        break;
                    case "Abs":
                        result = Math.Abs(int.Parse(ex));
                        break;


                }
                result = Convert.ToDouble(string.Format("{0:0.00}", result));
                input = input.Substring(0, index) + result + input.Substring(i + 1);

            }
        }
        private void Result_Click(object sender, EventArgs e)
        {

            string input = text_Display.Text;

            #region  trigonometric functions&funs
            //(cosh,sinh,tanh,sqrt)  +(===> count this 4
            calculate_(ref input, pattern: "cosh", increase: 5);
            calculate_(ref input, pattern: "sinh", increase: 5);
            calculate_(ref input, pattern: "tanh", increase: 5);
            calculate_(ref input, pattern: "sqrt", increase: 5);

            //(cos,sin,tan,log,sqrt)  +(===> count this 4
            calculate_(ref input, pattern: "cos", increase: 4);
            calculate_(ref input, pattern: "sin", increase: 4);
            calculate_(ref input, pattern: "tan", increase: 4);
            calculate_(ref input, pattern: "log", increase: 4);
            calculate_(ref input, pattern: "Abs", increase: 4);
            calculate_(ref input, pattern: "pow", increase: 4);
            #endregion

            x = new StringBuilder(input);
            for (int i = 1; i < x.Length - 1; i++)
            {
                if (x[i] == '(')
                {
                    int number;
                    if (int.TryParse(x[i - 1] + "", out number))
                    {
                        x.Insert(i, '*');
                    }
                }
                if (x[i] == ')')
                {
                    int number;
                    if (int.TryParse(x[i + 1] + "", out number))
                    {
                        x.Insert(i + 1, '*');
                    }
                }
            }


            DataTable dt = new DataTable();
            object result = dt.Compute(x.ToString(), "");
            text_Display.Text = "" + result;

        }
        private void btnNumbers_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            text_Display.AppendText(btn.Text);
        }

        private void btnsOperators_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            text_Display.AppendText(btn.Text);

        }

        private void PI_Click(object sender, EventArgs e)
        {
            text_Display.AppendText("3.14159265359");

        }

        private void Backspace_Click(object sender, EventArgs e)
        {
            string s = text_Display.Text;
            string temp = "";
            for (int i = 0; i < s.Length - 1; i++)
            {
                temp += s[i];
            }
            text_Display.Text = temp;
        }

        private void CLR_Click(object sender, EventArgs e)
        {
            text_Display.Text = "";
        }

        private void FunOp1_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            text_Display.AppendText(btn.Text + "("); //////here


        }

        private void bracket_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            text_Display.AppendText(btn.Text);

        }

        private void ePow_Click(object sender, EventArgs e)
        {
            text_Display.AppendText("2.71828182846");

        }

        private void point_Click(object sender, EventArgs e)
        {
            text_Display.AppendText(".");

        }

        private void Round_Click(object sender, EventArgs e)
        {
            text_Display.Text = Math.Round(Convert.ToDouble(text_Display.Text)) + "";

        }

        private void button19_Click(object sender, EventArgs e)
        {
            text_Display.AppendText(",");
        }
    }
}