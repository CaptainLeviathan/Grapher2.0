using System;
using System.Collections.Generic;
using CursesSharp.Gui;
using CursesSharp;

namespace Grapher
{
	public class GraphWidget : Widget
    {
		double charAspect = 1.5;

        //theses are the propertys that describe the graph window in the equlidean plane
        public double graphWith; 
        public double graphHight;
		public DoublePoint graphCenter;

		public DoublePoint moveSpeed;

		DoublePoint graphStep;

        public List<IGraphable> graphs;

		public GraphWidget(int X, int Y, int with, int hight) : base(X, Y, with, hight)
        {
			CanFocus = true;

			this.graphWith = 5; 
			this.graphHight = 2;

			this.graphCenter = new DoublePoint(0.0, 0.0);

			moveSpeed = new DoublePoint (1.0, 1.0);
			graphStep = new DoublePoint (0.0, 0.0);

			graphs = new List<IGraphable>();
        }

		public GraphWidget(int centerX, int centerY, int with, int hight, double graphCenterX, double graphCenterY, double graphWith, double graphHight) : base(centerX, centerY, with, hight)
        {
			CanFocus = true;

			this.graphWith = graphWith; 
			this.graphHight = graphHight;

			this.graphCenter = new DoublePoint(graphCenterX,graphCenterY);

			moveSpeed = new DoublePoint (1,1);
			graphStep = new DoublePoint (0,0);


			graphs = new List<IGraphable>();
		}


        public IntPoint GraphToAsciiTrans(DoublePoint gPoint)
        {
			IntPoint output = new IntPoint(
            //Transform Equetions for converting somthing a function out puts to something that can be more esily, drawn latter and worked with.
				gPoint.isXNaN ? 0 : Convert.ToInt32(((gPoint.x - graphCenter.x)*(w/(graphWith*charAspect))) + w/2), 
				gPoint.isYNaN ? 0 : Convert.ToInt32(((gPoint.y - graphCenter.y)*(h/graphHight)) + h/2) 
            );

			output.isXNaN = gPoint.isXNaN;
			output.isYNaN = gPoint.isYNaN;

			return output;
        }
			
        public DoublePoint AsciiToGraphTrans(IntPoint aPoint)
        {
			DoublePoint output = new DoublePoint(
            //Transform Equations for converting a point on the Ascii graph to somthing that a function can take
				aPoint.isXNaN ? Double.NaN : (Convert.ToDouble((aPoint.x - w/2) * ((graphWith*charAspect)/w) + graphCenter.x)),
				aPoint.isYNaN ? Double.NaN : (Convert.ToDouble((aPoint.y - h/2) * (graphHight/h) + graphCenter.y))
            );

			output.isXNaN = aPoint.isXNaN;
			output.isYNaN = aPoint.isYNaN;

			return output;
        }

		//final transormation to flips the y-axis to be printed on the widget
		CharPoint AsciiToCollineTrans(CharPoint aPoint)
		{
			return new CharPoint(
				aPoint.x,
				h - aPoint.y
			);
		}


		public override bool ProcessKey (int key)
		{
			calcStepSize ();

			switch (key) 
			{
			case 'w':
				graphCenter.y += graphStep.y;
				break;
			case 'a':
				graphCenter.x -= graphStep.x;
				break;
			case 's':
				graphCenter.y -= graphStep.y;
				break;
			case 'd':
				graphCenter.x += graphStep.x;
				break;
			case 'r':
				graphWith -= 2 * graphStep.x;
				graphHight -= 2 * graphStep.y;
				break;
			case 'f':
				graphWith += 2 * graphStep.x;
				graphHight += 2 * graphStep.y;
				break;
			default:
				return false;
			}

			Redraw ();

			return true;
		}

		void calcStepSize()
		{
			//computing the amount to move
			graphStep.x = Convert.ToDouble (((graphWith * charAspect) / w) * moveSpeed.x);
			graphStep.y = Convert.ToDouble ((graphHight / h) * moveSpeed.y);
		}

		//gets called if the wigets size ever changes
		public override void DoSizeChanged ()
		{
			Redraw ();
		}

        //redraws the grapg window
		public override void Redraw()
        {
            List<CharPoint> charPoints = new List<CharPoint>();

			Stdscr.Attr =  (ColorNormal);

			for(int col = 0; col < w; ++col)
			{
				for(int line = 0; line < h; ++line)
				{
					this.Move (line, col);
					Stdscr.Add (' ');
				}
			}

            for (int i = 0; i < graphs.Count; i++)
            {
                charPoints.AddRange(graphs[i].getPoints(this));
            }

			CharPoint CollineP = new CharPoint(0,0);

			for (int i = 0; i < charPoints.Count; i++) 
			{
				CollineP = AsciiToCollineTrans (charPoints[i]);

				if (CollineP.x >= 0 && CollineP.y >= 0 && CollineP.x < w && CollineP.y < h)
				{
					this.Move (CollineP.y, CollineP.x);
					Stdscr.Add (charPoints [i].symbol);
				}
			}
        }
    }
}