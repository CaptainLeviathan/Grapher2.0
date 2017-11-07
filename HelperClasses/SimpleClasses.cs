using System;

namespace Grapher
{
	public abstract class Point
	{
		private bool isXNaN_;
		private bool isYNaN_;
		//this is protected so that in the futcher IntPoint can make it setable
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


		public bool isXNaN
		{
			get 
			{
				return isXNaN_;
			}
			set 
			{
				isXNaN_ = value;
			}
		}

		public bool isYNaN		
		{
			get 
			{
				return isYNaN_;
			}
			set 
			{
				isYNaN_ = value;
			}
		}
	}

	public class IntPoint : Point
    {

		public int x;
        public int y;

        public IntPoint(int x,int y)
        {
			isXNaN = false;
			isYNaN = false;
            this.x = x;
            this.y = y;
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
    
	public class DoublePoint : Point
    {

		private double x_;
		private double y_;

        public double x 
		{
			get
			{
				return x_;
			}
			set 
			{
				x_ = value;
				isXNaN = Double.IsNaN (x) || Double.IsInfinity (x);
			}
		}
        public double y
		{
			get 
			{
				return y_;
			}
			set 
			{
				y_ = value;
				isYNaN = Double.IsNaN (y) || Double.IsInfinity (y);
			}
		}

        public DoublePoint(double x,double y)
        {
            this.x = x;
            this.y = y;
        }
    }
}