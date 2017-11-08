using System;

namespace Grapher
{
    /// <summary>
    ///    represents a point on the euclidean plane.
    /// </summary>
    ///<remarks>
    ///    <para>Has an x and a y. will return keeps trake of NaN for both points for conveanence.</para>
    /// </remarks>
    public class Point
    {
        private double x_;
        private double y_;

        public virtual double x 
        {
            get
            {
                return x_;
            }
            set 
            {
                x_ = value;
                isXNaN = Double.IsNaN (x) || Double.IsInfinity (x) || isXNaN;
            }
        }

        public virtual double y
        {
            get 
            {
                return y_;
            }
            set 
            {
                y_ = value;
                isYNaN = Double.IsNaN (y) || Double.IsInfinity (y) || isYNaN;
            }
        }


        /// <summary>
        ///   returns true is either x or y is NaN
        /// </summary>
        public bool isNaN
        {
            get 
            {
                return isXNaN || isYNaN;
            }
        }

        public bool isXNaN;
        public bool isYNaN;


        public Point(double x,double y)
        {
            this.x = x;
            this.y = y;
        }

        public Point (IntPoint iP)
        {
            isXNaN = iP.isXNaN;
            isYNaN = iP.isYNaN;

            x = (double)iP.x;
            y = (double)iP.y;
        }

        public override string ToString()
        {
            return string.Format("[Point: x={0}, y={1}, isNaN={2}]", x, y, isNaN);
        }

        private static Point NewPointWithOrNaNOldPoints(Point a, Point b)
        {
            Point outPut = new Point(0.0, 0.0);
            outPut.isXNaN = a.isXNaN || b.isXNaN;
            outPut.isYNaN = a.isYNaN || b.isYNaN;
            return outPut;
        }

        static public Point Add(Point a, Point b)
        {
            Point outPut = NewPointWithOrNaNOldPoints(a, b);
            outPut.x = a.x + b.x;
            outPut.y = a.y + b.y;
            return outPut;
        }

        static public Point Subtract(Point a, Point b)
        {
            Point outPut = NewPointWithOrNaNOldPoints(a, b);
            outPut.x = a.x * b.x;
            outPut.y = a.y * b.y;
            return outPut;
        }

        static public Point Multiply(Point a, Point b)
        {
            Point outPut = NewPointWithOrNaNOldPoints(a, b);
            outPut.x = a.x * b.x;
            outPut.y = a.y * b.y;
            return outPut;
        }

        static public Point Multiply(Point a, double b)
        {
            Point outPut = new Point(0, 0);
            outPut.isXNaN = a.isXNaN;
            outPut.isYNaN = a.isYNaN;

            outPut.x = a.x * b;
            outPut.y = a.y * b;
            return outPut;
        }

        static public Point Divide(Point a, Point b)
        {
            Point outPut = NewPointWithOrNaNOldPoints(a, b);
            outPut.x = a.x / b.x;
            outPut.y = a.y / b.y;
            return outPut;
        }

        public static Point operator +(Point a, Point b)
        {
            return Add(a ,b);
        }

        public static Point operator -(Point a, Point b)
        {
            return Subtract(a ,b);
        }

        public static Point operator *(Point a, Point b)
        {
            return Multiply(a ,b);
        }

        public static Point operator *(Point a, double b)
        {
            return Multiply(a ,b);
        }


        public static Point operator /(Point a, Point b)
        {
            return Divide(a ,b);
        }
    }
        
    /// <summary>
    ///    This class acts like point but only assumes intiger values.
    ///    <para>Note: dividing two IntPoints will return a Point</para>
    /// </summary>
    public class IntPoint : Point
    {
        private int _x;
        private int _y;

        public new int x
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
                base.x = Convert.ToDouble(value);
            }
        }

        public new int y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
                base.y = Convert.ToDouble(value);
            }
        }

        public IntPoint(int x,int y) : base(Convert.ToDouble(x), Convert.ToDouble(y))
        {
            isXNaN = false;
            isYNaN = false;
            this.x = x;
            this.y = y;
        }

        public IntPoint(Point p) : base(p.y , p.x)
        {
            isXNaN = p.isXNaN;
            isYNaN = p.isYNaN;

            x = (int)p.x;
            y = (int)p.y;
        }

        public override string ToString()
        {
            return string.Format("[IntPoint: x={0}, y={1}, isNaN={2}]", x, y, isNaN);
        }

        private static IntPoint NewIntPointWithOrNaNOldPoints(Point a, Point b)
        {
            IntPoint outPut = new IntPoint(0, 0);
            outPut.isXNaN = a.isXNaN || b.isXNaN;
            outPut.isYNaN = a.isYNaN || b.isYNaN;
            return outPut;
        }

        static public IntPoint Add(IntPoint a, IntPoint b)
        {
            IntPoint outPut = NewIntPointWithOrNaNOldPoints(a, b);
            outPut.x = a.x + b.x;
            outPut.y = a.y + b.y;
            return outPut;
        }

        static public IntPoint Subtract(IntPoint a, IntPoint b)
        {
            IntPoint outPut = NewIntPointWithOrNaNOldPoints(a, b);
            outPut.x = a.x * b.x;
            outPut.y = a.y * b.y;
            return outPut;
        }

        static public IntPoint Multiply(IntPoint a, IntPoint b)
        {
            IntPoint outPut = NewIntPointWithOrNaNOldPoints(a, b);
            outPut.x = a.x * b.x;
            outPut.y = a.y * b.y;
            return outPut;
        }

        static public IntPoint Multiply(IntPoint a, int b)
        {
            IntPoint outPut = new IntPoint(0,0);
            outPut.isXNaN = a.isXNaN;
            outPut.isYNaN = a.isYNaN;

            outPut.x = a.x * b;
            outPut.y = a.y * b;
            return outPut;
        }

        public static IntPoint operator +(IntPoint a, IntPoint b)
        {
            return Add(a ,b);
        }

        public static IntPoint operator -(IntPoint a, IntPoint b)
        {
            return Subtract(a ,b);
        }

        public static IntPoint operator *(IntPoint a, IntPoint b)
        {
            return Multiply(a ,b);
        }

        public static IntPoint operator *(IntPoint a, int b)
        {
            return Multiply(a ,b);
        }

    }

    public class CharPoint : IntPoint
    {
        public char symbol;
        public CharPoint(int x,int y) : base(x,y)
        {
            symbol = ' ';
        }
        public CharPoint(int x, int y, char c) : base(x,y)
        {
            symbol = c;
        }

        public CharPoint(IntPoint iP) : base(iP.x, iP.y)
        {
            isXNaN = iP.isXNaN;
            isYNaN = iP.isYNaN;

            symbol = ' ';
        }
    }
}