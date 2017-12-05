using System;
using CursesSharp.Gui;

namespace Grapher
{
    public abstract class Operator : IMFunc
    {
        public static char[] op = new char[]{'`','#','@','~'};

        public string[] stringForms;

        public IMFunc[] operands; 

        public Operator(IMFunc[] operands)
        {
            this.operands = operands;
        }

        public abstract double func(double x);

        public override string ToString()
        {
            string outPut = stringForms[0];

            for (int i = 0; i < stringForms[0].Length; ++i)
            {
                for (int k = 0; k < op.Length; ++k)
                {
                    if(op[k] == stringForms[0][i])
                    {
                        if(operands.Length > k && operands[k] != null)
                        {
                            outPut = outPut.Remove(i, 1);
                            outPut = outPut.Insert(i, operands[k].ToString());
                        }
                    }
                }
            }
            return outPut;
        }
    }

    public class Sin : Operator
    {
        public override double func(double x)
        {
            return Math.Sin(operands[0].func(x));
        }

        public Sin(Operator a) : base(new IMFunc[]{a})
        {
            stringForms = new string[]{"sin(" + op[0] + ")"}; 
        }
    }

    public class Variable : Operator
    {
        public string VariableName = "x";

        public override double func(double x)
        {
            return x;
        }
        public Variable() : base(null)
        {
            stringForms = new string[]{VariableName}; 
        }
    }

}

