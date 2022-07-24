using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CalcolatriceBRUT
{
	public partial class MainPage : ContentPage
	{
        string expre = "";

        public MainPage()
		{
			InitializeComponent();
		}
        private void BC_Clicked(object sender, EventArgs e)
        {
            expre = "";
            Result.Text = expre;
        }

        private void BCAN_Clicked(object sender, EventArgs e)
        {
            if (expre != "")
            {
                expre = expre.Remove(expre.Length - 1);
                Result.Text = expre;
            }
        }

        private void BPER_Clicked(object sender, EventArgs e)
        {
            Operator("%");
        }

        private void BDIV_Clicked(object sender, EventArgs e)
        {
            Operator("/");
        }

        private void B7_Clicked(object sender, EventArgs e)
        {
            expre += "7";
            Result.Text = expre;
        }

        private void B8_Clicked(object sender, EventArgs e)
        {
            expre += "8";
            Result.Text = expre;
        }

        private void B9_Clicked(object sender, EventArgs e)
        {
            expre += "9";
            Result.Text = expre;
        }

        private void BPE_Clicked(object sender, EventArgs e)
        {
            Operator("*");
        }

        private void B4_Clicked(object sender, EventArgs e)
        {
            expre += "4";
            Result.Text = expre;
        }

        private void B5_Clicked(object sender, EventArgs e)
        {
            expre += "5";
            Result.Text = expre;
        }

        private void B6_Clicked(object sender, EventArgs e)
        {
            expre += "6";
            Result.Text = expre;
        }

        private void BME_Clicked(object sender, EventArgs e)
        {
            Operator("-");
        }

        private void B1_Clicked(object sender, EventArgs e)
        {
            expre += "1";
            Result.Text = expre;
        }

        private void B2_Clicked(object sender, EventArgs e)
        {
            expre += "2";
            Result.Text = expre;
        }

        private void B3_Clicked(object sender, EventArgs e)
        {
            expre += "3";
            Result.Text = expre;
        }

        private void BPI_Clicked(object sender, EventArgs e)
        {
            Operator("+");
        }

        private void B0_Clicked(object sender, EventArgs e)
        {
            expre += "0";
            Result.Text = expre;
        }

        private void BVIR_Clicked(object sender, EventArgs e)
        {
            if (expre != "" && Evaluable)
            {
                expre += ".";
                Result.Text = expre;
            }
        }

        private void BUQ_Clicked(object sender, EventArgs e)
        {
            if (expre != "")
            {
                string res;

                if (Evaluable) res = Evaluate(expre).ToString().Replace(',', '.');
                else res = Evaluate(expre.Remove(expre.Length - 1)).ToString().Replace(',', '.');

                Result.Text = expre + "\n=\n" + res;

                if (res != "0") expre = res;
                else expre = "";
            }
        }

        private bool Evaluable => expre[expre.Length - 1] != '+' && expre[expre.Length - 1] != '-' && expre[expre.Length - 1] != '/' && expre[expre.Length - 1] != '*' && expre[expre.Length - 1] != '%';

        private void Operator(string op)
        {
            if (expre != "")
            {
                if (Evaluable)
                {
                    expre += op;
                    Result.Text = expre;
                }
                else
                {
                    expre = expre.Remove(expre.Length - 1);
                    expre += op;
                    Result.Text = expre;
                }
            }
        }
        public static double Evaluate(String expr)
        {
            Stack<String> stack = new Stack<String>();

            string value = "";
            for (int i = 0; i < expr.Length; i++)
            {
                String s = expr.Substring(i, 1);
                char chr = s.ToCharArray()[0];

                if (!char.IsDigit(chr) && chr != '.' && value != "")
                {
                    stack.Push(value);
                    value = "";
                }

                if (s.Equals("("))
                {

                    string innerExp = "";
                    i++; //Fetch Next Character
                    int bracketCount = 0;
                    for (; i < expr.Length; i++)
                    {
                        s = expr.Substring(i, 1);

                        if (s.Equals("("))
                            bracketCount++;

                        if (s.Equals(")"))
                            if (bracketCount == 0)
                                break;
                            else
                                bracketCount--;


                        innerExp += s;
                    }

                    stack.Push(Evaluate(innerExp).ToString());

                }
                else if (s.Equals("+")) stack.Push(s);
                else if (s.Equals("-")) stack.Push(s);
                else if (s.Equals("*")) stack.Push(s);
                else if (s.Equals("/")) stack.Push(s);
                else if (s.Equals("%")) stack.Push(s);
                else if (s.Equals(")"))
                {
                }
                else if (char.IsDigit(chr) || chr == '.')
                {
                    value += s;

                    if (value.Split('.').Length > 2)
                        throw new Exception("Invalid decimal.");

                    if (i == (expr.Length - 1))
                        stack.Push(value);

                }
                else
                    throw new Exception("Invalid character.");

            }


            double result = 0;
            while (stack.Count >= 3)
            {

                double right = Convert.ToDouble(stack.Pop());
                string op = stack.Pop();
                double left = Convert.ToDouble(stack.Pop());


                if (op == "+") result = left + right;
                else if (op == "-") result = left - right;
                else if (op == "*") result = left * right;
                else if (op == "/") result = left / right;
                else if (op == "%") result = left % right;

                stack.Push(result.ToString());
            }


            return Convert.ToDouble(stack.Pop());


        }
    }
}
