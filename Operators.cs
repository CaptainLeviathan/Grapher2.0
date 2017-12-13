using System;
using CursesSharp.Gui;

namespace Grapher
{

    //Every Operator will inhairite from this class. 
    //it will do a lot of the work of bilding the funtion trees and decoding those trees back into strings
  
    public abstract class Operator : IMFunc
    {
        /// <summary>
        /// the opperators that are taken as imputs to the operator
        /// </summary>
        protected IMFunc[] operands;

        /// <summary>
        /// charictors that are inserted into the string form to represent were the childd operators stings should go.
        /// </summary>
        public static char[] opDecode = new char[]{'#','@','~','$'};

        public static char opEncode = '#';

        /// <summary>
        /// the string form is the preferd textual repreentation of the operator and must be set by all oporators
        /// </summary>
        protected abstract string GetstringForm();

        /// <summary>
        /// Gets the recognizer strings that are used to recocnize the operator in incoming stirings.
        /// </summary>
        /// <returns>The recognizer strings.</returns>
        public  abstract string[] GetRecognizerStrings();


        /// <summary>
        /// The function to be preformed on the operators(almost alway used recursevly)
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        public abstract double func(double x);

        /// <summary>
        /// Returns a string that represents the operator including all operands
        /// </summary>
        public override string ToString()
        {
            string outPut = GetstringForm();

            for (int i = 0; i < GetstringForm().Length; ++i)
            {
                for (int k = 0; k < opDecode.Length; ++k)
                {
                    if(opDecode[k] == GetstringForm()[i])
                    {
                        if (operands != null)
                        {
                            if (operands.Length > k && operands[k] != null)
                            {
                                outPut = outPut.Remove(i, 1);
                                outPut = outPut.Insert(i, operands[k].ToString());
                            }
                        }
                    }
                }
            }
            return outPut;
        }
            
        /// <summary>
        /// Can be overrided if functions operators do not come in left to right.
        /// </summary>
        /// <param name="operands">Operands.</param>
        public virtual void SetOperands(IMFunc[] operands)
        {
            this.operands = operands;
        }
    }
        

    public class Sin : Operator
    {
        public override double func(double x)
        {
            return Math.Sin(operands[0].func(x));
        }

        protected override string GetstringForm()        
        {
            return "sin(" + opDecode[0] + ")";
        }

        public override string[] GetRecognizerStrings()
        {
            return new string[]{"sin(" + opEncode + ")", "sin" + opEncode};
        }
    }


    public class Add : Operator
    {
        public override double func(double x)
        {
            return operands[0].func(x) + operands[1].func(x);
        }

        protected override string GetstringForm()        
        {
            return  opDecode[0] + "+" + opDecode[1];
        }

        public override string[] GetRecognizerStrings()
        {
            return new string[]{opEncode + "+" + opEncode};
        }
    }

    public class Subtract : Operator
    {
        public override double func(double x)
        {
            return operands[0].func(x) - operands[1].func(x);
        }

        protected override string GetstringForm()        
        {
            return  opDecode[0] + "-" + opDecode[1];
        }

        public override string[] GetRecognizerStrings()
        {
            return new string[]{opEncode + "-" + opEncode};
        }
    }

    public class Multiply : Operator
    {
        public override double func(double x)
        {
            return operands[0].func(x) * operands[1].func(x);
        }

        protected override string GetstringForm()        
        {
            return  opDecode[0] + "*" + opDecode[1];
        }

        public override string[] GetRecognizerStrings()
        {
            return new string[]{opEncode + "*" + opEncode};
        }
    }

    public class Devide : Operator
    {
        public override double func(double x)
        {
            return operands[0].func(x) / operands[1].func(x);
        }

        protected override string GetstringForm()        
        {
            return  opDecode[0] + "/" + opDecode[1];
        }

        public override string[] GetRecognizerStrings()
        {
            return new string[]{opEncode + "/" + opEncode};
        }
    }

    //remeber the null check for all the other oporators
    public class Variable : Operator
    {
        //once data base is up and running replace this.
        public static string[] VariableNames = new string[]{"x"};

        public override double func(double x)
        {
            return x;
        }

        protected override string GetstringForm()        
        {
            return VariableNames[0];
        }


        public override string[] GetRecognizerStrings()
        {
            return VariableNames;
        }

    }

}

